using GameON.Common.Models;

namespace GameON.Web.Helpers
{

    public interface IMailHelper
    {
        Response SendMail(string to, string subject, string body);
    }

}
