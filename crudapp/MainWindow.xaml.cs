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

namespace crudapp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            System.Windows.Data.CollectionViewSource relacjeViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("relacjeViewSource")));
            // Load data by setting the CollectionViewSource.Source property:
            // relacjeViewSource.Source = [generic data source]
        }

        private void ViewButton_Click(object sender, RoutedEventArgs e)
        {
            string number = (string)(sender as Button).Content;
            InformacjeOLinii info = new InformacjeOLinii(number);

            info.Show();
        }

        private void ViewAllButton_Click(object sender, RoutedEventArgs e)
        {
            WszystkieLinie all = new WszystkieLinie();
            all.Show();
        }
    }

}
