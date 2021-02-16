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
    /// Interaction logic for RodzajeLinii.xaml
    /// </summary>
    public partial class RodzajeLinii : Window
    {
        public RodzajeLinii()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            crudapp.BazaDanych.RozkladJazdyKMDataSet rozkladJazdyKMDataSet = ((crudapp.BazaDanych.RozkladJazdyKMDataSet)(this.FindResource("rozkladJazdyKMDataSet")));
            // Load data into the table rodzajelinii. You can modify this code as needed.
            crudapp.BazaDanych.RozkladJazdyKMDataSetTableAdapters.rodzajeliniiTableAdapter rozkladJazdyKMDataSetrodzajeliniiTableAdapter = new crudapp.BazaDanych.RozkladJazdyKMDataSetTableAdapters.rodzajeliniiTableAdapter();
            rozkladJazdyKMDataSetrodzajeliniiTableAdapter.Fill(rozkladJazdyKMDataSet.rodzajelinii);
            System.Windows.Data.CollectionViewSource rodzajeliniiViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("rodzajeliniiViewSource")));
            rodzajeliniiViewSource.View.MoveCurrentToFirst();
        }
    }
}
