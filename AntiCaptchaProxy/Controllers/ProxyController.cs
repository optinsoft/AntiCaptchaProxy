using AntiCaptchaProxy.Requests;
using AntiCaptchaProxy.Responses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace AntiCaptchaProxy.Controllers
{
    //[Route("[controller]")]
    [ApiController]
    public class ProxyController : ControllerBase
    {
        private readonly HttpClient client;

        public ProxyController()
        {
            client = new HttpClient();
        }

        [HttpPost("getBalance")]
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
    }
}
