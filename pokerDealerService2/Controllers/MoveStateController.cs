using System;
using System.Web.Http;
using Microsoft.WindowsAzure.Mobile.Service;
using System.Data.SqlClient;

namespace pokerDealerService2.Controllers
{
    public class MoveStateController : ApiController
    {
        public ApiServices Services { get; set; }

        // GET api/MoveState
        public string GetMoveState()
        {
            String state;
            String newState = "clear";
            SqlConnection conn = new SqlConnection("Data Source=az4x4aulim.database.windows.net;Initial Catalog=pokerDealerService_db;Integrated Security=False;User ID=raf;Password=20031363rT;Connect Timeout=60;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            String sql = "SELECT state FROM dbo.Game;";
            SqlCommand cmd = new SqlCommand(sql, conn);
            conn.Open();
            state = (cmd.ExecuteScalar() ?? "0").ToString();
            conn.Close();
            
            if (state.Trim().Equals("clear"))
            {
                newState = "dealed";
            } else if (state.Trim().Equals("dealed"))
            {
                newState = "flop";
            } else if (state.Trim().Equals("flop"))
            {
                newState = "turn";
            } else if (state.Trim().Equals("turn"))
            {
                newState = "river";
            } else if (state.Trim().Equals("river"))
            {
                newState = "clear";
            }
            sql = "UPDATE dbo.Game SET state='"+ newState + "';";
            cmd = new SqlCommand(sql, conn);
            conn.Open();
            int rv = cmd.ExecuteNonQuery();
            conn.Close();
            return newState;
        }

    }
}
