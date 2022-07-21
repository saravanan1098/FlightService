using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthenticationService.Models
{
    public class Responsemodel
    {
        public string Username { get; set; }
        public string MailId { get; set; }
        public string Role { get; set; }
        public string Token { get; set; }
    }
}
