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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace medical_family_card.Views
{
    /// <summary>
    /// Логика взаимодействия для FamiliarItemView.xaml
    /// </summary>
    public partial class FamiliarItemView : UserControl
    {
        private int IdMainPatient { get; set; }
        private int IdFamiliarPatient { get; set; }
        private string NameTableDataBase { get; set; }

        private string NameColumnIdMainPatient { get; set; }
        private string NameColumnIdFamiliarPatient { get; set; }

        private string[] Titles { get; set; }
        private string[] Values { get; set; }
        private double DefaultWidht { get; set; }
        private TreeView TreeViewParent { get; set; }
        public int Regime { get; set; }

        //regime 1-добавленные, 3-полученные
        public FamiliarItemView(int regime, TreeView treeViewParent, int idMainPatient, int idFamiliarPatient, 
            string nameTableDataBase, string nameColumnIdMainPatient, string nameColumnIdFamiliarPatient)
        {
            InitializeComponent();

            IdMainPatient = idMainPatient;
            IdFamiliarPatient = idFamiliarPatient;

            NameTableDataBase = nameTableDataBase;

            NameColumnIdMainPatient = nameColumnIdMainPatient;
            NameColumnIdFamiliarPatient = nameColumnIdFamiliarPatient;

            DefaultWidht = this.Width;
            Regime = regime;

            TreeViewParent = treeViewParent;

            VisibleCommandButton();
        }

        public void UpDateInfo()
        {
            stackPanelTitle.Children.Clear();
            stackPanelValue.Children.Clear();
            this.Width = DefaultWidht;

            Titles = new string[] { "Логин:" };

            using (var connection = BaseRepository.GetConnection())
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = $"SELECT * FROM patients WHERE id=@id";
                command.Parameters.Add("@id", SqlDbType.Int).Value = this.IdFamiliarPatient;

                var dataReader = command.ExecuteReader();
                if (dataReader.HasRows)
                {
                    while (dataReader.Read())
                    {
                        Values = new string[] { Convert.ToString(dataReader["username"]) };
                    }
                }
            }

            foreach (string titel in Titles)
            {
                TextBlock textBlock = new TextBlock()
                {
                    Text = titel,
                    FontSize = 16,
                    FontWeight = FontWeights.Bold,
                    Foreground = new SolidColorBrush(Colors.Gray),
                    Margin = new Thickness(0, 0, 5, 0),
                    HorizontalAlignment = HorizontalAlignment.Right,
                };
                stackPanelTitle.Children.Add(textBlock);

                this.Width += textBlock.Width;
            }

            foreach (var value in Values)
            {
                TextBlock textBlock = new TextBlock()
                {
                    Text = value,
                    FontSize = 16,
                    FontWeight = FontWeights.Bold,
                    Foreground = new SolidColorBrush(Colors.Black),
                    Margin = new Thickness(5, 0, 0, 0),
                    HorizontalAlignment = HorizontalAlignment.Left,
                };
                stackPanelValue.Children.Add(textBlock);
            }
        }

        private void VisibleCommandButton()
        {
            if (Regime == FamiliarStatusModel.ReturnIndex("Добавленные"))
            {
                addFamiliar.Visibility = Visibility.Hidden;
                openFamiliar.Visibility = Visibility.Visible;
                deleteFamiliar.Visibility = Visibility.Visible;
            }
            if (Regime == FamiliarStatusModel.ReturnIndex("Полученные"))
            {
                addFamiliar.Visibility = Visibility.Visible;
                openFamiliar.Visibility = Visibility.Hidden;
                deleteFamiliar.Visibility = Visibility.Visible;
            }
        }

        private void addFamiliar_Click(object sender, RoutedEventArgs e)
        {
            foreach (TreeViewItem status in TreeViewParent.Items)
            {
                if ((string)status.Header == FamiliarStatusModel.ReturnValue(Regime))
                {
                    status.Items.Remove(this);
                }
            }

            this.Regime = FamiliarStatusModel.ReturnIndex("Добавленные");
            this.VisibleCommandButton();

            //Обновления статуса
            using (var connection = BaseRepository.GetConnection())
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText =
                    $"UPDATE {NameTableDataBase} SET " +
                    "familiar_status_id=@familiar_status_id " +
                    "WHERE " +
                    $"{NameColumnIdMainPatient}=@idMainPatient AND {NameColumnIdFamiliarPatient}=@idFamiliarPatient";

                command.Parameters.Add("@familiar_status_id", SqlDbType.Int).Value = Regime;
                command.Parameters.Add("@idMainPatient", SqlDbType.Int).Value = IdMainPatient;
                command.Parameters.Add("@idFamiliarPatient", SqlDbType.Int).Value = IdFamiliarPatient;
                command.ExecuteNonQuery();
            }

            foreach (TreeViewItem status in TreeViewParent.Items)
            {
                if ((string)status.Header == FamiliarStatusModel.ReturnValue(Regime))
                {
                    status.Items.Add(this);
                }
            }
        }

        private void openFamiliar_Click(object sender, RoutedEventArgs e)
        {
            FamiliarWindow familiarWindow = new FamiliarWindow(IdFamiliarPatient);
            familiarWindow.ShowDialog();
        }

        private void deleteFamiliar_Click(object sender, RoutedEventArgs e)
        {
            using (var connection = BaseRepository.GetConnection())
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText =
                    $"DELETE FROM {NameTableDataBase} " +
                    "WHERE " +
                    $"{NameColumnIdMainPatient}=@idMainPatient AND {NameColumnIdFamiliarPatient}=@idFamiliarPatient";
                command.Parameters.Add("@idMainPatient", SqlDbType.Int).Value = IdMainPatient;
                command.Parameters.Add("@idFamiliarPatient", SqlDbType.Int).Value = IdFamiliarPatient;
                command.ExecuteNonQuery();
            }

            foreach (TreeViewItem status in TreeViewParent.Items)
            {
                if ((string)status.Header == FamiliarStatusModel.ReturnValue(Regime))
                {
                    status.Items.Remove(this);
                }
            }
        }
    }
}
