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
    /// Interaction logic for Dni.xaml
    /// </summary>
    public partial class Dni : Window
    {
        public Dni()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            crudapp.BazaDanych.RozkladJazdyKMDataSet rozkladJazdyKMDataSet = ((crudapp.BazaDanych.RozkladJazdyKMDataSet)(this.FindResource("rozkladJazdyKMDataSet")));
            // Load data into the table dni. You can modify this code as needed.
            crudapp.BazaDanych.RozkladJazdyKMDataSetTableAdapters.dniTableAdapter rozkladJazdyKMDataSetdniTableAdapter = new crudapp.BazaDanych.RozkladJazdyKMDataSetTableAdapters.dniTableAdapter();
            rozkladJazdyKMDataSetdniTableAdapter.Fill(rozkladJazdyKMDataSet.dni);
            System.Windows.Data.CollectionViewSource dniViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("dniViewSource")));
            dniViewSource.View.MoveCurrentToFirst();

        }
    }
}
