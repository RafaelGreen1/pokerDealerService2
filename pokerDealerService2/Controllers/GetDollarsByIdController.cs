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
    public class GetDollarsByIdController : ApiController
    {
        public ApiServices Services { get; set; }

        // GET api/GetdollarsById
        public string GetGetDollarsById(Int32 id)
        {
            String result;
            SqlConnection conn = new SqlConnection("Data Source=az4x4aulim.database.windows.net;Initial Catalog=pokerDealerService_db;Integrated Security=False;User ID=raf;Password=20031363rT;Connect Timeout=60;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            String sql = "SELECT dollars FROM dbo.Users WHERE Id = " + id.ToString() + ";";
            SqlCommand cmd = new SqlCommand(sql, conn);
            conn.Open();
            result = (cmd.ExecuteScalar() ?? "0").ToString();
            conn.Close();
            return result.Trim();
        }

    }
}
