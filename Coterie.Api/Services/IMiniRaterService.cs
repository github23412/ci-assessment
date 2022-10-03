using Coterie.Api.Models.Requests;
using Coterie.Api.Models.Responses;

namespace Coterie.Api.Services
{
    public interface IMiniRaterService
    {
        BaseResponse ProjectRate(MiniRaterRequest request);
    }
}
