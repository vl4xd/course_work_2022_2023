using medical_family_card.Models;
using medical_family_card.Repositories;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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

namespace medical_family_card
{
    /// <summary>
    /// Логика взаимодействия для StartWindow.xaml
    /// </summary>
    public partial class StartWindow : Window
    {
        //0-авторизация, 1-регистрация
        private int CurrentRegime { get; set; }
        //Свойство для проверки корректности Логина
        private bool CheckLogin { get; set; }
        //Свойство для проверки корректности Пароля
        private bool CheckPassword { get; set; }

        public StartWindow()
        {
            InitializeComponent();

            //Устанавливаем режим авторизации
            CurrentRegime = 0;
        }
        
        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }

        private void buttonMinimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void buttonClose_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void textChangeRegime_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ChangeRegime();
            CheckStart();
        }

        private void ChangeRegime()
        {
            if (CurrentRegime == 0)
                CurrentRegime = 1;
            else
                CurrentRegime = 0;

            switch (CurrentRegime)
            {
                case 0:
                    textInfoRegim.Text = "Войдите в аккаунт";
                    textChangeRegime.Text = "Зарегистрируйтесь";
                    textBoxPassword2.Visibility = Visibility.Hidden;
                    buttonStart.Content = "Войти";
                    break;
                case 1:
                    textInfoRegim.Text = "Зарегистрируйтесь";
                    textChangeRegime.Text = "Войдите в аккаунт";
                    textBoxPassword2.Visibility = Visibility.Visible;
                    buttonStart.Content = "Зарегистрироваться";
                    break;
            }
        }

        private bool FindUserInDataBase()
        {
            bool validUser;
            switch (CurrentRegime)
            {
                //Проверяем при авторизации нахождение пользователя с такими данными в базе данных 
                case 0:
                    using (var connection = BaseRepository.GetConnection())
                    using (var command = new SqlCommand())
                    {
                        connection.Open();
                        command.Connection = connection;
                        command.CommandText = "SELECT * FROM patients WHERE username=@username AND passwd=@password";
                        command.Parameters.Add("@username", SqlDbType.NVarChar).Value = this.textBoxLogin.Text;
                        command.Parameters.Add("@password", SqlDbType.NVarChar).Value = this.textBoxPassword1.Password;
                        
                        if (command.ExecuteScalar() == null)
                        {
                            validUser = false;
                        }
                        else
                        {
                            var dataReader = command.ExecuteReader();
                            while (dataReader.Read())
                            {
                                MainPatientModel.Id = Convert.ToInt32(dataReader["id"]);
                                MainPatientModel.Username = Convert.ToString(dataReader["username"]);
                                MainPatientModel.Passwd = Convert.ToString(dataReader["passwd"]);
                            }
                            validUser = true;
                        }
                    }
                    return validUser;

                //Проверяем при регистрации нахождения пользователя с введеным логином в базе данных
                case 1:
                    using (var connection = BaseRepository.GetConnection())
                    using (var command = new SqlCommand())
                    {
                        connection.Open();
                        command.Connection = connection;
                        command.CommandText = "SELECT username FROM patients WHERE username=@username";
                        command.Parameters.Add("@username", SqlDbType.NVarChar).Value = this.textBoxLogin.Text;
                        validUser = command.ExecuteScalar() == null ? false : true;
                    }
                    return validUser;
            }

            return false;
        }

        private void CreateUserInDataBase()
        {
            using (var connection = BaseRepository.GetConnection())
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "INSERT INTO patients (username,passwd) VALUES (@username,@password)";
                command.Parameters.Add("@username", SqlDbType.NVarChar).Value = this.textBoxLogin.Text;
                command.Parameters.Add("@password", SqlDbType.NVarChar).Value = this.textBoxPassword1.Password;
                command.ExecuteNonQuery();
            }
        }
        private void buttonStart_Click(object sender, RoutedEventArgs e)
        {
            CheckStart();
            switch (CurrentRegime)
            {
                case 0:
                    if (CheckLogin && CheckPassword)
                    {
                        MainWindow mainWindow = new MainWindow();
                        mainWindow.Show();
                        this.Close();
                    }
                    break;

                case 1:
                    if (CheckLogin && CheckPassword)
                    {
                        CreateUserInDataBase();
                        ChangeRegime();
                    }
                    break;

            }
        }

        private void textBoxLogin_TextChanged(object sender, TextChangedEventArgs e)
        {
            CheckStart();
        }

        private void textBoxPassword1_PasswordChanged(object sender, RoutedEventArgs e)
        {
            CheckStart();
        }

        private void textBoxPassword2_PasswordChanged(object sender, RoutedEventArgs e)
        {
            CheckStart();
        }

        #region Проверка ввода Логин Пароль1 Пароль2
        private void EqualPassword1Password2()
        {
            CheckPassword = true;
            textBoxPassword2.ToolTip = null;
            textBoxPassword2.BorderBrush = Brushes.Gray;

            if (textBoxPassword1.Password != textBoxPassword2.Password)
            {
                textBoxPassword2.ToolTip = "Пароль не совпадает";
                textBoxPassword2.BorderBrush = Brushes.Red;
                CheckPassword = false;
            }
        }

        private void CheckStart()
        {
            //Присваиваем полям логин, пароль1, пароль2 стандартные значения
            CheckLogin = true;
            CheckPassword = true;

            textBoxLogin.ToolTip = null;
            textBoxLogin.BorderBrush = Brushes.Gray;

            textBoxPassword1.ToolTip = null;
            textBoxPassword1.BorderBrush = Brushes.Gray;

            textBoxPassword2.ToolTip = null;
            textBoxPassword2.BorderBrush = Brushes.Gray;

            switch (CurrentRegime)
            {
                case 0:
                    if (!FindUserInDataBase())
                    {
                        textBoxLogin.ToolTip = "Неверный логин или пароль";
                        textBoxLogin.BorderBrush = Brushes.Red;
                        textBoxPassword1.ToolTip = "Неверный логин или пароль";
                        textBoxPassword1.BorderBrush = Brushes.Red;

                        textBoxLogin.ToolTip = "Неверный логин или пароль";
                        textBoxLogin.BorderBrush = Brushes.Red;
                        textBoxPassword1.ToolTip = "Неверный логин или пароль";
                        textBoxPassword1.BorderBrush = Brushes.Red;

                        CheckLogin = false;
                    }
                    if (textBoxLogin.Text.Contains(" "))
                    {
                        textBoxLogin.ToolTip = "Логин не может содержать пробел(ы)";
                        textBoxLogin.BorderBrush = Brushes.Red;
                        CheckLogin = false;
                    }
                    if (textBoxPassword1.Password.Contains(" "))
                    {
                        textBoxPassword1.ToolTip = "Пароль не может содержать пробел(ы)";
                        textBoxPassword1.BorderBrush = Brushes.Red;
                        CheckPassword = false;
                    }
                    break;
                case 1:
                    if (FindUserInDataBase())
                    {
                        textBoxLogin.ToolTip = "Пользователь с данным логином уже зарегистрирован";
                        textBoxLogin.BorderBrush = Brushes.Red;
                        CheckLogin = false;
                    }
                    if (textBoxLogin.Text.Length < 4)
                    {
                        textBoxLogin.ToolTip = "Логин должен содержать не менее 4 символов";
                        textBoxLogin.BorderBrush = Brushes.Red;
                        CheckLogin = false;
                    }
                    if (textBoxLogin.Text.Contains(" "))
                    {
                        textBoxLogin.ToolTip = "Логин не может содержать пробел(ы)";
                        textBoxLogin.BorderBrush = Brushes.Red;
                        CheckLogin = false;
                    }
                    if (textBoxPassword1.Password.Length < 6)
                    {
                        textBoxPassword1.ToolTip = "Пароль должен содержать не менее 6 символов";
                        textBoxPassword1.BorderBrush = Brushes.Red;
                        CheckPassword = false;
                    }
                    if (textBoxPassword1.Password.Contains(" "))
                    {
                        textBoxPassword1.ToolTip = "Пароль не может содержать пробел(ы)";
                        textBoxPassword1.BorderBrush = Brushes.Red;
                        CheckPassword = false;
                    }
                    if (textBoxPassword2.Password.Length < 6)
                    {
                        textBoxPassword2.ToolTip = "Пароль должен содержать не менее 6 символов";
                        textBoxPassword2.BorderBrush = Brushes.Red;
                        CheckPassword = false;
                    }
                    if (textBoxPassword2.Password.Contains(" "))
                    {
                        textBoxPassword2.ToolTip = "Пароль не может содержать пробел(ы)";
                        textBoxPassword2.BorderBrush = Brushes.Red;
                        CheckPassword = false;
                    }
                    EqualPassword1Password2();
                    break;
            }
            
        }
        #endregion
    }
}
