using medical_family_card.Repositories;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace medical_family_card.Models
{
    public static class MedicalVisitModel
    {
        public static int Id { get; set; }
        public static int Patient_Id { get; set; }
        public static string Medical_Visit_Name { get; set; }
        public static DateTime St_Date { get; set; }
        public static DateTime End_Date { get; set; }
        public static decimal Cost { get; set; }
        public static string Comment { get; set; }

        public static int CountPhoto(int id)
        {
            int count;
            using (var connection = BaseRepository.GetConnection())
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "SELECT COUNT(*) AS count_photo FROM photo_about_medical_visits WHERE medical_visit_id=@id";
                command.Parameters.Add("@id", SqlDbType.Int).Value = id;

                var dataReader = command.ExecuteReader();
                dataReader.Read();
                count = Convert.ToInt32(dataReader["count_photo"]);
            }
            return count;
        }

        public static void DeleteAllPhoto(int id)
        {
            using (var connection = BaseRepository.GetConnection())
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText =
                    $"DELETE FROM photo_about_medical_visits " +
                    "WHERE " +
                    $"medical_visit_id=@id";
                command.Parameters.Add("@id", SqlDbType.Int).Value = id;
                command.ExecuteNonQuery();
            }
        }
    }
}
