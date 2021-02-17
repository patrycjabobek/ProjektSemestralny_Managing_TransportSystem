using crudapp.BazaDanych;
using System;
using System.Collections.Generic;
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
    /// Interaction logic for RodzajeLinii.xaml
    /// </summary>
    public partial class RodzajeLinii : Window
    {
        RozkladJazdyKMEntities1 bazaDanych = new RozkladJazdyKMEntities1();
        System.Windows.Data.CollectionViewSource rodzajeliniiViewSource;
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
            rodzajeliniiViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("rodzajeliniiViewSource")));
            rodzajeliniiViewSource.View.MoveCurrentToFirst();

            var query =
                from rodzaje in bazaDanych.rodzajelinii
                select rodzaje;

            rodzajeliniiDataGrid.ItemsSource = query.ToList();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            numerliniiTextBox.Text = String.Empty;
            nazwaTextBox.Text = String.Empty;

            this.Close();
        }
        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void AddAndSaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (numerliniiTextBox.Text == String.Empty ||
                 nazwaTextBox.Text == String.Empty)
            {
                MessageBox.Show("Uzupełnij wszystkie pola");
            }
            else
            {
                rodzajelinii nowyRodzajLinii = new rodzajelinii()
                {
                   numerlinii  = (short)Convert.ToInt16(numerliniiTextBox.Text),
                   nazwa = nazwaTextBox.Text
                };

                bazaDanych.rodzajelinii.Add(nowyRodzajLinii);
                bazaDanych.SaveChanges();

                rodzajeliniiDataGrid.ItemsSource = bazaDanych.rodzajelinii.ToList();

                numerliniiTextBox.Text = String.Empty;
                nazwaTextBox.Text = String.Empty;

            }

        }

        private void DeleteRow_Click(object sender, RoutedEventArgs e)
        {
            int rodzajlinii = (rodzajeliniiDataGrid.SelectedItem as rodzajelinii).numerlinii;
            rodzajelinii rodzaj = (from r in bazaDanych.rodzajelinii where r.numerlinii == rodzajlinii select r).SingleOrDefault();
            bazaDanych.rodzajelinii.Remove(rodzaj);
            bazaDanych.SaveChanges();
        }


        //private void DeleteRodzajeLinii(rodzajelinii rodzaje)
        //{
        //    //if (rodzaje != null)
        //    //{
        //        var rodzaj = (from r in bazaDanych.rodzajelinii.Local
        //                       where r.numerlinii == rodzaje.numerlinii
        //                       select r).FirstOrDefault();

        //        foreach (var item in rodzaj.relacje.ToList())
        //        {
        //            bazaDanych.relacje.Remove(item);
        //        }

        //        bazaDanych.rodzajelinii.Remove(rodzaj);
        //        bazaDanych.SaveChanges();

        //        rodzajeliniiViewSource.View.Refresh();
        //    //}

        //}

        //private void DeleteCommandHandler(object sender, ExecutedRoutedEventArgs e)
        //{
        //    rodzajelinii rodz = e.Parameter as rodzajelinii;
        //    DeleteRodzajeLinii(rodz);
        //}

    }
}
