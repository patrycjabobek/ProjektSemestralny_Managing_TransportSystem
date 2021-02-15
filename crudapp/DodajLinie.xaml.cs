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
        private readonly RozkladJazdyKMEntities dataEntities = new RozkladJazdyKMEntities();
        private readonly DataTable dt = new DataTable("relacje");
        private readonly WszystkieLinie wl = new WszystkieLinie();

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
        }

        private void AddAndSaveButton_Click(object sender, EventArgs e)
        {
            //DataRow dr = dt.NewRow();

            //dr["idrelacji"] = int.Parse(idrelacjiTextBox.Text);
            //dr["numerlinii"] = int.Parse(numerliniiTextBox.Text);
            //dr["idostatniegoprzystanku"] = int.Parse(idostatniegoprzystankuTextBox.Text);
            //dr["idpierwszegoprzystanku"] = int.Parse(idpierwszegoprzystankuTextBox.Text);
            //dt.Rows.Add(dr);

            
            ushort idOP = Convert.ToUInt16(idostatniegoprzystankuTextBox.Text);
            ushort idPP = Convert.ToUInt16(idpierwszegoprzystankuTextBox.Text);
            ushort idR = Convert.ToUInt16(idrelacjiTextBox.Text);
            ushort numerLinii = Convert.ToUInt16(numerliniiTextBox.Text);
            string query = "INSERT INTO relacje " +
                "VALUES(@idOP, @idPP, @idR , @numerLinii";
            using (SqlConnection connection = new SqlConnection("Data Source=(localDB)\\MSSQLLocalDB;Initial Catalog=RozkladJazdyKM;Integrated Security=SSPI"))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.Add("@idOP", SqlDbType.SmallInt).Value = idOP;
                command.Parameters.Add("@idPP", SqlDbType.SmallInt).Value = idPP;
                command.Parameters.Add("@idR", SqlDbType.SmallInt).Value = idR;
                command.Parameters.Add("@numerLinii", SqlDbType.SmallInt).Value = numerLinii;
                command.CommandType = CommandType.Text;

                try
                {

                    connection.Open();
                    command.ExecuteNonQuery();
                    MessageBox.Show("Dodano nową linię");
                }
                catch (SqlException exception)
                {
                    MessageBox.Show("Błąd: " + exception);
                }
                finally
                {
                    if (connection != null)
                        connection.Close();
                }
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
            
        }

        private void AddCommandHandler(object sender, ExecutedRoutedEventArgs e)
        {
           
        }

        private void CancelCommandHandler(object sender, ExecutedRoutedEventArgs e)
        {
            idostatniegoprzystankuTextBox.Text = "";
            idpierwszegoprzystankuTextBox.Text = "";
            numerliniiTextBox.Text = "";
            idrelacjiTextBox.Text = "";

            this.Close();
        }
    }
}
