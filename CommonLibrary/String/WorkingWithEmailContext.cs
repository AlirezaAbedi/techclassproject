using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLibrary.String
{
    public class WorkingWithEmailContext
    {
        public ServiceDto<string> ExtractDomainFromEmail(string Email)
        {
            if (Email.IndexOf("@") == -1 )
            {
                return new ServiceDto<string>
                {
                    Status = 0,
                    Message = "Not Supported Email Format"
                };
            }
            else
            {
                return new ServiceDto<string>
                {
                    Data = Email.Split("@")[1],
                    Status = 1
                };
            }
        }
    }
}
