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
    public class MoveCurrentController : ApiController
    {
        public ApiServices Services { get; set; }

        // GET api/MoveCurrent
        public int GetMoveCurrent()
        {
            int[] active = new int[4];
            int current_id = 0;
            int currentFirstPlayer = 0;
            string state = "clear";
            string newState = "clear";
            SqlConnection conn = new SqlConnection("Data Source=az4x4aulim.database.windows.net;Initial Catalog=pokerDealerService_db;Integrated Security=False;User ID=raf;Password=20031363rT;Connect Timeout=60;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            String sql = "SELECT * FROM dbo.Game";
            SqlCommand cmd = new SqlCommand(sql, conn);

            conn.Open();
            SqlDataReader myReader = cmd.ExecuteReader();
            myReader.Read();
            active[0] = Int32.Parse(myReader["active1"].ToString());
            active[1] = Int32.Parse(myReader["active2"].ToString());
            active[2] = Int32.Parse(myReader["active3"].ToString());
            active[3] = Int32.Parse(myReader["active4"].ToString());
            current_id = Int32.Parse(myReader["current_id"].ToString());
            currentFirstPlayer = Int32.Parse(myReader["currentFirstPlayer"].ToString());
            state = myReader["state"].ToString().Trim();
            newState = myReader["state"].ToString().Trim();
            myReader.Close();
            conn.Close();

            current_id--;
            do
            {
                current_id = (current_id + 1) % 4;
            } while (active[current_id] != 1);
            current_id++;
            if (current_id == currentFirstPlayer)
            {
                if (state.Equals("clear"))
                {
                    newState = "dealed";
                }
                else if (state.Equals("dealed"))
                {
                    newState = "flop";
                }
                else if (state.Equals("flop"))
                {
                    newState = "turn";
                }
                else if (state.Equals("turn"))
                {
                    newState = "river";
                }
                else if (state.Equals("river"))
                {
                    newState = "clear";
                }
            }
            sql = "UPDATE dbo.Game SET current_id=" + current_id + "', state='" + state + "';";
            cmd = new SqlCommand(sql, conn);
            conn.Open();
            int rowsAffected = cmd.ExecuteNonQuery();
            conn.Close();
            return rowsAffected;
        }

    }
}
