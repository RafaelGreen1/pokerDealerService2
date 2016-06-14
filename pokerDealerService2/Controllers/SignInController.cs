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
    public class SignInController : ApiController
    {
        public ApiServices Services { get; set; }

        // GET api/SignIn
        public string GetHello()
        {
            Services.Log.Info("Hello from custom controller!");
            return "Hello";
        }

        public string GetAddUser(string username, string password, 
            string forename, string lastname, string email, string phone)
        {
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = "Data Source=az4x4aulim.database.windows.net;Initial Catalog=pokerDealerService_db;Integrated Security=False;User ID=raf;Password=20031363rT;Connect Timeout=60;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            conn.Open();

            string queryString = "INSERT INTO dbo.Users VALUES ('" + username + "', '" + password +
                   "', '" + forename + "', '" + lastname +
                   "', '" + email + "', '" + phone + "');";
            SqlCommand command = new SqlCommand(queryString, conn);
            int rowsAffected = command.ExecuteNonQuery();
            
            conn.Close();
            if (rowsAffected == 1)
            {
                return "New user (" + username + ") has been added";
            }
            return "Sign in failed!";
        }

    }
}
