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
    /// Interaction logic for KolcsonzoPage.xaml
    /// </summary>
    public partial class KolcsonzoPage : Page
    {
        private List<TextBox> AddInputText { get; set; }
        private List<ComboBox> AddInputCombo { get; set; }

        public KolcsonzoPage()
        {
            InitializeComponent();
            AddInputText = new List<TextBox>() { BoxKolcsonzoNev, BoxKolcsonzoCim };
            AddInputCombo = new List<ComboBox>() { ComboKolcsonzoAuto, ComboKolcsonzoBerlo };
        }

        private void initialize()
        {
            using (var db = new AutoNyilvantartasDBEntities())
            {
                var auto = db.Auto;
                var berlo = db.Berlo;
                var kolcsonzo = db.Kolcsonzo;

                ComboKolcsonzoAuto.ItemsSource = auto.ToList();
                ComboKolcsonzoBerlo.ItemsSource = berlo.ToList();
                dg_kolcsonzo.ItemsSource = kolcsonzo.ToList();
            }
        }

        private void addNewKolcsonzo()
        {
            using (var db = new AutoNyilvantartasDBEntities())
            {
                var kolcsonzo = new Kolcsonzo();
                {
                    kolcsonzo.Auto_Id = (int)ComboKolcsonzoAuto.SelectedValue;
                    kolcsonzo.Berlo_Id = (int)ComboKolcsonzoBerlo.SelectedValue;
                    kolcsonzo.Nev = BoxKolcsonzoNev.Text;
                    kolcsonzo.Cim = BoxKolcsonzoCim.Text;
                }

               // MessageBox.Show(kolcsonzo.Berlo_Id.ToString());

                db.Kolcsonzo.Add(kolcsonzo);
                db.SaveChanges();
                dg_kolcsonzo.ItemsSource = db.Kolcsonzo.Include("Auto").Include("Berlo").ToList();
            }

        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            initialize();
        }

        private void btn_frissit_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btn_torol_Click(object sender, RoutedEventArgs e)
        {

        }


        private void btn_hozzaad_Click(object sender, RoutedEventArgs e)
        {
            var empty = Global.isInputsEmpty(AddInputText);
            if (empty)
            {
                MessageBox.Show("Minden mező kitöltése kötelező!");
                return;
            };

            //addNewKolcsonzo();
            //initialize();

        }

        private void dg_kolcsonzo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void btn_auto_new_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AutoPage());
            //NavigationService.Navigate(new Uri("AutoPage.xaml",UriKind.Relative));
        }

        private void btn_berlo_new_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new BerloPage());
        }
    }
}
