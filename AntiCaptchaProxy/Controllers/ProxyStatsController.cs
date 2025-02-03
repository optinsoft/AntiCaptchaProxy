﻿using AntiCaptchaProxy.Interfaces;
using AntiCaptchaProxy.Responses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AntiCaptchaProxy.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ProxyStatsController(IAntiCaptchaService antiCaptchaService) : ControllerBase
    {
        [HttpGet(Name = "getStats")]
        public ActionResult<StatsResponse> GetStats()
        {
            var proxyStats = antiCaptchaService.GetProxyStats();
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
                getTaskResultErrors = proxyStats.GetTaskResultErrors
            });
        }
    }
}
