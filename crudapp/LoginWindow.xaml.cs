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
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
        }

        private void Zaloguj_Button_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection sqlCon = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB; Initial Catalog=RozkladJazdyKM; Integrated Security=True;");
            try
            {
                if (sqlCon.State == ConnectionState.Closed)
                {
                    sqlCon.Open();
                    
                    String query = "SELECT COUNT(1) FROM Uzytkownik WHERE NazwaUzytkownika=@NazwaUzytkownika AND Haslo=@Haslo";
                    SqlCommand command = new SqlCommand(query, sqlCon);
                    command.CommandType = CommandType.Text;
                    command.Parameters.AddWithValue("@NazwaUzytkownika", NazwaUzytkownika_Box.Text);
                    command.Parameters.AddWithValue("@Haslo", Haslo_PasswordBox.Password);
                    int count = Convert.ToInt32(command.ExecuteScalar());
                    if(count == 1)
                    {
                        MainWindow window = new MainWindow();
                        window.Show();
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Nazwa uzytkownika lub hasło jest nieprawidłowe");
                    }
                    
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                sqlCon.Close();
            }
        }
    }
}
