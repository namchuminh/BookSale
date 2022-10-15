using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace BookSale.Models
{
    internal class ConnectionDB
    {
        SqlConnection conn;
        String connStr;
        public ConnectionDB()
        {
            connStr = @"Data Source=CHUMINHNAM; Initial Catalog=BookSale; Integrated Security=SSPI";
        }

        public SqlConnection ConnectDB()
        {
            conn = new SqlConnection(connStr);
            return conn;
        }
    }
}
