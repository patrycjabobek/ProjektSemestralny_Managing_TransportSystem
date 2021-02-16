using crudapp.BazaDanych;
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
        readonly RozkladJazdyKMEntities1 dataEntities = new RozkladJazdyKMEntities1();
        private readonly DataTable dtRelacje = new DataTable("relacje");
        private readonly DataTable dtDni = new DataTable("dni");
        //private readonly DataTable dtPrzejazdy = new DataTable("przejazdy");
        public InformacjeOLinii(string number)
        {
            InitializeComponent();

            titleTextBox.Text = number;
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
            crudapp.BazaDanych.RozkladJazdyKMDataSet rozkladJazdyKMDataSet = ((crudapp.BazaDanych.RozkladJazdyKMDataSet)(this.FindResource("rozkladJazdyKMDataSet")));
            // Load data into the table relacje. You can modify this code as needed.
            crudapp.BazaDanych.RozkladJazdyKMDataSetTableAdapters.relacjeTableAdapter rozkladJazdyKMDataSetrelacjeTableAdapter = new crudapp.BazaDanych.RozkladJazdyKMDataSetTableAdapters.relacjeTableAdapter();
            rozkladJazdyKMDataSetrelacjeTableAdapter.Fill(rozkladJazdyKMDataSet.relacje);
            System.Windows.Data.CollectionViewSource relacjeViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("relacjeViewSource")));
            relacjeViewSource.View.MoveCurrentToFirst();
            // Load data into the table dni. You can modify this code as needed.
            crudapp.BazaDanych.RozkladJazdyKMDataSetTableAdapters.dniTableAdapter rozkladJazdyKMDataSetdniTableAdapter = new crudapp.BazaDanych.RozkladJazdyKMDataSetTableAdapters.dniTableAdapter();
            rozkladJazdyKMDataSetdniTableAdapter.Fill(rozkladJazdyKMDataSet.dni);
            System.Windows.Data.CollectionViewSource dniViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("dniViewSource")));
            dniViewSource.View.MoveCurrentToFirst();
            // Load data into the table przejazdy. You can modify this code as needed.
            crudapp.BazaDanych.RozkladJazdyKMDataSetTableAdapters.przejazdyTableAdapter rozkladJazdyKMDataSetprzejazdyTableAdapter = new crudapp.BazaDanych.RozkladJazdyKMDataSetTableAdapters.przejazdyTableAdapter();
            rozkladJazdyKMDataSetprzejazdyTableAdapter.Fill(rozkladJazdyKMDataSet.przejazdy);
            System.Windows.Data.CollectionViewSource relacjeprzejazdyViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("relacjeprzejazdyViewSource")));
            relacjeprzejazdyViewSource.View.MoveCurrentToFirst();
            // Load data into the table rodzajelinii. You can modify this code as needed.
            crudapp.BazaDanych.RozkladJazdyKMDataSetTableAdapters.rodzajeliniiTableAdapter rozkladJazdyKMDataSetrodzajeliniiTableAdapter = new crudapp.BazaDanych.RozkladJazdyKMDataSetTableAdapters.rodzajeliniiTableAdapter();
            rozkladJazdyKMDataSetrodzajeliniiTableAdapter.Fill(rozkladJazdyKMDataSet.rodzajelinii);
            System.Windows.Data.CollectionViewSource rodzajeliniiViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("rodzajeliniiViewSource")));
            rodzajeliniiViewSource.View.MoveCurrentToFirst();
        }


    }
}
