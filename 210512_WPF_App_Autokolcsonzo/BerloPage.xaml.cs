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

namespace _210512_WPF_App_Autokolcsonzo
{
    /// <summary>
    /// Interaction logic for BerloPage.xaml
    /// </summary>
    public partial class BerloPage : Page
    {
        public BerloPage()
        {
            InitializeComponent();
        }
        private void initialize()
        {
            using (var db = new AutoNyilvantartasDBEntities())
            {
                var berlo = db.Berlo;
                dg_berlo.ItemsSource = berlo.ToList();
            }
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            initialize();
        }

        private void dg_berlo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void btn_hozzaad_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btn_torol_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btn_frissit_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
