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
    /// Interaction logic for CzasyOdjazdow.xaml
    /// Klasa odpowiada za wyświetlenie tabeli czasyodjazdow oraz
    /// za dodanie i usunięcie czasu odjazdów
    /// </summary>
    public partial class CzasyOdjazdow : Window
    {
        RozkladJazdyKMEntities1 bazaDanych = new RozkladJazdyKMEntities1();
        System.Windows.Data.CollectionViewSource czasyodjazdowViewSource;
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
            czasyodjazdowViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("czasyodjazdowViewSource")));
            czasyodjazdowViewSource.View.MoveCurrentToFirst();

            var query =
                from czasy in bazaDanych.czasyodjazdow
                select czasy;

            czasyodjazdowDataGrid.ItemsSource = query.ToList();
        }

        private void AddAndSaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (idprzejazduTextBox.Text == String.Empty ||
                 dniTextBox.Text == String.Empty ||
                 idczasuTextBox.Text == String.Empty ||
                 czasTextBox.Text == String.Empty)
            {
                MessageBox.Show("Uzupełnij wszystkie pola");
            }
            else
            {
                czasyodjazdow nowyOdjazd = new czasyodjazdow()
                {
                    idprzejazdu = (short)Convert.ToInt16(idprzejazduTextBox.Text),
                    dni = (short)Convert.ToInt16(dniTextBox.Text),
                    idczasu = (int)Convert.ToInt32(idczasuTextBox.Text),
                    czas = TimeSpan.Parse(czasTextBox.Text)
                };

                bazaDanych.czasyodjazdow.Add(nowyOdjazd);
                bazaDanych.SaveChanges();

                czasyodjazdowDataGrid.ItemsSource = bazaDanych.czasyodjazdow.ToList();

                idprzejazduTextBox.Text = String.Empty;
                dniTextBox.Text = String.Empty;
                idczasuTextBox.Text = String.Empty;
                czasTextBox.Text = String.Empty; 
            }

        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            idprzejazduTextBox.Text = String.Empty;
            dniTextBox.Text = String.Empty;
            idczasuTextBox.Text = String.Empty;
            czasTextBox.Text = String.Empty;

            this.Close();
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void DeleteRow_Click(object sender, RoutedEventArgs e)
        {
            int czasID = (czasyodjazdowDataGrid.SelectedItem as czasyodjazdow).idczasu;
            czasyodjazdow czas = bazaDanych.czasyodjazdow.Where(c => c.idczasu == czasID).Single();
            bazaDanych.czasyodjazdow.Remove(czas);
            bazaDanych.SaveChanges();

            this.czasyodjazdowDataGrid.ItemsSource = bazaDanych.czasyodjazdow.ToList();

        }

        private void DeleteCzas(czasyodjazdow czasy)
        {
            //if (dni != null)
            //{
            var czas = (from c in bazaDanych.czasyodjazdow.Local
                         where c.idczasu == czasy.idczasu
                         select c).FirstOrDefault();


            bazaDanych.czasyodjazdow.Remove(czas);
            bazaDanych.SaveChanges();

            czasyodjazdowViewSource.View.Refresh();
            //}

        }

        private void DeleteCommandHandler(object sender, ExecutedRoutedEventArgs e)
        {
            czasyodjazdow co = e.Parameter as czasyodjazdow;
            DeleteCzas(co);
        }
    }
}
