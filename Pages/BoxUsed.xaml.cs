using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ShreeGovardhanTextilesSystem.Pages
{
    /// <summary>
    /// Interaction logic for BoxUsed.xaml
    /// </summary>
    public partial class BoxUsed : Window
    {

        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\ADMIN\Source\Repos\ShreeGovardhanTextilesSystem\sgtdb.mdf;Integrated Security=True");
        String serial, dateused;
        String[] code;

        public BoxUsed()
        {
            InitializeComponent();
            DateTime now = DateTime.Now;

            //initialize the variable for the rest of program
            txtdaterec.Text = now.ToString("d");
            dateused = txtdaterec.Text;

            SqlCommand cmd = new SqlCommand("select serial from tbl_purchases", con);

            con.Open();
            SqlDataReader sdr = cmd.ExecuteReader();

            List<string> numbersList = new List<string>();

            while (sdr.Read())
            {
                numbersList.Add(Convert.ToString(sdr["serial"]));
            }
            txtserial.ItemsSource = numbersList;
            con.Close();
            loaddata();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                serial = txtserial.Text;

                SqlCommand cmd = new SqlCommand("update tbl_purchases set date_used = @dateused where serial = @serial ", con);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@serial", serial);
                cmd.Parameters.AddWithValue("@dateused", dateused);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                loaddata();
            }
            catch (SqlException err) { MessageBoxResult result = MessageBox.Show(serial + " " + dateused + "  " + err.ToString()); }
        }

        public void loaddata()
        {
            SqlCommand cmd = new SqlCommand("select * from tbl_purchases where date_used is not null order by date_used desc", con);
            DataTable dt = new DataTable();
            con.Open();
            SqlDataReader sdr = cmd.ExecuteReader();
            dt.Load(sdr);
            con.Close();

            datagrid.ItemsSource = dt.DefaultView;
        }
    }
}