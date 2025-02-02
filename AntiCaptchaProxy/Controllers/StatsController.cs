using AntiCaptchaProxy.Interfaces;
using AntiCaptchaProxy.Responses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AntiCaptchaProxy.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class StatsController : ControllerBase
    {
        private readonly IAntiCaptchaService _antiCaptchaService;

        public StatsController(IAntiCaptchaService antiCaptchaService)
        {
            _antiCaptchaService = antiCaptchaService;
        }

        [HttpGet(Name = "getStats")]
        public ActionResult<StatsResponse> GetStats()
        {
            return Ok(new StatsResponse
            {
                info = _antiCaptchaService.GetServiceInfo()
            });
        }
    }
}
