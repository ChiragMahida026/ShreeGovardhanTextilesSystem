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
    /// Lógica de interacción para PurchasesPage.xaml
    /// </summary>

    public partial class PurchasesPage : Page
    {


        String serial,daterec,dateused,qlty,weight;

        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\ADMIN\Source\Repos\ShreeGovardhanTextilesSystem\sgtdb.mdf;Integrated Security=True");

        String[] abc; 
       
        public PurchasesPage()
        {
            InitializeComponent();

            //get today's date
            DateTime now = DateTime.Now;

            //initialize the variable for the rest of program
            txtdaterec.Text = now.ToString("d");
            daterec=txtdaterec.Text;
            loaddata();
        }


        private void HandleCheck(object sender, RoutedEventArgs e)
        {
            RadioButton rb = sender as RadioButton;
            textBlock2.Text = "Selected Yarn : " + rb.Content;
            qlty = (rb.Content).ToString();


        }


        public void loaddata()
        {
            SqlCommand cmd = new SqlCommand("select * from tbl_purchases", con);
            DataTable dt = new DataTable();
            con.Open();
            SqlDataReader sdr = cmd.ExecuteReader();
            dt.Load(sdr);
            con.Close();

            datagrid.ItemsSource = dt.DefaultView;
            
            SqlCommand cmd2 = new SqlCommand("select COUNT(*)AS 'TOTAL' from tbl_purchases where qlty = '32/36' and date_used IS NULL", con);
            con.Open();
            tbox.Content = Convert.ToInt32(cmd2.ExecuteScalar());
            con.Close();

            SqlCommand cmd3 = new SqlCommand("select SUM(weight) from tbl_purchases where qlty = '32/36' and date_used IS NULL", con);
            con.Open();
            try
            {
                tweight.Content = Convert.ToDecimal(cmd3.ExecuteScalar());
            }
            catch (SqlException err) { }
            con.Close();

            SqlCommand cmd4 = new SqlCommand("select COUNT(*) from tbl_purchases where qlty = '40/24' and date_used IS NULL", con);
            con.Open();
            fbox.Content = Convert.ToDecimal(cmd4.ExecuteScalar());
            con.Close();

            SqlCommand cmd5 = new SqlCommand("select sum(weight) from tbl_purchases where qlty = '40/24' and date_used IS NULL", con);
            con.Open();
            try
            {
                fweight.Content = Convert.ToDecimal(cmd5.ExecuteScalar());
            }
            catch (SqlException err) {}
                con.Close();



        }

        //event on insert button click
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                serial = txtserial.Text;
                weight = txtweight.Text;

                SqlCommand cmd = new SqlCommand("insert into tbl_purchases (serial,date_rec,weight,qlty) values (@serial,@daterec,@weight,@qlty) ", con);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@serial", serial);
                cmd.Parameters.AddWithValue("@daterec", daterec);
                cmd.Parameters.AddWithValue("@weight", weight);
                cmd.Parameters.AddWithValue("@qlty", qlty);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                clearData();
                loaddata();
            }catch(SqlException err) { }
        }


        //to clear the text feilds
        public void clearData()
        {
            txtserial.Clear();
            txtweight.Clear();
        }

    }
}
