namespace AntiCaptchaProxy.Models
{
    public class ProxyStats
    {
        public Guid Id { get; set; }
        public int CreateTaskCount { get; set; }
        public int CreateTaskSucceeded {  get; set; }
        public int CreateTaskFailed { get; set; }
        public int CreateTaskErrors { get; set; }
        public int GetTaskResultCount { get; set; }
        public int GetTaskResultSucceeded { get; set; }
        public int GetTaskResultFailed { get; set; }
        public int GetTaskResultErrors { get; set; }
        public double? LastBalance { get; set; }
        public string? LastBalanceTime { get; set; }
        public void Assign(ProxyStats proxyStats)
        {
            Id = proxyStats.Id;
            CreateTaskCount = proxyStats.CreateTaskCount;
            CreateTaskSucceeded = proxyStats.CreateTaskSucceeded;
            CreateTaskFailed = proxyStats.CreateTaskFailed;
            CreateTaskErrors = proxyStats.CreateTaskErrors;
            GetTaskResultCount = proxyStats.GetTaskResultCount;
            GetTaskResultSucceeded = proxyStats.GetTaskResultSucceeded;
            GetTaskResultFailed = proxyStats.GetTaskResultFailed;
            GetTaskResultErrors = proxyStats.GetTaskResultErrors;
            LastBalance = proxyStats.LastBalance;
            LastBalanceTime = proxyStats.LastBalanceTime;
        }
    }
}
