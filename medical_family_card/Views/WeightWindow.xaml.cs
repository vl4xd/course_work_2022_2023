using medical_family_card.Repositories;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using medical_family_card.Models;

namespace medical_family_card.Views
{
    /// <summary>
    /// Логика взаимодействия для WeightWindow.xaml
    /// </summary>
    public partial class WeightWindow : Window
    {
        private int Id { get; set; }
        private int Patient_Id { get; set; }
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
        /// <param name="patient_id"></param>
        public WeightWindow(int regime, ItemView itemViewParent, int id, int patient_id)
        {
            InitializeComponent();

            Id = id;
            Patient_Id = patient_id;
            ItemViewParent = itemViewParent;
            Regime = regime;

            ShowInfo();
            CheckFields();
        }

        public WeightWindow(int regime, ItemView itemViewParent, StackPanel stackPanelParent, int id, int patient_id)
        {
            InitializeComponent();

            Id = id;
            Patient_Id = patient_id;
            ItemViewParent = itemViewParent;
            StackPanelParent = stackPanelParent;
            Regime = regime;

            ShowInfo();
            CheckFields();
        }

        public WeightWindow(int regime, int patient_id, StackPanel stackPanelParent)
        {
            InitializeComponent();

            Patient_Id = patient_id;
            Regime = regime;
            StackPanelParent = stackPanelParent;

            ShowInfo(); 
            CheckFields();
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }

        private void ShowInfo()
        {
            if (Regime == 0)
            {
                EnableEditFields();
                datePickerWeightDate.SelectedDate = DateTime.Now.Date;
            }
            if (Regime == 1 || Regime == 2)
            {
                using (var connection = BaseRepository.GetConnection())
                using (var command = new SqlCommand())
                {
                    connection.Open();
                    command.Connection = connection;
                    command.CommandText = "SELECT * FROM weights WHERE id=@id";
                    command.Parameters.Add("@id", SqlDbType.Int).Value = Id;
                    var dataReader = command.ExecuteReader();

                    while (dataReader.Read())
                    {
                        textBoxWeightValue.Text = Convert.ToString(dataReader["weight_value"]);
                        datePickerWeightDate.SelectedDate = Convert.ToDateTime(dataReader["weight_date"]);
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
            textBoxWeightValue.IsEnabled = true;
            datePickerWeightDate.IsEnabled = true;
            EnableEditMode();
        }

        private void DisableEditFields()
        {
            textBoxWeightValue.IsEnabled = false;
            datePickerWeightDate.IsEnabled = false;
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

            textBoxWeightValue.BorderBrush = Brushes.Gray;
            datePickerWeightDate.BorderBrush = Brushes.Gray;

            double temp;
            if (!double.TryParse(textBoxWeightValue.Text, out temp))
            {
                textBoxWeightValue.ToolTip = "Введите десятичное число";
                textBoxWeightValue.BorderBrush = Brushes.Red;
                Checher = false;
            }
            if (datePickerWeightDate.SelectedDate == null)
            {
                datePickerWeightDate.ToolTip = "Вы не ввели Дату имерения";
                datePickerWeightDate.BorderBrush = Brushes.Red;
                Checher = false;
            }

            //Присваеваем значение статическому классу для проверки
            WeightModel.Weight_Date = Convert.ToString(datePickerWeightDate.SelectedDate);
            if (datePickerWeightDate.SelectedDate != null)
            {
                if (Regime == 0 && !WeightModel.FindWeightWithDate(this.Id, MainPatientModel.Id))
                {
                    datePickerWeightDate.ToolTip = "Измерение с этой датой уже существует";
                    datePickerWeightDate.BorderBrush = Brushes.Red;
                    Checher = false;
                }
                if (Regime == 2 && !WeightModel.FindWeightWithDate(this.Id, MainPatientModel.Id))
                {
                    datePickerWeightDate.ToolTip = "Измерение с этой датой уже существует";
                    datePickerWeightDate.BorderBrush = Brushes.Red;
                    Checher = false;
                }
            }
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
            WeightModel.Weight_Date = Convert.ToString(datePickerWeightDate.SelectedDate);
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

        private void CreateNewWeightInfo()
        {
            //Создание новой записи
            using (var connection = BaseRepository.GetConnection())
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = 
                    "INSERT INTO weights " +
                    "(patient_id, weight_value, weight_date) " +
                    "VALUES " +
                    "(@patient_id,@weight_value,@weight_date)";
                command.Parameters.Add("@patient_id", SqlDbType.Int).Value = MainPatientModel.Id;
                command.Parameters.Add("@weight_value", SqlDbType.Float).Value = textBoxWeightValue.Text;
                command.Parameters.Add("@weight_date", SqlDbType.Date).Value = datePickerWeightDate.SelectedDate;
                command.ExecuteNonQuery();
            }
            //Ищем последннюю запись для присваения id
            using (var connection = BaseRepository.GetConnection())
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText =
                    "SELECT * FROM weights WHERE patient_id=@patient_id ORDER BY id DESC";
                command.Parameters.Add("@patient_id", SqlDbType.Int).Value = MainPatientModel.Id;

                var dataReader = command.ExecuteReader();
                dataReader.Read();
                WeightModel.Id = Convert.ToInt32(dataReader["id"]);
            }


            WeightModel.Patient_Id = MainPatientModel.Id;
            WeightModel.Weight_Value = Convert.ToDouble(textBoxWeightValue.Text);
            WeightModel.Weight_Date = Convert.ToString(datePickerWeightDate.SelectedDate);

            ItemView itemView = new ItemView
                (
                    StackPanelParent,
                    WeightModel.Id,
                    WeightModel.Patient_Id,
                    "weights",
                    "id",
                    new string[] { "Вес:", "Дата измерения:" },
                    new string[] { Convert.ToString(WeightModel.Weight_Value), Convert.ToString(WeightModel.Weight_Date) }
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
                    "UPDATE weights SET " +
                    "weight_value=@weight_value,weight_date=@weight_date " +
                    "WHERE " +
                    "id=@id";

                WeightModel.Weight_Value = Convert.ToDouble(textBoxWeightValue.Text);
                WeightModel.Weight_Date = Convert.ToString(datePickerWeightDate.SelectedDate);

                command.Parameters.Add("@weight_value", SqlDbType.Float).Value = WeightModel.Weight_Value;
                command.Parameters.Add("@weight_date", SqlDbType.Date).Value = WeightModel.Weight_Date;
                command.Parameters.Add("@id", SqlDbType.Int).Value = this.Id;
                command.ExecuteNonQuery();
            }

            ItemViewParent.UpDateInfo(
                new string[] { "Вес:", "Дата измерения:" },
                new string[] { Convert.ToString(WeightModel.Weight_Value), WeightModel.Weight_Date });
        }

        private void datePickerWeightDate_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            CheckFields();
        }

        private void textBoxWeightValue_TextChanged(object sender, TextChangedEventArgs e)
        {
            CheckFields();
        }
    }
}
