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
    public static class PressureModel
    {
        public static int Id { get; set; }

        public static bool FindPressureWithTimeDate(int id, int patient_id, DateTime pressureTime, DateTime pressureDate)
        {
            bool validWeight;

            using (var connection = BaseRepository.GetConnection())
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "SELECT * FROM pressures WHERE pressure_time=@pressure_time AND pressure_date=@pressure_date AND id!=@id AND patient_id=@patient_id";
                command.Parameters.Add("@pressure_time", SqlDbType.Time).Value = pressureTime.TimeOfDay;
                command.Parameters.Add("@pressure_date", SqlDbType.Date).Value = pressureDate;
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
