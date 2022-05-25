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
        String[] dataset = { "Stock", "Bim Stock" };
        public ReportPage()
        {
            InitializeComponent();
            dd.ItemsSource = dataset;
        }

        private void Hide_Click(object sender, RoutedEventArgs e)

        {

            MyPopup.IsOpen = false;

        }

        private void cmbCountryList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            loaddata(e.ToString());
        }

        public void loaddata(String i)
        {
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

        private void Box_used(object sender, RoutedEventArgs e)
        {
            BoxUsed page2Obj = new BoxUsed();
            page2Obj.Show();
        }

        private void Bim_entry(object sender, RoutedEventArgs e)
        {
            BinEntry page2Obj = new BinEntry();
            page2Obj.Show();
        }

        private void Create_challen(object sender, RoutedEventArgs e)
        {
            CreateChallen page2Obj = new CreateChallen();
            page2Obj.Show();
        }
    }

}