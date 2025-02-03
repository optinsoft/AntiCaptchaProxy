using AntiCaptchaProxy.Interfaces;
using AntiCaptchaProxy.Requests;
using AntiCaptchaProxy.Responses;
using AntiCaptchaProxy.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace AntiCaptchaProxy.Controllers
{
    //[Route("[controller]")]
    [ApiController]
    public class ProxyController(IAntiCaptchaService antiCaptchaService) : ControllerBase
    {
        private readonly HttpClient client = new();

        [HttpPost("getBalance")]
        [Consumes("application/json")]
        public async Task<IActionResult> GetBalance([FromBody] BalanceRequest request)
        {
            try
            {
                HttpResponseMessage responseMessage = await client.PostAsJsonAsync(
                    "https://api.anti-captcha.com/getBalance", request);
                if (responseMessage.IsSuccessStatusCode)
                {
                    var responseJson = await responseMessage.Content.ReadAsStringAsync();
                    return new ContentResult
                    {
                        Content = responseJson,
                        ContentType = responseMessage.Content.Headers.ContentType?.ToString() ?? "text/plain",
                        StatusCode = 200
                    };
                }
                return StatusCode(500, $"Request failed with status {responseMessage.StatusCode}");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal error: {ex.Message}");
            }
        }
        
        [HttpPost("getBalance")]
        [Consumes("application/x-www-form-urlencoded")]
        [ApiExplorerSettings(IgnoreApi = true)]
        public async Task<IActionResult> GetBalance() // this overload doesn't specify the payload in its signature
        {
            try
            {
                Request.Body.Seek(0, SeekOrigin.Begin);
                using StreamReader reader = new(Request.Body, Encoding.UTF8);
                string requestJson = await reader.ReadToEndAsync();
                HttpResponseMessage responseMessage = await client.PostAsync(
                    "https://api.anti-captcha.com/getBalance", 
                    new StringContent(requestJson, Encoding.UTF8, "application/json"));
                if (responseMessage.IsSuccessStatusCode)
                {
                    var responseJson = await responseMessage.Content.ReadAsStringAsync();
                    return new ContentResult
                    {
                        Content = responseJson,
                        ContentType = responseMessage.Content.Headers.ContentType?.ToString() ?? "text/plain",
                        StatusCode = 200
                    };
                }
                return StatusCode(500, $"Request failed with status {responseMessage.StatusCode}");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal error: {ex.Message}");
            }
        }
        
        [HttpPost("createTask")]
        [Consumes("application/json")]
        public async Task<ActionResult<CreateTaskResponse>> CreateTask([FromBody] CreateTaskRequest request)
        {
            try
            {
                antiCaptchaService.IncCreateTaskCount();
                Request.Body.Seek(0, SeekOrigin.Begin);
                using StreamReader reader = new(Request.Body, Encoding.UTF8);
                string requestJson = await reader.ReadToEndAsync();
                HttpResponseMessage responseMessage = await client.PostAsync(
                    "https://api.anti-captcha.com/createTask", 
                    new StringContent(requestJson, Encoding.UTF8, "application/json"));
                if (responseMessage.IsSuccessStatusCode)
                {
                    var responseJson = await responseMessage.Content.ReadAsStringAsync();
                    var response = JsonConvert.DeserializeObject<CreateTaskResponse>(responseJson);
                    if (response != null && response.errorId == 0)
                    {
                        antiCaptchaService.IncCreateTaskSucceeded();
                    }
                    else
                    {
                        antiCaptchaService.IncCreateTaskErrors();
                    }
                    return new ContentResult
                    {
                        Content = responseJson,
                        ContentType = responseMessage.Content.Headers.ContentType?.ToString() ?? "text/plain",
                        StatusCode = 200
                    };
                }
                antiCaptchaService.IncCreateTaskFailed();
                return StatusCode(500, $"Request failed with status {responseMessage.StatusCode}");
            }
            catch (Exception ex)
            {
                antiCaptchaService.IncCreateTaskFailed();
                return StatusCode(500, $"Internal error: {ex.Message}");
            }
        }

        [HttpPost("createTask")]
        [Consumes("application/x-www-form-urlencoded")]
        [ApiExplorerSettings(IgnoreApi = true)]
        public async Task<ActionResult<CreateTaskResponse>> CreateTask() // this overload doesn't specify the payload in its signature
        {
            try
            {
                antiCaptchaService.IncCreateTaskCount();
                Request.Body.Seek(0, SeekOrigin.Begin);
                using StreamReader reader = new(Request.Body, Encoding.UTF8);
                string requestJson = await reader.ReadToEndAsync();
                HttpResponseMessage responseMessage = await client.PostAsync(
                    "https://api.anti-captcha.com/createTask",
                    new StringContent(requestJson, Encoding.UTF8, "application/json"));
                if (responseMessage.IsSuccessStatusCode)
                {
                    var responseJson = await responseMessage.Content.ReadAsStringAsync();
                    var response = JsonConvert.DeserializeObject<CreateTaskResponse>(responseJson);
                    if (response != null && response.errorId == 0)
                    {
                        antiCaptchaService.IncCreateTaskSucceeded();
                    }
                    else
                    {
                        antiCaptchaService.IncCreateTaskErrors();
                    }
                    return new ContentResult
                    {
                        Content = responseJson,
                        ContentType = responseMessage.Content.Headers.ContentType?.ToString() ?? "text/plain",
                        StatusCode = 200
                    };
                }
                antiCaptchaService.IncCreateTaskFailed();
                return StatusCode(500, $"Request failed with status {responseMessage.StatusCode}");
            }
            catch (Exception ex)
            {
                antiCaptchaService.IncCreateTaskFailed();
                return StatusCode(500, $"Internal error: {ex.Message}");
            }
        }

        [HttpPost("getTaskResult")]
        [Consumes("application/json")]
        public async Task<ActionResult<GetTaskResultResponse>> GetTaskResult([FromBody] GetTaskResultRequest request)
        {
            try
            {
                antiCaptchaService.IncGetTaskResultCount();
                HttpResponseMessage responseMessage = await client.PostAsJsonAsync(
                    "https://api.anti-captcha.com/getTaskResult", request);
                if (responseMessage.IsSuccessStatusCode)
                {
                    var responseJson = await responseMessage.Content.ReadAsStringAsync();
                    var response = JsonConvert.DeserializeObject<GetTaskResultResponse>(responseJson);
                    if (response != null && response.errorId == 0)
                    {
                        antiCaptchaService.IncGetTaskResultSucceeded();
                    }
                    else
                    {
                        antiCaptchaService.IncGetTaskResultErrors();
                    }
                    return new ContentResult
                    {
                        Content = responseJson,
                        ContentType = responseMessage.Content.Headers.ContentType?.ToString() ?? "text/plain",
                        StatusCode = 200
                    };
                }
                antiCaptchaService.IncGetTaskResultFailed();
                return StatusCode(500, $"Request failed with status {responseMessage.StatusCode}");
            }
            catch (Exception ex)
            {
                antiCaptchaService.IncGetTaskResultFailed();
                return StatusCode(500, $"Internal error: {ex.Message}");
            }
        }

        [HttpPost("getTaskResult")]
        [Consumes("application/x-www-form-urlencoded")]
        [ApiExplorerSettings(IgnoreApi = true)]
        public async Task<ActionResult<GetTaskResultResponse>> GetTaskResult() // this overload doesn't specify the payload in its signature
        {
            try
            {
                antiCaptchaService.IncGetTaskResultCount();
                Request.Body.Seek(0, SeekOrigin.Begin);
                using StreamReader reader = new(Request.Body, Encoding.UTF8);
                string requestJson = await reader.ReadToEndAsync();
                HttpResponseMessage responseMessage = await client.PostAsync(
                    "https://api.anti-captcha.com/getTaskResult",
                    new StringContent(requestJson, Encoding.UTF8, "application/json"));
                if (responseMessage.IsSuccessStatusCode)
                {
                    var responseJson = await responseMessage.Content.ReadAsStringAsync();
                    var response = JsonConvert.DeserializeObject<GetTaskResultResponse>(responseJson);
                    if (response != null && response.errorId == 0)
                    {
                        antiCaptchaService.IncGetTaskResultSucceeded();
                    }
                    else
                    {
                        antiCaptchaService.IncGetTaskResultErrors();
                    }
                    return new ContentResult
                    {
                        Content = responseJson,
                        ContentType = responseMessage.Content.Headers.ContentType?.ToString() ?? "text/plain",
                        StatusCode = 200
                    };
                }
                antiCaptchaService.IncGetTaskResultFailed();
                return StatusCode(500, $"Request failed with status {responseMessage.StatusCode}");
            }
            catch (Exception ex)
            {
                antiCaptchaService.IncGetTaskResultFailed();
                return StatusCode(500, $"Internal error: {ex.Message}");
            }
        }
    }
}
