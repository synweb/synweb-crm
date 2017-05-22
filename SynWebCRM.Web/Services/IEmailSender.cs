using System.Threading.Tasks;

namespace SynWebCRM.Web.Services
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string email, string subject, string message);
    }
}
