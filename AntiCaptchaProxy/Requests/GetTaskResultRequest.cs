using AntiCaptchaProxy.Models;

namespace AntiCaptchaProxy.Requests
{
    public class GetTaskResultRequest
    {
#pragma warning disable IDE1006 // Naming Styles
        public string? clientKey { get; set; }
        public int? taskId { get; set; }
#pragma warning restore IDE1006 // Naming Styles
    }
}
