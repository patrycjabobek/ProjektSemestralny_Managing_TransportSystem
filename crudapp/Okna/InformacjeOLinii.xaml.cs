using crudapp.BazaDanych;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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
    /// Interaction logic for InformacjeOLinii.xaml
    /// Klasa odpowiada za wyświetlenie w textboxach danych z tabeli relacje
    /// dotyczących aktualnie wybranej linii
    /// </summary>
    public partial class InformacjeOLinii : Window
    {
        readonly RozkladJazdyKMEntities1 dataEntities = new RozkladJazdyKMEntities1();
        RozkladJazdyKMDataSet dataset = new RozkladJazdyKMDataSet();

        private string numerLinii;

        /// <summary>
        /// Konstruktor klasy InformacjeOLinie z jednym parametrem
        /// </summary>
        /// <param name="numerLinii"></param>
        public InformacjeOLinii(string numerLinii)
        {
            InitializeComponent();

            this.numerLinii = numerLinii;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            crudapp.BazaDanych.RozkladJazdyKMDataSet rozkladJazdyKMDataSet = ((crudapp.BazaDanych.RozkladJazdyKMDataSet)(this.FindResource("rozkladJazdyKMDataSet")));
            // Load data into the table relacje. You can modify this code as needed.
            crudapp.BazaDanych.RozkladJazdyKMDataSetTableAdapters.relacjeTableAdapter rozkladJazdyKMDataSetrelacjeTableAdapter = new crudapp.BazaDanych.RozkladJazdyKMDataSetTableAdapters.relacjeTableAdapter();
            rozkladJazdyKMDataSetrelacjeTableAdapter.Fill(rozkladJazdyKMDataSet.relacje);
            System.Windows.Data.CollectionViewSource relacjeViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("relacjeViewSource")));
            relacjeViewSource.View.MoveCurrentToFirst();
            // Load data into the table dni. You can modify this code as needed.
            crudapp.BazaDanych.RozkladJazdyKMDataSetTableAdapters.dniTableAdapter rozkladJazdyKMDataSetdniTableAdapter = new crudapp.BazaDanych.RozkladJazdyKMDataSetTableAdapters.dniTableAdapter();
            rozkladJazdyKMDataSetdniTableAdapter.Fill(rozkladJazdyKMDataSet.dni);
            System.Windows.Data.CollectionViewSource dniViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("dniViewSource")));
            dniViewSource.View.MoveCurrentToFirst();
            // Load data into the table przejazdy. You can modify this code as needed.
            crudapp.BazaDanych.RozkladJazdyKMDataSetTableAdapters.przejazdyTableAdapter rozkladJazdyKMDataSetprzejazdyTableAdapter = new crudapp.BazaDanych.RozkladJazdyKMDataSetTableAdapters.przejazdyTableAdapter();
            rozkladJazdyKMDataSetprzejazdyTableAdapter.Fill(rozkladJazdyKMDataSet.przejazdy);
            System.Windows.Data.CollectionViewSource relacjeprzejazdyViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("relacjeprzejazdyViewSource")));
            relacjeprzejazdyViewSource.View.MoveCurrentToFirst();
            // Load data into the table rodzajelinii. You can modify this code as needed.
            crudapp.BazaDanych.RozkladJazdyKMDataSetTableAdapters.rodzajeliniiTableAdapter rozkladJazdyKMDataSetrodzajeliniiTableAdapter = new crudapp.BazaDanych.RozkladJazdyKMDataSetTableAdapters.rodzajeliniiTableAdapter();
            rozkladJazdyKMDataSetrodzajeliniiTableAdapter.Fill(rozkladJazdyKMDataSet.rodzajelinii);
            System.Windows.Data.CollectionViewSource rodzajeliniiViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("rodzajeliniiViewSource")));
            rodzajeliniiViewSource.View.MoveCurrentToFirst();

            ClearTextBoxes();

            FillTextBoxesWithData(numerLinii);
        }

        private void ClearTextBoxes()
        {
            idrelacjiTextBox1.Text = "";
            numerliniiTextBox.Text = "";
            idostatniegoprzystankuTextBox.Text = "";
            idpierwszegoprzystankuTextBox.Text = "";
            
        }

        private void FillTextBoxesWithData(string numer)
        {
            titleTextBox.Text = numer;

            SqlConnection con1 = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=RozkladJazdyKM;Integrated Security=True");
            _ = new DataTable();
            con1.Open();
            SqlCommand myCommand = new SqlCommand("select * from relacje where idrelacji='" + numer + "'", con1);

            SqlDataReader myReader = myCommand.ExecuteReader();

            while (myReader.Read())
            {
                idrelacjiTextBox1.Text = (myReader["idrelacji"].ToString());
                idostatniegoprzystankuTextBox.Text = (myReader["idostatniegoprzystanku"].ToString());
                idpierwszegoprzystankuTextBox.Text = (myReader["idpierwszegoprzystanku"].ToString());
                numerliniiTextBox.Text = (myReader["numerLinii"].ToString());
                
            }
            con1.Close();   
        }
    }
}
