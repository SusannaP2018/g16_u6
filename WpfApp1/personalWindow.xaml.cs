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

        DbOperations db = new DbOperations();
        Personal selectedPersonal;
        Barn selectedBarn;

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
                listViewBarn.ItemsSource = null;
                listViewBarn.ItemsSource = db.GetBarnAvdelning1();
            }
            else if(selectedPersonal.avdelning == 2)
            {
                listViewBarn.ItemsSource = null;
                listViewBarn.ItemsSource = db.GetBarnAvdelning2();
            }
            else if(selectedPersonal.avdelning == 3)
            {
                listViewBarn.ItemsSource = null;
                listViewBarn.ItemsSource = db.GetBarnAvdelning3();
            }
            else
            {
                listViewBarn.ItemsSource = null;
                listViewBarn.ItemsSource = db.GetBarnAvdelning4();
            }
            
        }

        private void ListViewBarn_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            selectedBarn = (Barn)listViewBarn.SelectedItem;
            lblBarnNamn.Content = selectedBarn.FirstName.ToUpper();

            listBoxVard.ItemsSource = null;
            listBoxVard.ItemsSource = db.GetVhByBarn();
        }

        private void Btn_SchemaWindow(object sender, RoutedEventArgs e)
        {
            schema_personalvy sp = new schema_personalvy();
            sp.Show();
            this.Close();
        }
    }
}
