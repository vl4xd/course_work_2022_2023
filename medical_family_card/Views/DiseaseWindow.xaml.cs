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
    /// Логика взаимодействия для DiseaseWindow.xaml
    /// </summary>
    public partial class DiseaseWindow : Window
    {
        private int Id { get; set; }
        private ItemView ItemViewParent { get; set; }
        private StackPanel StackPanelParent { get; set; }
        private int Regime { get; set; }
        private bool Checher { get; set; }

        public DiseaseWindow(int regime, StackPanel stackPanelParent)
        {
            InitializeComponent();
            Regime = regime;
            StackPanelParent = stackPanelParent;

            ShowInfo();
        }

        public DiseaseWindow(int regime, int id, StackPanel stackPanelParent, ItemView itemViewParent)
        {
            InitializeComponent();
            Id = id;
            ItemViewParent = itemViewParent;
            StackPanelParent = stackPanelParent;
            Regime = regime;

            ShowInfo();
        }

        public DiseaseWindow(int regime, int id, ItemView itemViewParent)
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
                    command.CommandText = "SELECT * FROM diseases WHERE id=@id";
                    command.Parameters.Add("@id", SqlDbType.Int).Value = this.Id;
                    var dataReader = command.ExecuteReader();

                    while (dataReader.Read())
                    {
                        textBoxDiseaseName.Text = Convert.ToString(dataReader["disease_name"]);
                        datePickerStDate.SelectedDate = Convert.ToDateTime(dataReader["st_date"]);
                        datePickerEndDate.SelectedDate = Convert.ToDateTime(dataReader["end_date"]);
                        textBoxComment.Text = Convert.ToString(dataReader["comment"]);
                    }
                }

                using (var connection = BaseRepository.GetConnection())
                using (var command = new SqlCommand())
                {
                    connection.Open();
                    command.Connection = connection;
                    command.CommandText =
                        "SELECT * FROM photo_about_diseases " +
                        "WHERE " +
                        "disease_id=@disease_id";
                    command.Parameters.Add("@disease_id", SqlDbType.Int).Value = this.Id;
                    var dataReader = command.ExecuteReader();
                    if (dataReader.HasRows)
                    {
                        while (dataReader.Read())
                        {
                            PhotoView photoView = new PhotoView(stackPanelPhoto, EnumView.Disease) { Margin = new Thickness(0, 0, 5, 0) };
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

            textBoxDiseaseName.BorderBrush = Brushes.Gray;
            datePickerStDate.BorderBrush = Brushes.Gray;
            datePickerEndDate.BorderBrush = Brushes.Gray;

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
            if (textBoxDiseaseName.Text.Length < 1)
            {
                textBoxDiseaseName.ToolTip = "Вы не ввели Название";
                textBoxDiseaseName.BorderBrush = Brushes.Red;
                Checher = false;
            }
        }

        private void textBoxDiseaseName_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (datePickerStDate != null && datePickerEndDate != null)
                CheckFields();
        }

        private void datePickerEndDate_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (datePickerStDate != null)
                CheckFields();
        }

        private void datePickerStDate_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (datePickerEndDate != null)
                CheckFields();
        }

        private void EnableEditFields()
        {
            textBoxDiseaseName.IsEnabled = true;
            datePickerStDate.IsEnabled = true;
            datePickerEndDate.IsEnabled = true;
            textBoxComment.IsEnabled = true;
            EnableEditMode();
        }

        private void DisableEditFields()
        {
            textBoxDiseaseName.IsEnabled = false;
            datePickerStDate.IsEnabled = false;
            datePickerEndDate.IsEnabled = false;
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
                    "INSERT INTO diseases " +
                    "(patient_id, disease_name, st_date, end_date, comment) " +
                    "VALUES " +
                    "(@patient_id, @disease_name, @st_date, @end_date, @comment)";
                command.Parameters.Add("@patient_id", SqlDbType.Int).Value = MainPatientModel.Id;
                command.Parameters.Add("@disease_name", SqlDbType.NVarChar).Value = textBoxDiseaseName.Text;
                command.Parameters.Add("@st_date", SqlDbType.Date).Value = datePickerStDate.SelectedDate;
                command.Parameters.Add("@end_date", SqlDbType.Date).Value = datePickerEndDate.SelectedDate;
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
                    "SELECT * FROM diseases WHERE patient_id=@patient_id ORDER BY id DESC";
                command.Parameters.Add("@patient_id", SqlDbType.Int).Value = MainPatientModel.Id;

                var dataReader = command.ExecuteReader();
                dataReader.Read();
                DiseaseModel.Id = Convert.ToInt32(dataReader["id"]);
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
                        "INSERT INTO photo_about_diseases " +
                        "(disease_id, photo) " +
                        "VALUES " +
                        "(@disease_id, @photo)";

                    command.Parameters.Add("@disease_id", SqlDbType.Int).Value = DiseaseModel.Id;
                    command.Parameters.Add("@photo", SqlDbType.VarBinary).Value = PhotoModel.ConvertPhotoToBinaryData(photoView);
                    command.ExecuteNonQuery();

                }
            }

            DiseaseModel.Disease_Name = Convert.ToString(textBoxDiseaseName.Text);
            DiseaseModel.St_Date = Convert.ToDateTime(datePickerStDate.SelectedDate);
            DiseaseModel.End_Date = Convert.ToDateTime(datePickerEndDate.SelectedDate);
            DiseaseModel.Comment = Convert.ToString(textBoxComment.Text);

            ItemView itemView = new ItemView
                (
                    StackPanelParent,
                    DiseaseModel.Id,
                    MainPatientModel.Id,
                    "diseases",
                    "id",
                    new string[] { "Название:", "Дата начала болезни:", "Дата окончания болезни:", "Описание:", "Кол-во фотографий:" },
                    new string[]
                    {
                        DiseaseModel.Disease_Name,
                        DiseaseModel.St_Date.ToString("d"),
                        DiseaseModel.End_Date.ToString("d"),
                        DiseaseModel.Comment,
                        Convert.ToString(DiseaseModel.CountPhoto(DiseaseModel.Id)),
                    }
                );

            StackPanelParent.Children.Add(itemView);
        }

        private void UpDateMedicationInfo()
        {
            DiseaseModel.Disease_Name = Convert.ToString(textBoxDiseaseName.Text);
            DiseaseModel.St_Date = Convert.ToDateTime(datePickerStDate.SelectedDate);
            DiseaseModel.End_Date = Convert.ToDateTime(datePickerEndDate.SelectedDate);
            DiseaseModel.Comment = Convert.ToString(textBoxComment.Text);
            //Обновления текущей записи
            using (var connection = BaseRepository.GetConnection())
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText =
                    "UPDATE diseases SET " +
                    "disease_name=@disease_name,st_date=@st_date,end_date=@end_date,comment=@comment " +
                    "WHERE " +
                    "id=@id";

                command.Parameters.Add("@disease_name", SqlDbType.NVarChar).Value = DiseaseModel.Disease_Name;
                command.Parameters.Add("@st_date", SqlDbType.Date).Value = DiseaseModel.St_Date;
                command.Parameters.Add("@end_date", SqlDbType.Date).Value = DiseaseModel.End_Date;
                command.Parameters.Add("@comment", SqlDbType.NVarChar).Value = DiseaseModel.Comment;
                command.Parameters.Add("@id", SqlDbType.Int).Value = this.Id;
                command.ExecuteNonQuery();
            }

            //Удаляем фотографии
            DiseaseModel.DeleteAllPhoto(this.Id);
            //Добавляем фотографии
            foreach (PhotoView photoView in stackPanelPhoto.Children)
            {
                using (var connection = BaseRepository.GetConnection())
                using (var command = new SqlCommand())
                {
                    connection.Open();
                    command.Connection = connection;
                    command.CommandText =
                        "INSERT INTO photo_about_diseases " +
                        "(disease_id, photo) " +
                        "VALUES " +
                        "(@disease_id, @photo)";

                    command.Parameters.Add("@disease_id", SqlDbType.Int).Value = DiseaseModel.Id;
                    command.Parameters.Add("@photo", SqlDbType.VarBinary).Value = PhotoModel.ConvertPhotoToBinaryData(photoView);
                    command.ExecuteNonQuery();

                }
            }

            ItemViewParent.UpDateInfo(
                new string[] { "Название:", "Дата начала болезни:", "Дата окончания болезни:", "Описание:", "Кол-во фотографий:" },
                new string[]
                {
                    DiseaseModel.Disease_Name,
                    DiseaseModel.St_Date.ToString("d"),
                    DiseaseModel.End_Date.ToString("d"),
                    DiseaseModel.Comment,
                    Convert.ToString(DiseaseModel.CountPhoto(DiseaseModel.Id)),
                });
        }

        private void addPhoto_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            PhotoView photoView = new PhotoView(stackPanelPhoto, EnumView.Disease) { Margin = new Thickness(0, 0, 5, 0) };
            if (PhotoModel.UploadPhoto(photoView))
                stackPanelPhoto.Children.Add(photoView);
        }

        
    }
}
