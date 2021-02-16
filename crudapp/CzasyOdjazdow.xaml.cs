using crudapp.BazaDanych;
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
        RozkladJazdyKMEntities1 bazaDanych = new RozkladJazdyKMEntities1();
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
                    idczasu = (short)Convert.ToInt16(idczasuTextBox.Text),
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

        private void DeleteRow_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
