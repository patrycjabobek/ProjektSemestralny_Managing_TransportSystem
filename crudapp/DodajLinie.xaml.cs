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
    /// Interaction logic for DodajLinie.xaml
    /// </summary>
    public partial class DodajLinie : Window
    {
        public DodajLinie()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            System.Windows.Data.CollectionViewSource relacjeViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("relacjeViewSource")));
            // Load data by setting the CollectionViewSource.Source property:
            // relacjeViewSource.Source = [generic data source]
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {

        }

    }
}
