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
    public class FillFirstAvailableIdController : ApiController
    {
        public ApiServices Services { get; set; }

        // GET api/FillFirstAvailabeId
        public string GetFillFirstAvailableId(Int32 id_in)
        {
            String id = "Id";
            int rowsAffected = 0;
            SqlConnection conn = new SqlConnection("Data Source=az4x4aulim.database.windows.net;Initial Catalog=pokerDealerService_db;Integrated Security=False;User ID=raf;Password=20031363rT;Connect Timeout=60;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            String sql = "SELECT * FROM dbo.Game";
            SqlCommand cmd = new SqlCommand(sql, conn);

            conn.Open();
            SqlDataReader myReader = cmd.ExecuteReader();
            myReader.Read();
            for (int i = 1; i <= 4; i++)
            {
                if (myReader[id + i.ToString()].ToString().Equals("0"))
                {
                    myReader.Close();
                    sql = "UPDATE dbo.game SET Id" + i.ToString() + "=" + id_in.ToString() + ";";
                    SqlCommand command = new SqlCommand(sql, conn);
                    rowsAffected = command.ExecuteNonQuery();
                    break;
                }
            }
            conn.Close();
            return rowsAffected.ToString();
        }

    }
}
