namespace AntiCaptchaProxy.Responses
{
    public class StatsResponse
    {
#pragma warning disable IDE1006 // Naming Styles
        public string? serviceInfo { get; set; }
        public int? createTaskCount { get; set; }
        public int? createTaskSucceeded {  get; set; }
        public int? createTaskFailed { get; set; }
        public int? createTaskErrors { get; set; }
        public int? getTaskResultCount { get; set; }
        public int? getTaskResultSucceeded { get; set; }
        public int? getTaskResultFailed { get; set;}
        public int? getTaskResultErrors { get; set; }
#pragma warning restore IDE1006 // Naming Styles
    }
}
