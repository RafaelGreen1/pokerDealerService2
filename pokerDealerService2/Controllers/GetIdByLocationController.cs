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
    public class GetIdByLocationController : ApiController
    {
        public ApiServices Services { get; set; }

        // GET api/GetIdByLocation
        public int GetGetIdByLocation(Int32 location)
        {
            String result;
            SqlConnection conn = new SqlConnection("Data Source=az4x4aulim.database.windows.net;Initial Catalog=pokerDealerService_db;Integrated Security=False;User ID=raf;Password=20031363rT;Connect Timeout=60;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            String sql = "SELECT Id" + location.ToString() + " FROM dbo.Game;";
            SqlCommand cmd = new SqlCommand(sql, conn);
            conn.Open();
            result = (cmd.ExecuteScalar() ?? 0).ToString();
            conn.Close();
            return Int32.Parse(result);
        }

    }
}
