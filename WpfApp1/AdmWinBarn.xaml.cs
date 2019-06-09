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

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for AdmWinBarn.xaml
    /// </summary>
    public partial class AdmWinBarn : Window
    {
        public AdmWinBarn()
        {
            InitializeComponent();

            DbOperations db = new DbOperations();
            List<Barn> bs = db.GetAllBarn();
            listBarn.ItemsSource = bs;
        }

        private void btnAddBarn_Click(object sender, RoutedEventArgs e)
        {
            DbOperations db = new DbOperations();
            db.AddNewBarn(txtBfornamn.Text, txtBefternamn.Text, txtBlokal.Text, int.Parse(txtBavd.Text)); // Lägger till nytt barn

            listBarn.ItemsSource = null;
            List<Barn> bs = db.GetAllBarn(); // refreshar barnlistan 
            listBarn.ItemsSource = bs;
            MessageBox.Show("Vårdnadshavare registrerad!");
        }

        private void btnKopplaWin_Click(object sender, RoutedEventArgs e)
        {
            AdmWinKoppla w = new AdmWinKoppla();
            w.Show();
            this.Close();
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mw = new MainWindow();
            mw.Show();
            this.Close();
        }
    }
}
