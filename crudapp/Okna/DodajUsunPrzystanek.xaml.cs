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
    /// Interaction logic for DodajPrzystanek.xaml
    /// Klasa odpowiada za wyświetlenie tabeli przystanki oraz
    /// za dodanie i usunięcie przystanku
    /// </summary>
    public partial class DodajPrzystanek : Window
    {
        RozkladJazdyKMEntities1 bazaDanych = new RozkladJazdyKMEntities1();
        System.Windows.Data.CollectionViewSource przystankiViewSource;

        /// <summary>
        /// Konstruktor klasy DodajPrzystanek
        /// </summary>
        public DodajPrzystanek()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            przystankiViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("przystankiViewSource")));
            // Load data by setting the CollectionViewSource.Source property:
            // przystankiViewSource.Source = [generic data source]
            crudapp.BazaDanych.RozkladJazdyKMDataSet rozkladJazdyKMDataSet = ((crudapp.BazaDanych.RozkladJazdyKMDataSet)(this.FindResource("rozkladJazdyKMDataSet")));
            // Load data into the table przystanki. You can modify this code as needed.
            crudapp.BazaDanych.RozkladJazdyKMDataSetTableAdapters.przystankiTableAdapter rozkladJazdyKMDataSetprzystankiTableAdapter = new crudapp.BazaDanych.RozkladJazdyKMDataSetTableAdapters.przystankiTableAdapter();
            rozkladJazdyKMDataSetprzystankiTableAdapter.Fill(rozkladJazdyKMDataSet.przystanki);

            var query =
                from przystanek in bazaDanych.przystanki
                select przystanek;

            przystankiDataGrid.ItemsSource = query.ToList();
        }

        private void AddAndSaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (idprzystankuTextBox.Text == String.Empty ||
                 nazwaTextBox.Text == String.Empty)
            {
                MessageBox.Show("Uzupełnij wszystkie pola");
            }
            else
            {
                przystanki nowyPrzystanek = new przystanki()
                {
                    idprzystanku = (short)Convert.ToInt16(idprzystankuTextBox.Text),
                    nazwa = nazwaTextBox.Text                    
                };

                bazaDanych.przystanki.Add(nowyPrzystanek);
                bazaDanych.SaveChanges();

                przystankiDataGrid.ItemsSource = bazaDanych.przystanki.ToList();

                idprzystankuTextBox.Text = String.Empty;
                nazwaTextBox.Text = String.Empty;

            }
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            idprzystankuTextBox.Text = String.Empty;
            nazwaTextBox.Text = String.Empty;

            this.Close();
        }

        private void DeleteRow_Click(object sender, RoutedEventArgs e)
        {
            int przystanekID = (przystankiDataGrid.SelectedItem as przystanki).idprzystanku;
            przystanki przystanek = bazaDanych.przystanki.Where(p => p.idprzystanku == przystanekID).Single();
            bazaDanych.przystanki.Remove(przystanek);
            bazaDanych.SaveChanges();

            this.przystankiDataGrid.ItemsSource = bazaDanych.przystanki.ToList();
        }

        private void DeletePrzystanek(przystanki przystanki)
        {
            //if (dni != null)
            //{
            var przystanek = (from p in bazaDanych.przystanki.Local
                        where p.idprzystanku == przystanki.idprzystanku
                        select p).FirstOrDefault();

            foreach (var item in przystanek.relacje.ToList())
            {
                bazaDanych.relacje.Remove(item);
            }

            foreach (var item in przystanek.przejazdy.ToList())
            {
                bazaDanych.przejazdy.Remove(item);
            }

            bazaDanych.przystanki.Remove(przystanek);
            bazaDanych.SaveChanges();

            przystankiViewSource.View.Refresh();
            //}

        }

        private void DeleteCommandHandler(object sender, ExecutedRoutedEventArgs e)
        {
            przystanki p = e.Parameter as przystanki;
            DeletePrzystanek(p);
        }
    }
}
