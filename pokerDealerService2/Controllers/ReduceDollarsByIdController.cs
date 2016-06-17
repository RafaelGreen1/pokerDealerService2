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
    public class ReduceDollarsByIdController : ApiController
    {
        public ApiServices Services { get; set; }

        // GET api/ReduceDollarsById
        public int GetReduceDollarsByIdController(Int32 Id, Int32 dollars)
        {
            SqlConnection conn = new SqlConnection("Data Source=az4x4aulim.database.windows.net;Initial Catalog=pokerDealerService_db;Integrated Security=False;User ID=raf;Password=20031363rT;Connect Timeout=60;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            String sql = "Update dbo.Users SET dollars = dollars - " + dollars.ToString() +
                "WHERE Id=" + Id.ToString() + ";";
            SqlCommand cmd = new SqlCommand(sql, conn);
            conn.Open();
            int rowsAffected = cmd.ExecuteNonQuery();
            conn.Close();
            return rowsAffected;
        }

    }
}
