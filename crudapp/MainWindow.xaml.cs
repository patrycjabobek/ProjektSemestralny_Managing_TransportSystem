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

        private void ViewDeparturesButton_Click(object sender, RoutedEventArgs e)
        {
            CzasyOdjazdow window = new CzasyOdjazdow();
            window.Show();
        }


        private void ViewDaysButton_Click(object sender, RoutedEventArgs e)
        {
            Dni window = new Dni();
            window.Show();
        }

        private void ViewTypesOfTransportButton_Click(object sender, RoutedEventArgs e)
        {
            RodzajeLinii window = new RodzajeLinii();
            window.Show();
        }
    }

}
