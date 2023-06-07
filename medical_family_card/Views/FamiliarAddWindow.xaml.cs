using medical_family_card.Models;
using medical_family_card.Repositories;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
//using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace medical_family_card.Views
{
    /// <summary>
    /// Логика взаимодействия для FamiliarAddWindow.xaml
    /// </summary>
    public partial class FamiliarAddWindow : Window
    {
        private int FamiliarId { get; set; }
        private bool Checher { get; set; }

        public FamiliarAddWindow()
        {
            InitializeComponent();
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }

        private void menuItemAdd_Click(object sender, RoutedEventArgs e)
        {
            if (Checher)
            {
                CreateNewFamiliar();
                this.Close();
            }
        }

        private void menuItemCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void CreateNewFamiliar()
        {
            using (var connection = BaseRepository.GetConnection())
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText =
                    "INSERT INTO familiars " +
                    "(from_patient_id, to_patient_id, familiar_status_id) " +
                    "VALUES " +
                    "(@from_patient_id,@to_patient_id,@familiar_status_id)";
                command.Parameters.Add("@from_patient_id", SqlDbType.Int).Value = MainPatientModel.Id;
                command.Parameters.Add("@to_patient_id", SqlDbType.Int).Value = FamiliarId;
                command.Parameters.Add("@familiar_status_id", SqlDbType.Int).Value = FamiliarStatusModel.ReturnIndex("Полученные");
                command.ExecuteNonQuery();
            }
        }

        private void CheckFields()
        {
            Checher = true;

            textBoxPatientLogin.BorderBrush = Brushes.Gray;
            textBoxPatientLogin.ToolTip = null;

            if (!CheckValidFamiliar())
            {
                textBoxPatientLogin.BorderBrush = Brushes.Red;
                Checher = false;
            }
            if (textBoxPatientLogin.Text.Length < 4)
            {
                textBoxPatientLogin.ToolTip = "Логин должен содержать не менее 4 символов";
                textBoxPatientLogin.BorderBrush = Brushes.Red;
                Checher = false;
            }
            if (textBoxPatientLogin.Text.Contains(" "))
            {
                textBoxPatientLogin.ToolTip = "Логин не может содержать пробел(ы)";
                textBoxPatientLogin.BorderBrush = Brushes.Red;
                Checher = false;
            }
        }

        private bool CheckValidFamiliar()
        {
            bool valid = true;
            FamiliarId = 0;

            if (textBoxPatientLogin.Text == MainPatientModel.Username)
            {
                valid = false;
                textBoxPatientLogin.ToolTip = "Вы ввели свой логин";
                return valid;
            }

            using (var connection = BaseRepository.GetConnection())
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "SELECT * FROM patients WHERE username=@username";
                command.Parameters.Add("@username", SqlDbType.NVarChar).Value = this.textBoxPatientLogin.Text;

                if (command.ExecuteScalar() == null)
                {
                    valid = false;
                    textBoxPatientLogin.ToolTip = "Данный пользователь не существует";
                    return valid;
                }
                else
                {
                    var dataReader = command.ExecuteReader();
                    while (dataReader.Read())
                    {
                        FamiliarId = Convert.ToInt32(dataReader["id"]);
                    }
                }
            }

            using (var connection = BaseRepository.GetConnection())
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "SELECT * FROM familiars " +
                    "WHERE " +
                    "(from_patient_id=@idFamiliarPatient AND to_patient_id=@idMainPatient) " +
                    "OR " +
                    "(to_patient_id=@idFamiliarPatient AND from_patient_id=@idMainPatient)";
                command.Parameters.Add("@idFamiliarPatient", SqlDbType.Int).Value = FamiliarId;
                command.Parameters.Add("@idMainPatient", SqlDbType.Int).Value = MainPatientModel.Id;

                if (command.ExecuteScalar() == null)
                {
                    valid = true;
                }
                else
                {
                    textBoxPatientLogin.ToolTip = "Пользователь уже находится в Вашем списке";
                    valid = false;
                    return valid;
                }
            }

            return valid;
        }

        private void textBoxWeightValue_TextChanged(object sender, TextChangedEventArgs e)
        {
            CheckFields();
        }
    }
}
