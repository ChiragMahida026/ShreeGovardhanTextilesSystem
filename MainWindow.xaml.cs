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
using ShreeGovardhanTextilesSystem.Pages;
using System.Diagnostics;
//using UIKitTutorials.Pages;

namespace ShreeGovardhanTextilesSystem
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private DragAndDrop dragAndDropWindow;

        public MainWindow()
        {
            InitializeComponent();
            dragAndDropWindow = new DragAndDrop(this);
        }

        private new void PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
          
            this.OnMouseLeftButtonDown(sender, e);
        }

        private void OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
           
            dragAndDropWindow.OnMouseLeftButtonDown(sender, e);
        }

        private void OnMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
           
            dragAndDropWindow.OnMouseLeftButtonUp(sender, e);
        }

        private void OnMouseMove(object sender, MouseEventArgs e)
        {
            
            dragAndDropWindow.OnMouseMove(sender, e);
        }


        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btnRestore_Click(object sender, RoutedEventArgs e)
        {
            if (WindowState == WindowState.Normal)
                WindowState = WindowState.Maximized;
            else
                WindowState = WindowState.Normal;
        }

        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void rdPurchases_Click(object sender, RoutedEventArgs e)
        {
           // PagesNavigation.Navigate(new HomePage());
            
            PagesNavigation.Navigate(new System.Uri("Pages/Purchases.xaml", UriKind.RelativeOrAbsolute));
        }

        private void rdProduction_Click(object sender, RoutedEventArgs e)
        {
            PagesNavigation.Navigate(new System.Uri("Pages/ProductionPage.xaml", UriKind.RelativeOrAbsolute));
        }

        private void rdAddCompany_Click(object sender, RoutedEventArgs e)
        {
            PagesNavigation.Navigate(new System.Uri("Pages/AddCompanyPage.xaml", UriKind.RelativeOrAbsolute));
        }

        private void rdBackup_Click(object sender, RoutedEventArgs e)
        {
            PagesNavigation.Navigate(new System.Uri("Pages/BackupPage.xaml", UriKind.RelativeOrAbsolute));
        }
        private void rdReport_Click(object sender, RoutedEventArgs e)
        {
            PagesNavigation.Navigate(new System.Uri("Pages/ReportPage.xaml", UriKind.RelativeOrAbsolute));
        }
    }
}
