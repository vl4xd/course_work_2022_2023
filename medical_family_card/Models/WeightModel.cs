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
    public static class WeightModel
    {
        private static int _id;
        private static int _patient_id;
        private static double _weight_value;
        private static DateTime _weight_date;

        public static int Id
        {
            get { return _id; }
            set { _id = value; }
        }
        public static int Patient_Id
        {
            get { return _patient_id; }
            set { _patient_id = value; }
        }
        public static double Weight_Value
        {
            get { return _weight_value; }
            set { _weight_value = value; }
        }
        public static string Weight_Date
        {
            get { return _weight_date.ToString("d"); }
            set { _weight_date = Convert.ToDateTime(value); }
        }

        public static bool FindWeightWithDate()
        {
            bool validWeight;

            using (var connection = BaseRepository.GetConnection())
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "SELECT * FROM weights WHERE weight_date=@weight_date";
                command.Parameters.Add("@weight_date", SqlDbType.Date).Value = Convert.ToDateTime(Weight_Date);

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

        public static bool FindWeightWithDate(int id, int patient_id)
        {
            bool validWeight;

            using (var connection = BaseRepository.GetConnection())
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "SELECT * FROM weights WHERE weight_date=@weight_date AND id!=@id AND patient_id=@patient_id";
                command.Parameters.Add("@weight_date", SqlDbType.Date).Value = Convert.ToDateTime(Weight_Date);
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
