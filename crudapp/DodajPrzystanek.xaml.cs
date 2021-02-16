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
using System.Windows.Shapes;

namespace crudapp
{
    /// <summary>
    /// Interaction logic for DodajPrzystanek.xaml
    /// </summary>
    public partial class DodajPrzystanek : Window
    {
        public DodajPrzystanek()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            System.Windows.Data.CollectionViewSource przystankiViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("przystankiViewSource")));
            // Load data by setting the CollectionViewSource.Source property:
            // przystankiViewSource.Source = [generic data source]
            crudapp.BazaDanych.RozkladJazdyKMDataSet rozkladJazdyKMDataSet = ((crudapp.BazaDanych.RozkladJazdyKMDataSet)(this.FindResource("rozkladJazdyKMDataSet")));
            // Load data into the table przystanki. You can modify this code as needed.
            crudapp.BazaDanych.RozkladJazdyKMDataSetTableAdapters.przystankiTableAdapter rozkladJazdyKMDataSetprzystankiTableAdapter = new crudapp.BazaDanych.RozkladJazdyKMDataSetTableAdapters.przystankiTableAdapter();
            rozkladJazdyKMDataSetprzystankiTableAdapter.Fill(rozkladJazdyKMDataSet.przystanki);
        }

        private void AddAndSaveButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
