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

namespace ShreeGovardhanTextilesSystem.Pages
{
    /// <summary>
    /// Interaction logic for BoxUsed.xaml
    /// </summary>

   
    ///select date_used,count(*),sum(weight) from tbl_purchases where date_rec = @date_used and qlty = '40/24' group by date_rec
    ///select * from tbl_stock order by id desc limit 1

    public partial class BinEntry : Window
    {
        String date;
        String ptbox, ptweight, utbox, utweight;

        private void getdata(object sender, RoutedEventArgs e)
        {
            loadata();
        }

        float sfbox, sfweight, stbox, stweight, bimstock;

        private void txtbimrec_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (txtbimrec.Text != "" && txtbimused.Text != "")
            {
                bimrec = txtbimrec.Text;
                bimused = txtbimused.Text;
                bimstock = float.Parse((string)bstk.Content) + float.Parse(bimused) + float.Parse(bimrec);
                txtbimstock.Text = bimstock.ToString();
            }
        }

        private void txtbimused_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (txtbimrec.Text != "" && txtbimused.Text != "")
            {
                bimrec = txtbimrec.Text;
                bimused = txtbimused.Text;
                bimstock = float.Parse((string)bstk.Content) + float.Parse(bimused) + float.Parse(bimrec);
                txtbimstock.Text=bimstock.ToString();
            }
            }

        String pfbox,pfweight, ufbox, ufweight;
        String bimused, bimrec, cbimstock;
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\ADMIN\Source\Repos\ShreeGovardhanTextilesSystem\sgtdb.mdf;Integrated Security=True");


        public BinEntry()
        {
            InitializeComponent();
            DateTime now = DateTime.Now;
            //initialize the variable for the rest of program
            txtdaterec.Text = now.ToString("d");
            
            loadata();

        }


        public void loadata() {
            date = txtdaterec.Text;
            SqlCommand cmd = new SqlCommand("select date_rec,count(*),sum(weight) from tbl_purchases where date_rec = @date_rec and qlty = '32/36' group by date_rec", con);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@date_rec", date);
            
            con.Open();
            SqlDataReader reader = cmd.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    txtptbox.Text = reader.GetInt32(1).ToString();
                    txtptweight.Text = reader.GetDouble(2).ToString();
                  
                }
            }
            else
            {
                Console.WriteLine("No rows found.");
                txtptbox.Text = "0";
                txtptweight.Text = "0";
            }
            ptbox = txtptbox.Text;
            ptweight = txtptweight.Text;
            reader.Close();

            con.Close();

           
            SqlCommand cmd2 = new SqlCommand("select date_rec,count(*),sum(weight) from tbl_purchases where date_rec = @date_rec and qlty = '40/24' group by date_rec", con);

            cmd2.CommandType = CommandType.Text;
            cmd2.Parameters.AddWithValue("@date_rec", date);

            con.Open();
            SqlDataReader reader2 = cmd2.ExecuteReader();

            if (reader2.HasRows)
            {
                while (reader2.Read())
                {
                    txtpfbox.Text = reader2.GetInt32(1).ToString();
                    txtpfweight.Text = reader2.GetDouble(2).ToString();
                  
                }
            }
            else
            {
                txtpfbox.Text = "0";
                txtpfweight.Text = "0";
                Console.WriteLine("No rows found.");
            }
            pfbox = txtpfbox.Text;
            pfweight = txtpfweight.Text;
            reader2.Close();

            con.Close();



            SqlCommand cmd3 = new SqlCommand("select date_used,count(*),sum(weight) from tbl_purchases where date_used = @date_rec and qlty = '40/24' group by date_used", con);

            cmd3.CommandType = CommandType.Text;
            cmd3.Parameters.AddWithValue("@date_rec", date);

            con.Open();
            SqlDataReader reader3 = cmd3.ExecuteReader();

            if (reader3.HasRows)
            {
                while (reader3.Read())
                {
                    txtufbox.Text = reader3.GetInt32(1).ToString();
                    txtufweight.Text = reader3.GetDouble(2).ToString();
                  
                }
            }
            else
            {
                Console.WriteLine("No rows found.");
                txtufbox.Text = "0";
                txtufweight.Text = "0";

            }
            ufbox = txtufbox.Text;
            ufweight = txtufweight.Text;
            reader3.Close();

            con.Close();



            SqlCommand cmd4 = new SqlCommand("select date_used,count(*),sum(weight) from tbl_purchases where date_used = @date_rec and qlty = '32/36' group by date_used", con);

            cmd4.CommandType = CommandType.Text;
            cmd4.Parameters.AddWithValue("@date_rec", date);

            con.Open();
            SqlDataReader reader4 = cmd4.ExecuteReader();

            if (reader4.HasRows)
            {
                while (reader4.Read())
                {
                    txtutbox.Text = reader4.GetInt32(1).ToString();
                    txtutweight.Text = reader4.GetDouble(2).ToString();
                   
                }
            }
            else
            {
                Console.WriteLine("No rows found.");
                txtutbox.Text = "0";
                txtutweight.Text = "0";
            }
            utbox = txtutbox.Text;
            utweight = txtutweight.Text;
            reader4.Close();

            con.Close();

            SqlCommand cmd5 = new SqlCommand("select s_boxt,s_weightt,s_boxf,s_weightf,bim from tbl_stock  WHERE ID = (SELECT COUNT(*) FROM tbl_stock)", con);

            cmd5.CommandType = CommandType.Text;
            
            con.Open();
            SqlDataReader reader5 = cmd5.ExecuteReader();

            if (reader5.HasRows)
            {
                while (reader5.Read())
                {
                    tstbox.Content=reader5.GetString(0);
                    tstweight.Content=reader5.GetString(1);
                    fsfbox.Content=reader5.GetString(2);
                    fsfweight.Content=reader5.GetString(3);
                    bstk.Content = reader5.GetString(4);
                   
                }

            }
            else
            {
                tstbox.Content = "0";
                tstweight.Content = "0";
                fsfbox.Content = "0";
                fsfweight.Content = "0";
            }
            sfbox = float.Parse((string)fsfbox.Content) + float.Parse(pfbox) - float.Parse(ufbox);
            stbox = float.Parse((string)tstbox.Content) + float.Parse(ptbox) - float.Parse(utbox);
            sfweight = float.Parse((string)fsfweight.Content) + float.Parse(pfweight) - float.Parse(ufweight);
            stweight = float.Parse((string)tstweight.Content) + float.Parse(ptweight) - float.Parse(utweight);
            

            txtsfbox.Text=sfbox.ToString();
            txtsfweight.Text=sfweight.ToString();
            txtstbox.Text=stbox.ToString();
            txtstweight.Text=stweight.ToString();

            reader5.Close();

            con.Close();


        }



        private void filldata(object sender, RoutedEventArgs e)
        {

            try
            {
              
                bimrec = txtbimrec.Text;
                bimused=txtbimused.Text;

                SqlCommand cmd = new SqlCommand("insert into tbl_stock (date,p_boxt,p_weightt,u_boxt,u_weightt," +
                    "s_boxt,s_weightt,p_boxf,p_weightf,u_boxf,u_weightf,s_boxf,s_weightf,bim,r_bim,u_bim)" +
                    "values (@date,@p_boxt,@p_weightt,@u_boxt,@u_weightt," +
                    "@s_boxt,@s_weightt,@p_boxf,@p_weightf,@u_boxf,@u_weightf,@s_boxf,@s_weightf,@bim,@r_bim,@u_bim)", con);
                
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@p_boxt", ptbox);
                cmd.Parameters.AddWithValue("@p_weightt", ptweight);
                cmd.Parameters.AddWithValue("@u_boxt", utbox);
                cmd.Parameters.AddWithValue("@u_weightt", utweight);
                cmd.Parameters.AddWithValue("@s_boxt", stbox);
                cmd.Parameters.AddWithValue("@s_weightt", stweight);
                cmd.Parameters.AddWithValue("@p_boxf", pfbox);
                cmd.Parameters.AddWithValue("@p_weightf", pfweight);
                cmd.Parameters.AddWithValue("@u_boxf", ufbox);
                cmd.Parameters.AddWithValue("@u_weightf", ufweight);
                cmd.Parameters.AddWithValue("@s_boxf", sfbox);
                cmd.Parameters.AddWithValue("@s_weightf", sfweight);
                cmd.Parameters.AddWithValue("@bim", bimstock);
                cmd.Parameters.AddWithValue("@r_bim", bimrec);
                cmd.Parameters.AddWithValue("@u_bim", bimstock);
                cmd.Parameters.AddWithValue("@date",date );

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                
                loadata();
            }
            catch (SqlException err) { MessageBoxResult result = MessageBox.Show(err.ToString()); }

        }
    }
}