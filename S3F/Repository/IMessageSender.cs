using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentitySample
{
    public interface IMessageSender
    {
         Task SendEmailAsync(string toEmail, string subject, string message, bool isMessageHtml = false);
    }
}
