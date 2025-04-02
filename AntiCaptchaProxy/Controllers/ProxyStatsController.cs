using AntiCaptchaProxy.Interfaces;
using AntiCaptchaProxy.Models;
using AntiCaptchaProxy.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AntiCaptchaProxy.Controllers
{
    [Authorize]
    [Route("[controller]")]
    [ApiController]
    public class ProxyStatsController(IAntiCaptchaService antiCaptchaService, ProxyStatsDb db) : ControllerBase
    {
        [HttpGet(Name = "getStats")]
        public async Task<ActionResult<StatsResponse>> GetStats()
        {
            var proxyStats = await antiCaptchaService.GetProxyStats(db);
            return Ok(new StatsResponse
            {
                serviceInfo = antiCaptchaService.GetServiceInfo(),
                createTaskCount = proxyStats.CreateTaskCount,
                createTaskSucceeded = proxyStats.CreateTaskSucceeded,
                createTaskFailed = proxyStats.CreateTaskFailed,
                createTaskErrors = proxyStats.CreateTaskErrors,
                getTaskResultCount = proxyStats.GetTaskResultCount,
                getTaskResultSucceeded = proxyStats.GetTaskResultSucceeded,
                getTaskResultFailed = proxyStats.GetTaskResultFailed,
                getTaskResultErrors = proxyStats.GetTaskResultErrors,
                lastBalance = proxyStats.LastBalance,
                lastBalanceTime = proxyStats.LastBalanceTime
            });
        }

        [HttpPost("incCreateTaskCount")]
        public async Task<ActionResult<SuccessResponse>> IncCreateTaskCount()
        {
            await antiCaptchaService.IncCreateTaskCount(db);
            return Ok(new SuccessResponse
            {
                success = true,
                message = "SUCCESS"
            });
        }

        [HttpPost("incGetTaskResultCount")]
        public async Task<ActionResult<SuccessResponse>> IncGetTaskResultCount()
        {
            await antiCaptchaService.IncGetTaskResultCount(db);
            return Ok(new SuccessResponse
            {
                success = true,
                message = "SUCCESS"
            });
        }

    }
}
