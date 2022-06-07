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
    /// Lógica de interacción para BackupPage.xaml
    /// </summary>
    public partial class BackupPage : Page
    {

        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\ADMIN\Source\Repos\ShreeGovardhanTextilesSystem\sgtdb.mdf;Integrated Security=True");
        SqlCommandBuilder cmdbl;
        SqlDataAdapter adap;
        DataSet ds;

        public BackupPage()
        {
            InitializeComponent();
            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DateTime now = DateTime.Now;
            
            loaddatapur();
            ExportToExcelAndCsv(now.ToString("dd-mm-yyyy")+"_Purchases");
            loaddatapro();
            ExportToExcelAndCsv(now.ToString("dd-mm-yyyy")+"_Production");
            loaddatacomp();
            ExportToExcelAndCsv(now.ToString("dd-mm-yyyy")+"_Companies");
            loaddatacomp();
            ExportToExcelAndCsv(now.ToString("dd-mm-yyyy")+ "_Challen");
            loaddatastock();
            ExportToExcelAndCsv(now.ToString("dd-mm-yyyy") + "_stock");


        }

        private void ExportToExcelAndCsv(String name)
        {
            datagrid.SelectAllCells();
            datagrid.ClipboardCopyMode = DataGridClipboardCopyMode.IncludeHeader;
            ApplicationCommands.Copy.Execute(null, datagrid);
            String resultat = (string)Clipboard.GetData(DataFormats.CommaSeparatedValue);
            String result = (string)Clipboard.GetData(DataFormats.Text);
            datagrid.UnselectAllCells();
            System.IO.StreamWriter file1 = new System.IO.StreamWriter(@"D:\DemoExample\" + name + ".xls");
            file1.WriteLine(result.Replace(',', ' '));
            file1.Close();

            MessageBox.Show(" Exporting DataGrid data to Excel file created.xls");
        }

        public void loaddatapur()
        {
            adap = new SqlDataAdapter("select id,serial as 'Serial', weight as 'Weight(Kg)', date_rec as 'Rec Date', " +
           "date_used as 'Use Date', qlty as 'Quality'  from tbl_purchases", con);

            ds = new DataSet();
            adap.Fill(ds, "purchases detail");
            datagrid.ItemsSource = ds.Tables[0].DefaultView;
            datagrid.ScrollIntoView(datagrid.Items.GetItemAt(datagrid.Items.Count - 1));
            datagrid.FontSize = 20;
        }

        public void loaddatapro()
        {
            adap = new SqlDataAdapter("select * from tbl_production", con);

            ds = new DataSet();
            adap.Fill(ds, "purchases detail");
            datagrid.ItemsSource = ds.Tables[0].DefaultView;
            datagrid.ScrollIntoView(datagrid.Items.GetItemAt(datagrid.Items.Count - 1));
            datagrid.FontSize = 20;
        }

        public void loaddatacomp()
        {
            adap = new SqlDataAdapter("select * from tbl_company", con);

            ds = new DataSet();
            adap.Fill(ds, "purchases detail");
            datagrid.ItemsSource = ds.Tables[0].DefaultView;
            datagrid.ScrollIntoView(datagrid.Items.GetItemAt(datagrid.Items.Count - 1));
            datagrid.FontSize = 20;
        }

        public void loaddatachallen()
        {
            adap = new SqlDataAdapter("select * from tbl_challen", con);

            ds = new DataSet();
            adap.Fill(ds, "purchases detail");
            datagrid.ItemsSource = ds.Tables[0].DefaultView;
            datagrid.ScrollIntoView(datagrid.Items.GetItemAt(datagrid.Items.Count - 1));
            datagrid.FontSize = 20;
        }

        public void loaddatastock()
        {
            adap = new SqlDataAdapter("select * from tbl_stock", con);

            ds = new DataSet();
            adap.Fill(ds, "purchases detail");
            datagrid.ItemsSource = ds.Tables[0].DefaultView;
            datagrid.ScrollIntoView(datagrid.Items.GetItemAt(datagrid.Items.Count - 1));
            datagrid.FontSize = 20;
        }
    }
    }
