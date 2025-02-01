using AntiCaptchaProxy.Interfaces;

namespace AntiCaptchaProxy.Services
{
    public class AntiCaptchaService : IAntiCaptchaService
    {
        private readonly string _serviceInfo;

        public AntiCaptchaService()
        {
            _serviceInfo = $"Service created at {DateTime.Now}";
        }

        public string GetServiceInfo()
        {
            return _serviceInfo;
        }
    }
}
