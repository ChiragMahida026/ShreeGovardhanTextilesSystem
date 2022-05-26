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
    /// Lógica de interacción para ProductionPage.xaml
    /// </summary>
    public partial class ProductionPage : Page
    {
        String serial, mcno;
        float gmtr, weight, gqlty, em, nm, nw;

        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\ADMIN\Source\Repos\ShreeGovardhanTextilesSystem\sgtdb.mdf;Integrated Security=True");


        public ProductionPage()
        {
            InitializeComponent();
        }

        private void txtweight_TextChanged(object sender, TextChangedEventArgs e)
        {
           filldata();
        }

        private void txtgm_TextChanged(object sender, TextChangedEventArgs e)
        {
            filldata();
        }

        private void txtserial_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void txtem_TextChanged(object sender, TextChangedEventArgs e)
        {
            filldata();
        }


      


        private void DatePicker_SizeChanged(object sender, SizeChangedEventArgs e)
        {

        }

        public void filldata()
        {
            if (txtgm.Text != "" && txtweight.Text != "")
            {
                gmtr = float.Parse(txtgm.Text);
                weight = float.Parse(txtweight.Text);
                txtgq.Text = ((weight / gmtr) * 100).ToString();
                if (txtem.Text != "")
                {
                    em = float.Parse(txtem.Text);

                }
                else
                {
                    em = 0;
                }
                nm = gmtr + em;
                txtnm.Text = nm.ToString();
                nw = (weight / nm) * 100;
                txtnw.Text = nw.ToString();
            }
        }



        private void Button_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                
                serial = txtserial.Text;
                mcno = txtmno.Text;
                gmtr = float.Parse(txtgm.Text);
                weight = float.Parse(txtweight.Text);
                gqlty = float.Parse(txtgq.Text);
                em = float.Parse(txtem.Text);
                nm = float.Parse(txtnm.Text);
                nw = float.Parse(txtnw.Text);

                SqlCommand cmd = new SqlCommand("insert into tbl_production (serial,machno,gmeter,weight,gqlty,emtr,nmtr,nweight) values (@serial,@mcno,@gmtr,@weight,@gqlty,@em,@nm,@nw) ", con);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@serial", serial);
                cmd.Parameters.AddWithValue("@mcno", mcno);
                cmd.Parameters.AddWithValue("@gmtr", gmtr);
                cmd.Parameters.AddWithValue("@weight", weight);
                cmd.Parameters.AddWithValue("@gqlty", gqlty);
                cmd.Parameters.AddWithValue("@em", em);
                cmd.Parameters.AddWithValue("@nm", nm);
                cmd.Parameters.AddWithValue("@nw", nw);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                loaddata();
                clearData();
                
            }
            catch (SqlException err) { }
           
        }

        public void loaddata()
        {
            SqlCommand cmd = new SqlCommand("select * from tbl_production", con);
            DataTable dt = new DataTable();
            con.Open();
            SqlDataReader sdr = cmd.ExecuteReader();
            dt.Load(sdr);
            con.Close();
            datagrid.ItemsSource = dt.DefaultView;
        }

        public void clearData()
        {
            txtserial.Clear();
            txtmno.Clear();
            txtgm.Clear();
            txtweight.Clear();
            txtgq.Clear();
            txtem.Clear();
            txtnm.Clear();
            txtnw.Clear();
        }


    }
    }
