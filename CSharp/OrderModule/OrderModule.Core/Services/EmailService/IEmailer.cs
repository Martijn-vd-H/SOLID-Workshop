using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderModule.Core.Services.EmailService
{
    public interface IEmailer
    {
        void SendEmail(Email email);
    }
}