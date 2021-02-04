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

namespace crudapp
{
    /// <summary>
    /// Interaction logic for WszystkieLinie.xaml
    /// </summary>
    public partial class WszystkieLinie : Window
    {
        RozkladJazdyKMEntities dataEntities = new RozkladJazdyKMEntities();
        public WszystkieLinie()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            System.Windows.Data.CollectionViewSource relacjeViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("relacjeViewSource")));
            // Load data by setting the CollectionViewSource.Source property:
            // relacjeViewSource.Source = [generic data source]
            var query =
                 from relacje in dataEntities.relacje
                 select new { relacje.idrelacji, relacje.numerlinii, relacje.idpierwszegoprzystanku, relacje.idostatniegoprzystanku };

            relacjeDataGrid.ItemsSource = query.ToList();

            var query2 =
                from przejazdy in dataEntities.przejazdy
                select new { przejazdy.idprzejazdu, przejazdy.idprzystanku, przejazdy.idrelacji };

            przejazdyDataGrid.ItemsSource = query2.ToList();

            var query3 =
                from przystanki in dataEntities.przystanki
                select new { przystanki.idprzystanku, przystanki.nazwa };

            przystankiDataGrid.ItemsSource = query3.ToList();
            System.Windows.Data.CollectionViewSource rozkladJazdyKMEntitiesViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("rozkladJazdyKMEntitiesViewSource")));
            // Load data by setting the CollectionViewSource.Source property:
            // rozkladJazdyKMEntitiesViewSource.Source = [generic data source]
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void DodajLinieButton_Click(object sender, RoutedEventArgs e)
        {
            DodajLinie dL = new DodajLinie();
            dL.Show();
        }
    }
}
