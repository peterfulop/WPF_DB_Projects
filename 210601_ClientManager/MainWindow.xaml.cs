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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public static int NavigationIndex { get; set; }


        public MainWindow()
        {
            InitializeComponent();
            NavigationIndex = 2;
        }

        private void window_header_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);
            this.DragMove();
        }

        private void window_close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            main_frame.Content = new homePage();
        }

        private void Window_Activated(object sender, EventArgs e)
        {
            Console.WriteLine("Window_Activated! NavigationIndex: " + NavigationIndex);
            // Reload the Pages, to update the datagrids!
            switch (NavigationIndex)
            {
                case 0:
                    main_frame.Content = new clientPage();
                    break;
                case 1:
                    main_frame.Content = new servicePage();
                    break;
                case 2:
                    main_frame.Content = new orderPage();
                    break;
                default:
                    break;
            }
        }
    }
}
