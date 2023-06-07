using medical_family_card.Models;
using medical_family_card.Repositories;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
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
    /// Логика взаимодействия для HeightWindow.xaml
    /// </summary>
    public partial class HeightWindow : Window
    {
        private int Id { get; set; }
        private ItemView ItemViewParent { get; set; }
        private StackPanel StackPanelParent { get; set; }
        private int Regime { get; set; }
        private bool Checher { get; set; }

        public HeightWindow(int regime, StackPanel stackPanelParent)
        {
            InitializeComponent();
            Regime = regime;
            StackPanelParent = stackPanelParent;

            ShowInfo();
            CheckFields();
        }

        public HeightWindow(int regime, int id, ItemView itemViewParent)
        {
            InitializeComponent();
            Regime = regime;
            Id = id;
            ItemViewParent = itemViewParent;

            ShowInfo();
            CheckFields();
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }

        private void menuItemCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void menuItemClose_Click(object sender, RoutedEventArgs e)
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
                    CreateNewHeightInfo();
                    this.Close();
                }
                if (Regime == 2)
                {
                    UpDateHeightInfo();
                    this.Close();
                }
            }
        }

        private void ShowInfo()
        {
            if (Regime == 0)
            {
                EnableEditFields();
                datePickerHeightDate.SelectedDate = DateTime.Now.Date;
            }
            if (Regime == 1 || Regime == 2)
            {
                using (var connection = BaseRepository.GetConnection())
                using (var command = new SqlCommand())
                {
                    connection.Open();
                    command.Connection = connection;
                    command.CommandText = "SELECT * FROM heights WHERE id=@id";
                    command.Parameters.Add("@id", SqlDbType.Int).Value = Id;
                    var dataReader = command.ExecuteReader();

                    while (dataReader.Read())
                    {
                        textBoxHeightValue.Text = Convert.ToString(dataReader["height_value"]);
                        datePickerHeightDate.SelectedDate = Convert.ToDateTime(dataReader["height_date"]);
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

        private void EnableEditFields()
        {
            textBoxHeightValue.IsEnabled = true;
            datePickerHeightDate.IsEnabled = true;
            EnableEditMode();
        }

        private void DisableEditFields()
        {
            textBoxHeightValue.IsEnabled = false;
            datePickerHeightDate.IsEnabled = false;
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

            textBoxHeightValue.BorderBrush = Brushes.Gray;
            datePickerHeightDate.BorderBrush = Brushes.Gray;

            double temp;
            if (!double.TryParse(textBoxHeightValue.Text, out temp))
            {
                textBoxHeightValue.ToolTip = "Введите десятичное число";
                textBoxHeightValue.BorderBrush = Brushes.Red;
                Checher = false;
            }
            if (datePickerHeightDate.SelectedDate == null)
            {
                datePickerHeightDate.ToolTip = "Вы не ввели Дату имерения";
                datePickerHeightDate.BorderBrush = Brushes.Red;
                Checher = false;
            }

            //Присваеваем значение статическому классу для проверки
            HeightModel.Height_Date = (DateTime)datePickerHeightDate.SelectedDate;
            if (datePickerHeightDate.SelectedDate != null)
            {
                if (Regime == 0 && !HeightModel.FindHeightWithDate(this.Id, MainPatientModel.Id))
                {
                    datePickerHeightDate.ToolTip = "Измерение с этой датой уже существует";
                    datePickerHeightDate.BorderBrush = Brushes.Red;
                    Checher = false;
                }
                if (Regime == 2 && !HeightModel.FindHeightWithDate(this.Id, MainPatientModel.Id))
                {
                    datePickerHeightDate.ToolTip = "Измерение с этой датой уже существует";
                    datePickerHeightDate.BorderBrush = Brushes.Red;
                    Checher = false;
                }
            }
        }

        private void CreateNewHeightInfo()
        {
            //Создание новой записи
            using (var connection = BaseRepository.GetConnection())
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText =
                    "INSERT INTO heights " +
                    "(patient_id, height_value, height_date) " +
                    "VALUES " +
                    "(@patient_id,@height_value,@height_date)";
                command.Parameters.Add("@patient_id", SqlDbType.Int).Value = MainPatientModel.Id;
                command.Parameters.Add("@height_value", SqlDbType.Float).Value = textBoxHeightValue.Text;
                command.Parameters.Add("@height_date", SqlDbType.Date).Value = datePickerHeightDate.SelectedDate;
                command.ExecuteNonQuery();
            }
            //Ищем последннюю запись для присваения id
            using (var connection = BaseRepository.GetConnection())
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText =
                    "SELECT * FROM heights WHERE patient_id=@patient_id ORDER BY id DESC";
                command.Parameters.Add("@patient_id", SqlDbType.Int).Value = MainPatientModel.Id;

                var dataReader = command.ExecuteReader();
                dataReader.Read();
                HeightModel.Id = Convert.ToInt32(dataReader["id"]);
            }


            HeightModel.Patient_Id = MainPatientModel.Id;
            HeightModel.Height_Value = Convert.ToDouble(textBoxHeightValue.Text);
            HeightModel.Height_Date = (DateTime)datePickerHeightDate.SelectedDate;

            ItemView itemView = new ItemView
                (
                    StackPanelParent,
                    HeightModel.Id,
                    HeightModel.Patient_Id,
                    "heights",
                    "id",
                    new string[] { "Рост:", "Дата измерения:" },
                    new string[] { Convert.ToString(HeightModel.Height_Value), HeightModel.Height_Date.ToString("d") }
                );

            StackPanelParent.Children.Add(itemView);
        }

        private void UpDateHeightInfo()
        {
            //Обновления текущей записи
            using (var connection = BaseRepository.GetConnection())
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText =
                    "UPDATE heights SET " +
                    "height_value=@height_value,height_date=@height_date " +
                    "WHERE " +
                    "id=@id";

                WeightModel.Weight_Value = Convert.ToDouble(textBoxHeightValue.Text);
                WeightModel.Weight_Date = Convert.ToString(datePickerHeightDate.SelectedDate);

                command.Parameters.Add("@height_value", SqlDbType.Float).Value = HeightModel.Height_Value;
                command.Parameters.Add("@height_date", SqlDbType.Date).Value = HeightModel.Height_Date;
                command.Parameters.Add("@id", SqlDbType.Int).Value = this.Id;
                command.ExecuteNonQuery();
            }

            ItemViewParent.UpDateInfo(
                new string[] { "Рост:", "Дата измерения:" },
                new string[] { Convert.ToString(HeightModel.Height_Value), HeightModel.Height_Date.ToString("d") });
        }

        private void datePickerHeightDate_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            CheckFields();
        }

        private void textBoxHeightValue_TextChanged(object sender, TextChangedEventArgs e)
        {
            CheckFields();
        }
    }
}
