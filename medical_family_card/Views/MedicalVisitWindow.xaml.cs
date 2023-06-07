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
    /// Логика взаимодействия для MedicalVisitWindow.xaml
    /// </summary>
    public partial class MedicalVisitWindow : Window
    {
        private int Id { get; set; }
        private ItemView ItemViewParent { get; set; }
        private StackPanel StackPanelParent { get; set; }
        private int Regime { get; set; }
        private bool Checher { get; set; }

        public MedicalVisitWindow(int regime, StackPanel stackPanelParent)
        {
            InitializeComponent();
            Regime = regime;
            StackPanelParent = stackPanelParent;

            ShowInfo();
        }
        public MedicalVisitWindow(int regime, int id, StackPanel stackPanelParent, ItemView itemViewParent)
        {
            InitializeComponent();
            Id = id;
            ItemViewParent = itemViewParent;
            StackPanelParent = stackPanelParent;
            Regime = regime;

            ShowInfo();
        }
        public MedicalVisitWindow(int regime, int id, ItemView itemViewParent)
        {
            InitializeComponent();
            Id = id;
            ItemViewParent = itemViewParent;
            Regime = regime;

            ShowInfo();
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
                textBoxVisitCost.Text = "0";
                datePickerStDate.SelectedDate = DateTime.Now.Date;
                datePickerEndDate.SelectedDate = DateTime.Now.Date;
            }
            if (Regime == 1 || Regime == 2)
            {
                using (var connection = BaseRepository.GetConnection())
                using (var command = new SqlCommand())
                {
                    connection.Open();
                    command.Connection = connection;
                    command.CommandText = "SELECT * FROM medical_visits WHERE id=@id";
                    command.Parameters.Add("@id", SqlDbType.Int).Value = this.Id;
                    var dataReader = command.ExecuteReader();

                    while (dataReader.Read())
                    {
                        textBoxVisitName.Text = Convert.ToString(dataReader["medical_visit_name"]);
                        datePickerStDate.SelectedDate = Convert.ToDateTime(dataReader["st_date"]);
                        datePickerEndDate.SelectedDate = Convert.ToDateTime(dataReader["end_date"]);
                        textBoxVisitCost.Text = Convert.ToString(dataReader["cost"]);
                        textBoxComment.Text = Convert.ToString(dataReader["comment"]);
                    }
                }

                using (var connection = BaseRepository.GetConnection())
                using (var command = new SqlCommand())
                {
                    connection.Open();
                    command.Connection = connection;
                    command.CommandText =
                        "SELECT * FROM photo_about_medical_visits " +
                        "WHERE " +
                        "medical_visit_id=@medical_visit_id";
                    command.Parameters.Add("@medical_visit_id", SqlDbType.Int).Value = this.Id;
                    var dataReader = command.ExecuteReader();
                    if (dataReader.HasRows)
                    {
                        while (dataReader.Read())
                        {
                            PhotoView photoView = new PhotoView(stackPanelPhoto, EnumView.MedicalVisit) { Margin = new Thickness(0, 0, 5, 0) };
                            if (Regime == 1)
                                photoView.ExtraButtonVisible(false);
                            if (Regime == 2)
                                photoView.ExtraButtonVisible(true);
                            PhotoModel.ConvertBinaryDataToPhoto((byte[])dataReader["photo"], photoView);
                            stackPanelPhoto.Children.Add(photoView);
                        }
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

        private void CheckFields()
        {
            Checher = true;

            textBoxVisitName.BorderBrush = Brushes.Gray;
            datePickerStDate.BorderBrush = Brushes.Gray;
            datePickerEndDate.BorderBrush = Brushes.Gray;
            textBoxVisitCost.BorderBrush = Brushes.Gray;

            if (datePickerStDate.SelectedDate == null)
            {
                datePickerStDate.ToolTip = "Вы не ввели Дату имерения";
                datePickerStDate.BorderBrush = Brushes.Red;
                Checher = false;
            }
            if (datePickerEndDate.SelectedDate == null)
            {
                datePickerEndDate.ToolTip = "Вы не ввели Дату имерения";
                datePickerEndDate.BorderBrush = Brushes.Red;
                Checher = false;
            }
            if (textBoxVisitName.Text.Length < 1)
            {
                textBoxVisitName.ToolTip = "Вы не ввели Название";
                textBoxVisitName.BorderBrush = Brushes.Red;
                Checher = false;
            }

            decimal temp;
            if (!decimal.TryParse(textBoxVisitCost.Text, out temp))
            {
                textBoxVisitName.ToolTip = "Введите стоимость в рублях";
                textBoxVisitName.BorderBrush = Brushes.Red;
                Checher = false;
            }
            else
            {
                if(temp < 0)
                {
                    textBoxVisitCost.ToolTip = "Значение не может быть отрицательным";
                    textBoxVisitCost.BorderBrush = Brushes.Red;
                    Checher = false;
                }
            }
        }

        private void textBoxVisitCost_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (datePickerStDate != null && datePickerEndDate != null && textBoxVisitCost != null)
                CheckFields();
        }

        private void textBoxVisitName_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (datePickerStDate != null && datePickerEndDate != null && textBoxVisitCost != null)
                CheckFields();
        }

        private void datePickerEndDate_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (datePickerStDate != null && textBoxVisitCost != null)
                CheckFields();
        }

        private void datePickerStDate_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (datePickerEndDate != null && textBoxVisitCost != null)
                CheckFields();
        }

        private void EnableEditFields()
        {
            textBoxVisitName.IsEnabled = true;
            datePickerStDate.IsEnabled = true;
            datePickerEndDate.IsEnabled = true;
            textBoxVisitCost.IsEnabled = true;
            textBoxComment.IsEnabled = true;
            EnableEditMode();
        }

        private void DisableEditFields()
        {
            textBoxVisitName.IsEnabled = false;
            datePickerStDate.IsEnabled = false;
            datePickerEndDate.IsEnabled = false;
            textBoxVisitCost.IsEnabled = true;
            textBoxComment.IsEnabled = false;
            DisableEditMode();
        }

        private void EnableEditMode()
        {
            menuItemSave.Visibility = Visibility.Visible;
            menuItemCancel.Visibility = Visibility.Visible;
            menuItemClose.Visibility = Visibility.Hidden;
            addPhoto.Visibility = Visibility.Visible;
        }

        private void DisableEditMode()
        {
            menuItemSave.Visibility = Visibility.Hidden;
            menuItemCancel.Visibility = Visibility.Hidden;
            menuItemClose.Visibility = Visibility.Visible;
            addPhoto.Visibility = Visibility.Hidden;
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
            if (Checher)
            {
                if (Regime == 0)
                {
                    CreateNewMedicationInfo();
                    this.Close();
                }
                if (Regime == 2)
                {
                    UpDateMedicationInfo();
                    this.Close();
                }
            }
        }

        private void CreateNewMedicationInfo()
        {
            //Создание новой записи в medications
            using (var connection = BaseRepository.GetConnection())
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText =
                    "INSERT INTO medical_visits " +
                    "(patient_id, medical_visit_name, st_date, end_date, cost, comment) " +
                    "VALUES " +
                    "(@patient_id, @medical_visit_name, @st_date, @end_date, @cost, @comment)";
                command.Parameters.Add("@patient_id", SqlDbType.Int).Value = MainPatientModel.Id;
                command.Parameters.Add("@medical_visit_name", SqlDbType.NVarChar).Value = textBoxVisitName.Text;
                command.Parameters.Add("@st_date", SqlDbType.Date).Value = datePickerStDate.SelectedDate;
                command.Parameters.Add("@end_date", SqlDbType.Date).Value = datePickerEndDate.SelectedDate;
                command.Parameters.Add("@cost", SqlDbType.Money).Value = Convert.ToDecimal(textBoxVisitCost.Text);
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
                    "SELECT * FROM medical_visits WHERE patient_id=@patient_id ORDER BY id DESC";
                command.Parameters.Add("@patient_id", SqlDbType.Int).Value = MainPatientModel.Id;

                var dataReader = command.ExecuteReader();
                dataReader.Read();
                MedicalVisitModel.Id = Convert.ToInt32(dataReader["id"]);
            }

            //Добавляем фотографии
            foreach (PhotoView photoView in stackPanelPhoto.Children)
            {
                using (var connection = BaseRepository.GetConnection())
                using (var command = new SqlCommand())
                {
                    connection.Open();
                    command.Connection = connection;
                    command.CommandText =
                        "INSERT INTO photo_about_medical_visits " +
                        "(medical_visit_id, photo) " +
                        "VALUES " +
                        "(@medical_visit_id, @photo)";

                    command.Parameters.Add("@medical_visit_id", SqlDbType.Int).Value = MedicalVisitModel.Id;
                    command.Parameters.Add("@photo", SqlDbType.VarBinary).Value = PhotoModel.ConvertPhotoToBinaryData(photoView);
                    command.ExecuteNonQuery();

                }
            }

            MedicalVisitModel.Medical_Visit_Name = Convert.ToString(textBoxVisitName.Text);
            MedicalVisitModel.St_Date = Convert.ToDateTime(datePickerStDate.SelectedDate);
            MedicalVisitModel.End_Date = Convert.ToDateTime(datePickerEndDate.SelectedDate);
            MedicalVisitModel.Cost = Convert.ToDecimal(textBoxVisitCost.Text);
            MedicalVisitModel.Comment = Convert.ToString(textBoxComment.Text);

            ItemView itemView = new ItemView
                (
                    StackPanelParent,
                    MedicalVisitModel.Id,
                    MainPatientModel.Id,
                    "medical_visits",
                    "id",
                    new string[] { "Название:", "Дата начала:", "Дата окончания:", "Стоимость (руб.):","Описание:", "Кол-во фотографий:" },
                    new string[]
                    {
                        MedicalVisitModel.Medical_Visit_Name,
                        MedicalVisitModel.St_Date.ToString("d"),
                        MedicalVisitModel.End_Date.ToString("d"),
                        MedicalVisitModel.Cost.ToString(),
                        MedicalVisitModel.Comment,
                        Convert.ToString(MedicalVisitModel.CountPhoto(MedicalVisitModel.Id)),
                    }
                );

            StackPanelParent.Children.Add(itemView);
        }

        private void UpDateMedicationInfo()
        {
            MedicalVisitModel.Medical_Visit_Name = Convert.ToString(textBoxVisitName.Text);
            MedicalVisitModel.St_Date = Convert.ToDateTime(datePickerStDate.SelectedDate);
            MedicalVisitModel.End_Date = Convert.ToDateTime(datePickerEndDate.SelectedDate);
            MedicalVisitModel.Cost = Convert.ToDecimal(textBoxVisitCost.Text);
            MedicalVisitModel.Comment = Convert.ToString(textBoxComment.Text);
            //Обновления текущей записи
            using (var connection = BaseRepository.GetConnection())
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText =
                    "UPDATE medical_visits SET " +
                    "medical_visit_name=@medical_visit_name,st_date=@st_date,end_date=@end_date,cost=@cost,comment=@comment " +
                    "WHERE " +
                    "id=@id";

                command.Parameters.Add("@medical_visit_name", SqlDbType.NVarChar).Value = MedicalVisitModel.Medical_Visit_Name;
                command.Parameters.Add("@st_date", SqlDbType.Date).Value = MedicalVisitModel.St_Date;
                command.Parameters.Add("@end_date", SqlDbType.Date).Value = MedicalVisitModel.End_Date;
                command.Parameters.Add("@cost", SqlDbType.Money).Value = MedicalVisitModel.Cost;
                command.Parameters.Add("@comment", SqlDbType.NVarChar).Value = MedicalVisitModel.Comment;
                command.Parameters.Add("@id", SqlDbType.Int).Value = this.Id;
                command.ExecuteNonQuery();
            }

            //Удаляем фотографии
            MedicalVisitModel.DeleteAllPhoto(this.Id);
            //Добавляем фотографии
            foreach (PhotoView photoView in stackPanelPhoto.Children)
            {
                using (var connection = BaseRepository.GetConnection())
                using (var command = new SqlCommand())
                {
                    connection.Open();
                    command.Connection = connection;
                    command.CommandText =
                        "INSERT INTO photo_about_medical_visits " +
                        "(medical_visit_id, photo) " +
                        "VALUES " +
                        "(@medical_visit_id, @photo)";

                    command.Parameters.Add("@medical_visit_id", SqlDbType.Int).Value = MedicalVisitModel.Id;
                    command.Parameters.Add("@photo", SqlDbType.VarBinary).Value = PhotoModel.ConvertPhotoToBinaryData(photoView);
                    command.ExecuteNonQuery();

                }
            }

            ItemViewParent.UpDateInfo(
                new string[] { "Название:", "Дата начала:", "Дата окончания:", "Стоимость (руб.):", "Описание:", "Кол-во фотографий:" },
                new string[]
                {
                    MedicalVisitModel.Medical_Visit_Name,
                    MedicalVisitModel.St_Date.ToString("d"),
                    MedicalVisitModel.End_Date.ToString("d"),
                    MedicalVisitModel.Cost.ToString(),
                    MedicalVisitModel.Comment,
                    Convert.ToString(MedicalVisitModel.CountPhoto(MedicalVisitModel.Id)),
                });
        }

        private void addPhoto_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            PhotoView photoView = new PhotoView(stackPanelPhoto, EnumView.MedicalVisit) { Margin = new Thickness(0, 0, 5, 0) };
            if (PhotoModel.UploadPhoto(photoView))
                stackPanelPhoto.Children.Add(photoView);
        }

        
    }
}
