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
        Gatthem gh;

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

            listViewBarn.ItemsSource = null;
            listViewBarn.ItemsSource = db.GetBarnByAvdelning(selectedPersonal.avdelning);            
        }

        private void ListViewBarn_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            selectedBarn = (Barn)listViewBarn.SelectedItem;
            if (selectedBarn != null)
            {
                lblBarnNamn.Content = selectedBarn.FirstName.ToUpper();
                listViewVard.ItemsSource = null;
                listViewVard.ItemsSource = db.GetVhByBarn(selectedBarn.Id);
            }
            else
            {
                listViewVard.ItemsSource = null;
            }
        }

        private void Btn_SchemaWindow(object sender, RoutedEventArgs e)
        {
            int nr = 0;
            selectedBarn = (Barn)listViewBarn.SelectedItem;

            if (selectedBarn == null)
            {
                MessageBox.Show("Du måste välja ett barn!");
            }
            else
            {
                nr = db.BarnIDForSchema(selectedBarn.Id);

                schema_personalvy sp = new schema_personalvy();
                sp.Show();

                sp.lvSchemaPersonalVy.ItemsSource = db.GetOneBarnSchema(nr);
            }
        }

        private void BtnRegister_Click(object sender, RoutedEventArgs e)
        {
            gh = new Gatthem();  

            gh.gattHem = false;

            int max = db.gattHemIDMax();
            max++;
            try
            {
                if (checkBoxHem.IsChecked == true)
                {
                    gh.gattHem = true;

                    db.Hemgang(max, gh.gattHem, selectedBarn.Id, selectedPersonal.id);

                    MessageBox.Show(selectedBarn.FirstName.ToUpper() + " är registrerad som hemgången av: " + selectedPersonal.firstname.ToUpper());
                }
                else
                {
                    MessageBox.Show("Bocka i checkboxen om du vill registrera hemgång för ett barn");
                }
            }
            catch (PostgresException ex)
            {
                MessageBox.Show(ex.Message);
                throw;
            }
        }

        private void BtnHome_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                hemgangna_barn hb = new hemgangna_barn();
                hb.Show();
            }
            catch (PostgresException ex)
            {
                MessageBox.Show(ex.Message);
                throw;
            }
        }
    }
}
