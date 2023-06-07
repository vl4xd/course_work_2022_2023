using medical_family_card.Repositories;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace medical_family_card.Models
{
    public static class HeightModel
    {
        public static int Id { get; set; }
        public static int Patient_Id { get; set; }
        public static DateTime Height_Date { get; set; }
        public static double Height_Value { get; set; }

        public static bool FindHeightWithDate(int id, int patient_id)
        {
            bool validWeight;

            using (var connection = BaseRepository.GetConnection())
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "SELECT * FROM heights WHERE height_date=@height_date AND id!=@id AND patient_id=@patient_id";
                command.Parameters.Add("@height_date", SqlDbType.Date).Value = Height_Date;
                command.Parameters.Add("@id", SqlDbType.Int).Value = id;
                command.Parameters.Add("@patient_id", SqlDbType.Int).Value = patient_id;

                if (command.ExecuteScalar() == null)
                {
                    validWeight = true;
                }
                else
                {
                    validWeight = false;
                }
            }

            return validWeight;
        }
    }
}
