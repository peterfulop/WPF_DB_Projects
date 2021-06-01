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
    /// Interaction logic for orderPage.xaml
    /// </summary>
    public partial class orderPage : Page
    {
        public int SelectedId { get; set; }

        public orderPage()
        {
            InitializeComponent();
        }

 
        private void btn_backHome_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new homePage());

        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            using (var db = new DatabaseClientManagerEntities())
            {
                dg_order.ItemsSource = db.Order.Include("Client").Include("Service").ToList();
            }
        }

        private void dg_order_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void btn_editRow_Click(object sender, RoutedEventArgs e)
        {



            using (var db = new DatabaseClientManagerEntities())
            {

                if (dg_order.SelectedIndex >= 0)
                {
                    Order data = (Order)dg_order.SelectedItem;
                    SelectedId = data.Id;
                    var ow = new orderWindow(false, data.Id, Convert.ToInt32(data.FK_Client_Name), Convert.ToInt32(data.FK_Service_Name), SelectedId);
                    ow.ShowDialog();
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
            var ow = new orderWindow(true);
            ow.ShowDialog();
        }
    }
}
