using crudapp.BazaDanych;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Klasa odpowiada za wyświetlenie tabeli roelacje, przejazdy,
    /// przystanki oraz za wyświetlenie przycisków
    /// </summary>
    public partial class WszystkieLinie : Window
    {
        readonly RozkladJazdyKMEntities1 dataEntities = new RozkladJazdyKMEntities1();

        /// <summary>
        /// Konstruktor klasy WszystkieLinie
        /// </summary>
        public WszystkieLinie()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var query =
                 from relacja in dataEntities.relacje
                 select relacja;

            relacjeDataGrid.ItemsSource = query.ToList();

            var query2 =
                from przejazd in dataEntities.przejazdy
                select przejazd;

            przejazdyDataGrid.ItemsSource = query2.ToList();

            var query3 =
                from przystanek in dataEntities.przystanki
                select przystanek;

            przystankiDataGrid.ItemsSource = query3.ToList();

            crudapp.BazaDanych.RozkladJazdyKMDataSet rozkladJazdyKMDataSet = ((crudapp.BazaDanych.RozkladJazdyKMDataSet)(this.FindResource("rozkladJazdyKMDataSet")));
            // Load data into the table relacje. You can modify this code as needed.
            crudapp.BazaDanych.RozkladJazdyKMDataSetTableAdapters.relacjeTableAdapter rozkladJazdyKMDataSetrelacjeTableAdapter = new crudapp.BazaDanych.RozkladJazdyKMDataSetTableAdapters.relacjeTableAdapter();
            rozkladJazdyKMDataSetrelacjeTableAdapter.Fill(rozkladJazdyKMDataSet.relacje);
            System.Windows.Data.CollectionViewSource relacjeViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("relacjeViewSource")));
            relacjeViewSource.View.MoveCurrentToFirst();
            // Load data into the table przejazdy. You can modify this code as needed.
            crudapp.BazaDanych.RozkladJazdyKMDataSetTableAdapters.przejazdyTableAdapter rozkladJazdyKMDataSetprzejazdyTableAdapter = new crudapp.BazaDanych.RozkladJazdyKMDataSetTableAdapters.przejazdyTableAdapter();
            rozkladJazdyKMDataSetprzejazdyTableAdapter.Fill(rozkladJazdyKMDataSet.przejazdy);
            System.Windows.Data.CollectionViewSource przejazdyViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("przejazdyViewSource")));
            przejazdyViewSource.View.MoveCurrentToFirst();
            // Load data into the table przystanki. You can modify this code as needed.
            crudapp.BazaDanych.RozkladJazdyKMDataSetTableAdapters.przystankiTableAdapter rozkladJazdyKMDataSetprzystankiTableAdapter = new crudapp.BazaDanych.RozkladJazdyKMDataSetTableAdapters.przystankiTableAdapter();
            rozkladJazdyKMDataSetprzystankiTableAdapter.Fill(rozkladJazdyKMDataSet.przystanki);
            System.Windows.Data.CollectionViewSource przystankiViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("przystankiViewSource")));
            przystankiViewSource.View.MoveCurrentToFirst();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnDodajLinie_Click(object sender, RoutedEventArgs e)
        {
            DodajLinie dL = new DodajLinie();
            dL.Show();
        }

        private void BtnDodajPrzystanek_Click(object sender, RoutedEventArgs e)
        {
            DodajPrzystanek dp = new DodajPrzystanek();
            dp.Show();
        }

        private void BtnDodajPrzejazd_Click(object sender, RoutedEventArgs e)
        {
            DodajPrzejazd dp = new DodajPrzejazd();
            dp.Show();
        }


        private void LeaveBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }


    }
}
