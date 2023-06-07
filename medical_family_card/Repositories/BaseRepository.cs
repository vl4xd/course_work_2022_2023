using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Configuration;

namespace medical_family_card.Repositories
{
    public static class BaseRepository
    {
        private static readonly string _connectionString = $"Server={ConfigurationManager.AppSettings["Server"]};" +
                                                           $"Database={ConfigurationManager.AppSettings["Database"]};" +
                                                           $"Integrated Security=true";

        public static SqlConnection GetConnection()
        {
            return new SqlConnection( _connectionString );
        }
    }
}
