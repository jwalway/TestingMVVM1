using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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
using TestingMVVM1.Models;

namespace TestingMVVM1.Views
{
    /// <summary>
    /// Interaction logic for DisplayInfoWindow.xaml
    /// </summary>
    public partial class DisplayInfoWindow : Window
    {

        //public string _connectionString = @"Data Source=LAPTOP-CMOUV9KC\SQLEXPRESS;Initial Catalog=Trucking;Persist Security Info=True;User ID=Tracking_Trucks;Password=AtomBomb";
        //public string _connectionString = @"Data Source=OWNER-PC\SQLEXPRESS (owner-PC\owner);Initial Catalog=Trucking;Persist Security Info=True;User ID=Tracking_Trucks;Password=AtomBomb";
        public string _connectionString = @"Data Source=OWNER-PC\SQLEXPRESS;Initial Catalog=Trucking;Integrated Security=True";
        public string connectionString;

        MainWindowDetails _details;
        public DisplayInfoWindow(MainWindowDetails mainWindowDetails)
        {

            _details = mainWindowDetails;
            InitializeComponent();
            listview();
            listview2();
            loadCustomersFromDatabase();
        }

        private void listview()
        {
            // Add columns
            var gridView = new GridView();
            lstview.View = gridView;
            gridView.Columns.Add(new GridViewColumn
            {
                Header = "Id",
                DisplayMemberBinding = new Binding("Id")
            });
            gridView.Columns.Add(new GridViewColumn
            {
                Header = "Customer Name",
                DisplayMemberBinding = new Binding("CustomerName")
            });
            gridView.Columns.Add(new GridViewColumn
            {
                Header = "Company Name",
                DisplayMemberBinding = new Binding("CompanyName")
            });
            gridView.Columns.Add(new GridViewColumn
            {
                Header = "Id Card",
                DisplayMemberBinding = new Binding("IdCard")
            });
            gridView.Columns.Add(new GridViewColumn
            {
                Header = "Area",
                DisplayMemberBinding = new Binding("Area")
            });
            gridView.Columns.Add(new GridViewColumn
            {
                Header = "City",
                DisplayMemberBinding = new Binding("City")
            });
            gridView.Columns.Add(new GridViewColumn
            {
                Header = "Phone Number",
                DisplayMemberBinding = new Binding("PhoneNumber")
            });


        }
        private void listview2()
        {
            // Add columns
            var gridView = new GridView();
            lstview2.View = gridView;
            gridView.Columns.Add(new GridViewColumn
            {
                Header = "Transport Date",
                DisplayMemberBinding = new Binding("TransportDate")
            });
            gridView.Columns.Add(new GridViewColumn
            {
                Header = "transport Price",
                DisplayMemberBinding = new Binding("transportPrice")
            });
            gridView.Columns.Add(new GridViewColumn
            {
                Header = "Area Transportation",
                DisplayMemberBinding = new Binding("AreaTransportation")
            });
            gridView.Columns.Add(new GridViewColumn
            {
                Header = "Number of Trucks",
                DisplayMemberBinding = new Binding("NumberofTrucks")
            });

        }



        private void loadCustomersFromDatabase()
        {
            int CustomerId;
            string CustomerName, CompanyName, IdCard, Area, City, PhoneNumber;

            string connectionString;
            SqlDataReader dataReader;
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
                    command.CommandText = "Select * From CustomerDetails_Table";

                    // Attempt to commit the transaction.
                    dataReader = command.ExecuteReader();

                    while (dataReader.Read())
                    {
                        Customer customer = new Customer();

                        CustomerId = (int)dataReader.GetValue(0);
                        CustomerName = (string)dataReader.GetValue(1);
                        CompanyName = (string)dataReader.GetValue(2);
                        IdCard = (string)dataReader.GetValue(3);
                        Area = (string)dataReader.GetValue(4);
                        City = (string)dataReader.GetValue(5);
                        PhoneNumber = (string)dataReader.GetValue(6);


                        customer.Id = CustomerId;
                        customer.CustomerName = CustomerName;
                        customer.CompanyName = CompanyName;
                        customer.IdCard = IdCard;
                        customer.Area = Area;
                        customer.City = City;
                        customer.PhoneNumber = PhoneNumber;
                        _details._Customer.Add(customer);

                        lstview.Items.Add(customer);
                    }
                    dataReader.Close();
                    command.Dispose();

                    MessageBox.Show("Selection from CustomerDetails_Table worked.");

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
