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
    /// Interaction logic for homePage.xaml
    /// </summary>
    /// 

    public partial class homePage : Page
    {
        public homePage()
        {
            InitializeComponent();
        }

        private static int getDataContext(object sender)
        {
            var btn = (Button)sender;
            var data = btn.DataContext;
            return Convert.ToInt32(data);
        }
        private void setNavigationIndex(object sender)
        {
            var index = getDataContext(sender);
            MainWindow.NavigationIndex = index;
        }

        private void clientPage_Btn_Click(object sender, RoutedEventArgs e)
        {
            setNavigationIndex(sender);
            NavigationService.Navigate(new clientPage());
        }

        private void servicePage_Btn_Click(object sender, RoutedEventArgs e)
        {
            setNavigationIndex(sender);
            NavigationService.Navigate(new servicePage());
        }

        private void orderPage_Btn_Click(object sender, RoutedEventArgs e)
        {
            setNavigationIndex(sender);
            NavigationService.Navigate(new orderPage());
        }
    }
}
