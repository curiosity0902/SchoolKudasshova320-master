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
using SchoolKudasshova320.DB;

namespace SchoolKudasshova320.Pages
{
    /// <summary>
    /// Логика взаимодействия для EngenerPage.xaml
    /// </summary>
    public partial class EngenerPage : Page
    {
        public static List<Worker> workers { get; set; }
        public static List<Chair> chairs { get; set; }
        public EngenerPage()
        {
            InitializeComponent();
            chairs = new List<Chair>(DBConnection.practise320_KudashovaAnnaEntities.Chair.ToList());
            workers = new List<Worker>(DBConnection.practise320_KudashovaAnnaEntities.Worker.ToList());
            EngenerLv.ItemsSource = new List<Worker>(DBConnection.practise320_KudashovaAnnaEntities.Worker.ToList());
            Refresh();
            this.DataContext = this;
          
        }

        private void InitializeDataInPage()
        {
            chairs = new List<Chair>(DBConnection.practise320_KudashovaAnnaEntities.Chair.ToList());
            workers = new List<Worker>(DBConnection.practise320_KudashovaAnnaEntities.Worker.ToList());
            EngenerLv.ItemsSource = new List<Worker>(DBConnection.practise320_KudashovaAnnaEntities.Worker.ToList());
        }
        private void Refresh() //Обновление листа
        {
            EngenerLv.ItemsSource = DBConnection.practise320_KudashovaAnnaEntities.Worker.ToList();
        }
        private void ExitBT_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AutorizationPage());
        }

        private void EngenerLv_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (EngenerLv.SelectedItem is Worker worker)
            {
                EngenerLv.SelectedItem = null;
                NavigationService.Navigate(new WriteWorkerPage(worker));
            }
        }


        private void ADDBT_Click_1(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new WorkersPage());
            Refresh();

        }


        //private void DeleteBT_Click(object sender, RoutedEventArgs e)
        //{
        //        if (EngenerLv.SelectedItem != null)
        //        {
        //        EngenerLv.Items.Remove(EngenerLv.SelectedItem);
        //        DBConnection.practise320_KudashovaAnnaEntities.SaveChanges();
        //        }
        //}


    }
}

