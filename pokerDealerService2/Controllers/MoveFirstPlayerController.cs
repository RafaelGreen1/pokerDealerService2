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
    public class MoveFirstPlayerController : ApiController
    {
        public ApiServices Services { get; set; }

        // GET api/MoveCurrentFirstPlayer
        public int GetMoveFirstPlayer()
        {
            int[] Id = new int[4];
            int firstPlayer = 0;
            SqlConnection conn = new SqlConnection("Data Source=az4x4aulim.database.windows.net;Initial Catalog=pokerDealerService_db;Integrated Security=False;User ID=raf;Password=20031363rT;Connect Timeout=60;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            String sql = "SELECT * FROM dbo.Game";
            SqlCommand cmd = new SqlCommand(sql, conn);

            conn.Open();
            SqlDataReader myReader = cmd.ExecuteReader();
            myReader.Read();
            Id[0] = Int32.Parse(myReader["Id1"].ToString());
            Id[1] = Int32.Parse(myReader["Id2"].ToString());
            Id[2] = Int32.Parse(myReader["Id3"].ToString());
            Id[3] = Int32.Parse(myReader["Id4"].ToString());
            firstPlayer = Int32.Parse(myReader["firstPlayer"].ToString());
            myReader.Close();
            conn.Close();

            if (Id[0] == 0 && Id[1] == 0 && Id[2] == 0 && Id[3] == 0)
            {
                return 0;
            }
            firstPlayer--;
            do
            {
                firstPlayer = (firstPlayer + 1) % 4;
            } while (Id[firstPlayer] == 0);
            firstPlayer++;
            sql = "UPDATE dbo.Game SET firstPlayer=" + firstPlayer + ", current_id=" + firstPlayer +
                ", currentFirstPlayer=" + firstPlayer +";";
            cmd = new SqlCommand(sql, conn);
            conn.Open();
            int rowsAffected = cmd.ExecuteNonQuery();
            conn.Close();
            return rowsAffected;
        }

    }
}
