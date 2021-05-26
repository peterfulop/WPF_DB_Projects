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
    /// Interaction logic for AutoPage.xaml
    /// </summary>
    public partial class AutoPage : Page
    {
        public AutoPage()
        {
            InitializeComponent();
            AddInputs = new List<TextBox> { BoxMarka, BoxTipus, BoxRendszam, BoxSzin, BoxEvjarat };
            EditInputs = new List<TextBox> { BoxMarkaFriss, BoxTipusFriss, BoxRendszamFriss, BoxSzinFriss, BoxEvjaratFriss };
        }
        public int SelectedId { get; set; }

        private List<TextBox> AddInputs { get; set; }
        private List<TextBox> EditInputs { get; set; }
        private List<TextBox> RequiredInputs { get; set; }


        private void clearInputs(List<TextBox> Inputs)
        {
            Inputs.ForEach(input => input.Clear());
        }


        public static bool isInputsEmpty(List<TextBox> Inputs)
        {
            var leave = false;
            Inputs.ForEach(input => {
                if (input.Text.Length == 0)
                {
                    leave = true;
                };
            });

            return leave;
        }

        public static bool isInputNumber(string textToInt)
        {

            if (int.TryParse(textToInt, out int number))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void CreateData(string marka, string tipus, string rendszam, string szin, string evjarat)
        {

            var empty = isInputsEmpty(AddInputs);
            if (empty)
            {
                MessageBox.Show("Minden mező kitöltése kötelező!");
                return;
            };

            var isNumber = isInputNumber(evjarat);
            if (!isNumber)
            {
                MessageBox.Show("Az évszám nem szám formátum!");
                return;
            }

            using (var db = new AutoNyilvantartasDBEntities())
            {
                var newAuto = new Auto();
                {
                    newAuto.Marka = marka;
                    newAuto.Tipus = tipus;
                    newAuto.Rendszam = rendszam;
                    newAuto.Szin = szin;
                    newAuto.Evjarat = Int32.Parse(evjarat);
                }
                db.Auto.Add(newAuto);
                db.SaveChanges();
                dg_auto.ItemsSource = db.Auto.ToList();

                clearInputs(AddInputs);
            };
        }

        private void UpdateData(string marka, string tipus, string rendszam,string szin, string evjarat)
        {

            var empty = isInputsEmpty(EditInputs);
            if (empty)
            {
                MessageBox.Show("Minden mező kitöltése kötelező!");
                return;
            };

            var isNumber = isInputNumber(evjarat);
            if (!isNumber)
            {
                MessageBox.Show("Az évszám nem szám formátum!");
                return;
            };


            using (var db = new AutoNyilvantartasDBEntities())
            {
                if (!dg_auto.SelectedIndex.Equals(-1))
                {
                    var autoadat = db.Auto.SingleOrDefault(adat => adat.Id == SelectedId);
                    if (autoadat != null)
                    {
                        autoadat.Marka = marka;
                        autoadat.Tipus = tipus;
                        autoadat.Rendszam = rendszam;
                        autoadat.Szin = szin;
                        autoadat.Evjarat = int.Parse(evjarat);
                    }

                    db.SaveChanges();
                    dg_auto.ItemsSource = db.Auto.ToList();
                }
                else
                {
                    MessageBox.Show("Nincs tétel kijelölve!");
                }

            }

            clearInputs(EditInputs);
        }

        private void DeleteData()
        {
            if (SelectedId != 0)
            {
                MessageBoxResult result = MessageBox.Show("Biztosan törölni szeretnéd a kijelölt tételt?", "Figyelem", MessageBoxButton.YesNo, MessageBoxImage.Warning);

                switch (result)
                {
                    case MessageBoxResult.Yes:
                        using (var db = new AutoNyilvantartasDBEntities())
                        {
                            var auto = db.Auto.Where(a => a.Id.Equals(SelectedId));
                            db.Auto.RemoveRange(auto);
                            db.SaveChanges();
                            dg_auto.ItemsSource = db.Auto.ToList();
                            clearInputs(EditInputs);
                        }
                        break;
                    case MessageBoxResult.No:
                        break;
                };

            }
        }

        private void LoadData()
        {
            using (var db = new AutoNyilvantartasDBEntities())
            {
                var auto = db.Auto;
                dg_auto.ItemsSource = auto.ToList();

            };
        }
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            LoadData();
        }

        private void btn_hozzaad_Click(object sender, RoutedEventArgs e)
        {
            CreateData(BoxMarka.Text,BoxTipus.Text,BoxRendszam.Text,BoxSzin.Text, BoxEvjarat.Text);
        }

        private void dg_auto_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var index = dg_auto.SelectedIndex;

            using (var db = new AutoNyilvantartasDBEntities())
            {
                var auto = db.Auto;

                if (dg_auto.SelectedIndex >= 0)
                {
                    Auto adat = (Auto)dg_auto.SelectedItem;
                    BoxRendszamFriss.Text = adat.Rendszam;
                    BoxMarkaFriss.Text = adat.Marka;
                    BoxTipusFriss.Text = adat.Tipus;
                    BoxSzinFriss.Text = adat.Szin;
                    BoxEvjaratFriss.Text = adat.Evjarat.ToString();

                    SelectedId = adat.Id;
                }
            }
        }

        private void btn_torol_Click(object sender, RoutedEventArgs e)
        {
            DeleteData();
        }

        private void btn_frissit_Click(object sender, RoutedEventArgs e)
        {

            UpdateData(BoxMarkaFriss.Text, BoxTipusFriss.Text, BoxRendszamFriss.Text, BoxSzinFriss.Text, BoxEvjaratFriss.Text);

        }
    }
}
