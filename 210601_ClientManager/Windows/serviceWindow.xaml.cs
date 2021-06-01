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
using System.Windows.Shapes;

namespace _210601_ClientManager.Windows
{
    /// <summary>
    /// Interaction logic for serviceWindow.xaml
    /// </summary>
    public partial class serviceWindow : Window
    {

        private int DataId { get; set; }
        private int Index { get; set; }
        private List<TextBox> TextBoxes { get; set; }
        private List<TextBox> NumericTextBoxes { get; set; }
        public static bool AddNew { get; set; }


        public serviceWindow(bool addNew = false, int id=0, string name = "", int duration=0, int price=0, int index=0)
        {
            InitializeComponent();

            DataId = id;
            Index = index;
            AddNew = addNew;

            TextBoxes = new List<TextBox>() { edit_service_name, edit_service_duration, edit_service_price };
            NumericTextBoxes = new List<TextBox>() { edit_service_duration, edit_service_price };

            this.Title = Helpers.Helpers.setWindowTitle(addNew,"service");
            save_service_btn.Content = Helpers.Helpers.setAcceptBtnText(addNew, "service");
            delete_service_btn.Visibility = addNew ? Visibility.Hidden : Visibility.Visible;

            if (addNew) clearInputs();
            else initInputs(name, duration, price);
        }

        private void initInputs(string name, int duration, int price)
        {
            edit_service_name.Text = name;
            edit_service_duration.Text = duration.ToString();
            edit_service_price.Text = price.ToString();
        }

        private void clearInputs()
        {
            TextBoxes.ForEach(textbox => textbox.Clear());
        }


        private void CreateData(string name, int duration, int price)
        {

            using (var db = new DatabaseClientManagerEntities())
            {
                var newService = new Service();
                {
                    newService.Name = name;
                    newService.Duration = duration;
                    newService.Price = price;
                }
                db.Service.Add(newService);
                db.SaveChanges();
            };
        }

        private void UpdateData(string name, int duration, int price)
        {

            using (var db = new DatabaseClientManagerEntities())
            {
                if (!Index.Equals(-1))
                {
                    var servieData = db.Service.SingleOrDefault(adat => adat.Id == DataId);
                    if (servieData != null)
                    {
                        servieData.Name = name;
                        servieData.Duration = duration;
                        servieData.Price = price;
                    }
                    db.SaveChanges();
                }
                else
                {
                    MessageBox.Show("Nincs tétel kijelölve!", "Hiba!", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void DeleteData()
        {
            if (Index != 0)
            {
                MessageBoxResult result = MessageBox.Show("Biztosan törölni szeretnéd a kijelölt tételt?", "Figyelem", MessageBoxButton.YesNo, MessageBoxImage.Warning);

                switch (result)
                {
                    case MessageBoxResult.Yes:
                        using (var db = new DatabaseClientManagerEntities())
                        {
                            var service = db.Service.Where(a => a.Id.Equals(DataId));
                            db.Service.RemoveRange(service);
                            db.SaveChanges();
                        }
                        break;
                    case MessageBoxResult.No:
                        break;
                };

            }
        }

        private void save_service_btn_Click(object sender, RoutedEventArgs e)
        {
            var empty = Helpers.Helpers.areInputsEmpty(TextBoxes);
            if (empty)
            {
                MessageBox.Show("Minden mező kitöltése kötelező!", "Hiba!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            };

            var isNumeric = Helpers.Helpers.areInputsNumeric(NumericTextBoxes);
            if (!isNumeric)
            {
                MessageBox.Show("Nem megfelelő formátumok!", "Hiba!",  MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            };


            if (AddNew)
            {
                CreateData(edit_service_name.Text, Convert.ToInt32(edit_service_duration.Text), Convert.ToInt32(edit_service_price.Text));
            }
            else
            {
                UpdateData(edit_service_name.Text, Convert.ToInt32(edit_service_duration.Text), Convert.ToInt32(edit_service_price.Text));
            }

            this.Close();
        }

        private void delete_service_btn_Click(object sender, RoutedEventArgs e)
        {
            DeleteData();
            this.Close();
        }
    }
}
