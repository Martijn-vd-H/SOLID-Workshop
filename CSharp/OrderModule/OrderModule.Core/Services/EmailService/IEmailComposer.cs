using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderModule.Core.Services.EmailService
{
    public interface IEmailComposer
    {
        Email ComposeEmail(string address, string type, string body);
    }
}
