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

namespace medical_family_card.Views
{
    /// <summary>
    /// Логика взаимодействия для MyCardIsNotExistView.xaml
    /// </summary>
    /// 

    public partial class MyCardIsNotExistView : UserControl
    {
        private readonly string _nameMenu = "Моя карта";
        public string NameMenu { get { return _nameMenu; } }
        //Свойсвто хранящая ссылку на класс Родителя
        public MainWindow ParentWindow { get; set; }

        public MyCardIsNotExistView(MainWindow parentWindow)
        {
            InitializeComponent();
            //Привязка свойства с классом Роделем
            ParentWindow = parentWindow;
        }

        private void TextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            ParentWindow.contentControlChildView.Content = new MyCardView(EnumView.MyCardIsNotExist, ParentWindow);
        }
    }
}
