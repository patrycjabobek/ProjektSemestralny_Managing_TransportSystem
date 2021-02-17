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
    /// Interaction logic for DodajPrzejazd.xaml
    /// </summary>
    public partial class DodajPrzejazd : Window
    {
        readonly RozkladJazdyKMEntities1 bazaDanych = new RozkladJazdyKMEntities1();
        System.Windows.Data.CollectionViewSource przejazdyViewSource;
        public DodajPrzejazd()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            crudapp.BazaDanych.RozkladJazdyKMDataSet rozkladJazdyKMDataSet = ((crudapp.BazaDanych.RozkladJazdyKMDataSet)(this.FindResource("rozkladJazdyKMDataSet")));
            // Load data into the table przejazdy. You can modify this code as needed.
            crudapp.BazaDanych.RozkladJazdyKMDataSetTableAdapters.przejazdyTableAdapter rozkladJazdyKMDataSetprzejazdyTableAdapter = new crudapp.BazaDanych.RozkladJazdyKMDataSetTableAdapters.przejazdyTableAdapter();
            rozkladJazdyKMDataSetprzejazdyTableAdapter.Fill(rozkladJazdyKMDataSet.przejazdy);
            przejazdyViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("przejazdyViewSource")));
            przejazdyViewSource.View.MoveCurrentToFirst();

            var query =
                from przejazd in bazaDanych.przejazdy
                select przejazd;

            przejazdyDataGrid.ItemsSource = query.ToList();
        }
        private void AddAndSaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (idprzejazduTextBox.Text == String.Empty ||
                 idrelacjiTextBox.Text == String.Empty ||
                 idprzystankuTextBox.Text == String.Empty)
            {
                MessageBox.Show("Uzupełnij wszystkie pola");
            }
            else
            {
                przejazdy nowyPrzejazd = new przejazdy()
                {
                    idprzejazdu = (short)Convert.ToInt16(idprzejazduTextBox.Text),
                    idrelacji = (short)Convert.ToInt16(idrelacjiTextBox.Text),
                    idprzystanku = (short)Convert.ToInt16(idprzystankuTextBox.Text)
                };

                bazaDanych.przejazdy.Add(nowyPrzejazd);
                bazaDanych.SaveChanges();

                przejazdyDataGrid.ItemsSource = bazaDanych.przejazdy.ToList();

                idprzejazduTextBox.Text = String.Empty;
                idrelacjiTextBox.Text = String.Empty;
                idprzystankuTextBox.Text = String.Empty;

            }
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            idprzejazduTextBox.Text = String.Empty;
            idrelacjiTextBox.Text = String.Empty;
            idprzystankuTextBox.Text = String.Empty;

            this.Close();
        }

        private void DeletePrzejazd(przejazdy prz)
        {
            //if (dni != null)
            //{
            var przejazd = (from p in bazaDanych.przejazdy.Local
                        where p.idprzejazdu == prz.idprzejazdu
                            select p).FirstOrDefault();

            foreach (var item in przejazd.czasyodjazdow.ToList())
            {
                bazaDanych.czasyodjazdow.Remove(item);
            }

            bazaDanych.przejazdy.Remove(przejazd);
            bazaDanych.SaveChanges();

            przejazdyViewSource.View.Refresh();
            //}

        }

        private void DeleteCommandHandler(object sender, ExecutedRoutedEventArgs e)
        {
            przejazdy p = e.Parameter as przejazdy;
            DeletePrzejazd(p);
        }


    }
}
