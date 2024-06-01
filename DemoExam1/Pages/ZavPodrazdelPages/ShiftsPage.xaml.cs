using DemoExam1.DB;
using DemoExam1.Windows;
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

namespace DemoExam1.Pages.ZavPodrazdelPages
{
    /// <summary>
    /// Логика взаимодействия для ShiftsPage.xaml
    /// </summary>
    public partial class ShiftsPage : Page
    {
        public Page CurrentPage { get; set; }
        public List<Shift> Shifts { get; set; }
        public ShiftsPage()
        {
            InitializeComponent();
            DataContext = this;
            Shifts = DB.ConferenceContext.Instance().Shifts.ToList();
        }
    }
}
