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
    /// Interaction logic for orderWindow.xaml
    /// </summary>
    public partial class orderWindow : Window
    {
        private int DataId { get; set; }
        private int Index { get; set; }
        public static bool AddNew { get; set; }

        private List<TextBox> TextBoxes { get; set; }
        private List<ComboBox> ComboBoxes { get; set; }
        private List<DatePicker> DatePickers { get; set; }

        public orderWindow(bool addNew = false, int id=0, int clientIndex=0, int serviceIndex=0, int index=0 )
        {
            InitializeComponent();
            DataId = id;
            Index = index;
            AddNew = addNew;

            ComboBoxes = new List<ComboBox>() { edit_order_clientName, edit_order_serviceName };
            DatePickers = new List<DatePicker>() { edit_order_eventDate };

            this.Title = Helpers.Helpers.setWindowTitle(addNew,"order");
            save_order_btn.Content = Helpers.Helpers.setAcceptBtnText(addNew,"order");
            delete_order_btn.Visibility = addNew ? Visibility.Hidden : Visibility.Visible;

            if (addNew) clearInputs();
            else initInputs(clientIndex, serviceIndex);
        }




        private void initInputs(int clientIndex, int serviceIndex)
        {
            edit_order_clientName.SelectedIndex = clientIndex;
            edit_order_serviceName.SelectedIndex = serviceIndex;

        }

        private void clearInputs()
        {
            ComboBoxes.ForEach(comboBox => comboBox.SelectedIndex = 0);
            DatePickers.ForEach(dateTime => dateTime.SelectedDate = DateTime.Now);
        }


        private void CreateData(int clientId, int serviceId, DateTime date)
        {

            using (var db = new DatabaseClientManagerEntities())
            {
                var newOrder = new Order();
                {
                    newOrder.FK_Client_Name = clientId;
                    newOrder.FK_Service_Name = serviceId;
                    newOrder.Event_Date = date;
                    newOrder.Order_Date = DateTime.Now;
                }
                db.Order.Add(newOrder);
                db.SaveChanges();
            };
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
                            var order = db.Order.Where(a => a.Id.Equals(DataId));
                            db.Order.RemoveRange(order);
                            db.SaveChanges();
                        }
                        break;
                    case MessageBoxResult.No:
                        break;
                };

            }
        }



        private void save_order_btn_Click(object sender, RoutedEventArgs e)
        {


            if (AddNew)
            {
                var date = edit_order_eventDate.SelectedDate.HasValue ? edit_order_eventDate.SelectedDate.Value : DateTime.Now;
                CreateData(Convert.ToInt32(edit_order_clientName.SelectedValue), Convert.ToInt32(edit_order_serviceName.SelectedValue), date);
            }
            else
            {
                Console.WriteLine("Frissítés metódusa....");
            }

            this.Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            using (var db = new DatabaseClientManagerEntities())
            {
                var client = db.Client;
                var service = db.Service;

                edit_order_clientName.ItemsSource = client.ToList();
                edit_order_serviceName.ItemsSource = service.ToList();
            }
        }

        private void delete_order_btn_Click(object sender, RoutedEventArgs e)
        {
            DeleteData();
            this.Close();
        }
    }
}
