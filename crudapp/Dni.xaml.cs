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
    /// Interaction logic for Dni.xaml
    /// </summary>
    public partial class Dni : Window
    {
        RozkladJazdyKMEntities1 bazaDanych = new RozkladJazdyKMEntities1();
        public Dni()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            crudapp.BazaDanych.RozkladJazdyKMDataSet rozkladJazdyKMDataSet = ((crudapp.BazaDanych.RozkladJazdyKMDataSet)(this.FindResource("rozkladJazdyKMDataSet")));
            // Load data into the table dni. You can modify this code as needed.
            crudapp.BazaDanych.RozkladJazdyKMDataSetTableAdapters.dniTableAdapter rozkladJazdyKMDataSetdniTableAdapter = new crudapp.BazaDanych.RozkladJazdyKMDataSetTableAdapters.dniTableAdapter();
            rozkladJazdyKMDataSetdniTableAdapter.Fill(rozkladJazdyKMDataSet.dni);
            System.Windows.Data.CollectionViewSource dniViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("dniViewSource")));
            dniViewSource.View.MoveCurrentToFirst();

            var query =
                from dni in bazaDanych.dni
                select dni;

            dniDataGrid.ItemsSource = query.ToList();

        }

        private void AddAndSaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (idDniTextBox.Text == String.Empty ||
                 tydzienTextBox.Text == String.Empty )
            {
                MessageBox.Show("Uzupełnij wszystkie pola");
            }
            else
            {
                dni nowyDzienOdjazdow = new dni()
                {
                    idDni = (short)Convert.ToInt16(idDniTextBox.Text),
                    tydzien = tydzienTextBox.Text,
                    czynocny = czynocnyCheckBox.IsChecked
                };

                bazaDanych.dni.Add(nowyDzienOdjazdow);
                bazaDanych.SaveChanges();

                dniDataGrid.ItemsSource = bazaDanych.dni.ToList();

                idDniTextBox.Text = String.Empty;
                tydzienTextBox.Text = String.Empty;

            }

        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            idDniTextBox.Text = String.Empty;
            tydzienTextBox.Text = String.Empty;

            this.Close();
        }

        private void DeleteRow_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
