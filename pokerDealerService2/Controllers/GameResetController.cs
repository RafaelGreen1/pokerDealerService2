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
    public class GameResetController : ApiController
    {
        public ApiServices Services { get; set; }

        // GET api/GameReset
        public int GetGameReset()
        {
            SqlConnection conn = new SqlConnection("Data Source=az4x4aulim.database.windows.net;Initial Catalog=pokerDealerService_db;Integrated Security=False;User ID=raf;Password=20031363rT;Connect Timeout=60;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            String sql = "UPDATE dbo.Game SET" +
                                 " Id1=0" +
                                 " ,Id2=0" +
                                 " ,Id3=0" +
                                 " ,Id4=0" +
                                 " ,active1=0" +
                                 " ,active2=0" +
                                 " ,active3=0" +
                                 " ,active4=0" +
                                 " ,current_id=0" +
                                 " ,pot1=0" +
                                 " ,pot2=0" +
                                 " ,pot3=0" +
                                 " ,pot4=0" +
                                 " ,active=0" +
                                 " ,firstPlayer=0" +
                                 " ,currentFirstPlayer=0" +
                                 " ,state='clear';";
            SqlCommand command = new SqlCommand(sql, conn);
            conn.Open();
            int rv = command.ExecuteNonQuery();
            conn.Close();
            return rv;
        }

    }
}
