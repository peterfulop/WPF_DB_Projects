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

    public partial class clientWindow : Window
    {

        private int DataId { get; set; }
        private int Index { get; set; }
        private List<TextBox> TextBoxes { get; set; }
        public static bool AddNew { get; set; }

        public clientWindow(bool addNew = false, int id = 0, string name = "", string address = "", string email = "", string phone = "", int index = 0)
        {
            InitializeComponent();
            DataId = id;
            Index = index;
            AddNew = addNew;
            TextBoxes = new List<TextBox>() { edit_client_name, edit_client_address, edit_client_email, edit_client_phone };

            this.Title = Helpers.Helpers.setWindowTitle(addNew,"client");
            save_client_btn.Content = Helpers.Helpers.setAcceptBtnText(addNew, "client");
            delete_client_btn.Visibility = addNew ? Visibility.Hidden : Visibility.Visible;

            if (addNew) clearInputs();
            else initInputs( name,  address,  email,  phone);
        }

        private void initInputs(string name, string address, string email,string phone)
        {
            edit_client_name.Text = name;
            edit_client_address.Text = address;
            edit_client_email.Text = email;
            edit_client_phone.Text = phone;
        }

        private void clearInputs()
        {
            TextBoxes.ForEach(textbox => textbox.Clear());
        }


        private void CreateData(string name, string address, string email, string phone)
        {

            using (var db = new DatabaseClientManagerEntities())
            {
                var newClient = new Client();
                {
                    newClient.Name = name;
                    newClient.Address = address;
                    newClient.Email = email;
                    newClient.Phone = phone;
                }
                db.Client.Add(newClient);
                db.SaveChanges();
            };
        }


        private void UpdateData(string name, string address, string email, string phone)
        {

            using (var db = new DatabaseClientManagerEntities())
            {
                if (!Index.Equals(-1))
                {
                    var clientData = db.Client.SingleOrDefault(adat => adat.Id == DataId);
                    if (clientData != null)
                    {
                        clientData.Name = name;
                        clientData.Address = address;
                        clientData.Email = email;
                        clientData.Phone = phone;
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
                            var client = db.Client.Where(a => a.Id.Equals(DataId));
                            db.Client.RemoveRange(client);
                            db.SaveChanges();
                        }
                        break;
                    case MessageBoxResult.No:
                        break;
                };

            }
        }



        private void save_client_btn_Click(object sender, RoutedEventArgs e)
        {
            var empty = Helpers.Helpers.areInputsEmpty(TextBoxes);
            if (empty)
            {
                MessageBox.Show("Minden mező kitöltése kötelező!", "Hiba!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            };

            if (AddNew)
            {
                CreateData(edit_client_name.Text, edit_client_address.Text, edit_client_email.Text, edit_client_phone.Text);
            }
            else
            {
                UpdateData(edit_client_name.Text, edit_client_address.Text, edit_client_email.Text, edit_client_phone.Text);

            }
            this.Close();
        }

        private void delete_client_btn_Click(object sender, RoutedEventArgs e)
        {
            DeleteData();
            this.Close();

        }
    }
}
