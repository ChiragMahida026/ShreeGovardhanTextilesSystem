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
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Data.OleDb;
using Excel = Microsoft.Office.Interop.Excel;
using System.Runtime.InteropServices;

namespace ShreeGovardhanTextilesSystem.Pages
{
    /// <summary>
    /// Lógica de interacción para AddCompanyPage.xaml
    /// </summary>
    /// 

    public partial class ReportPage : Page
    {

        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\ADMIN\Source\Repos\ShreeGovardhanTextilesSystem\sgtdb.mdf;Integrated Security=True");
        String[] dataset = { "Stock", "Bim Stock" };
        public ReportPage()
        {
            InitializeComponent();
            dd.ItemsSource = dataset;
        }

        private void cmbCountryList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            loaddata(e.ToString());
        }

        public void loaddata(String i)
        {
            if (i == "Stock")
            {
                SqlCommand cmd = new SqlCommand("select * from tbl_purchases", con);
                DataTable dt = new DataTable();
                con.Open();
                SqlDataReader sdr = cmd.ExecuteReader();
                dt.Load(sdr);
                con.Close();
                datagrid.ItemsSource = dt.DefaultView;
            }
        }

        private void Box_used(object sender, RoutedEventArgs e)
        {
            BoxUsed page2Obj = new BoxUsed();
            page2Obj.Show();
        }

        private void Bim_entry(object sender, RoutedEventArgs e)
        {
            BinEntry page2Obj = new BinEntry();
            page2Obj.Show();
        }

        private void Create_challen(object sender, RoutedEventArgs e)
        {
            CreateChallen page2Obj = new CreateChallen();
            page2Obj.Show();
        }

        private void Create_Pdf(object sender, RoutedEventArgs e)
        {
            CreatePdf page2Obj = new CreatePdf();
            page2Obj.Show();
        }

        private void Send_Data(object sender, RoutedEventArgs e)
        {
            SendData page2Obj = new SendData();
            page2Obj.Show();
        }

        private void Insert_data_Excel(object sender, RoutedEventArgs e)
        {
            Excel.Workbook workBook;
            Excel.Worksheet worksheet;
            Excel.Range sampleCell;

            string excelFinalPath = @"G:\DemoExample\XmlFileuse1.xlsx";
            Microsoft.Office.Interop.Excel.Application application = new Microsoft.Office.Interop.Excel.Application();
            workBook = application.Workbooks.Open(excelFinalPath);


            for (int i = 1; i <= workBook.Sheets.Count; i++)
            {
                worksheet = workBook.Worksheets[i];
                object cellValue = ((Microsoft.Office.Interop.Excel.Range)worksheet.Cells[2, 1]).Value;

                //Name Of Compeny
                sampleCell = ((Excel.Worksheet)worksheet).get_Range("A5");
                sampleCell.Value = "New Compeny Name";

                //Compeny Address
                sampleCell = ((Excel.Worksheet)worksheet).get_Range("A6");
                sampleCell.Value = "Surat";

                //Gst No.
                sampleCell = ((Excel.Worksheet)worksheet).get_Range("B7");
                sampleCell.Value = "980765ytghbno";

                //Change Master.
                sampleCell = ((Excel.Worksheet)worksheet).get_Range("E7");
                sampleCell.Value = "MASTER : ABHAMASTERsasas";

                //Change Ch.No.
                sampleCell = ((Excel.Worksheet)worksheet).get_Range("J2");
                sampleCell.Value = "50";

                //Change Date.
                sampleCell = ((Excel.Worksheet)worksheet).get_Range("J4");
                sampleCell.Value = "18-06-2022";

                //Change Broker.
                sampleCell = ((Excel.Worksheet)worksheet).get_Range("I6");
                sampleCell.Value = "New BrokerName";

                //Change S.No.
                sampleCell = ((Excel.Worksheet)worksheet).get_Range("A9");
                sampleCell.Value = "1925";

                //Refrence Cell
                sampleCell = ((Excel.Worksheet)worksheet).get_Range("L1");
                sampleCell.Value = "45";
                
                workBook.SaveAs(excelFinalPath);

            }
            workBook.Close(false, excelFinalPath, null);
            Marshal.ReleaseComObject(workBook);
        }

        private void PdfDownload(object sender, RoutedEventArgs e)
        {

        }
    }

}
