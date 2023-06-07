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
using System.Windows.Shapes;

namespace medical_family_card.Views
{
    /// <summary>
    /// Логика взаимодействия для FamiliarWindow.xaml
    /// </summary>
    public partial class FamiliarWindow : Window
    {
        private int FamiliarId { get; set; }
        public string SelectedMenu { get; set; }

        public FamiliarWindow(int familiarId)
        {
            InitializeComponent();

            FamiliarId = familiarId;

            SelectedMenu = ((ComboBoxItem)comboBoxMenu.SelectedItem).Content.ToString();

            ShowInfo();
        }

        private void ShowInfo()
        {
            SelectedMenu = ((ComboBoxItem)comboBoxMenu.SelectedItem).Content.ToString();

            switch (SelectedMenu)
            {
                case "Карта":
                    MyCardView myCardView = new MyCardView(FamiliarId);
                    contentControlFamiliar.Content = myCardView;
                    break;
                case "Аллергии":
                    ListItemView listItemView = new ListItemView(EnumView.FamiliarAllergy, FamiliarId);
                    contentControlFamiliar.Content = listItemView;
                    break;
            }
        }

        private void comboBoxMenu_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (contentControlFamiliar != null && comboBoxMenu != null)
                ShowInfo();
        }

        private void menuItemClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }
    }
}
