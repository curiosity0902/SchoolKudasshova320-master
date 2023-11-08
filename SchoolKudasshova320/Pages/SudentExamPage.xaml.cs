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
    /// Логика взаимодействия для SudentExamPage.xaml
    /// </summary>
    public partial class SudentExamPage : Page
    {
        public static List <Student> students { get; set; }
        public static List<Discipline> disciplines { get; set; }
        public static List<Exam> exams { get; set; }
        Exam contextExam;
        public SudentExamPage(Exam exam)
        {
            InitializeComponent();
            contextExam = exam;
        
            TB_DateE.Text = Convert.ToString(contextExam.Date);
            //TB_NameE.Text = Convert.ToString(contextExam.Discipline.Name);
            InitializeDataInPage();
            this.DataContext = this;
            //StudentExamLv.ItemsSource = new List<Student>(DBConnection.practise320_KudashovaAnnaEntities.Student.ToList());
        }
        private void InitializeDataInPage()
        {
            CBStudents.ItemsSource = DBConnection.practise320_KudashovaAnnaEntities.Student.ToList();
            
            //TBDisp.Text = DBConnection.practise320_KudashovaAnnaEntities.Discipline;
            students = new List<Student>(DBConnection.practise320_KudashovaAnnaEntities.Student).ToList();
            disciplines = new List<Discipline>(DBConnection.practise320_KudashovaAnnaEntities.Discipline.ToList());
            exams = new List<Exam>(DBConnection.practise320_KudashovaAnnaEntities.Exam.Where(x => x.Date == contextExam.Date && x.Discipline.ID == contextExam.ID_Discipline).ToList());
        }
        private void StudentExamLv_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void ExitBT_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
        private void Refresh() //Обновление листа
        {
            StudentExamLv.ItemsSource = DBConnection.practise320_KudashovaAnnaEntities.Exam.
                Where(i => i.Date == contextExam.Date && i.Discipline.ID == contextExam.ID_Discipline).ToList();
        }
        private void BAddStudent_Click(object sender, RoutedEventArgs e)
        {
             string mark = "2 ";
            var TBmark = CBMark.SelectedValue as TextBlock;
            if(TBmark != null)
                 mark = TBmark.Text;
            if (CBStudents.SelectedItem is Student student)
            {
                var exam = contextExam;
                exam.Student = student;
                exam.Mark = int.Parse(mark);
                var studentInList = exams.FirstOrDefault(x => x.ID_Student == student.ID);
                if(studentInList != null)
                {
                    MessageBox.Show("Такой студент уже есть в экзамене");
                    return;
                }
                DBConnection.practise320_KudashovaAnnaEntities.Exam.Add(exam);
                DBConnection.practise320_KudashovaAnnaEntities.SaveChanges();
                Refresh();
                InitializeDataInPage();

            }
        }

        private void DeleteBT_Click(object sender, RoutedEventArgs e)
        {
            if (StudentExamLv.SelectedItem is Exam exam)
                {
                DBConnection.practise320_KudashovaAnnaEntities.Exam.Remove(exam);
                DBConnection.practise320_KudashovaAnnaEntities.SaveChanges();
                InitializeDataInPage();
                Refresh();
            }
        }
    }
}
