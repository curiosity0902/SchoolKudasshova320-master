using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
using SchoolKudasshova320.DB;

namespace SchoolKudasshova320.Pages
{
    /// <summary>
    /// Логика взаимодействия для AddDepartmentPage.xaml
    /// </summary>
    public partial class AddDepartmentPage : Page
    {
        public static List<Chair> chairs { get; set; }
        public static List<Discipline> disciplines { get; set; }

        Chair contextChair;
        public AddDepartmentPage(Chair chair)
        {
            InitializeComponent();
            contextChair = chair;
            InitializeDataInPage();
            this.DataContext = this;

        }

        private void InitializeDataInPage()
        {
            chairs = new List<Chair>(DBConnection.practise320_KudashovaAnnaEntities.Chair.
       Where(i => i.Name == contextChair.Name).ToList());
            AddDepartmentLv.ItemsSource = DBConnection.practise320_KudashovaAnnaEntities.Discipline.Where(x => x.ID_Chair == contextChair.ID).ToList();
            TBNameChair.Text = Convert.ToString(contextChair.Name);
            disciplines = new List<Discipline>(DBConnection.practise320_KudashovaAnnaEntities.Discipline.ToList());

        }
        private void Refresh() //Обновление листа
        {
            AddDepartmentLv.ItemsSource = DBConnection.practise320_KudashovaAnnaEntities.Discipline.
                            Where(i => i.ID_Chair == contextChair.ID).ToList();
        }
        private void ExitBT_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void ChairLv_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
          
        }

        private void DeleteBTN_Click(object sender, RoutedEventArgs e)
        {
            if (AddDepartmentLv.SelectedItem is Discipline discipline)
            {
                DBConnection.practise320_KudashovaAnnaEntities.Discipline.Remove(discipline);
                DBConnection.practise320_KudashovaAnnaEntities.SaveChanges();
                InitializeDataInPage();
                Refresh();
            }
        }


        
        private void AddDisp_Click(object sender, RoutedEventArgs e)
        {
            Discipline disc = new Discipline();

            if (CBDisp.SelectedItem is Discipline discipline)
            {
                var d = CBDisp.SelectedItem as Discipline;
                disc.Name = d.Name;
                disc.Volume = Convert.ToInt32(TBVolume.Text);
                disc.ID_Chair = contextChair.ID;
                
                DBConnection.practise320_KudashovaAnnaEntities.Discipline.Add(disc);
                DBConnection.practise320_KudashovaAnnaEntities.SaveChanges();
                Refresh();
                InitializeDataInPage();
                //var error = string.Empty;
                //var validationContext = new ValidationContext(contextChair);
                //var results = new List<System.ComponentModel.DataAnnotations.ValidationResult>();
                //var disciplina = DBConnection.practise320_KudashovaAnnaEntities.Discipline.FirstOrDefault(x => x.ID_Chair == contextChair.ID);
                ////if (disciplina != null && disciplina != contextChair)
                ////    error += "Такое уже есть";
                //if (Validator.TryValidateObject(contextChair, validationContext, results, true))
                //{
                //    foreach (var result in results)
                //    {
                //        error += $"{result.ErrorMessage}\n";
                //    }
                //}
                //if (!string.IsNullOrWhiteSpace(error))
                //{
                //    MessageBox.Show(error);
                //    return;
                //}
                //if (discipline == null)
                //    DBConnection.practise320_KudashovaAnnaEntities.Discipline.Add(discipline);
                //DBConnection.practise320_KudashovaAnnaEntities.SaveChanges();

            }
        }

        

        private void ChangeBTN_Click(object sender, RoutedEventArgs e)
        {
            if (AddDepartmentLv.SelectedItem is Discipline discipline)
            {
                AddDepartmentLv.SelectedItem = null;
                NavigationService.Navigate(new AddChairPage(discipline));
            }
        }
    }
}
