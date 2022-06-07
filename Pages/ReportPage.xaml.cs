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
using MessageBox = System.Windows.Forms.MessageBox;
using System.Net;
using System.Diagnostics;
using System.IO;

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
            con.Open();
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
                
                SqlDataReader sdr = cmd.ExecuteReader();
                dt.Load(sdr);
                
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
           
        }

        private void Send_Data(object sender, RoutedEventArgs e)
        {
            //SendData page2Obj = new SendData();
            //page2Obj.Show();

            Insert_data_Excel();
        }

        String cname, cno, cdate;
        List<Double> lstmtr = new List<Double>();
        List<Int32> lstser = new List<Int32>();

        String name,gst,del,broker,master,address;
        
        public void getdata()
        {
            cno = "2";
            SqlCommand cmd2 = new SqlCommand("SELECT * FROM tbl_challen where chlno = @name", con);

            cmd2.CommandType = CommandType.Text;
            cmd2.Parameters.AddWithValue("@name", cno);



            SqlDataReader sdr2 = cmd2.ExecuteReader();


            while (sdr2.Read())
            {
                cname = ((string)sdr2["party"]);
                cdate = ((string)sdr2["date"]);
                lstmtr.Add(((double)sdr2["meter"]));
                lstser.Add(((Int32)sdr2["serial"]));
            }
            sdr2.Close();


            SqlCommand cmd = new SqlCommand("SELECT * FROM tbl_company where name = @name", con);

            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@name", cname);



            SqlDataReader sdr = cmd.ExecuteReader();


            while (sdr.Read())
            {
                name = ((string)sdr["name"]);
                gst = ((string)sdr["gst"]);
                del = ((string)sdr["delivery"]);
                broker = ((string)sdr["broker"]);
                master = ((string)sdr["master"]);
                address = ((string)sdr["address"]);
            }
            sdr.Close();
        }

        private void Insert_data_Excel()
        {
            try {
                getdata();
            Excel.Workbook workBook;
            Excel.Worksheet worksheet;
            Excel.Range sampleCell;

            string excelFinalPath = @"D:\DemoExample\XmlFileuse3.xlsx";
            Microsoft.Office.Interop.Excel.Application application = new Microsoft.Office.Interop.Excel.Application();
                workBook = application.Workbooks.Open(excelFinalPath);
                
                


                for (int i = 1; i <= workBook.Sheets.Count; i++)
                {
                    worksheet = workBook.Worksheets[i];
                    object cellValue = ((Microsoft.Office.Interop.Excel.Range)worksheet.Cells[2, 1]).Value;

                    //Name Of Compeny
                    sampleCell = ((Excel.Worksheet)worksheet).get_Range("A5");
                    sampleCell.Value = cname;

                    //Compeny Address
                    sampleCell = ((Excel.Worksheet)worksheet).get_Range("A6");
                    sampleCell.Value = address;

                    //Gst No.
                    sampleCell = ((Excel.Worksheet)worksheet).get_Range("B7");
                    sampleCell.Value = gst;

                    //Change Master.
                    sampleCell = ((Excel.Worksheet)worksheet).get_Range("J4");
                    sampleCell.Value = cdate;

                    //Change Ch.No.
                    sampleCell = ((Excel.Worksheet)worksheet).get_Range("J2");
                    sampleCell.Value = cno;

                    //Change Date.
                    sampleCell = ((Excel.Worksheet)worksheet).get_Range("I6");
                    sampleCell.Value = broker;

                    //Change Broker.
                    sampleCell = ((Excel.Worksheet)worksheet).get_Range("F7");
                    sampleCell.Value = master;

                    int count = 9;

                    sampleCell = ((Excel.Worksheet)worksheet).get_Range("A9:H20");
                    sampleCell.Value = "";

                    for (int j=0; j < lstmtr.Count(); j++)
                    {
                        if ((float)j / 12 == 1 || (float)j / 12 == 2 || (float)j / 12 == 3 || (float)j / 12 == 4)
                            count = 9;

                        Console.WriteLine(j / 12);

                        if ((float)j / 12 < 1 && (float)j / 12 >= 0)
                        {
                            sampleCell = ((Excel.Worksheet)worksheet).get_Range("A"+count.ToString());
                            sampleCell.Value = lstser[j];
                            sampleCell = ((Excel.Worksheet)worksheet).get_Range("B" + count.ToString());
                            sampleCell.Value = lstmtr[j];
                            count++;
                        }
                        if ((float)j / 12 < 2 && (float)j / 12 >= 1)
                        {
                            sampleCell = ((Excel.Worksheet)worksheet).get_Range("C" + count.ToString());
                            sampleCell.Value = lstser[j];
                            sampleCell = ((Excel.Worksheet)worksheet).get_Range("D" + count.ToString());
                            sampleCell.Value = lstmtr[j];
                            count++;
                        }
                        if ((float)j / 12 < 3 && (float)j / 12 >= 2)
                        {
                            sampleCell = ((Excel.Worksheet)worksheet).get_Range("E" + count.ToString());
                            sampleCell.Value = lstser[j];
                            sampleCell = ((Excel.Worksheet)worksheet).get_Range("F" + count.ToString());
                            sampleCell.Value = lstmtr[j];
                            count++;

                        }
                        if ((float)j / 12 < 4 && (float)j / 12 >= 3)
                        {
                            sampleCell = ((Excel.Worksheet)worksheet).get_Range("G" + count.ToString());
                            sampleCell.Value = lstser[j];
                            sampleCell = ((Excel.Worksheet)worksheet).get_Range("H" + count.ToString());
                            sampleCell.Value = lstmtr[j];
                            count++;
                        }



                    }
                    
                    
                   
                
            }
                try
                {
                     workBook.SaveAs(excelFinalPath);
                }
                catch (COMException err) {
                    workBook.Close(false, excelFinalPath, null);
                    application.Quit();
                    Marshal.ReleaseComObject(workBook);
                    MessageBox.Show(err.Message);
                }
                


                // PdfDownload();

            }
            catch (SqlException err) { MessageBox.Show("File not Saved"); }
        }

        private void PdfDownload()
        {
            string path = @"D:\DemoExample\XmlFileuse4.xlsx";
            using (WebClient client = new WebClient())
            {
                client.DownloadFile(path, @"D:\DemoExample\"+cno+".xlsx");
            }
            if (File.Exists(@"D:\DemoExample\XmlFileuse4.xlsx"))
            {
                Console.WriteLine("File Downloaded Successfully");

                //Print Excel Sheet
                using (var pd = new System.Windows.Forms.PrintDialog())
                {
                    pd.ShowDialog();
                    var info = new ProcessStartInfo()
                    {
                        Verb = "print",
                        CreateNoWindow = true,
                        FileName = @"D:\DemoExample\" + cno + ".xlsx",
                        WindowStyle = ProcessWindowStyle.Hidden
                    };
                    Process.Start(info);
                }

                //Open file
                /*  using (var fileStream = new FileStream(@"G:\DemoExample\Book1.xlsx", FileMode.Open))
                  {
                      // read from file or write to file
                  }*/
            }
            else
            {
                Console.WriteLine("Not able to download the file.");
            }
        }
    }

}
