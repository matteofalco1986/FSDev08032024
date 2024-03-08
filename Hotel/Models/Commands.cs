using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Hotel.Models
{
    public class Commands
    {
        public static SqlConnection ConnectToDb()
        {
            // Get connection data
            string connString = ConfigurationManager.ConnectionStrings["DbHotelConnection"].ToString();
            var conn = new SqlConnection(connString);

            // Open connection to database
            conn.Open();
            return conn;
        }
    }
}
