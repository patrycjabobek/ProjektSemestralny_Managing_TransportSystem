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
using System.Text.RegularExpressions;
using System.Data;
using System.Data.SqlClient;
using crudapp.BazaDanych;

namespace crudapp
{

    /// <summary>
    /// Interaction logic for DodajLinie.xaml
    /// </summary>
    public partial class DodajLinie : Window
    {
        RozkladJazdyKMEntities1 bazaDanych = new RozkladJazdyKMEntities1();
        WszystkieLinie wl = new WszystkieLinie();

        public DodajLinie(WszystkieLinie wl)
        {
            InitializeComponent();
            this.wl = wl;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            System.Windows.Data.CollectionViewSource relacjeViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("relacjeViewSource")));
            // Load data by setting the CollectionViewSource.Source property:
            // relacjeViewSource.Source = [generic data source]
            crudapp.BazaDanych.RozkladJazdyKMDataSet rozkladJazdyKMDataSet = ((crudapp.BazaDanych.RozkladJazdyKMDataSet)(this.FindResource("rozkladJazdyKMDataSet")));
            // Load data into the table relacje. You can modify this code as needed.
            crudapp.BazaDanych.RozkladJazdyKMDataSetTableAdapters.relacjeTableAdapter rozkladJazdyKMDataSetrelacjeTableAdapter = new crudapp.BazaDanych.RozkladJazdyKMDataSetTableAdapters.relacjeTableAdapter();
            rozkladJazdyKMDataSetrelacjeTableAdapter.Fill(rozkladJazdyKMDataSet.relacje);

            var query =
                from relacje in bazaDanych.relacje
                select new { relacje.idrelacji, relacje.numerlinii, relacje.idpierwszegoprzystanku, relacje.idostatniegoprzystanku };

            relacjeDataGrid.ItemsSource = query.ToList();
        }

        private void AddAndSaveButton_Click(object sender, EventArgs e)
        {
            if (idrelacjiTextBox.Text == String.Empty ||
                 numerliniiTextBox.Text == String.Empty ||
                 idpierwszegoprzystankuTextBox.Text == String.Empty ||
                 idostatniegoprzystankuTextBox.Text == String.Empty)
            {
                MessageBox.Show("Uzupełnij wszystkie pola");
            }
            else
            {
                relacje nowaRelacja = new relacje()
                {
                    idrelacji = (short)Convert.ToInt16(idrelacjiTextBox.Text),
                    numerlinii = (short)Convert.ToInt16(numerliniiTextBox.Text),
                    idpierwszegoprzystanku = (short)Convert.ToInt16(idpierwszegoprzystankuTextBox.Text),
                    idostatniegoprzystanku = (short)Convert.ToInt16(idostatniegoprzystankuTextBox.Text)
                };

                bazaDanych.relacje.Add(nowaRelacja);
                bazaDanych.SaveChanges();

                relacjeDataGrid.ItemsSource = bazaDanych.relacje.ToList();

                idrelacjiTextBox.Text = String.Empty;
                numerliniiTextBox.Text = String.Empty;
                idpierwszegoprzystankuTextBox.Text = String.Empty;
                idostatniegoprzystankuTextBox.Text = String.Empty;

            }
          
        }

        // checks if input has only numbers
        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            idrelacjiTextBox.Text = String.Empty;
            numerliniiTextBox.Text = String.Empty;
            idpierwszegoprzystankuTextBox.Text = String.Empty;
            idostatniegoprzystankuTextBox.Text = String.Empty;

            this.Close();
        }

        private void DeleteRow_Click(object sender, RoutedEventArgs e)
        {
            int relacjaId = (relacjeDataGrid.SelectedItem as relacje).idrelacji;
            relacje relacje = (from r in bazaDanych.relacje where r.idrelacji == relacjaId select r).SingleOrDefault();
            bazaDanych.relacje.Remove(relacje);
            bazaDanych.SaveChanges();

            relacjeDataGrid.ItemsSource = bazaDanych.relacje.ToList();
        }
    }
}
