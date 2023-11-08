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
    /// Логика взаимодействия для DisaplineList.xaml
    /// </summary>
    public partial class DisaplineList : Page
    {
        public static List<Discipline> disciplines { get; set; }
        public DisaplineList()
        {
            InitializeComponent();
            disciplines = new List<Discipline>(DBConnection.practise320_KudashovaAnnaEntities.Discipline.ToList());
            this.DataContext = this;
            StudentLv.ItemsSource = new List<Discipline>(DBConnection.practise320_KudashovaAnnaEntities.Discipline.ToList());
        }

        private void ExitBT_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
