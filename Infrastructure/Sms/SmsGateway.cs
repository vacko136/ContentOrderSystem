// Infrastructure/Sms/SmsGateway.cs
using System;

namespace ContentOrderSystem.Infrastructure.Sms
{
    public class SmsGateway
    {
        private readonly SmsProviderA _providerA = new();
        private readonly SmsProviderB _providerB = new();

        public void SendSms(string to, string content)
        {
            try
            {
                if (_providerA.CanSend())
                    _providerA.SendSms(to, content);
                else if (_providerB.CanSend())
                    _providerB.SendSms(to, content);
                else
                    throw new InvalidOperationException("All SMS providers are rate-limited.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to send SMS: {ex.Message}");
            }
        }
    }
}
