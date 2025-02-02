namespace AntiCaptchaProxy.Responses
{
    public class BalanceResponse
    {
#pragma warning disable IDE1006 // Naming Styles
        public int? errorId { get; set; }
        public string? errorCode { get; set; }
        public string? errorDescription { get; set; }
        public double? balance { get; set; }
        public int? captchaCredits { get; set; }
#pragma warning restore IDE1006 // Naming Styles
    }
}
