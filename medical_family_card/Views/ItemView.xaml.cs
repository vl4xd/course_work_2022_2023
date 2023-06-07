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
using System.Windows.Markup.Localizer;

namespace medical_family_card.Views
{
    /// <summary>
    /// Логика взаимодействия для ItemView.xaml
    /// </summary>
    public partial class ItemView : UserControl
    {
        private int Id { get; set; }
        private int Patient_Id { get; set; }
        private string NameTableDataBase { get; set; }
        private string NameColumnIdDataBase { get; set; }
        private string[] Titles { get; set; }
        private string[] Values { get; set; }
        private double DefaultWidht { get; set; }
        private StackPanel StackPanelParent { get; set; }
        private TreeView TreeViewParent { get; set; }
        private EnumView Regime { get; set; }
        

        public ItemView(StackPanel stackPanelParent, int id, int patient_id, string nameTableDataBase, string nameColumnIdDataBase, string[] titels, string[] values)
        {
            InitializeComponent();

            Id = id;
            Patient_Id = patient_id;
            NameTableDataBase = nameTableDataBase;
            NameColumnIdDataBase = nameColumnIdDataBase;
            Titles = titels;
            Values = values;
            DefaultWidht = this.Width;

            StackPanelParent = stackPanelParent;

            UpDateInfo(Titles, Values);
        }

        public ItemView(EnumView regime, StackPanel stackPanelParent, int id, int patient_id, string nameTableDataBase, string nameColumnIdDataBase, string[] titels, string[] values)
        {
            InitializeComponent();

            Id = id;
            Patient_Id = patient_id;
            NameTableDataBase = nameTableDataBase;
            NameColumnIdDataBase = nameColumnIdDataBase;
            Titles = titels;
            Values = values;
            DefaultWidht = this.Width;
            Regime = regime;
            SetFamiliarMode();

            StackPanelParent = stackPanelParent;

            UpDateInfo(Titles, Values);
        }

        public ItemView(TreeView treeViewParent, int id, int patient_id, string nameTableDataBase, string nameColumnIdDataBase, string[] titels, string[] values)
        {
            InitializeComponent();

            Id = id;
            Patient_Id = patient_id;
            NameTableDataBase = nameTableDataBase;
            NameColumnIdDataBase = nameColumnIdDataBase;
            Titles = titels;
            Values = values;
            DefaultWidht = this.Width;

            TreeViewParent = treeViewParent;


            UpDateInfo(Titles, Values);
        }

        private void SetFamiliarMode()
        {
            deleteItem.Visibility = Visibility.Hidden;
            editItem.Visibility = Visibility.Hidden;
        }

        public void UpDateInfo(string[] titels, string[] values)
        {
            stackPanelTitle.Children.Clear();
            stackPanelValue.Children.Clear();
            this.Width = DefaultWidht;


            Titles = titels;
            Values = values;


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

        private void deleteItem_Click(object sender, RoutedEventArgs e)
        {
            //Если элемент находится в StackPanel
            if (StackPanelParent != null)
            {
                //Удаляем из Базы данных
                using (var connection = BaseRepository.GetConnection())
                using (var command = new SqlCommand())
                {
                    connection.Open();
                    command.Connection = connection;
                    command.CommandText =
                        $"DELETE FROM {NameTableDataBase} " +
                        "WHERE " +
                        $"{NameColumnIdDataBase}=@id";
                    command.Parameters.Add("@id", SqlDbType.Int).Value = this.Id;
                    command.ExecuteNonQuery();
                }

                //Удаляем из StackPanel
                StackPanelParent.Children.Remove(this);
            }
        }

        private void openItem_Click(object sender, RoutedEventArgs e)
        {
            switch (NameTableDataBase)
            {
                case "weights":
                    WeightWindow weightWindow = new WeightWindow(1, this, this.Id, this.Patient_Id);
                    weightWindow.ShowDialog();
                    break;
                case "allergies":
                    AllergyWindow allergyWindow = new AllergyWindow(1, this.Id, this);
                    allergyWindow.ShowDialog();
                    break;
                case "medications":
                    MedicationWindow medicationWindow = new MedicationWindow(1, this.Id, this);
                    medicationWindow.ShowDialog();
                    break;
                case "heights":
                    HeightWindow heightWindow = new HeightWindow(1, this.Id, this);
                    heightWindow.ShowDialog();
                    break;
                case "pressures":
                    PressureWindow pressureWindow = new PressureWindow(1, this.Id, this);
                    pressureWindow.ShowDialog();
                    break;
                case "diseases":
                    DiseaseWindow diseaseWindow = new DiseaseWindow(1, this.Id, this);
                    diseaseWindow.ShowDialog();
                    break;
                case "medical_visits":
                    MedicalVisitWindow medicalVisitWindow = new MedicalVisitWindow(1, this.Id, this);
                    medicalVisitWindow.ShowDialog();
                    break;
            }
            
        }

        private void editItem_Click(object sender, RoutedEventArgs e)
        {
            switch (NameTableDataBase)
            {
                case "weights":
                    WeightWindow weightWindow = new WeightWindow(2, this, this.Id, this.Patient_Id);
                    weightWindow.ShowDialog();
                    break;
                case "allergies":
                    AllergyWindow allergyWindow = new AllergyWindow(2, this.Id, this);
                    allergyWindow.ShowDialog();
                    break;
                case "medications":
                    MedicationWindow medicationWindow = new MedicationWindow(2, this.Id, this);
                    medicationWindow.ShowDialog();
                    break;
                case "heights":
                    HeightWindow heightWindow = new HeightWindow(2, this.Id, this);
                    heightWindow.ShowDialog();
                    break;
                case "pressures":
                    PressureWindow pressureWindow = new PressureWindow(2, this.Id, this);
                    pressureWindow.ShowDialog();
                    break;
                case "diseases":
                    DiseaseWindow diseaseWindow = new DiseaseWindow(2, this.Id, this);
                    diseaseWindow.ShowDialog();
                    break;
                case "medical_visits":
                    MedicalVisitWindow medicalVisitWindow = new MedicalVisitWindow(2, this.Id, this);
                    medicalVisitWindow.ShowDialog();
                    break;
            }
        }
    }
}
