using System.Collections.Generic;

namespace Coterie.Api.Models.Responses
{
    public class MiniRaterResponse : BaseResponse
    {
        public string Business { get; set; }
        public decimal Revenue { get; set; }
        public List<MiniRaterPremium> Premiums { get; set; }
    }

    public class MiniRaterPremium
    {
        public decimal Premium { get; set; }
        public string State { get; set; }
    }
}