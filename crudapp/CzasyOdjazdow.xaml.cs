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
    /// Interaction logic for CzasyOdjazdow.xaml
    /// </summary>
    public partial class CzasyOdjazdow : Window
    {
        public CzasyOdjazdow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            crudapp.BazaDanych.RozkladJazdyKMDataSet rozkladJazdyKMDataSet = ((crudapp.BazaDanych.RozkladJazdyKMDataSet)(this.FindResource("rozkladJazdyKMDataSet")));
            // Load data into the table czasyodjazdow. You can modify this code as needed.
            crudapp.BazaDanych.RozkladJazdyKMDataSetTableAdapters.czasyodjazdowTableAdapter rozkladJazdyKMDataSetczasyodjazdowTableAdapter = new crudapp.BazaDanych.RozkladJazdyKMDataSetTableAdapters.czasyodjazdowTableAdapter();
            rozkladJazdyKMDataSetczasyodjazdowTableAdapter.Fill(rozkladJazdyKMDataSet.czasyodjazdow);
            System.Windows.Data.CollectionViewSource czasyodjazdowViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("czasyodjazdowViewSource")));
            czasyodjazdowViewSource.View.MoveCurrentToFirst();
        }
    }
}
