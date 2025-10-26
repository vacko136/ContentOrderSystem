// Infrastructure/Sms/SmsProviderA.cs
using System;

namespace ContentOrderSystem.Infrastructure.Sms
{
    public class SmsProviderA
    {
        private int _sentInLastMinute = 0;

        public bool CanSend() => _sentInLastMinute < 5;

        public void SendSms(string to, string content)
        {
            if (!CanSend())
                throw new InvalidOperationException("Rate limit exceeded for Provider A.");

            Console.WriteLine($"[Provider A] Sending SMS to {to}: {content}");
            _sentInLastMinute++;
        }

        public void ResetCounter() => _sentInLastMinute = 0;
    }
}
