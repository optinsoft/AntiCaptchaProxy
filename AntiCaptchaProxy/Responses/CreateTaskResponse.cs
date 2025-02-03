namespace AntiCaptchaProxy.Responses
{
    public class CreateTaskResponse
    {
#pragma warning disable IDE1006 // Naming Styles
        public int? errorId { get; set; }
        public string? errorCode { get; set; }
        public string? errorDescription { get; set; }
        public int? taskId { get; set; }
#pragma warning restore IDE1006 // Naming Styles
    }
}
