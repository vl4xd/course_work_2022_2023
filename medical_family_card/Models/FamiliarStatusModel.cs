using medical_family_card.Repositories;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace medical_family_card.Models
{
    public static class FamiliarStatusModel
    {
        private static Dictionary<int, string> _familiarStatus = new Dictionary<int, string>();

        public static Dictionary<int, string> FamiliarStatus 
        {
            get
            {
                return _familiarStatus;
            }
            private set
            {
                _familiarStatus = value;
            }
        }

        public static void CollectFromDataBase()
        {
            FamiliarStatus.Clear();

            using (var connection = BaseRepository.GetConnection())
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "SELECT * FROM familiar_statuses";
                var dataReader = command.ExecuteReader();
                while (dataReader.Read())
                {
                    FamiliarStatusModel.FamiliarStatus.Add(Convert.ToInt32(dataReader["id"]), Convert.ToString(dataReader["status_name"]));
                }
            }
        }

        public static int ReturnIndex(string valueItem)
        {
            int index = 0;
            foreach (var item in FamiliarStatus)
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
            return FamiliarStatus[keyValue];
        }

    }
}
