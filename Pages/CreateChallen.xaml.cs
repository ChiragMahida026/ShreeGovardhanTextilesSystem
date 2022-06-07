using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
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
            try {
                InitializeComponent();
                con.Open();
                loaddata();
                settaka();

                DateTime now = DateTime.Now;

                //initialize the variable for the rest of program
                txtdaterec.Text = now.ToString("d");
                dateused = txtdaterec.Text;

                SqlCommand cmd = new SqlCommand("select name from tbl_company", con);

                
                SqlDataReader sdr = cmd.ExecuteReader();
                
                List<string> numbersList = new List<string>();

                while (sdr.Read())
                {
                    numbersList.Add(Convert.ToString(sdr["name"]));
                }
                txtparty.ItemsSource = numbersList;
                party = txtparty.Text;
                
            }
            catch (SqlException err) {
                
                MessageBox.Show("Something gone wrong"); }
        }

            
            private void Button_Click(object sender, RoutedEventArgs e)
        {
            createinsert();
            insertdate();
            lstmtr.Clear();
            lstser.Clear();
            loaddata();
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
                SqlCommand cmd = new SqlCommand("select max(chlno)+1 as chlno from tbl_challen", con);

              
                if (cmd.ExecuteScalar() != null)
                {
                    txtchlno.Text = cmd.ExecuteScalar().ToString();
                }
                else { txtchlno.Text = "1"; }
                

                SqlCommand cmd2 = new SqlCommand("select max(serial)+1 as chlno from tbl_challen", con);

               if (cmd2.ExecuteScalar() != null)
                {
                    txttakano.Text = cmd2.ExecuteScalar().ToString();
                }
                else { txttakano.Text = "1"; }
                
            }
            catch (SqlException err) { txttakano.Text = "1";
                
            }
            




        }




        public void createinsert()
        {

            lstmtr.Clear();
            lstser.Clear();
            try
            {
                lot = 0;
                serial = txttakano.Text;
                chlno = txtchlno.Text;
                lot = float.Parse(txtlot.Text);
                lot = (lot * 12)+int.Parse(serial);

                SqlCommand cmd = new SqlCommand("SELECT * FROM (SELECT ROW_NUMBER() OVER(ORDER BY id ASC)" +
                    " AS rownumber, serial, nmtr, nweight FROM tbl_production ) AS foo " +
                    "WHERE rownumber >= @serial and rownumber < @lot", con);

                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@serial", serial);
                cmd.Parameters.AddWithValue("@lot", lot.ToString());


                
                SqlDataReader sdr = cmd.ExecuteReader();


                while (sdr.Read())
                {
                    lstser.Add((string)sdr["serial"]);
                    lstmtr.Add((double)sdr["nmtr"]);
                }
                sdr.Close();
                
                
                totmtr.Content = lstmtr.Sum().ToString();
                tottaka.Content = lstmtr.Count().ToString();
                settaka();
                
                loaddata();

            }
            catch(SqlException err) {
                
                MessageBox.Show("Fill all the detials correctly...");
                
            }
        }

        public void insertdate()
        {
            try { 
            for (int i = 0; i < lstmtr.Count(); i++)
            {
                SqlCommand cmd = new SqlCommand("insert into tbl_challen (date,chlno,party,serial,meter) values (@date,@chlno,@party,@serial,@weight) ", con);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@date", dateused);
                cmd.Parameters.AddWithValue("@chlno", chlno);
                cmd.Parameters.AddWithValue("@party", txtparty.Text);
                cmd.Parameters.AddWithValue("@serial", lstser[i].ToString());
                cmd.Parameters.AddWithValue("@weight", lstmtr[i].ToString());
                
                cmd.ExecuteNonQuery();
               
                    settaka();
                    
                }
               
            }
            catch(SqlException err) {
                
                MessageBox.Show("Fill all the detials correctly..."); 
            }
}
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {
                cmdbl = new SqlCommandBuilder(adap);
                adap.Update(ds, "purchases detail");
                MessageBox.Show("Information Updated", "Update");
                loaddata();
                settaka();
            }
            catch (SqlException err) { MessageBox.Show("Check Datagrid"); }
        }
    }


    }
