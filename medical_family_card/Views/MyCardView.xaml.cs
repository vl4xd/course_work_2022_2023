using medical_family_card.Models;
using medical_family_card.Repositories;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Globalization;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace medical_family_card.Views
{
    /// <summary>
    /// Логика взаимодействия для MyCardView.xaml
    /// </summary>
    public partial class MyCardView : UserControl
    {
        private readonly string _nameMenu = "Моя карта";
        public string NameMenu { get { return _nameMenu; } }
        public static EnumView Regime { get; private set; }

        private bool Checher { get; set; }

        private bool EditMode { get; set; }

        public MainWindow ParentWindow { get; set; }
        private int FamiliarId { get; set; }

        /// <summary>
        /// Режим 0 - создание новой
        /// Режим 1 - просмотр существующей
        /// </summary>
        /// <param name="regime"></param>
        public MyCardView(EnumView regime, MainWindow parentWindow)
        {
            InitializeComponent();
            Regime = regime;
            ParentWindow = parentWindow;

            switch (Regime)
            {
                case EnumView.MyCardIsNotExist:
                    //Выполняем метод удаления, чтобы он присвоил стандартную фотографию профиля
                    PhotoModel.DeleteImageFromPhotoView(imageUserPhoto);
                    EnableEditFields();
                    break;
                case EnumView.MyCard:
                    DisableEditFields();
                    break;
            }

            //Устанавливаем режим для типа удаления фото
            imageUserPhoto.Regime = EnumView.MyCard;
            ShowInfo();

            CheckFields();
        }

        public MyCardView(int familiarId)
        {
            InitializeComponent();

            FamiliarId = familiarId;

            SetFamiliarMode();

            ShowFamiliarInfo();

            if (imageUserPhoto.photo.Source == null)
            {
                PhotoModel.DeleteImageFromPhotoView(imageUserPhoto);
            }
        }

        private void ShowInfo()
        {
            MainPatientModel.FindPatientInfo();

            GenderModel.CollectFromDataBase();
            foreach (string valueItem in GenderModel.Gender.Values)
            {
                comboBoxGenderId.Items.Add(valueItem);
            }

            BloodTypeModel.CollectFromDataBase();
            foreach (string valueItem in BloodTypeModel.BloodType.Values)
            {
                comboBoxBloodTypeId.Items.Add(valueItem);
            }

            RhesusFactorModel.CollectFromDataBase();
            foreach (string valueItem in RhesusFactorModel.RhesusFactor.Values)
            {
                comboBoxRhesusFactorId.Items.Add(valueItem);
            }

            switch (Regime)
            {
                case EnumView.MyCardIsNotExist:
                    textBoxUsername.Text = MainPatientModel.Username;
                    textBoxPasswd.Password = MainPatientModel.Passwd;
                    break;
                case EnumView.MyCard:
                    textBoxUsername.Text = MainPatientModel.Username;
                    textBoxPasswd.Password = MainPatientModel.Passwd;
                    textBoxFirstName.Text = MainPatientModel.FirstName;
                    textBoxLastName.Text = MainPatientModel.LastName;
                    textBoxMiddleName.Text = MainPatientModel.MiddleName;
                    datePickerDateOfBirth.SelectedDate = MainPatientModel.DateOfBirth;
                    PhotoModel.ConvertBinaryDataToPhoto(MainPatientModel.Photo, imageUserPhoto);
                    comboBoxBloodTypeId.Text = BloodTypeModel.ReturnValue(MainPatientModel.BloodTypeId);
                    comboBoxRhesusFactorId.Text = RhesusFactorModel.ReturnValue(MainPatientModel.RhesusFactorId);
                    comboBoxGenderId.Text = GenderModel.ReturnValue(MainPatientModel.GenderId);
                    break;
            }
        }

        private void SetFamiliarMode()
        {
            imageUserPhoto.ExtraButtonVisible(false);
            textBoxUsername.IsEnabled = false;
            textBlockPasswd.Visibility = Visibility.Hidden;
            textBoxPasswd.Visibility = Visibility.Hidden;
            textBoxFirstName.IsEnabled = false;
            textBoxLastName.IsEnabled = false;
            textBoxMiddleName.IsEnabled = false;
            comboBoxGenderId.IsEnabled = false;
            datePickerDateOfBirth.IsEnabled = false;
            comboBoxBloodTypeId.IsEnabled = false;
            comboBoxRhesusFactorId.IsEnabled = false;
            menuItemSave.Visibility = Visibility.Hidden;
            menuItemCancel.Visibility = Visibility.Hidden;
            menuItemEdit.Visibility = Visibility.Hidden;
            menuItemDelete.Visibility = Visibility.Hidden;

            borderShodow.Visibility = Visibility.Hidden;
        }

        private void ShowFamiliarInfo()
        {
            

            GenderModel.CollectFromDataBase();
            foreach (string valueItem in GenderModel.Gender.Values)
            {
                comboBoxGenderId.Items.Add(valueItem);
            }

            BloodTypeModel.CollectFromDataBase();
            foreach (string valueItem in BloodTypeModel.BloodType.Values)
            {
                comboBoxBloodTypeId.Items.Add(valueItem);
            }

            RhesusFactorModel.CollectFromDataBase();
            foreach (string valueItem in RhesusFactorModel.RhesusFactor.Values)
            {
                comboBoxRhesusFactorId.Items.Add(valueItem);
            }

            if (FamiliarModel.FindPatientInfo(FamiliarId))
            {
                textBoxUsername.Text = FamiliarModel.Username;
                textBoxFirstName.Text = FamiliarModel.FirstName;
                textBoxLastName.Text = FamiliarModel.LastName;
                textBoxMiddleName.Text = FamiliarModel.MiddleName;
                datePickerDateOfBirth.SelectedDate = FamiliarModel.DateOfBirth;
                PhotoModel.ConvertBinaryDataToPhoto(FamiliarModel.Photo, imageUserPhoto);
                comboBoxBloodTypeId.Text = BloodTypeModel.ReturnValue(FamiliarModel.BloodTypeId);
                comboBoxRhesusFactorId.Text = RhesusFactorModel.ReturnValue(FamiliarModel.RhesusFactorId);
                comboBoxGenderId.Text = GenderModel.ReturnValue(FamiliarModel.GenderId);
            }
            else
            {
                textBoxUsername.Text = FamiliarModel.Username;
                datePickerDateOfBirth.SelectedDate = null;
            }
        }

        private void EnableEditFields()
        {
            EditMode = true;
            imageUserPhoto.ExtraButtonVisible(true);
            textBoxUsername.IsEnabled = true;
            textBoxPasswd.IsEnabled = true;
            textBoxFirstName.IsEnabled = true;
            textBoxLastName.IsEnabled = true;
            textBoxMiddleName.IsEnabled = true;
            comboBoxGenderId.IsEnabled = true;
            datePickerDateOfBirth.IsEnabled = true;
            comboBoxBloodTypeId.IsEnabled = true;
            comboBoxRhesusFactorId.IsEnabled = true;
            EnableEditMode();
        }
        
        private void DisableEditFields()
        {
            EditMode = false;
            imageUserPhoto.ExtraButtonVisible(false);
            textBoxUsername.IsEnabled = false;
            textBoxPasswd.IsEnabled = false;
            textBoxFirstName.IsEnabled = false;
            textBoxLastName.IsEnabled = false;
            textBoxMiddleName.IsEnabled = false;
            comboBoxGenderId.IsEnabled = false;
            datePickerDateOfBirth.IsEnabled = false;
            comboBoxBloodTypeId.IsEnabled = false;
            comboBoxRhesusFactorId.IsEnabled = false;
            DisableEditMode();
        }

        private void EnableEditMode()
        {
            menuItemSave.Visibility = Visibility.Visible;
            menuItemCancel.Visibility = Visibility.Visible;
            menuItemEdit.Visibility = Visibility.Hidden;

            switch (Regime)
            {
                case EnumView.MyCardIsNotExist:
                    menuItemDelete.Visibility = Visibility.Hidden;
                    break;
                case EnumView.MyCard:
                    menuItemDelete.Visibility = Visibility.Visible;
                    break;
            }
        }

        private void DisableEditMode()
        {
            menuItemSave.Visibility = Visibility.Hidden;
            menuItemCancel.Visibility = Visibility.Hidden;
            menuItemEdit.Visibility = Visibility.Visible;
            menuItemDelete.Visibility = Visibility.Hidden;
        }



        #region Провекра вводимых данных
        private void CheckFields()
        {
            Checher = true;

            textBoxUsername.BorderBrush = Brushes.Gray;
            textBoxPasswd.BorderBrush = Brushes.Gray;
            textBoxFirstName.BorderBrush = Brushes.Gray;
            textBoxLastName.BorderBrush = Brushes.Gray;
            textBoxMiddleName.BorderBrush = Brushes.Gray;
            comboBoxGenderId.BorderBrush = Brushes.Gray;
            datePickerDateOfBirth.BorderBrush = Brushes.Gray;
            comboBoxBloodTypeId.BorderBrush = Brushes.Gray;
            comboBoxRhesusFactorId.BorderBrush= Brushes.Gray;

            //Проверка логина
            if (textBoxUsername.Text.Length < 4)
            {
                textBoxUsername.ToolTip = "Логин должен содержать не менее 4 символов";
                textBoxUsername.BorderBrush = Brushes.Red;
                Checher = false;
            }
            if (textBoxUsername.Text.Contains(" "))
            {
                textBoxUsername.ToolTip = "Логин не может содержать пробел(ы)";
                textBoxUsername.BorderBrush = Brushes.Red;
                Checher = false;
            }
            //Проверка пароля
            if (textBoxPasswd.Password.Length < 6)
            {
                textBoxPasswd.ToolTip = "Пароль должен содержать не менее 6 символов";
                textBoxPasswd.BorderBrush = Brushes.Red;
                Checher = false;
            }
            if (textBoxPasswd.Password.Contains(" "))
            {
                textBoxPasswd.ToolTip = "Пароль не может содержать пробел(ы)";
                textBoxPasswd.BorderBrush = Brushes.Red;
                Checher = false;
            }
            //Проверка Имени
            if (textBoxFirstName.Text.Length < 1)
            {
                textBoxFirstName.ToolTip = "Вы не ввели cвое Имя";
                textBoxFirstName.BorderBrush = Brushes.Red;
                Checher = false;
            }
            if (textBoxFirstName.Text.Contains(" "))
            {
                textBoxFirstName.ToolTip = "Имя не может содержать проеблы";
                textBoxFirstName.BorderBrush = Brushes.Red;
                Checher = false;
            }
            //Проверка Фамилии
            if (textBoxLastName.Text.Length < 1)
            {
                textBoxLastName.ToolTip = "Вы не ввели cвою Фамилию";
                textBoxLastName.BorderBrush = Brushes.Red;
                Checher = false;
            }
            if (textBoxLastName.Text.Contains(" "))
            {
                textBoxLastName.ToolTip = "Фамилия не может содержать проеблы";
                textBoxLastName.BorderBrush = Brushes.Red;
                Checher = false;
            }
            //Проверка Отчество
            if (textBoxMiddleName.Text.Length < 1)
            {
                textBoxMiddleName.ToolTip = "Вы не ввели cвое Отчество";
                textBoxMiddleName.BorderBrush = Brushes.Red;
                Checher = false;
            }
            if (textBoxMiddleName.Text.Contains(" "))
            {
                textBoxMiddleName.ToolTip = "Отчество не может содержать проеблы";
                textBoxMiddleName.BorderBrush = Brushes.Red;
                Checher = false;
            }
            //Проверка Гендер
            if (comboBoxGenderId.SelectedItem == null)
            {
                comboBoxGenderId.ToolTip = "Вы не ввели свой Гендер";
                comboBoxGenderId.BorderBrush = Brushes.Red;
                Checher = false;
            }
            //Проверка Дата рождения
            if (datePickerDateOfBirth.SelectedDate == null)
            {
                datePickerDateOfBirth.ToolTip = "Вы не ввели свою Дату рождения";
                datePickerDateOfBirth.BorderBrush = Brushes.Red;
                Checher = false;
            }
            //Проверка Группа крови
            if (comboBoxBloodTypeId.SelectedItem == null)
            {
                comboBoxBloodTypeId.ToolTip = "Вы не ввели свою Группу крови";
                comboBoxBloodTypeId.BorderBrush = Brushes.Red;
                Checher = false;
            }
            //Проверка Резус-фатор
            if (comboBoxRhesusFactorId.SelectedItem == null)
            {

                comboBoxRhesusFactorId.ToolTip = "Вы не ввели свой Резус-фактор";
                comboBoxRhesusFactorId.BorderBrush = Brushes.Red;
                Checher = false;
            }
        }
        #endregion

        #region События для проверки вводимых данных
        //Событие Логин
        private void textBoxUsername_TextChanged(object sender, TextChangedEventArgs e)
        {
            CheckFields();
        }
        //Событие Пароль
        private void textBoxPasswd_PasswordChanged(object sender, RoutedEventArgs e)
        {
            CheckFields();
        }
        //Событие Имя
        private void textBoxFirstName_TextChanged(object sender, TextChangedEventArgs e)
        {
            CheckFields();
        }
        //Событие Фамилия
        private void textBoxLastName_TextChanged(object sender, TextChangedEventArgs e)
        {
            CheckFields();
        }
        //Событие Отчества
        private void textBoxMiddleName_TextChanged(object sender, TextChangedEventArgs e)
        {
            CheckFields();
        }
        //Событие Гендер
        private void comboBoxGenderId_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CheckFields();
        }
        //Событие Дата рождения
        private void datePickerDateOfBirth_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (comboBoxBloodTypeId != null && comboBoxRhesusFactorId != null)
                CheckFields();
        }
        //Событие Группа крови
        private void comboBoxBloodTypeId_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CheckFields();
        }
        //Событие Резус-фактор
        private void comboBoxRhesusFactorId_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CheckFields();
        }
        #endregion

        //Событие Сохранить
        private void menuItemSave_Click(object sender, RoutedEventArgs e)
        {
            switch (Regime)
            {
                case EnumView.MyCardIsNotExist:
                    if (Checher)
                    {
                        CreateNewPatientInfo();
                        Regime = EnumView.MyCard;
                        ShowInfo();
                        DisableEditFields();
                    }
                    break;
                case EnumView.MyCard:
                    if (Checher)
                    {
                        UpDatePatientInfo();
                        ShowInfo();
                        DisableEditFields();
                    }
                    break;
            }
        }
        //Собтыие Отмена
        private void menuItemCancel_Click(object sender, RoutedEventArgs e)
        {
            switch (Regime)
            {
                case EnumView.MyCardIsNotExist:
                    ParentWindow.contentControlChildView.Content = new MyCardIsNotExistView(ParentWindow);
                    break;
                case EnumView.MyCard:
                    DisableEditFields();
                    ShowInfo();
                    break;
            }
        }
        // Событие Редактировать
        private void menuItemEdit_Click(object sender, RoutedEventArgs e)
        {
            EnableEditFields();
        }

        //Событие Удалить
        private void menuItemDelete_Click(object sender, RoutedEventArgs e)
        {
            DeletePatientInfo();
            ParentWindow.contentControlChildView.Content = new MyCardIsNotExistView(ParentWindow);
        }

        private void CreateNewPatientInfo()
        {
            //Обновление Логина и Пароля
            using (var connection = BaseRepository.GetConnection())
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "UPDATE patients SET " +
                    "username=@username,passwd=@passwd " +
                    "WHERE " +
                    "id=@id";
                command.Parameters.Add("@username", SqlDbType.NVarChar).Value = this.textBoxUsername.Text;
                command.Parameters.Add("@passwd", SqlDbType.NVarChar).Value = this.textBoxPasswd.Password;
                command.Parameters.Add("@id", SqlDbType.Int).Value = MainPatientModel.Id;
                command.ExecuteNonQuery();
            }

            //Создание новой MyCard
            using (var connection = BaseRepository.GetConnection())
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "INSERT INTO info_about_patients " +
                    "(patient_id,first_name,last_name,middle_name,date_of_birth,photo,blood_type_id,rhesus_factor_id,gender_id) " +
                    "VALUES " +
                    "(@patient_id,@first_name,@last_name,@middle_name,@date_of_birth,@photo,@blood_type_id,@rhesus_factor_id,@gender_id)";
                command.Parameters.Add("@patient_id", SqlDbType.Int).Value = MainPatientModel.Id;
                command.Parameters.Add("@first_name", SqlDbType.NVarChar).Value = textBoxFirstName.Text;
                command.Parameters.Add("@last_name", SqlDbType.NVarChar).Value = textBoxLastName.Text;
                command.Parameters.Add("@middle_name", SqlDbType.NVarChar).Value = textBoxMiddleName.Text;
                command.Parameters.Add("@date_of_birth", SqlDbType.Date).Value = datePickerDateOfBirth.SelectedDate;
                command.Parameters.Add("@photo", SqlDbType.VarBinary).Value = PhotoModel.ConvertPhotoToBinaryData(imageUserPhoto);
                command.Parameters.Add("@blood_type_id", SqlDbType.Int).Value = BloodTypeModel.ReturnIndex(comboBoxBloodTypeId.Text);
                command.Parameters.Add("@rhesus_factor_id", SqlDbType.Int).Value = RhesusFactorModel.ReturnIndex(comboBoxRhesusFactorId.Text);
                command.Parameters.Add("@gender_id", SqlDbType.Int).Value = GenderModel.ReturnIndex(comboBoxGenderId.Text);
                command.ExecuteNonQuery();
            }
        }

        private void UpDatePatientInfo()
        {   
            //Обновление Логина и Пароля
            using (var connection = BaseRepository.GetConnection())
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = 
                    "UPDATE patients SET " +
                    "username=@username,passwd=@passwd " +
                    "WHERE " +
                    "id=@id";
                command.Parameters.Add("@username", SqlDbType.NVarChar).Value = this.textBoxUsername.Text;
                command.Parameters.Add("@passwd", SqlDbType.NVarChar).Value = this.textBoxPasswd.Password;
                command.Parameters.Add("@id", SqlDbType.Int).Value = MainPatientModel.Id;
                command.ExecuteNonQuery();
            }

            //Обновления текущей MyCard
            using (var connection = BaseRepository.GetConnection())
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = 
                    "UPDATE info_about_patients SET " +
                    "first_name=@first_name,last_name=@last_name,middle_name=@middle_name,date_of_birth=@date_of_birth," +
                    "photo=@photo,blood_type_id=@blood_type_id,rhesus_factor_id=@rhesus_factor_id,gender_id=@gender_id " +
                    "WHERE " +
                    "patient_id=@patient_id";
                command.Parameters.Add("@first_name", SqlDbType.NVarChar).Value = textBoxFirstName.Text;
                command.Parameters.Add("@last_name", SqlDbType.NVarChar).Value = textBoxLastName.Text;
                command.Parameters.Add("@middle_name", SqlDbType.NVarChar).Value = textBoxMiddleName.Text;
                command.Parameters.Add("@date_of_birth", SqlDbType.Date).Value = datePickerDateOfBirth.SelectedDate;
                command.Parameters.Add("@photo", SqlDbType.VarBinary).Value = PhotoModel.ConvertPhotoToBinaryData(imageUserPhoto);
                command.Parameters.Add("@blood_type_id", SqlDbType.Int).Value = BloodTypeModel.ReturnIndex(comboBoxBloodTypeId.Text);
                command.Parameters.Add("@rhesus_factor_id", SqlDbType.Int).Value = RhesusFactorModel.ReturnIndex(comboBoxRhesusFactorId.Text);
                command.Parameters.Add("@gender_id", SqlDbType.Int).Value = GenderModel.ReturnIndex(comboBoxGenderId.Text);
                command.Parameters.Add("@patient_id", SqlDbType.Int).Value = MainPatientModel.Id;
                command.ExecuteNonQuery();
            }
        }
        
        private void DeletePatientInfo()
        {
            //Обновления текущей MyCard
            using (var connection = BaseRepository.GetConnection())
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = 
                    "DELETE FROM info_about_patients " +
                    "WHERE " +
                    "patient_id=@patient_id";
                command.Parameters.Add("@patient_id", SqlDbType.Int).Value = MainPatientModel.Id;
                command.ExecuteNonQuery();
            }
        }
    }
}
