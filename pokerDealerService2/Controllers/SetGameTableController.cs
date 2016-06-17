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
    public class SetGameTableController : ApiController
    {
        public ApiServices Services { get; set; }

        // GET api/SetGameTable
        public int GetSetGameTable(Int32 Id1
                                    , Int32 Id2
                                    , Int32 Id3
                                    , Int32 Id4
                                    , Int32 active1
                                    , Int32 active2
                                    , Int32 active3
                                    , Int32 active4
                                    , Int32 current_id
                                    , Int32 pot1
                                    , Int32 pot2
                                    , Int32 pot3
                                    , Int32 pot4
                                    , Int32 active
                                    , Int32 firstPlayer
                                    , Int32 currentFirstPlayer
                                    , string state)
        {
            SqlConnection conn = new SqlConnection("Data Source=az4x4aulim.database.windows.net;Initial Catalog=pokerDealerService_db;Integrated Security=False;User ID=raf;Password=20031363rT;Connect Timeout=60;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            String sql = "UPDATE dbo.Game SET" +
                                 " Id1=" + Id1 +
                                 " ,Id2=" + Id2 +
                                 " ,Id3=" + Id3 +
                                 " ,Id4=" + Id4 +
                                 " ,active1=" + active1 +
                                 " ,active2=" + active2 +
                                 " ,active3=" + active3 +
                                 " ,active4=" + active4 +
                                 " ,current_id=" + current_id +
                                 " ,pot1=" + pot1 +
                                 " ,pot2=" + pot2 +
                                 " ,pot3=" + pot3 +
                                 " ,pot4=" + pot4 +
                                 " ,active=" + active +
                                 " ,firstPlayer=" + firstPlayer +
                                 " ,currentFirstPlayer=" + currentFirstPlayer +
                                 " ,state='" + state + "';";
            SqlCommand command = new SqlCommand(sql, conn);
            conn.Open();
            int rv = command.ExecuteNonQuery();
            conn.Close();
            return rv;
        }

    }
}
