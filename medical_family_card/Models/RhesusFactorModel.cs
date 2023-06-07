using medical_family_card.Repositories;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace medical_family_card.Models
{
    public static class RhesusFactorModel
    {
        private static Dictionary<int, string> _rhesusFactor = new Dictionary<int, string>();

        public static Dictionary<int, string> RhesusFactor
        {
            get
            {
                return _rhesusFactor;
            }
            private set
            {
                _rhesusFactor = value;
            }
        }

        public static void CollectFromDataBase()
        {
            RhesusFactor.Clear();

            using (var connection = BaseRepository.GetConnection())
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "SELECT * FROM rhesus_factors";
                var dataReader = command.ExecuteReader();
                while (dataReader.Read())
                {
                    RhesusFactorModel.RhesusFactor.Add(Convert.ToInt32(dataReader["id"]), Convert.ToString(dataReader["rhesus_factor"]));
                }
            }
        }

        public static int ReturnIndex(string valueItem)
        {
            int index = 0;
            foreach (var item in RhesusFactor)
            {
                if (item.Value == valueItem)
                {
                    index = item.Key;
                    break;
                }
            }
            return index;
        }

        public static string ReturnValue(int keyValue)
        {
            return RhesusFactor[keyValue];
        }
    }
}
