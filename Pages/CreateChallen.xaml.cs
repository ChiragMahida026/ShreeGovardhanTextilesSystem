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
    public partial class CreateChallen : Window
    {
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\ADMIN\Source\Repos\ShreeGovardhanTextilesSystem\sgtdb.mdf;Integrated Security=True");
        String serial, dateused,chlno,takano,party;
        float lot;
        SqlCommandBuilder cmdbl;
        SqlDataAdapter adap;
        DataSet ds;
        List<Double> lstmtr = new List<Double>();
        List<String> lstser = new List<String>();


        public CreateChallen()
        {
            InitializeComponent();
            loaddata();
            settaka(); 
            setser();
            DateTime now = DateTime.Now;

            //initialize the variable for the rest of program
            txtdaterec.Text = now.ToString("d");
            dateused = txtdaterec.Text;

            SqlCommand cmd = new SqlCommand("select name from tbl_company", con);

            con.Open();
            SqlDataReader sdr = cmd.ExecuteReader();

            List<string> numbersList = new List<string>();

            while (sdr.Read())
            {
                numbersList.Add(Convert.ToString(sdr["name"]));
            }
            txtparty.ItemsSource = numbersList;
            party=txtparty.Text;
            con.Close();
        }

            
            private void Button_Click(object sender, RoutedEventArgs e)
        {
            setcount();
            
            //loaddata();
            createinsert();
            insertdate();
        }

        
       

        public void loaddata()
        {

            adap = new SqlDataAdapter("select * from tbl_challen", con);

            ds = new DataSet();
            adap.Fill(ds, "purchases detail");
            datagrid.ItemsSource = ds.Tables[0].DefaultView;


            datagrid.ScrollIntoView(datagrid.Items.GetItemAt(datagrid.Items.Count - 1));
            datagrid.FontSize = 20;


        }

        public void settaka()
        {
             try
            {
                SqlCommand cmd = new SqlCommand("select chlno+1 as chlno from tbl_challen where id=(select max(id) from tbl_challen)", con);

                con.Open();
                if (cmd.ExecuteScalar() != null)
                {
                    txtchlno.Text = cmd.ExecuteScalar().ToString();
                }
                else { txtchlno.Text = "1"; }
                con.Close();
            }
            catch (SqlException err) { txtchlno.Text = "1"; }
            




        }

        public void setser()
        {
            SqlCommand cmd2 = new SqlCommand("select MAX(serial)+1 as ser from tbl_production where serial IN (select serial from tbl_challen) ", con);
            con.Open();
            try
            {
                txttakano.Text = cmd2.ExecuteScalar().ToString();
            }
            catch (SqlException err) { }
            con.Close();
        }

        public void setcount()
        {
            try
            {
                serial = txttakano.Text;
                chlno = txtchlno.Text;
                lot = float.Parse(txtlot.Text);
                lot = lot * 12;

                SqlCommand cmd = new SqlCommand("select sum(nmtr),count(nmtr) from tbl_production " +
                    "where id >= (select id from tbl_production where serial = @serial) and " +
                    "id<(select id from tbl_production where serial = @serial)+@lot", con);

                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@serial", serial);
                cmd.Parameters.AddWithValue("@lot", lot.ToString());

                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    totmtr.Content = reader.GetDouble(0).ToString();
                    tottaka.Content = reader.GetInt32(1).ToString();
                }
                else
                {
                    Console.WriteLine("No rows found.");
                }
                reader.Close();
                con.Close();
            }catch (Exception err) { }
        }


        public void createinsert()
        {
            try
            {
                serial = txttakano.Text;
                chlno = txtchlno.Text;
                lot = float.Parse(txtlot.Text);
                lot = lot * 12;

                SqlCommand cmd = new SqlCommand("select serial,nmtr,nweight from tbl_production " +
                    "where id >= (select id from tbl_production where serial = @serial) and " +
                    "id<(select id from tbl_production where serial = @serial)+@lot", con);

                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@serial", serial);
                cmd.Parameters.AddWithValue("@lot", lot.ToString());


                con.Open();
                SqlDataReader sdr = cmd.ExecuteReader();


                while (sdr.Read())
                {
                    lstser.Add((string)sdr["serial"]);
                    lstmtr.Add((double)sdr["nmtr"]);
                }
             
                con.Close();
                settaka();
                setser();
            }
            catch(SqlException err) {MessageBox.Show("Fill all the detials correctly..."); }
        }

        public void insertdate()
        {
            try { 
            Console.WriteLine(lstmtr.Count);
            for (int i = 0; i < Int32.Parse((string)tottaka.Content); i++)
            {
                SqlCommand cmd = new SqlCommand("insert into tbl_challen (date,chlno,party,serial,meter) values (@date,@chlno,@party,@serial,@weight) ", con);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@date", dateused);
                cmd.Parameters.AddWithValue("@chlno", chlno);
                cmd.Parameters.AddWithValue("@party", txtparty.Text);
                cmd.Parameters.AddWithValue("@serial", lstser[i].ToString());
                cmd.Parameters.AddWithValue("@weight", lstmtr[i].ToString());
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                    settaka();
                    setser();
                }

        }catch(SqlException err) {MessageBox.Show("Fill all the detials correctly..."); }
}
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            cmdbl = new SqlCommandBuilder(adap);
            adap.Update(ds, "purchases detail");
            MessageBox.Show("Information Updated", "Update");
            loaddata();
            settaka();
            setser();
        }
    }


    }
