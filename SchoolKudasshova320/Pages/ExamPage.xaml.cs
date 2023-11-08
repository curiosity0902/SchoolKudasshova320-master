using SchoolKudasshova320.DB;
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

namespace SchoolKudasshova320.Pages
{
    /// <summary>
    /// Логика взаимодействия для ExamPage.xaml
    /// </summary>
    public partial class ExamPage : Page
    {
        public static List<Exam> exams { get; set; }
        public static List<Worker> workers { get; set; }
        public static List<Discipline> disciplines { get; set; }
        public static Worker loggedUser;

        public ExamPage()
        {
            InitializeComponent();
            TBUser.Text = DBConnection.loginedUser.Fullname;
            exams = DBConnection.practise320_KudashovaAnnaEntities.Exam.Where(x=>x.ID_Worker == DBConnection.loginedUser.ID).ToList();
            workers = new List<Worker>(DBConnection.practise320_KudashovaAnnaEntities.Worker.ToList());
            disciplines = new List<Discipline>(DBConnection.practise320_KudashovaAnnaEntities.Discipline.ToList());
            loggedUser = DBConnection.loginedUser;
            this.DataContext = this;
        }
        private static void Refresh()
        {

        }
        private void ExitBT_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void ExamLv_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(ExamLv.SelectedItem is Exam exam)
            {
                ExamLv.SelectedItem = null;
                NavigationService.Navigate(new SudentExamPage(exam));
            }
        }
    }
}
