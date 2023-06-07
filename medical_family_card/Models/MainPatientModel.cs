using medical_family_card.Repositories;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls.Primitives;

namespace medical_family_card.Models
{
    public static class MainPatientModel
    {
        #region Поля
        private static int _id;
        private static string _username;
        private static string _passwd;
        private static string _firstName;
        private static string _lastName;
        private static string _middleName;
        private static DateTime _dateOfBirth;
        private static byte[] _photo;
        private static int _bloodTypeId;
        private static int _rhesusFactorId;
        private static int _genderId;
        #endregion

        #region Свойства
        public static int Id
        {
            get { return _id; }
            set { _id = value; }
        }
        public static string Username
        {
            get { return _username; }
            set { _username = value; }
        }
        public static string Passwd
        {
            get { return _passwd; }
            set { _passwd = value; }
        }
        public static string FirstName
        {
            get { return _firstName; }
            set { _firstName = value; }
        }
        public static string LastName
        {
            get { return _lastName; }
            set { _lastName = value; }
        }
        public static string MiddleName
        {
            get { return _middleName; }
            set { _middleName = value; }
        }
        public static DateTime DateOfBirth
        {
            get { return _dateOfBirth.Date; }
            set { _dateOfBirth = value; }
        }
        public static byte[] Photo
        {
            get { return _photo; }
            set { _photo = value; }
        }
        public static int BloodTypeId
        {
            get { return _bloodTypeId; }
            set { _bloodTypeId = value; }
        }
        public static int RhesusFactorId
        {
            get { return _rhesusFactorId; }
            set { _rhesusFactorId = value; }
        }
        public static int GenderId
        {
            get { return _genderId; }
            set { _genderId = value; }
        }
        #endregion

        /// <summary>
        /// Метод для нахождения (обновления ифнормации о пациенте)
        /// </summary>
        /// <returns></returns>
        public static bool FindPatientInfo()
        {
            bool validInfo;

            //Обновления Логина и Пароля
            using (var connection = BaseRepository.GetConnection())
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "SELECT * FROM patients WHERE id=@id";
                command.Parameters.Add("@id", SqlDbType.Int).Value = Id;

                var dataReader = command.ExecuteReader();
                while (dataReader.Read())
                {
                    MainPatientModel.Username = Convert.ToString(dataReader["username"]);
                    MainPatientModel.Passwd = Convert.ToString(dataReader["passwd"]);
                }
            }

            using (var connection = BaseRepository.GetConnection())
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "SELECT * FROM info_about_patients WHERE patient_id=@patient_id";
                command.Parameters.Add("@patient_id", SqlDbType.Int).Value = Id;

                //validInfo = command.ExecuteScalar() == null ? false : true;

                if (command.ExecuteScalar() == null)
                {
                    validInfo = false;
                }
                else
                {
                    var dataReader = command.ExecuteReader();
                    while (dataReader.Read())
                    {
                        MainPatientModel.FirstName = Convert.ToString(dataReader["first_name"]);
                        MainPatientModel.LastName = Convert.ToString(dataReader["last_name"]);
                        MainPatientModel.MiddleName = Convert.ToString(dataReader["middle_name"]);
                        MainPatientModel.DateOfBirth = Convert.ToDateTime(dataReader["date_of_birth"]);
                        MainPatientModel.Photo = (byte[])dataReader["photo"];
                        MainPatientModel.BloodTypeId = Convert.ToInt32(dataReader["blood_type_id"]);
                        MainPatientModel.RhesusFactorId = Convert.ToInt32(dataReader["rhesus_factor_id"]);
                        MainPatientModel.GenderId = Convert.ToInt32(dataReader["gender_id"]);
                    }
                    validInfo = true;
                }
            }
            return validInfo;
        }
    }
}
