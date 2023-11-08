using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
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
using Microsoft.Win32;
using SchoolKudasshova320.DB;

namespace SchoolKudasshova320.Pages
{
    /// <summary>
    /// Логика взаимодействия для WriteWorkerPage.xaml
    /// </summary>
    public partial class WriteWorkerPage : Page
    {
        public static List<Worker> workers = new List<Worker>();
        public static List<Chair> chairs { get; set; }
        public static Worker work { get; set; }
        Worker contextWorker;

        public WriteWorkerPage(Worker worker)
        {
            InitializeComponent();
            if (worker.ID == worker.ID_Chief)
                MyCheckBox.IsChecked = true;
            else
                MyCheckBox.IsChecked = false;

            contextWorker = worker;
            work = worker;
            InitializeDataInPage();
            this.DataContext = this;
        }

        private void InitializeDataInPage()
        {
           
            ChairCB.ItemsSource = DBConnection.practise320_KudashovaAnnaEntities.Chair.ToList();
            //TBDisp.Text = DBConnection.practise320_KudashovaAnnaEntities.Discipline.oString();
            workers = new List<Worker>(DBConnection.practise320_KudashovaAnnaEntities.Worker).ToList();
            chairs = new List<Chair>(DBConnection.practise320_KudashovaAnnaEntities.Chair.ToList());
            //exams = new List<Exam>(DBConnection.practise320_KudashovaAnnaEntities.Exam.Where(x => x.Date == contextExam.Date && x.Discipline.ID == contextExam.ID_Discipline).ToList());
        }


        private void SaveChageBtn_Click(object sender, RoutedEventArgs e)
        {
                var error = string.Empty;
                var validationContext = new ValidationContext(contextWorker);
                var results = new List<System.ComponentModel.DataAnnotations.ValidationResult>();

                if (Validator.TryValidateObject(contextWorker, validationContext, results, true))
                {
                    foreach (var result in results)
                    {
                        error += $"{result.ErrorMessage}\n";
                    }
                }
                if (!string.IsNullOrWhiteSpace(error))
                {
                    MessageBox.Show(error);
                    return;
                }
   
                if (contextWorker.ID == 0)
                DBConnection.practise320_KudashovaAnnaEntities.Worker.Add(contextWorker);
                DBConnection.practise320_KudashovaAnnaEntities.SaveChanges();
                NavigationService.GoBack();
            }

        private void AddImageChageBtn_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog()
            {
                Filter = "*.png|*.png|*.jpeg|*.jpeg|*.jpg|*.jpg"
            };

            if (openFileDialog.ShowDialog().GetValueOrDefault())
            {
                work.Photo = File.ReadAllBytes(openFileDialog.FileName);
                TestImage.Source = new BitmapImage(new Uri(openFileDialog.FileName));
            }
        }

        private void BackBTN_Click_1(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void dELBTN_Click(object sender, RoutedEventArgs e)
        {
            if (work != null)

            {
                DBConnection.practise320_KudashovaAnnaEntities.Worker.Remove(work);
                //DBCORemove(work);
                DBConnection.practise320_KudashovaAnnaEntities.SaveChanges();
                
                InitializeDataInPage();
                NavigationService.Navigate(new EngenerPage());
            }
        }
    }
}
