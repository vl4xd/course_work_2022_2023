using medical_family_card.Repositories;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace medical_family_card.Models
{
    public static class BloodTypeModel
    {
        private static Dictionary<int, string> _bloodType = new Dictionary<int, string>();

        public static Dictionary<int, string> BloodType 
        {
            get
            {
                return _bloodType;
            }
            private set
            {
                _bloodType = value;
            }
        }

        public static void CollectFromDataBase()
        {
            BloodType.Clear();

            using (var connection = BaseRepository.GetConnection())
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "SELECT * FROM blood_types";
                var dataReader = command.ExecuteReader();
                while (dataReader.Read())
                {
                    BloodTypeModel.BloodType.Add(Convert.ToInt32(dataReader["id"]), Convert.ToString(dataReader["blood_type"]));
                }
            }
        }

        public static int ReturnIndex(string valueItem)
        {
            int index = 0;
            foreach (var item in BloodType) 
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
            return BloodType[keyValue];
        }
    }
}
