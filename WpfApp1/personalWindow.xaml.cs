using Npgsql;
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
    /// Interaction logic for personalWindow.xaml
    /// </summary>
    public partial class personalWindow : Window
    {
        public personalWindow()
        {
            InitializeComponent();
            List<Personal> personal = db.GetAllPersonal();
            listBoxPersonal.ItemsSource = null;
            listBoxPersonal.ItemsSource = personal;
        }
        Personal p;
        DbOperations db = new DbOperations();
        Personal selectedPersonal;

        private void btn_vardWindow(object sender, RoutedEventArgs e)
        {
            MainWindow mw = new MainWindow();
            mw.Show();
            this.Close();
        }

        private void ListBoxPersonal_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selectedPersonal = (Personal)listBoxPersonal.SelectedItem;
            lblAvdelning.Content = selectedPersonal.avdelning;

            if(selectedPersonal.avdelning == 1)
            {
                listBoxBarn.ItemsSource = null;
                listBoxBarn.ItemsSource = db.GetBarnAvdelning1();
            }
            else if(selectedPersonal.avdelning == 2)
            {
                listBoxBarn.ItemsSource = null;
                listBoxBarn.ItemsSource = db.GetBarnAvdelning2();
            }
            else if(selectedPersonal.avdelning == 3)
            {
                listBoxBarn.ItemsSource = null;
                listBoxBarn.ItemsSource = db.GetBarnAvdelning3();
            }
            else
            {
                listBoxBarn.ItemsSource = null;
                listBoxBarn.ItemsSource = db.GetBarnAvdelning4();
            }
            
        }

        private void ListBoxBarn_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void ListViewPersonal_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
        }
    }
}
