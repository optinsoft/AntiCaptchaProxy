using AntiCaptchaProxy.Models;

namespace AntiCaptchaProxy.Requests
{
    public class CreateTaskRequest
    {
#pragma warning disable IDE1006 // Naming Styles
        public string? clientKey { get; set; }
        public CaptchaTask? task { get; set; }
        public int? softId { get; set; }
        public string? callbackUrl { get; set; }
#pragma warning restore IDE1006 // Naming Styles
    }
}
