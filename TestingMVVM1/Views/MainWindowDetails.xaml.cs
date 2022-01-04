using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using TestingMVVM1.ViewModels;
using TestingMVVM1.Models;
using System.Data.SqlClient;
using System.Data;

namespace TestingMVVM1.Views
{
    /// <summary>
    /// Interaction logic for MainWindowDetails.xaml
    /// </summary>
    public partial class MainWindowDetails : Window
    {

        private MainViewModel _vm;

        // how many buttons added but we need to do it by the database...
        int ListCounter = 1;


        //public string _connectionString = @"Data Source=LAPTOP-CMOUV9KC\SQLEXPRESS;Initial Catalog=Trucking;Persist Security Info=True;User ID=Tracking_Trucks;Password=AtomBomb";
        //Data Source = OWNER - PC\SQLEXPRESS;Initial Catalog = Trucking; Integrated Security = True
        public string _connectionString = @"Data Source=OWNER-PC\SQLEXPRESS;Initial Catalog=Trucking;Integrated Security=True";
        public ObservableCollection<Customer> _Customer = new ObservableCollection<Customer>();

        public MainWindowDetails()
        {

            InitializeComponent();
            Test();
        }

        // the button
        private void Test()
        {
            _vm = new MainViewModel();
            for (int i = 1; i <= ListCounter; i++)
                _vm.Numbers.Add(new(i));

            DataContext = _vm;
        }



        private void Button_Click(object sender, RoutedEventArgs e)
        {
            int index = int.Parse(((Button)e.Source).Uid);

            GridCursor.Margin = new Thickness(10 + (150 * index), 0, 0, 0);
            CustomerViewer.Visibility = Visibility.Hidden;
            AddCustomer.Visibility = Visibility.Hidden;

            switch (index)
            {
                case 0:

                    break;
                case 1:

                    break;
                case 2:

                    AddCustomer.Visibility = Visibility.Visible;
                    break;
                case 3:

                    CustomerViewer.Visibility = Visibility.Visible;



                    break;
            }
        }

        private void Close_ProgramBtn(object sender, RoutedEventArgs e)
        {

        }


        private void btn1_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var context = button.DataContext as NumberViewModel;
            MessageBox.Show(context.Number.ToString());
            MessageBox.Show(button.Content.ToString());

            DisplayInfoWindow displayInfoWindow = new DisplayInfoWindow(this);
            displayInfoWindow.Show();

        }





        private void AddCustomerToMain(object sender, RoutedEventArgs e)
        {

            string error = "";
            //Make sure that the information is correct
            if (cust_AddName.Text.Length == 0)
            {
                error = "No Name Entered";
            }
            if (cust_AddCompnyName.Text.Length == 0)
            {
                error = "No Compny Name Entered";
            }

            if (cust_Addid.Text.Length == 0)
            {
                error = "No Compny Name Entered";
            }
            if (cust_AddArea.Text.Length == 0)
            {
                error = "No Area Name Entered";
            }
            if (cust_AddCity.Text.Length == 0)
            {
                error = "No City name Entered";
            }
            if (cust_Addnumberphone.Text.Length == 0)
            {
                error = "No Number Phone Name Entered";
            }

            if (error != "")
            {
                MessageBox.Show(error, "try again");
                return;
            }


            Customer customer = new Customer();
            customer.CustomerName = cust_AddName.Text;
            customer.CompanyName = cust_AddCompnyName.Text;
            customer.IdCard = cust_Addid.Text;
            customer.Area = cust_AddArea.Text;
            customer.City = cust_AddCity.Text;
            customer.PhoneNumber = cust_Addnumberphone.Text;
            addNewCustomerToDatabase(customer.CustomerName, customer.CompanyName, customer.IdCard, customer.Area, customer.City, customer.PhoneNumber);
            _Customer.Add(customer);
        }








        private void addNewCustomerToDatabase(string CustomerName, string CompanyName, string IdCard, string Area, string City, string PhoneNumber)
        {
            string connectionString;
            connectionString = _connectionString;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = connection.CreateCommand();
                // Must assign both transaction object and connection
                // to Command object for a pending local transaction
                command.Connection = connection;
                try
                {
                    command.Connection = connection;
                    command.CommandText =
                    "Insert into CustomerDetails_Table (CustomerName,CompanyName,IdCard,Area,City,PhoneNumber) VALUES (@CustomerName,@CompanyName,@IdCard,@Area,@City,@PhoneNumber)";
                    command.CommandType = CommandType.Text;
                    command.Parameters.AddWithValue("@CustomerName", CustomerName);
                    command.Parameters.AddWithValue("@CompanyName", CompanyName);
                    command.Parameters.AddWithValue("@IdCard", IdCard);
                    command.Parameters.AddWithValue("@Area", Area);
                    command.Parameters.AddWithValue("@City", City);
                    command.Parameters.AddWithValue("@PhoneNumber", PhoneNumber);
                    command.ExecuteNonQuery();
                    MessageBox.Show("Selection from CustomerDetails_Table  worked.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Commit Exception Type: {0}", ex.GetType());
                    Console.WriteLine("  Message: {0}", ex.Message);
                }
            }
        }
    }
}
