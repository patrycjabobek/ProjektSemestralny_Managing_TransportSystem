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
    /// </summary>
    public partial class DodajPrzystanek : Window
    {
        RozkladJazdyKMEntities1 bazaDanych = new RozkladJazdyKMEntities1();
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

                przystankiDataGrid.ItemsSource = bazaDanych.relacje.ToList();

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
    }
}
