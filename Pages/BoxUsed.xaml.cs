using System;
using System.Collections.Generic;
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
    public partial class BoxUsed : Window
    {

        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\ADMIN\Source\Repos\ShreeGovardhanTextilesSystem\sgtdb.mdf;Integrated Security=True");
        String serial, dateused;

        public BoxUsed()
        {
            InitializeComponent();
            DateTime now = DateTime.Now;

            //initialize the variable for the rest of program
            txtdaterec.Text = now.ToString("d");
            dateused = txtdaterec.Text;

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            serial = txtserial.Text;

        }
    }
}