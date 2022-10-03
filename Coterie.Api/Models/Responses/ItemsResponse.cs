
using System.Collections.Generic;

namespace Coterie.Api.Models.Responses
{
    public class ItemsResponse<T> : BaseResponse
    {
        public List<T> Items { get; set; }
    }
}