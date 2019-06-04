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
    //Charlies kommentar
    // Håkans komentar 2 
    // Susannas kommentar
    //

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

            DbOperations db = new DbOperations();
        Vardnadshavare selectedVardnadshavare;
        
        Schema schema = new Schema();
        
        //Klicka här för att visa alla VÅRDNADSHAVARE i listboxen
        private void Button_Click(object sender, RoutedEventArgs e)        {
             List<Vardnadshavare> vardnadshavares = db.GetAllVardnadshavare();
            listBox1.ItemsSource = vardnadshavares;
            //try
            //{
            //    List<Person> persons = db.GetAllPersons();
            //    listBox1.ItemsSource = persons;
            //}
            //catch (PostgresException ex)
            //{

            //    MessageBox.Show(ex.Message);
            //}
            

        }

        // Markera en VÅRDNADSHAVARE.
        private void ListBox1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selectedVardnadshavare = (Vardnadshavare)listBox1.SelectedItem;
        }

        // visa Vilket BARN som hör till vilken VÅRDNADSHAVARE
        private void BtnTest_Click(object sender, RoutedEventArgs e)
        {
            selectedVardnadshavare = (Vardnadshavare)listBox1.SelectedItem;
            
            
            List<Barn> barns = db.GetAllBarn();

            foreach (var b in barns)
            {
                
                if (selectedVardnadshavare.Id == b.Id)
                {
                    cmbBoxBarn.Items.Add(b);
                    

                }


            }




        }

        private void CmbBoxBarn_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        


          //  DbOperations db = new DbOperations();

          // // int id = Convert.ToInt32(idTextBox.Text);

          //  try
          //  {
          ////      Person p = db.GetPersonById(id);
          //      MessageBox.Show(p.ToString());
          //  }
          //  catch (PostgresException ex)
          //  {

          //      MessageBox.Show(ex.Message);


        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            //DbOperations db = new DbOperations();

            //int id = Convert.ToInt32(personidTextBox.Text);
            //string fname = fnameTextBox.Text;
            //string lname = lnameTextBox.Text;

            //db.AddNewPerson(id, fname, lname);
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
        //    DbOperations db = new DbOperations();

        //    int id = Convert.ToInt32(personidTextBox.Text);
        //    string fname = fnameTextBox.Text;
        //    string lname = lnameTextBox.Text;

        //    db.UpdatePerson(id, fname, lname);
       }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
         //   DbOperations db = new DbOperations();

         ////   int id = Convert.ToInt32(idTextBox.Text);

         //   db.DeletePerson(id);
        }

        private void btn_personalWindow(object sender, RoutedEventArgs e)
        {
            personalWindow pw = new personalWindow();
            pw.Show();
            this.Close();
            
        }
        // avmarkeras
        //private void BtnSchema_Click(object sender, RoutedEventArgs e)
        //{
        //    int nr = 0;
        //    Barn selectedbarn;
        //    selectedbarn = (Barn)cmbBoxBarn.SelectedItem;
        //   nr = db.BarnIDForSchema(selectedbarn.Id);
            

        //    SchemaWin sw = new SchemaWin();
        //    sw.Show();
        //    sw.lstviewSchema.ItemsSource = db.GetOneBarnSchema(nr);
        //    this.Close();
        //}

        //private void BtnTest1_Click(object sender, RoutedEventArgs e)
        //{
        //    List<Schema> schemas = db.GetOneBarnSchema(3);
        //}

        //private void Button_Click_1(object sender, RoutedEventArgs e)
        //{

        //    List<Schema> schemas = db.GetSchemaBarn();
        //    SchemaWin sw = new SchemaWin();
        //    sw.Show();
        //    this.Close();

        //}

        //private void btnClickSchedule(object sender, RoutedEventArgs e)
        //{
        //    SchemalaggningWindow pw = new SchemalaggningWindow();
        //    pw.Show();
        //    this.Close();
        //}
    }
}
