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
using Npgsql;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for SchemalaggningWindow.xaml
    /// </summary>
    public partial class SchemalaggningWindow : Window
    {
        public SchemalaggningWindow()
        {
            InitializeComponent();
            string inloggad = "Vivi Matsson";
            lblLoggedIn.Content = "Inloggad: " + inloggad;

            List<Barn> barn = db.GetBarnByVH(3);
            listBarn.ItemsSource = null;
            listBarn.ItemsSource = barn;
        }

        DbOperations db = new DbOperations();

        private void btnBackToMain_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mw = new MainWindow();
            mw.Show();
            this.Close();
        }

        private void listBarn_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
