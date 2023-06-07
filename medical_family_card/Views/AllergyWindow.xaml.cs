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
    /// Логика взаимодействия для AllergyWindow.xaml
    /// </summary>
    public partial class AllergyWindow : Window
    {
        private int Id { get; set; }
        private ItemView ItemViewParent { get; set; }
        private StackPanel StackPanelParent { get; set; }
        private int Regime { get; set; }
        private bool Checher { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="regime">0 - создание новой, 1 - просомтр, 2 - редактирование</param>
        /// <param name="itemViewParent"></param>
        /// <param name="id"></param>
        /// 


        public AllergyWindow(int regime, StackPanel stackPanelParent)
        {
            InitializeComponent();
            Regime = regime;
            StackPanelParent = stackPanelParent;

            ShowInfo();
        }
        public AllergyWindow(int regime, int id, ItemView itemViewParent)
        {
            InitializeComponent();
            Regime = regime;
            Id = id;
            ItemViewParent = itemViewParent;

            ShowInfo();
        }


        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }

        private void menuItemClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void menuItemCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void menuItemSave_Click(object sender, RoutedEventArgs e)
        {
            CheckFields();
            if (Checher)
            {
                if (Regime == 0)
                {
                    CreateNewWeightInfo();
                    this.Close();
                }
                if (Regime == 2)
                {
                    UpDateWeightInfo();
                    this.Close();
                }
            }
        }

        private void ShowInfo()
        {
            if (Regime == 0)
            {
                EnableEditFields();
            }
            if (Regime == 1 || Regime == 2)
            {
                using (var connection = BaseRepository.GetConnection())
                using (var command = new SqlCommand())
                {
                    connection.Open();
                    command.Connection = connection;
                    command.CommandText = "SELECT * FROM allergies WHERE id=@id";
                    command.Parameters.Add("@id", SqlDbType.Int).Value = Id;
                    var dataReader = command.ExecuteReader();

                    while (dataReader.Read())
                    {
                        textBoxAllergyName.Text = Convert.ToString(dataReader["allergy_name"]);
                        textBoxComment.Text = Convert.ToString(dataReader["comment"]);
                    }
                }

                if (Regime == 1)
                {
                    DisableEditFields();
                }

                if (Regime == 2)
                {
                    EnableEditFields();
                }
            }
        }

        private void CreateNewWeightInfo()
        {
            //Создание новой записи
            using (var connection = BaseRepository.GetConnection())
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText =
                                    "INSERT INTO allergies " +
                                    "(patient_id, allergy_name,comment) " +
                                    "VALUES " +
                                    "(@patient_id,@allergy_name,@comment)";
                command.Parameters.Add("@patient_id", SqlDbType.Int).Value = MainPatientModel.Id;
                command.Parameters.Add("@allergy_name", SqlDbType.NVarChar).Value = textBoxAllergyName.Text;
                command.Parameters.Add("@comment", SqlDbType.NVarChar).Value = textBoxComment.Text;
                command.ExecuteNonQuery();
            }
            //Ищем последннюю запись для присваения id
            using (var connection = BaseRepository.GetConnection())
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText =
                    "SELECT * FROM allergies WHERE patient_id=@patient_id ORDER BY id DESC";
                command.Parameters.Add("@patient_id", SqlDbType.Int).Value = MainPatientModel.Id;

                var dataReader = command.ExecuteReader();
                dataReader.Read();
                AllergyModel.Id = Convert.ToInt32(dataReader["id"]);
            }

            ItemView itemView = new ItemView
                (
                    StackPanelParent,
                    AllergyModel.Id,
                    MainPatientModel.Id,
                    "allergies",
                    "id",
                    new string[] { "Название:", "Описание:" },
                    new string[] 
                    { 
                        Convert.ToString(textBoxAllergyName.Text),
                        Convert.ToString(textBoxComment.Text),
                    }
                );

            StackPanelParent.Children.Add(itemView);
        }

        private void UpDateWeightInfo()
        {
            //Обновления текущей записи
            using (var connection = BaseRepository.GetConnection())
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText =
                    "UPDATE allergies SET " +
                    "allergy_name=@allergy_name, comment=@comment " +
                    "WHERE " +
                    "id=@id";

                command.Parameters.Add("@allergy_name", SqlDbType.NVarChar).Value = textBoxAllergyName.Text;
                command.Parameters.Add("@comment", SqlDbType.NVarChar).Value = textBoxComment.Text;
                command.Parameters.Add("@id", SqlDbType.Int).Value = this.Id;
                command.ExecuteNonQuery();
            }

            ItemViewParent.UpDateInfo(
                new string[] { "Название:", "Описание:" },
                new string[] 
                {
                    Convert.ToString(textBoxAllergyName.Text),
                    Convert.ToString(textBoxComment.Text),
                }
                );
        }

        private void EnableEditFields()
        {
            textBoxAllergyName.IsEnabled = true;
            textBoxComment.IsEnabled = true;
            EnableEditMode();
        }

        private void DisableEditFields()
        {
            textBoxAllergyName.IsEnabled = false;
            textBoxComment.IsEnabled = false;
            DisableEditMode();
        }

        private void EnableEditMode()
        {
            menuItemSave.Visibility = Visibility.Visible;
            menuItemCancel.Visibility = Visibility.Visible;
            menuItemClose.Visibility = Visibility.Hidden;
        }

        private void DisableEditMode()
        {
            menuItemSave.Visibility = Visibility.Hidden;
            menuItemCancel.Visibility = Visibility.Hidden;
            menuItemClose.Visibility = Visibility.Visible;
        }

        private void CheckFields()
        {
            Checher = true;

            textBoxAllergyName.BorderBrush = Brushes.Gray;

            if (textBoxAllergyName.Text.Length < 1)
            {
                textBoxAllergyName.ToolTip = "Введите название";
                textBoxAllergyName.BorderBrush = Brushes.Red;
                Checher = false;
            }
        }

        private void textBoxAllergyName_TextChanged(object sender, TextChangedEventArgs e)
        {
            CheckFields();
        }
    }
}
