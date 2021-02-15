using System;
using System.Collections.Generic;
using System.Data;
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
    /// Interaction logic for InformacjeOLinii.xaml
    /// </summary>
    public partial class InformacjeOLinii : Window
    {
        //private readonly RozkladJazdyKMEntities dataEntities = new RozkladJazdyKMEntities();
        private readonly DataTable dtRelacje = new DataTable("relacje");
        private readonly DataTable dtDni = new DataTable("dni");
        //private readonly DataTable dtPrzejazdy = new DataTable("przejazdy");
        public InformacjeOLinii(string number)
        {
            InitializeComponent();

            string idRelacji;
            idRelacji = $"idrelacji={Convert.ToUInt16(number)}";
            DataRow[] drRelacje = dtRelacje.Select(idRelacji);
            foreach (DataRow row in drRelacje)
            {
                idostatniegoprzystankuTextBox.Text = row["idostatniegoprzystanku"].ToString();
                idpierwszegoprzystankuTextBox.Text = row["idpierwszegoprzystanku"].ToString();
                nazwaTextBox.Text = row["numerLinii"].ToString();
            }

            string idDni;
            idDni = $"idDni={Convert.ToUInt16(number)}";
            DataRow[] drDni = dtDni.Select(idDni);
            foreach (DataRow row in drDni)
            {
               // czynocnyCheckBox.IsChecked = row["czynocny"].ToString());
                tydzienTextBox.Text = row["tydzien"].ToString();

            }

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            System.Windows.Data.CollectionViewSource relacjeViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("relacjeViewSource")));
            // Load data by setting the CollectionViewSource.Source property:
            // relacjeViewSource.Source = [generic data source]
            System.Windows.Data.CollectionViewSource rodzajeliniiViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("rodzajeliniiViewSource")));
            // Load data by setting the CollectionViewSource.Source property:
            // rodzajeliniiViewSource.Source = [generic data source]
            System.Windows.Data.CollectionViewSource dniViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("dniViewSource")));
            // Load data by setting the CollectionViewSource.Source property:
            // dniViewSource.Source = [generic data source]
            System.Windows.Data.CollectionViewSource czasyodjazdowViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("czasyodjazdowViewSource")));
            // Load data by setting the CollectionViewSource.Source property:
            // czasyodjazdowViewSource.Source = [generic data source]

           
        }


    }
}
