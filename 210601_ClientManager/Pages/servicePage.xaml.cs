using _210601_ClientManager.Windows;
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

namespace _210601_ClientManager
{
    /// <summary>
    /// Interaction logic for servicePage.xaml
    /// </summary>
    public partial class servicePage : Page
    {

        public int SelectedId { get; set; }
        public servicePage()
        {
            InitializeComponent();
        }

        private void dg_service_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void btn_backHome_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new homePage());

        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            using (var db = new DatabaseClientManagerEntities())
            {
                dg_service.ItemsSource = db.Service.ToList();
            }
        }

        private void btn_editRow_Click(object sender, RoutedEventArgs e)
        {
            using (var db = new DatabaseClientManagerEntities())
            {

                if (dg_service.SelectedIndex >= 0)
                {
                    Service data = (Service)dg_service.SelectedItem;
                    SelectedId = data.Id;
                    var sw = new serviceWindow(false, data.Id, data.Name, (int)data.Duration, (int)data.Price, dg_service.SelectedIndex);
                    sw.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Nincs tétel kijelölve!", "Hiba!", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
        }

        private void btn_search_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btn_add_Click(object sender, RoutedEventArgs e)
        {
            var sw = new serviceWindow(true);
            sw.ShowDialog();
        }
    }
}
