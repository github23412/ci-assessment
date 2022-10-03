using Coterie.Api.Models.Requests;
using Coterie.Api.Models.Responses;
using System;
using System.Collections.Generic;

namespace Coterie.Api.Services
{
    public class MiniRaterService : IMiniRaterService
    {
        public BaseResponse ProjectRate(MiniRaterRequest request)
        {
            if (request == null || request.States == null || request.Business == null)
            {
                return new ExceptionResponse() { IsSuccessful = false, Message = "Input must not be null" };
            }

            try
            {
                var response = new MiniRaterResponse()
                {
                    Business = request.Business,
                    Revenue = request.Revenue,
                    Premiums = new List<MiniRaterPremium>()
                };

                foreach (var state in request.States)
                {
                    var premium = GetPremium(request.Business, request.Revenue, state);
                    response.Premiums.Add(premium);
                }

                return response;
            }
            catch (Exception e)
            {
                return new ExceptionResponse() { IsSuccessful = false, Message = e.Message };
            }
        }

        private MiniRaterPremium GetPremium(string business, decimal revenue, string state)
        {
            // Note - hazardFactor is included in the spreadsheet,
            // however not in the documentation / sample requests.
            // Hardcoding to 4 to match the spreadsheet.
            var hazardFactor = 4m;
            var stateFactor = GetStateFactor(state);
            var businessFactor = GetBusinessFactor(business);
            var basePremium = Math.Max(1, revenue / 1000);

            var premium = new MiniRaterPremium()
            {
                State = state,
                Premium = hazardFactor * stateFactor * businessFactor * basePremium
            };
            
            return premium;
        }

        private decimal GetStateFactor(string state)
        {
            return state?.ToLower() switch
            {
                "oh" or "ohio" => 1m,
                "tx" or "texas" => 0.943m,
                "fl" or "florida" => 1.2m,
                _ => 0m,
            };
        }

        private decimal GetBusinessFactor(string business)
        {
            return business?.ToLower() switch
            {
                "architect" => 1m,
                "plumber" => 0.5m,
                "programmer" => 1.25m,
                _ => 0m,
            };
        }
    }
}
