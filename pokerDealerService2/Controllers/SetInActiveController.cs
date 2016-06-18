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
    public class SetInActiveController : ApiController
    {
        public ApiServices Services { get; set; }

        // GET api/SetInActive
        public int GetSetInActive(Int32 Id)
        {
            int[] Ids = new int[4];
            int location = 0;
            SqlConnection conn = new SqlConnection("Data Source=az4x4aulim.database.windows.net;Initial Catalog=pokerDealerService_db;Integrated Security=False;User ID=raf;Password=20031363rT;Connect Timeout=60;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            String sql = "SELECT * FROM dbo.Game";
            SqlCommand cmd = new SqlCommand(sql, conn);

            conn.Open();
            SqlDataReader myReader = cmd.ExecuteReader();
            myReader.Read();
            Ids[0] = Int32.Parse(myReader["Id1"].ToString());
            Ids[1] = Int32.Parse(myReader["Id2"].ToString());
            Ids[2] = Int32.Parse(myReader["Id3"].ToString());
            Ids[3] = Int32.Parse(myReader["Id4"].ToString());
            myReader.Close();
            conn.Close();

            if (Id != Ids[0] && Id != Ids[1] && Id != Ids[2] && Id != Ids[3])
            {
                return 0;
            }

            if (Id == Ids[0])
            {
                location = 1;
            } else if (Id == Ids[1])
            {
                location = 2;
            } else if (Id == Ids[2])
            {
                location = 3;
            } else if (Id == Ids[3])
            {
                location = 4;
            }

            sql = "UPDATE dbo.Game SET active" + location + "=0;";
            cmd = new SqlCommand(sql, conn);
            conn.Open();
            int rowsAffected = cmd.ExecuteNonQuery();
            conn.Close();
            return rowsAffected;
        }
    }
}
