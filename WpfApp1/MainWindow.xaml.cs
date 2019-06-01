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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Npgsql;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DbOperations db = new DbOperations();

            try
            {
                List<Person> persons = db.GetAllPersons();
                listBox1.ItemsSource = persons;
            }
            catch (PostgresException ex)
            {

                MessageBox.Show(ex.Message);
            }


        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            DbOperations db = new DbOperations();

            int id = Convert.ToInt32(idTextBox.Text);

            try
            {
                Person p = db.GetPersonById(id);
                MessageBox.Show(p.ToString());
            }
            catch (PostgresException ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            DbOperations db = new DbOperations();

            int id = Convert.ToInt32(personidTextBox.Text);
            string fname = fnameTextBox.Text;
            string lname = lnameTextBox.Text;

            db.AddNewPerson(id, fname, lname);
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            DbOperations db = new DbOperations();

            int id = Convert.ToInt32(personidTextBox.Text);
            string fname = fnameTextBox.Text;
            string lname = lnameTextBox.Text;

            db.UpdatePerson(id, fname, lname);
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            DbOperations db = new DbOperations();

            int id = Convert.ToInt32(idTextBox.Text);

            db.DeletePerson(id);
        }
    }
}
