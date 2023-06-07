using System;
using System.Collections.Generic;
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
using System.Runtime.InteropServices;
using System.Runtime;
using System.Windows.Interop;
using medical_family_card.Views;
using medical_family_card.Models;

namespace medical_family_card
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        //Метод для перетаскивания окна и раскрытие на все окно и обратный возврат внормальный режим
        [DllImport("user32.dll")]
        public static extern IntPtr SendMessage(IntPtr hWnd, int wMsg, int wParam, int lParam);

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            WindowInteropHelper helper = new WindowInteropHelper(this);
            SendMessage(helper.Handle, 161, 2, 0);
        }

        private void buttonMinimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void buttonClose_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void buttonScale_Click(object sender, RoutedEventArgs e)
        {
            if (this.WindowState == WindowState.Normal)
                this.WindowState = WindowState.Maximized;
            else this.WindowState = WindowState.Normal; 
        }

        private void buttonMyCard_Click(object sender, RoutedEventArgs e)
        {
            this.contentControlChildView.Content = null;
            

            if (MainPatientModel.FindPatientInfo())
            {
                MyCardView myCardView = new MyCardView(EnumView.MyCard, this);
                this.contentControlChildView.Content = myCardView;
                this.textBlockMenu.Text = myCardView.NameMenu;
            }
            else
            {
                MyCardIsNotExistView myCardIsNotExistView = new MyCardIsNotExistView(this);
                this.contentControlChildView.Content = myCardIsNotExistView;
                this.textBlockMenu.Text = myCardIsNotExistView.NameMenu;
            }
        }

        private void buttonWeight_Click(object sender, RoutedEventArgs e)
        {
            this.contentControlChildView.Content = null;

            ListItemView listItemView = new ListItemView(EnumView.Weight);
            this.contentControlChildView.Content = listItemView;
            this.textBlockMenu.Text = listItemView.NameMenu;
        }

        private void buttonMedication_Click(object sender, RoutedEventArgs e)
        {
            this.contentControlChildView.Content = null;

            ListItemView listItemView = new ListItemView(EnumView.Medication);
            this.contentControlChildView.Content = listItemView;
            this.textBlockMenu.Text = listItemView.NameMenu;
        }

        private void buttonAllergy_Click(object sender, RoutedEventArgs e)
        {
            this.contentControlChildView.Content = null;

            ListItemView listItemView = new ListItemView(EnumView.Allergy);
            this.contentControlChildView.Content = listItemView;
            this.textBlockMenu.Text = listItemView.NameMenu;
        }

        private void buttonFamiliar_Click(object sender, RoutedEventArgs e)
        {
            ListItemView listItemView = new ListItemView(EnumView.Familiar);
            this.contentControlChildView.Content = listItemView;
            this.textBlockMenu.Text = listItemView.NameMenu;
        }

        private void buttonHeight_Click(object sender, RoutedEventArgs e)
        {
            ListItemView listItemView = new ListItemView(EnumView.Height);
            this.contentControlChildView.Content = listItemView;
            this.textBlockMenu.Text = listItemView.NameMenu;
        }

        private void buttonPressure_Click(object sender, RoutedEventArgs e)
        {
            ListItemView listItemView = new ListItemView(EnumView.Pressure);
            this.contentControlChildView.Content = listItemView;
            this.textBlockMenu.Text = listItemView.NameMenu;
        }

        private void buttonDisease_Click(object sender, RoutedEventArgs e)
        {
            ListItemView listItemView = new ListItemView(EnumView.Disease);
            this.contentControlChildView.Content = listItemView;
            this.textBlockMenu.Text = listItemView.NameMenu;
        }

        private void buttonVisit_Click(object sender, RoutedEventArgs e)
        {
            ListItemView listItemView = new ListItemView(EnumView.MedicalVisit);
            this.contentControlChildView.Content = listItemView;
            this.textBlockMenu.Text = listItemView.NameMenu;
        }

        private void TextBlock_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            StartWindow startWindow = new StartWindow();
            startWindow.Show();
            this.Close();
        }
    }
}
