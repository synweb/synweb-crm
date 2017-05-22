using System.Threading.Tasks;

namespace SynWebCRM.Web.Services
{
    public interface ISmsSender
    {
        Task SendSmsAsync(string number, string message);
    }
}
