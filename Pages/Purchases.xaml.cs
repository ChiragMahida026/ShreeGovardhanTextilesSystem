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
        SqlCommandBuilder cmdbl;
        SqlDataAdapter adap;
        DataSet ds;


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
            adap = new SqlDataAdapter("select id,serial as 'Serial', weight as 'Weight(Kg)', date_rec as 'Rec Date', " +
           "date_used as 'Use Date', qlty as 'Quality'  from tbl_purchases", con);

            ds = new DataSet();
            adap.Fill(ds, "purchases detail");
            datagrid.ItemsSource = ds.Tables[0].DefaultView;



            SqlCommand cmd2 = new SqlCommand("select COUNT(*)AS 'TOTAL' from tbl_purchases where qlty = '32/36' and date_used IS NULL", con);
            con.Open();
            try
            {
                if (cmd2.ExecuteScalar() != null)
                {
                    tbox.Content = Convert.ToInt32(cmd2.ExecuteScalar());
                }
            }
            catch (SqlException err) { }
            con.Close();

            SqlCommand cmd3 = new SqlCommand("select SUM(weight) from tbl_purchases where qlty = '32/36' and date_used IS NULL", con);
            con.Open();
            try
            {
                if (cmd3.ExecuteScalar() != null)
                {
                    tweight.Content = Convert.ToDecimal(cmd3.ExecuteScalar());
                }
            }
            catch (SqlException err) { }
            con.Close();

            SqlCommand cmd4 = new SqlCommand("select COUNT(*) from tbl_purchases where qlty = '40/24' and date_used IS NULL", con);
            con.Open();
            if (cmd4.ExecuteScalar() != null)
            {
                fbox.Content = Convert.ToDecimal(cmd4.ExecuteScalar());
            }
            con.Close();

            SqlCommand cmd5 = new SqlCommand("select sum(weight) from tbl_purchases where qlty = '40/24' and date_used IS NULL", con);
            con.Open();
            try
            {
                if (cmd5.ExecuteScalar() != null)
                {
                    fweight.Content = Convert.ToDecimal(cmd5.ExecuteScalar());
                }
            }
            catch (SqlException err) {}
                con.Close();

            datagrid.ScrollIntoView(datagrid.Items.GetItemAt(datagrid.Items.Count - 1));
            datagrid.FontSize = 20;
        }

        //event on insert button click
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            serial = txtserial.Text;
            weight = txtweight.Text;

            SqlCommand cmd2 = new SqlCommand("select * from tbl_purchases where serial = @serial", con);
            cmd2.CommandType = CommandType.Text;
            cmd2.Parameters.AddWithValue("@serial", serial);
            con.Open();
            SqlDataReader sdr = cmd2.ExecuteReader();
            if (sdr.HasRows)
            {
                con.Close();
                MessageBox.Show("Cartoon Serial already exist");
            }
            


            else
            {
                con.Close();
                try
                {

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
                    MessageBox.Show("Cartoon Serial added successfully");

                }
                catch (SqlException err) { MessageBox.Show("Fill all the detials correctly..."); }
            }
            con.Close();
        }


        //to clear the text feilds
        public void clearData()
        {
            txtserial.Clear();
            txtweight.Clear();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            cmdbl = new SqlCommandBuilder(adap);
            adap.Update(ds, "purchases detail");
            MessageBox.Show("Information Updated","Update");
            loaddata();
        }


    }
}
