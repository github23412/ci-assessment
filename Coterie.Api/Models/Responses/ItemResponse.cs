namespace Coterie.Api.Models.Responses
{
    public class ItemResponse<T> : BaseResponse
    {
        public T Item { get; set; }
    }
}