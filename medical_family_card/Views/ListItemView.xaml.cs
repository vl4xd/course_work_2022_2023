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
using System.Drawing;
using System.Runtime.InteropServices;

namespace medical_family_card.Views
{
    /// <summary>
    /// Логика взаимодействия для ListItemView.xaml
    /// </summary>
    public partial class ListItemView : UserControl
    {
        public string NameMenu { get; private set; }
        private string NameTableDataBase { get; set; }
        public EnumView Regime { get; set; }
        public TreeView TreeViewFamiliar { get; set; } //treeView для familiars

        private int FamiliarId { get; set; }

        public ListItemView(EnumView regime)
        {
            InitializeComponent();
            Regime = regime;

            switch (Regime)
            {
                case EnumView.Weight:
                    NameMenu = "Вес";
                    NameTableDataBase = "weights";
                    LoadItem();
                    break;
                case EnumView.Medication:
                    NameMenu = "Лекарства";
                    NameTableDataBase = "medications";
                    LoadItem();
                    break;
                case EnumView.Allergy:
                    NameMenu = "Аллергии";
                    NameTableDataBase = "allergies";
                    LoadItem();
                    break;
                case EnumView.Familiar:
                    NameMenu = "Знакомые";
                    NameTableDataBase = "familiars";
                    LoadFamiliar();
                    break;
                case EnumView.Height:
                    NameMenu = "Рост";
                    NameTableDataBase = "heights";
                    LoadItem();
                    break;
                case EnumView.Pressure:
                    NameMenu = "Давление";
                    NameTableDataBase = "pressures";
                    LoadItem();
                    break;
                case EnumView.Disease:
                    NameMenu = "Болезни";
                    NameTableDataBase = "diseases";
                    LoadItem();
                    break;
                case EnumView.MedicalVisit:
                    NameMenu = "Посещения";
                    NameTableDataBase = "medical_visits";
                    LoadItem();
                    break;
            } 
        }

        public ListItemView(EnumView regime, int familiarId)
        {
            InitializeComponent();

            Regime = regime;

            switch (Regime)
            {
                case EnumView.FamiliarAllergy:
                    NameTableDataBase = "allergies";
                    break;
            }

            FamiliarId = familiarId;

            SetFamiliarMode();

            LoadFamiliarItem();
        }

        private void SetFamiliarMode()
        {
            menuItemAdd.Visibility = Visibility.Hidden;
            borderEffect.Visibility = Visibility.Hidden;
        }

        private void LoadFamiliarItem()
        {
            
            using (var connection = BaseRepository.GetConnection())
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = $"SELECT * FROM {NameTableDataBase} WHERE patient_id=@id";
                command.Parameters.Add("@id", SqlDbType.Int).Value = FamiliarId;

                var dataReader = command.ExecuteReader();
                if (dataReader.HasRows)
                {
                    switch (Regime)
                    {
                        case EnumView.FamiliarAllergy:
                            while (dataReader.Read())
                            {
                                ItemView itemView = new ItemView
                                    (
                                        Regime,
                                        stackPanelListItem,
                                        Convert.ToInt32(dataReader["id"]),
                                        FamiliarId,
                                        NameTableDataBase,
                                        "id",
                                        new string[] { "Название:", "Описание:" },
                                        new string[]
                                        {
                                            Convert.ToString(dataReader["allergy_name"]),
                                            Convert.ToString(dataReader["comment"]),
                                        }
                                    );

                                stackPanelListItem.Children.Add(itemView);
                            }
                            break;
                    }
                }
            }
        }

        private void LoadItem()
        {
            using (var connection = BaseRepository.GetConnection())
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = $"SELECT * FROM {NameTableDataBase} WHERE patient_id=@id";
                command.Parameters.Add("@id", SqlDbType.Int).Value = MainPatientModel.Id;                

                var dataReader = command.ExecuteReader();
                if (dataReader.HasRows)
                {
                    switch (Regime)
                    {
                        case EnumView.Weight:
                            while (dataReader.Read())
                            {
                                WeightModel.Id = Convert.ToInt32(dataReader["id"]);
                                WeightModel.Patient_Id = MainPatientModel.Id;
                                WeightModel.Weight_Value = Convert.ToDouble(dataReader["weight_value"]);
                                WeightModel.Weight_Date = Convert.ToString(dataReader["weight_date"]);

                                ItemView itemView = new ItemView
                                    (
                                        stackPanelListItem,
                                        WeightModel.Id,
                                        WeightModel.Patient_Id,
                                        NameTableDataBase,
                                        "id",
                                        new string[] {"Вес:", "Дата измерения:"},
                                        new string[] { Convert.ToString(WeightModel.Weight_Value), Convert.ToString(WeightModel.Weight_Date) }
                                    );

                                stackPanelListItem.Children.Add(itemView);
                            }
                            break;
                        case EnumView.Medication:
                            while (dataReader.Read())
                            {
                                MedicationModel.Id = Convert.ToInt32(dataReader["id"]);
                                MedicationModel.Patient_Id = Convert.ToInt32(dataReader["patient_id"]);
                                MedicationModel.Medication_Name = Convert.ToString(dataReader["medication_name"]);
                                MedicationModel.St_Date = Convert.ToDateTime(dataReader["st_date"]);
                                MedicationModel.End_Date = Convert.ToDateTime(dataReader["end_date"]);
                                MedicationModel.Comment = Convert.ToString(dataReader["comment"]);

                                ItemView itemView = new ItemView
                                    (
                                        stackPanelListItem,
                                        MedicationModel.Id,
                                        MainPatientModel.Id,
                                        NameTableDataBase,
                                        "id",
                                        new string[] { "Название:", "Дата начала приёма:", "Дата окончания приёма:", "Описание:", "Кол-во фотографий:" },
                                        new string[] 
                                        { 
                                            MedicationModel.Medication_Name, 
                                            MedicationModel.St_Date.ToString("d"), 
                                            MedicationModel.End_Date.ToString("d"),
                                            MedicationModel.Comment,
                                            MedicationModel.CountPhoto(MedicationModel.Id).ToString(),
                                        }
                                    );

                                stackPanelListItem.Children.Add(itemView);
                            }
                            break;
                        case EnumView.Disease:
                            while (dataReader.Read())
                            {
                                DiseaseModel.Id = Convert.ToInt32(dataReader["id"]);
                                DiseaseModel.Patient_Id = Convert.ToInt32(dataReader["patient_id"]);
                                DiseaseModel.Disease_Name = Convert.ToString(dataReader["disease_name"]);
                                DiseaseModel.St_Date = Convert.ToDateTime(dataReader["st_date"]);
                                DiseaseModel.End_Date = Convert.ToDateTime(dataReader["end_date"]);
                                DiseaseModel.Comment = Convert.ToString(dataReader["comment"]);

                                ItemView itemView = new ItemView
                                    (
                                        stackPanelListItem,
                                        DiseaseModel.Id,
                                        MainPatientModel.Id,
                                        NameTableDataBase,
                                        "id",
                                        new string[] { "Название:", "Дата начала болезни:", "Дата окончания болезни:", "Описание:", "Кол-во фотографий:" },
                                        new string[]
                                        {
                                            DiseaseModel.Disease_Name,
                                            DiseaseModel.St_Date.ToString("d"),
                                            DiseaseModel.End_Date.ToString("d"),
                                            DiseaseModel.Comment,
                                            DiseaseModel.CountPhoto(DiseaseModel.Id).ToString(),
                                        }
                                    );

                                stackPanelListItem.Children.Add(itemView);
                            }
                            break;
                        case EnumView.Allergy:
                            while (dataReader.Read())
                            {
                                ItemView itemView = new ItemView
                                    (
                                        stackPanelListItem,
                                        Convert.ToInt32(dataReader["id"]),
                                        MainPatientModel.Id,
                                        NameTableDataBase,
                                        "id",
                                        new string[] { "Название:", "Описание:" },
                                        new string[] 
                                        {
                                            Convert.ToString(dataReader["allergy_name"]),
                                            Convert.ToString(dataReader["comment"]),
                                        }
                                    );

                                stackPanelListItem.Children.Add(itemView);
                            }
                            break;
                        case EnumView.Height:
                            while (dataReader.Read())
                            {
                                HeightModel.Id = Convert.ToInt32(dataReader["id"]);
                                HeightModel.Patient_Id = MainPatientModel.Id;
                                HeightModel.Height_Value = Convert.ToDouble(dataReader["height_value"]);
                                HeightModel.Height_Date = Convert.ToDateTime(dataReader["height_date"]);

                                ItemView itemView = new ItemView
                                    (
                                        stackPanelListItem,
                                        HeightModel.Id,
                                        HeightModel.Patient_Id,
                                        NameTableDataBase,
                                        "id",
                                        new string[] { "Рост:", "Дата измерения:" },
                                        new string[] { Convert.ToString(HeightModel.Height_Value), HeightModel.Height_Date.ToString("d") }
                                    );

                                stackPanelListItem.Children.Add(itemView);
                            }
                            break;
                        case EnumView.Pressure:
                            while (dataReader.Read())
                            {
                                ItemView itemView = new ItemView
                                    (
                                        stackPanelListItem,
                                        Convert.ToInt32(dataReader["id"]),
                                        MainPatientModel.Id,
                                        NameTableDataBase,
                                        "id",
                                        new string[] { "Верхнее давление:", "Нижнее давление:", "Время измерения:", "Дата измерения:", "Описание:" },
                                        new string[] 
                                        {
                                            Convert.ToString(dataReader["systolic"]),
                                            Convert.ToString(dataReader["diastolic"]),
                                            Convert.ToString(dataReader["pressure_time"]),
                                            Convert.ToDateTime(dataReader["pressure_date"]).ToString("d"),
                                            Convert.ToString(dataReader["comment"]),
                                        }
                                    );

                                stackPanelListItem.Children.Add(itemView);
                            }
                            break;
                        case EnumView.MedicalVisit:
                            while (dataReader.Read())
                            {
                                MedicalVisitModel.Id = Convert.ToInt32(dataReader["id"]);
                                MedicalVisitModel.Patient_Id = Convert.ToInt32(dataReader["patient_id"]);
                                MedicalVisitModel.Medical_Visit_Name = Convert.ToString(dataReader["medical_visit_name"]);
                                MedicalVisitModel.St_Date = Convert.ToDateTime(dataReader["st_date"]);
                                MedicalVisitModel.End_Date = Convert.ToDateTime(dataReader["end_date"]);
                                MedicalVisitModel.Cost = Convert.ToDecimal(dataReader["cost"]);
                                MedicalVisitModel.Comment = Convert.ToString(dataReader["comment"]);

                                ItemView itemView = new ItemView
                                    (
                                        stackPanelListItem,
                                        Convert.ToInt32(dataReader["id"]),
                                        MainPatientModel.Id,
                                        NameTableDataBase,
                                        "id",
                                        new string[] { "Название:", "Дата начала:", "Дата окончания:", "Стоимость (руб.):", "Описание:", "Кол-во фотографий:" },
                                        new string[]
                                        {
                                            MedicalVisitModel.Medical_Visit_Name,
                                            MedicalVisitModel.St_Date.ToString("d"),
                                            MedicalVisitModel.End_Date.ToString("d"),
                                            MedicalVisitModel.Cost.ToString(),
                                            DiseaseModel.Comment,
                                            MedicalVisitModel.CountPhoto(MedicalVisitModel.Id).ToString(),
                                        }
                                    );

                                stackPanelListItem.Children.Add(itemView);
                            }
                            break;
                    }
                }
            }
        }

        private void LoadFamiliar()
        {

            TreeView treeView = new TreeView();
            TreeViewFamiliar = treeView;

            FamiliarStatusModel.CollectFromDataBase();

            foreach(var item in FamiliarStatusModel.FamiliarStatus)
            {
                TreeViewItem newStatus = new TreeViewItem() { Header = $"{item.Value}", FontSize = 18, FontWeight = FontWeights.Bold};
                TreeViewFamiliar.Items.Add(newStatus);
            }

            using (var connection = BaseRepository.GetConnection())
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = $"SELECT * FROM {NameTableDataBase} WHERE from_patient_id=@id AND familiar_status_id=@familiar_status_id";
                command.Parameters.Add("@id", SqlDbType.Int).Value = MainPatientModel.Id;
                command.Parameters.Add("@familiar_status_id", SqlDbType.Int).Value = FamiliarStatusModel.ReturnIndex("Добавленные");

                var dataReader = command.ExecuteReader();
                if (dataReader.HasRows)
                {
                    while (dataReader.Read())
                    {
                        FamiliarItemView familiarItemView = new FamiliarItemView
                            (
                                Convert.ToInt32(dataReader["familiar_status_id"]),
                                TreeViewFamiliar,
                                MainPatientModel.Id,
                                Convert.ToInt32(dataReader["to_patient_id"]),
                                NameTableDataBase,
                                "from_patient_id",
                                "to_patient_id"
                            );

                        foreach(TreeViewItem status in TreeViewFamiliar.Items)
                        {
                            if ((string)status.Header == FamiliarStatusModel.ReturnValue(familiarItemView.Regime))
                            {
                                status.Items.Add(familiarItemView);
                            }
                        }
                    }
                }
            }
            
            using (var connection = BaseRepository.GetConnection())
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = $"SELECT * FROM {NameTableDataBase} WHERE to_patient_id=@id";
                command.Parameters.Add("@id", SqlDbType.Int).Value = MainPatientModel.Id;

                var dataReader = command.ExecuteReader();
                if (dataReader.HasRows)
                {
                    while (dataReader.Read())
                    {
                        FamiliarItemView familiarItemView = new FamiliarItemView
                            (
                                Convert.ToInt32(dataReader["familiar_status_id"]),
                                TreeViewFamiliar,
                                MainPatientModel.Id,
                                Convert.ToInt32(dataReader["from_patient_id"]),
                                NameTableDataBase,
                                "to_patient_id",
                                "from_patient_id"
                            );

                        foreach (TreeViewItem status in TreeViewFamiliar.Items)
                        {
                            if ((string)status.Header == FamiliarStatusModel.ReturnValue(familiarItemView.Regime))
                            {
                                status.Items.Add(familiarItemView);
                            }
                        }
                    }
                }
            }

            foreach (TreeViewItem status in TreeViewFamiliar.Items)
            {
                foreach (FamiliarItemView familiarItemView in status.Items)
                {
                    familiarItemView.UpDateInfo();
                }
            }

            stackPanelListItem.Children.Add(treeView);
        }

        private void menuItemAdd_Click(object sender, RoutedEventArgs e)
        {
            switch (Regime)
            {
                case EnumView.Weight:
                    WeightWindow weightWindow = new WeightWindow(0, MainPatientModel.Id, this.stackPanelListItem);
                    weightWindow.ShowDialog();
                    break;
                case EnumView.Medication:
                    MedicationWindow medicationWindow = new MedicationWindow(0, this.stackPanelListItem);
                    medicationWindow.ShowDialog();
                    break;
                case EnumView.Allergy:
                    AllergyWindow allergyWindow = new AllergyWindow(0, this.stackPanelListItem);
                    allergyWindow.ShowDialog();
                    break;
                case EnumView.Familiar:
                    FamiliarAddWindow familiarAddWindow = new FamiliarAddWindow();
                    familiarAddWindow.ShowDialog();
                    break;
                case EnumView.Height:
                    HeightWindow heightWindow = new HeightWindow(0, this.stackPanelListItem);
                    heightWindow.ShowDialog();
                    break;
                case EnumView.Pressure:
                    PressureWindow pressureWindow = new PressureWindow(0, this.stackPanelListItem);
                    pressureWindow.ShowDialog();
                    break;
                case EnumView.Disease:
                    DiseaseWindow diseaseWindow = new DiseaseWindow(0, this.stackPanelListItem);
                    diseaseWindow.ShowDialog();
                    break;
                case EnumView.MedicalVisit:
                    MedicalVisitWindow medicalVisitWindow = new MedicalVisitWindow(0, this.stackPanelListItem);
                    medicalVisitWindow.ShowDialog();
                    break;
            }
            
        }
    }
}
