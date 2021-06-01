using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace _210512_WPF_App_Autokolcsonzo
{


    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            main.Content = new KolcsonzoPage();

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void bt_autok_Click(object sender, RoutedEventArgs e)
        {
            main.Content = new AutoPage();

        }

        private void Bezar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();

        }

        private void Grid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);
            this.DragMove();
        }

        private void bt_kolcsonzes_Click(object sender, RoutedEventArgs e)
        {
            main.Content = new KolcsonzoPage();

        }

        private void bt_berlo_Click(object sender, RoutedEventArgs e)
        {
            main.Content = new BerloPage();
        }
    }
}
