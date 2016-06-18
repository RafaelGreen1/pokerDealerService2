using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Microsoft.WindowsAzure.Mobile.Service;
using System.Data.SqlClient;
using pokerDealerService2.DataObjects;

namespace pokerDealerService2.Controllers
{
    public class ZeroAllPotsController : ApiController
    {
        public ApiServices Services { get; set; }

        // GET api/ZeroAllPots
        public int GetZeroAllPots()
        {
            SqlConnection conn = new SqlConnection("Data Source=az4x4aulim.database.windows.net;Initial Catalog=pokerDealerService_db;Integrated Security=False;User ID=raf;Password=20031363rT;Connect Timeout=60;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            string sql = "UPDATE dbo.Game SET pot1=0, pot2=0, pot3=0, pot4=0;";
            SqlCommand cmd = new SqlCommand(sql, conn);
            conn.Open();
            int rv = cmd.ExecuteNonQuery();
            conn.Close();
            return rv;
        }

    }
}
