namespace AntiCaptchaProxy.Models
{
    public class ProxyStats
    {
        public int CreateTaskCount { get; set; }
        public int CreateTaskSucceeded {  get; set; }
        public int CreateTaskFailed { get; set; }
        public int CreateTaskErrors { get; set; }
        public int GetTaskResultCount { get; set; }
        public int GetTaskResultSucceeded { get; set; }
        public int GetTaskResultFailed { get; set; }
        public int GetTaskResultErrors { get; set; }
    }
}
