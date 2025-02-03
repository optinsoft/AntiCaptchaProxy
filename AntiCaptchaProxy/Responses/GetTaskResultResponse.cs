namespace AntiCaptchaProxy.Responses
{
    public class GetTaskResultResponse
    {
#pragma warning disable IDE1006 // Naming Styles
        public int? errorId { get; set; }
        public string? errorCode { get; set; }
        public string? errorDescription { get; set; }
        public string? status { get; set; }
        public double? cost { get; set; }
        public string? ip { get; set; }
        public int? createTime { get; set; }
        public int? endTime { get; set; }
        public int? solveCount { get; set; }
#pragma warning restore IDE1006 // Naming Styles
    }
}
