using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Microsoft.WindowsAzure.Mobile.Service;
using System.Data.SqlClient;

namespace pokerDealerService2.Controllers
{
    public class LogInUserController : ApiController
    {
        public ApiServices Services { get; set; }

        // GET api/LogInUser

        public string GetLogInUser(string username, string password)
        {
            String result;
            SqlConnection conn = new SqlConnection("Data Source=az4x4aulim.database.windows.net;Initial Catalog=pokerDealerService_db;Integrated Security=False;User ID=raf;Password=20031363rT;Connect Timeout=60;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            String sql = "SELECT Id FROM dbo.Users WHERE username = '" + username + "' AND password ='" + password + "';";
            SqlCommand cmd = new SqlCommand(sql, conn);
            conn.Open();
            result = (cmd.ExecuteScalar() ?? "0").ToString();
            conn.Close();
            return result;
        }
    }
}
