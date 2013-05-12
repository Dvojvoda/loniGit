using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;

namespace WindowsFormsApplication1.Class
{
    public static class Klasa1
    {
        private   static MySqlConnection con = new MySqlConnection("Server=localhost;Port=3306;Database=distribuimi;Uid=root;");
        public static MySqlConnection MerrConnection()
        {
            if (con.State == System.Data.ConnectionState.Closed )
            {
               

                    con.Open();
           
            }
            return con;
        }
        public static MySqlConnection MbyllConnection()
        {
            if (con.State == System.Data.ConnectionState.Open )
            {
                con.Close();
            }
            return con;
        }
    }
}
