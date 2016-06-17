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
    public class GetGameTableController : ApiController
    {
        public ApiServices Services { get; set; }

        // GET api/GetGameTable
        public GameTable GetGetGameTable()
        {
            SqlConnection conn = new SqlConnection("Data Source=az4x4aulim.database.windows.net;Initial Catalog=pokerDealerService_db;Integrated Security=False;User ID=raf;Password=20031363rT;Connect Timeout=60;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            String sql = "SELECT * FROM dbo.Game";
            SqlCommand cmd = new SqlCommand(sql, conn);

            conn.Open();
            SqlDataReader myReader = cmd.ExecuteReader();
            myReader.Read();
            GameTable res = new GameTable()
            {
                Id1 = Int32.Parse(myReader["Id1"].ToString()),
                Id2 = Int32.Parse(myReader["Id2"].ToString()),
                Id3 = Int32.Parse(myReader["Id3"].ToString()),
                Id4 = Int32.Parse(myReader["Id4"].ToString()),
                active1 = Int32.Parse(myReader["active1"].ToString()),
                active2 = Int32.Parse(myReader["active2"].ToString()),
                active3 = Int32.Parse(myReader["active3"].ToString()),
                active4 = Int32.Parse(myReader["active4"].ToString()),
                current_id = Int32.Parse(myReader["current_id"].ToString()),
                pot1 = Int32.Parse(myReader["pot1"].ToString()),
                pot2 = Int32.Parse(myReader["pot2"].ToString()),
                pot3 = Int32.Parse(myReader["pot3"].ToString()),
                pot4 = Int32.Parse(myReader["pot4"].ToString()),
                active = Int32.Parse(myReader["active"].ToString()),
                firstPlayer = Int32.Parse(myReader["firstPlayer"].ToString()),
                currentFirstPlayer = Int32.Parse(myReader["currentFirstPlayer"].ToString()),
                state = myReader["state"].ToString()
            };
            myReader.Close();
            conn.Close();
            return res;
        }

    }
}
