using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace OrderModule.Core.Services.EmailService
{
    public class EmailComposer : IEmailComposer
    {
        public Email ComposeEmail(string address, string type, string body)
        {
            var email = new Email()
            {
                To = address,
                From = "Ordermodule@example.com",
                Header = $"Invoice {type}",
                Body = body,
            };

            return email;
        }
    }
}