using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace sciwebsitewordcounter.Models
{
    public class WordCountStore : IDisposable
    {
        public MySqlConnection Connection;
        public WordCountStore(string connectionString)
        {
            Connection = new MySqlConnection(connectionString);
            this.Connection.Open();
        }
        public void Dispose()
        {
            Connection.Close();
        }
    }
}
