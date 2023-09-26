using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Requests.Messaging;

namespace Client.Infrastructure.Routes
{
    public static class MessagingEndpoints
    {
        public static string Send = "api/email";
        
        public static string SendMultiple = "api/email/multiple";
    }
}
