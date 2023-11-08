using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Common;
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
using SchoolKudasshova320.Pages;

namespace SchoolKudasshova320.Pages
{
    /// <summary>
    /// Логика взаимодействия для AddChairPage.xaml
    /// </summary>
    public partial class AddChairPage : Page
    {
        public static List<Discipline> disciplines { get; set; }
    
        public static List<Chair> chairs { get; set; }

        public static Discipline disc { get; set; }
        Discipline contextdisc;

        public static Worker loggedUser;

        public AddChairPage(Discipline discipline)
        {
            InitializeComponent();
            loggedUser = DBConnection.loginedUser;
            contextdisc = discipline;
            disc = discipline;
            DiscCB.ItemsSource = DBConnection.practise320_KudashovaAnnaEntities.Discipline.ToList();
           
            disciplines = new List <Discipline> (DBConnection.practise320_KudashovaAnnaEntities.Discipline.ToList());
            this.DataContext = this;
            //FacultyCB.ItemsSource = DbConnection.practise320_KudashovaAnnaEntities.Faculty.ToList();
        }

        private void SaveChageBtn_Click(object sender, RoutedEventArgs e)
        {
            var error = string.Empty;
            var validationContext = new ValidationContext(contextdisc);
            var results = new List<System.ComponentModel.DataAnnotations.ValidationResult>();

            if (Validator.TryValidateObject(contextdisc, validationContext, results, true))
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

            if (contextdisc.ID == 0)
                DBConnection.practise320_KudashovaAnnaEntities.Discipline.Add(contextdisc);
            DBConnection.practise320_KudashovaAnnaEntities.SaveChanges();
            NavigationService.GoBack();
        }

        private void BackBTN_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}

