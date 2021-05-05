using System;
using System.Collections.Generic;
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

namespace _210505_WPF_App_Tanarok
{

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<TextBoxInput> TextBoxInputs { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            TextBoxInputs = SetTexBoxes.set_textBoxInputs(inputFieldsPanel, "req");
            SetTexBoxes.set_maximum_length_of_inputs(TextBoxInputs, "Tanarok");
        }


        private bool isIntputEmpty()
        {
            var empty = false;
            TextBoxInputs.ForEach(element =>
            {
                if (element.UIElement.Text.Length == 0)
                {
                    empty = true;
                    return;
                }
            });
            return empty;
        }

        private void AddData(string name, string city, string prof, bool male)
        {

            var controll = isIntputEmpty();
            if (controll) { MessageBox.Show("Minden mező kitöltése kötelező!"); return; }

            using (var db = new TanarokDBEntities())
            {
                var Tanar = new Tanarok();
                {
                    Tanar.name = name;
                    Tanar.city = city;
                    Tanar.prof = prof;
                    Tanar.male = male;
                };

                db.Tanarok.Add(Tanar);
                db.SaveChanges();
            }

            LoadData();
            clearInputs();
        }

        private void LoadData()
        {
            using (var db = new TanarokDBEntities())
            {
                dg_tanarok.ItemsSource = db.Tanarok.ToList();
            };
        }

        private void clearInputs()
        {
            TextBoxInputs.ForEach(element => element.UIElement.Clear());
        }


        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LoadData();

        }

        private void addDataBtn_Click(object sender, RoutedEventArgs e)
        {
            AddData(nameInputBox.Text, cityInputBox.Text, profInputBox.Text, (bool)genderRadioMale.IsChecked);

        }
    }
}
