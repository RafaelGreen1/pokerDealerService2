using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Microsoft.WindowsAzure.Mobile.Service;

namespace pokerDealerService2.Controllers
{
    public class SignInController : ApiController
    {
        public ApiServices Services { get; set; }

        // GET api/SignIn
        public string GetHello()
        {
            Services.Log.Info("Hello from custom controller!");
            return "Hello";
        }

    }
}
