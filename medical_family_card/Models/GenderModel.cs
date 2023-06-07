using medical_family_card.Repositories;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace medical_family_card.Models
{
    public static class GenderModel
    {
        private static Dictionary<int, string> _gender = new Dictionary<int, string>();

        public static Dictionary<int, string> Gender 
        {
            get 
            {
                return _gender; 
            } 
            private set
            {
                _gender = value;
            }
        
        }

        public static void CollectFromDataBase()
        {
            GenderModel.Gender.Clear();

            using (var connection = BaseRepository.GetConnection())
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "SELECT * FROM genders";
                var dataReader = command.ExecuteReader();
                while (dataReader.Read())
                {
                    GenderModel.Gender.Add(Convert.ToInt32(dataReader["id"]), Convert.ToString(dataReader["gender"]));
                }
            }
        }

        public static int ReturnIndex(string valueItem)
        {
            int index = 0;
            foreach (var item in Gender)
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
            return Gender[keyValue];
        }

    }
}
