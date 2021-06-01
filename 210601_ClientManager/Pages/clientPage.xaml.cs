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
    /// Interaction logic for clientPage.xaml
    /// </summary>
    public partial class clientPage : Page
    {

        public int SelectedId { get; set; }

        public clientPage()
        {
            InitializeComponent();
        }


        private void btn_backHome_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new homePage());

        }

        public  void loadGrid()
        {
            using (var db = new DatabaseClientManagerEntities())
            {
                dg_client.ItemsSource = db.Client.ToList();
            }
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("client Page loaded!!");
            using (var db = new DatabaseClientManagerEntities())
            {
                dg_client.ItemsSource = db.Client.ToList();
            }
        }

        private void btn_editRow_Click(object sender, RoutedEventArgs e)
        {

            using (var db = new DatabaseClientManagerEntities())
            {

                if (dg_client.SelectedIndex >= 0)
                {
                    Client data = (Client)dg_client.SelectedItem;
                    SelectedId = data.Id;
                    var cw = new clientWindow(false, data.Id, data.Name, data.Address, data.Email, data.Phone, dg_client.SelectedIndex);
                    cw.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Nincs tétel kijelölve!", "Hiba!", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }

        }


        public  void SearchByName( string query )
        {
            using (var db = new DatabaseClientManagerEntities())
            {
                var search = db.Client;
                var res = db.Client.Where(element => element.Name.Contains(query));
                dg_client.ItemsSource = res.ToList();
            }
        }
        public void SearchByEmail(string query)
        {
            using (var db = new DatabaseClientManagerEntities())
            {
                var search = db.Client;
                var res = db.Client.Where(element => element.Email.Contains(query));
                dg_client.ItemsSource = res.ToList();
            }
        }

        private void btn_search_Click(object sender, RoutedEventArgs e)
        {
            var query = search_box.Text;
            if (query.Length > 0)
            {
                if (search_combo.SelectedIndex == 0) SearchByName(query);
                else SearchByEmail(query);
            }
            else
            {
                loadGrid();
            }
        }

        private void dg_client_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void btn_add_Click(object sender, RoutedEventArgs e)
        {
            var cw = new clientWindow(true);
            cw.ShowDialog();
        }
    }
}
