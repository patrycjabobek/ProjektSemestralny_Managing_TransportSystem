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
        readonly RozkladJazdyKMEntities1 bazaDanych = new RozkladJazdyKMEntities1();
        System.Windows.Data.CollectionViewSource relacjeViewSource;
        public DodajLinie()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            relacjeViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("relacjeViewSource")));
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


        private void DeleteRelacje(relacje relacje)
        {
            if (relacje != null)
            {
                var relacja = (from r in bazaDanych.relacje.Local
                               where r.idrelacji == relacje.idrelacji
                               select r).FirstOrDefault();

                foreach (var item in relacja.przejazdy.ToList())
                {
                    bazaDanych.przejazdy.Remove(item);
                }

                bazaDanych.relacje.Remove(relacja);
                bazaDanych.SaveChanges();

                relacjeViewSource.View.Refresh();
           }
  
        }

        private void DeleteCommandHandler(object sender, ExecutedRoutedEventArgs e)
        {
            relacje rel = e.Parameter as relacje;
            DeleteRelacje(rel);
        }
    }
}
