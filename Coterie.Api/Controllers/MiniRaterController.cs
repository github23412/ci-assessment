using System.Linq;
using Coterie.Api.Interfaces;
using Coterie.Api.Models.Requests;
using Coterie.Api.Models.Responses;
using Coterie.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace Coterie.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MiniRaterController : CoterieBaseController
    {
        private readonly IMiniRaterService _miniRaterService;
        
        public MiniRaterController(IMiniRaterService miniRaterService)
        {
            _miniRaterService = miniRaterService;
        }

        [HttpPost]
        public ActionResult<BaseResponse> PostMiniRater(MiniRaterRequest request)
        {
            var result = _miniRaterService.ProjectRate(request);
            return result;
        }
    }
}