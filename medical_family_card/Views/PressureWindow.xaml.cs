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
    /// Логика взаимодействия для PressureWindow.xaml
    /// </summary>
    public partial class PressureWindow : Window
    {
        private int Id { get; set; }
        private ItemView ItemViewParent { get; set; }
        private StackPanel StackPanelParent { get; set; }
        private int Regime { get; set; }
        private bool Checher { get; set; }

        public PressureWindow(int regime, StackPanel stackPanelParent)
        {
            InitializeComponent();
            Regime = regime;
            StackPanelParent = stackPanelParent;

            ShowInfo();
        }

        public PressureWindow(int regime, int id, ItemView itemViewParent)
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
                    CreateNewPressureInfo();
                    this.Close();
                }
                if (Regime == 2)
                {
                    UpDatePressureInfo();
                    this.Close();
                }
            }
        }

        private void ShowInfo()
        {
            if (Regime == 0)
            {
                EnableEditFields();
                timePickerPressureTime.SelectedTime = Convert.ToDateTime(Convert.ToString(DateTime.Now.TimeOfDay));
                datePickerPressureDate.SelectedDate = DateTime.Now.Date;
            }
            if (Regime == 1 || Regime == 2)
            {
                using (var connection = BaseRepository.GetConnection())
                using (var command = new SqlCommand())
                {
                    connection.Open();
                    command.Connection = connection;
                    command.CommandText = "SELECT * FROM pressures WHERE id=@id";
                    command.Parameters.Add("@id", SqlDbType.Int).Value = Id;
                    var dataReader = command.ExecuteReader();

                    while (dataReader.Read())
                    {
                        textBoxTopPressureValue.Text = Convert.ToString(dataReader["systolic"]);
                        textBoxLowerPressureValue.Text = Convert.ToString(dataReader["diastolic"]);
                        timePickerPressureTime.SelectedTime = Convert.ToDateTime(Convert.ToString(dataReader["pressure_time"]));
                        datePickerPressureDate.SelectedDate = Convert.ToDateTime(dataReader["pressure_date"]);
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

        private void CreateNewPressureInfo()
        {
            //Создание новой записи
            using (var connection = BaseRepository.GetConnection())
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText =
                                    "INSERT INTO pressures " +
                                    "(patient_id, systolic,diastolic,pressure_time,pressure_date,comment) " +
                                    "VALUES " +
                                    "(@patient_id,@systolic,@diastolic,@pressure_time,@pressure_date,@comment)";
                command.Parameters.Add("@patient_id", SqlDbType.Int).Value = MainPatientModel.Id;
                command.Parameters.Add("@systolic", SqlDbType.Int).Value = textBoxTopPressureValue.Text;
                command.Parameters.Add("@diastolic", SqlDbType.Int).Value = textBoxLowerPressureValue.Text;
                command.Parameters.Add("@pressure_time", SqlDbType.Time).Value = ((DateTime)timePickerPressureTime.SelectedTime).TimeOfDay;
                command.Parameters.Add("@pressure_date", SqlDbType.Date).Value = datePickerPressureDate.SelectedDate;
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
                    "SELECT * FROM pressures WHERE patient_id=@patient_id ORDER BY id DESC";
                command.Parameters.Add("@patient_id", SqlDbType.Int).Value = MainPatientModel.Id;

                var dataReader = command.ExecuteReader();
                dataReader.Read();
                PressureModel.Id = Convert.ToInt32(dataReader["id"]);
            }

            ItemView itemView = new ItemView
                (
                    StackPanelParent,
                    PressureModel.Id,
                    MainPatientModel.Id,
                    "pressures",
                    "id",
                    new string[] { "Верхнее давление:","Нижнее давление:","Время измерения:","Дата измерения:","Описание:" },
                    new string[]
                    {
                        Convert.ToString(textBoxTopPressureValue.Text),
                        Convert.ToString(textBoxLowerPressureValue.Text),
                        ((DateTime)timePickerPressureTime.SelectedTime).TimeOfDay.ToString(),
                        ((DateTime)datePickerPressureDate.SelectedDate).ToString("d"),
                        Convert.ToString(textBoxComment.Text),
                    }
                );

            StackPanelParent.Children.Add(itemView);
        }

        private void UpDatePressureInfo()
        {
            //Обновления текущей записи
            using (var connection = BaseRepository.GetConnection())
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText =
                    "UPDATE pressures SET " +
                    "systolic=@systolic,diastolic=@diastolic,pressure_time=@pressure_time,pressure_date=@pressure_date,comment=@comment " +
                    "WHERE " +
                    "id=@id";

                command.Parameters.Add("@systolic", SqlDbType.Int).Value = textBoxTopPressureValue.Text;
                command.Parameters.Add("@diastolic", SqlDbType.Int).Value = textBoxLowerPressureValue.Text;
                command.Parameters.Add("@pressure_time", SqlDbType.Time).Value = ((DateTime)timePickerPressureTime.SelectedTime).TimeOfDay;
                command.Parameters.Add("@pressure_date", SqlDbType.Date).Value = datePickerPressureDate.SelectedDate;
                command.Parameters.Add("@comment", SqlDbType.NVarChar).Value = textBoxComment.Text;
                command.Parameters.Add("@id", SqlDbType.Int).Value = this.Id;
                command.ExecuteNonQuery();
            }

            ItemViewParent.UpDateInfo(
                new string[] { "Верхнее давление:", "Нижнее давление:", "Время измерения:", "Дата измерения:", "Описание:" },
                new string[]
                {
                    Convert.ToString(textBoxTopPressureValue.Text),
                    Convert.ToString(textBoxLowerPressureValue.Text),
                    ((DateTime)timePickerPressureTime.SelectedTime).TimeOfDay.ToString(),
                    ((DateTime)datePickerPressureDate.SelectedDate).ToString("d"),
                    Convert.ToString(textBoxComment.Text),
                }
                );
        }

        private void EnableEditFields()
        {
            textBoxTopPressureValue.IsEnabled = true;
            textBoxLowerPressureValue.IsEnabled = true;
            timePickerPressureTime.IsEnabled = true;
            datePickerPressureDate.IsEnabled = true;
            textBoxComment.IsEnabled = true;
            EnableEditMode();
        }

        private void DisableEditFields()
        {
            textBoxTopPressureValue.IsEnabled = false;
            textBoxLowerPressureValue.IsEnabled = false;
            timePickerPressureTime.IsEnabled = false;
            datePickerPressureDate.IsEnabled = false;
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

            textBoxTopPressureValue.BorderBrush = Brushes.Gray;
            textBoxLowerPressureValue.BorderBrush = Brushes.Gray;
            timePickerPressureTime.BorderBrush = Brushes.Gray;
            datePickerPressureDate.BorderBrush = Brushes.Gray;
            textBoxComment.BorderBrush = Brushes.Gray;

            int temp;
            if (!int.TryParse(textBoxTopPressureValue.Text, out temp))
            {
                textBoxTopPressureValue.ToolTip = "Введите целое число";
                textBoxTopPressureValue.BorderBrush = Brushes.Red;
                Checher = false;
            }
            if (!int.TryParse(textBoxLowerPressureValue.Text, out temp))
            {
                textBoxLowerPressureValue.ToolTip = "Введите целое число";
                textBoxLowerPressureValue.BorderBrush = Brushes.Red;
                Checher = false;
            }
            if (timePickerPressureTime.SelectedTime == null)
            {
                timePickerPressureTime.ToolTip = "Вы не ввели Время имерения";
                timePickerPressureTime.BorderBrush = Brushes.Red;
                Checher = false;
            }
            if (datePickerPressureDate.SelectedDate == null)
            {
                datePickerPressureDate.ToolTip = "Вы не ввели Дату имерения";
                datePickerPressureDate.BorderBrush = Brushes.Red;
                Checher = false;
            }

            if (timePickerPressureTime.SelectedTime != null && datePickerPressureDate.SelectedDate != null)
            {
                if (Regime == 0 && !PressureModel.FindPressureWithTimeDate(this.Id, MainPatientModel.Id, (DateTime)timePickerPressureTime.SelectedTime, (DateTime)datePickerPressureDate.SelectedDate))
                {
                    timePickerPressureTime.ToolTip = "Измерение с этим Временем и датой уже существует";
                    timePickerPressureTime.BorderBrush = Brushes.Red;
                    datePickerPressureDate.ToolTip = "Измерение с этим временем и Датой уже существует";
                    datePickerPressureDate.BorderBrush = Brushes.Red;
                    Checher = false;
                }
                if (Regime == 2 && !PressureModel.FindPressureWithTimeDate(this.Id, MainPatientModel.Id, (DateTime)timePickerPressureTime.SelectedTime, (DateTime)datePickerPressureDate.SelectedDate))
                {
                    timePickerPressureTime.ToolTip = "Измерение с этим Временем и датой уже существует";
                    timePickerPressureTime.BorderBrush = Brushes.Red;
                    datePickerPressureDate.ToolTip = "Измерение с этим временем и Датой уже существует";
                    datePickerPressureDate.BorderBrush = Brushes.Red;
                    Checher = false;
                }
            }
        }

        private void textBoxTopPressureValue_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (timePickerPressureTime != null && datePickerPressureDate != null && textBoxComment != null)
                CheckFields();
        }

        private void textBoxLowerPressureValue_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (timePickerPressureTime != null && datePickerPressureDate != null && textBoxComment != null)
                CheckFields();
        }

        private void timePickerPressureTime_SelectedTimeChanged(object sender, RoutedPropertyChangedEventArgs<DateTime?> e)
        {
            if (timePickerPressureTime != null && datePickerPressureDate != null && textBoxComment != null)
                CheckFields();
        }

        private void datePickerPressureDate_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (timePickerPressureTime != null && datePickerPressureDate != null && textBoxComment != null)
                CheckFields();
        }
    }
}
