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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ShreeGovardhanTextilesSystem.Pages
{
    /// <summary>
    /// Lógica de interacción para AddCompanyPage.xaml
    /// </summary>
    public partial class ReportPage : Page
    {

        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\ADMIN\Source\Repos\ShreeGovardhanTextilesSystem\sgtdb.mdf;Integrated Security=True");
        String[] dataset = {"Stock","Bim Stock"};
        public ReportPage()
        {
            InitializeComponent();
            dd.ItemsSource = dataset;
        }

        private void Box_used(object sender, RoutedEventArgs e)
        {
<<<<<<< HEAD
            Console.WriteLine("hdkashdaskjdhj");
            this.NavigationService.Navigate(new Uri("Boxused.xaml", UriKind.Relative));
        }
=======
           
            //NavigationService.Navigate(new Uri(@"C:\Users\ADMIN\Source\Repos\ShreeGovardhanTextilesSystem\Pages\ProductionPage.xaml", UriKind.Relative));
            //ProductionPage productionPage = new ProductionPage();
            //this.Content = productionPage;
        }



        private void Hide_Click(object sender, RoutedEventArgs e)

        {

            MyPopup.IsOpen = false;

        }

        private void cmbCountryList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            loaddata(e.ToString());
        }

        public void loaddata(String i) {
            if (i == "Stock")
            {
                SqlCommand cmd = new SqlCommand("select * from tbl_purchases", con);
                DataTable dt = new DataTable();
                con.Open();
                SqlDataReader sdr = cmd.ExecuteReader();
                dt.Load(sdr);
                con.Close();
                datagrid.ItemsSource = dt.DefaultView;
            }
        }
             
>>>>>>> 12b29528332ae5eab827d2e8ea2019b1fe4538a2
    }

}
