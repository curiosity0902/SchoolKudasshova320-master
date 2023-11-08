using System;
using System.Collections.Generic;
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
    /// Логика взаимодействия для WorkersPage.xaml
    /// </summary>
    public partial class WorkersPage : Page
    {
        public static List<Worker> workers { get; set; }
        public static List<Chair> chairs { get; set; }

        public static Worker worker = new Worker();
        public WorkersPage()
        {
            InitializeComponent();
            chairs = new List<Chair>(DBConnection.practise320_KudashovaAnnaEntities.Chair.ToList());
            workers = new List<Worker>(DBConnection.practise320_KudashovaAnnaEntities.Worker.ToList());

            this.DataContext = this;

            SurnameTBL.Text = DBConnection.loginedUser.Fullname;
            //worker1 = worker;
            //InitializeComponent();
            //if (worker is null)
            //{

            //}
            //else
            //{
            //    TableNumTB.Text = worker.ID.ToString();
            //    SurnameTB.Text = worker.Fullname;
            //    SalaryTB.Text = worker.Salary.ToString();
            //    ChairCB.SelectedItem = worker.ID_Chair;
            //    PositionCB.SelectedItem = worker.Position;
            //}
        }
   

        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            var dcvd = /*TableNumTB.Text + " " +*/ SurnameTB.Text + " " + SalaryTB.Text + " " + PositionCB.Text + " " + LoginTB.Text + " " + PasswordTB.Text;
            if (MessageBox.Show(dcvd, "Проверьте корректность введенных данных", MessageBoxButton.YesNoCancel) == MessageBoxResult.Yes)
            {


                var t = ChairCB.SelectedItem as Chair;
                worker.Fullname = SurnameTB.Text.Trim();
                worker.Login = LoginTB.Text.Trim();
                worker.Password = PasswordTB.Text.Trim();
                worker.ID_Chair = t.ID;
                worker.Salary = decimal.Parse(SalaryTB.Text);

                worker.Position = PositionCB.Text.Trim();



                DBConnection.practise320_KudashovaAnnaEntities.Worker.Add(worker);
                DBConnection.practise320_KudashovaAnnaEntities.SaveChanges();

                NavigationService.Navigate(new EngenerPage());
            }

        }

        private void AddImageBtn_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog()
            {
                Filter = "*.png|*.png|*.jpeg|*.jpeg|*.jpg|*.jpg"
            };

            if (openFileDialog.ShowDialog().GetValueOrDefault())
            {
                worker.Photo = File.ReadAllBytes(openFileDialog.FileName);
                TestImage.Source = new BitmapImage(new Uri(openFileDialog.FileName));
            }
        }

        private void dELBTN_Click(object sender, RoutedEventArgs e)
        {
            //if (StudentExamLv.SelectedItem is Exam exam)
            //{
            //    DBConnection.practise320_KudashovaAnnaEntities.Exam.Remove(exam);
            //    DBConnection.practise320_KudashovaAnnaEntities.SaveChanges();
            //    InitializeDataInPage();
            //} }
        }

        private void BackBTN_Click_1(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}