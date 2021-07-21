using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PodBor
{
    class BZ
    {
        MySqlConnection connection = new MySqlConnection("host=z92656hn.beget.tech; username=z92656hn_medicat; password=A1s2D3f4; database=z92656hn_medicat;");
        // host=pg2.sweb.ru; 


        public void openConnection()
        {
            if (connection.State == System.Data.ConnectionState.Closed)
                connection.Open();
        }
        public void closeConnection()
        {
            if (connection.State == System.Data.ConnectionState.Open)
                connection.Close();
        }
        public MySqlConnection getConnection()
        {
            return connection;
        }
    }
}
