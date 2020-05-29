using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GameON.Common.Models;

namespace GameON.Web.Helpers
{
    
        public interface IMailHelper
        {
            Response SendMail(string to, string subject, string body);
        }
    
}
