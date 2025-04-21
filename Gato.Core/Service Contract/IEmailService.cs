using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gato.Core.Service_Contract
{
   
        public interface IEmailService
        {
            Task SendEmailAsync(string receptor, string subject, string body);
        }

    
}
