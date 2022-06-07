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
    public partial class AddCompanyPage : Page
    {

        String name, gst, broker, master, address, del;

        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\ADMIN\Source\Repos\ShreeGovardhanTextilesSystem\sgtdb.mdf;Integrated Security=True");


        public AddCompanyPage()
        {
            InitializeComponent();
            loaddata();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            name = txtname.Text;
            gst = txtgst.Text;
            broker = txtbroker.Text;
            master = txtmst.Text;
            address = txtadd.Text;
            del = txtdel.Text;

            SqlCommand cmd = new SqlCommand("insert into tbl_company (name,gst,delivery,broker,master,address)" +
                " values (@name,@gst,@del,@broker,@master,@address) ", con);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@name", name);
            cmd.Parameters.AddWithValue("@gst", gst);
            cmd.Parameters.AddWithValue("@del", del);
            cmd.Parameters.AddWithValue("@broker", broker);
            cmd.Parameters.AddWithValue("@master", master);
            cmd.Parameters.AddWithValue("@address", address);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            clearData();
            loaddata();

        }

        //to clear the text feilds
        public void clearData()
        {
            txtname.Clear();
            txtadd.Clear();
            txtbroker.Clear();
            txtdel.Clear();
            txtgst.Clear();
            txtmst.Clear();
        }


        public void loaddata()
        {
            SqlCommand cmd = new SqlCommand("select * from tbl_company", con);
            DataTable dt = new DataTable();
            con.Open();
            SqlDataReader sdr = cmd.ExecuteReader();
            dt.Load(sdr);
            con.Close();
            datagrid.ItemsSource = dt.DefaultView;
            datagrid.ScrollIntoView(datagrid.Items.GetItemAt(datagrid.Items.Count - 1));
            datagrid.FontSize = 20;
        }

        }
    }
