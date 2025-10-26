// Application/Interfaces/ISmsGateway.cs
namespace ContentOrderSystem.Application.Interfaces
{
    public interface ISmsGateway
    {
        void SendSms(string to, string content);
    }
}
