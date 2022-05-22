using System;
using System.Collections.Generic;
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
        public PurchasesPage()
        {
            InitializeComponent();
        }

        private void btnLastName_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void HandleCheck(object sender, RoutedEventArgs e)
        {
            RadioButton rb = sender as RadioButton;
            textBlock2.Text = "Selected Yarn : " + rb.Content;
        }
    }
}
