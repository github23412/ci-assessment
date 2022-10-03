using System;

namespace Coterie.Api.Models.Responses
{
    public abstract class BaseResponse
    {
        public bool IsSuccessful { get; set; } = true;
        public string TransactionId { get; set; }  = Guid.NewGuid().ToString();
    }
}