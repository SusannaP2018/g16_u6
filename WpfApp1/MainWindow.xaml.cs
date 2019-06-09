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

        DbOperations db = new DbOperations();
        Vardnadshavare selectedVardnadshavare;
        Barn selectedBarn;

        Schema schema = new Schema();

        //Klicka här för att visa alla VÅRDNADSHAVARE i listboxen
        private void BtnVardnadshavare_Click(object sender, RoutedEventArgs e)
        {
            List<Vardnadshavare> vardnadshavares = db.GetAllVardnadshavare();
            listBox1.ItemsSource = vardnadshavares;
        }

        private void btn_personalWindow(object sender, RoutedEventArgs e)
        {
            personalWindow pw = new personalWindow();
            pw.Show();
            this.Close();
        }

        private void BtnSchema_Click(object sender, RoutedEventArgs e)
        {
            int nr = 0;
            Barn selectedbarn;
            selectedbarn = (Barn)cmbBoxBarn.SelectedItem;
            
            if (selectedbarn == null)
            {
                MessageBox.Show("Du måste välja ett barn för att se schemat!");
            }
            else
            {
                nr = db.BarnIDForSchema(selectedbarn.Id);
                SchemaWin sw = new SchemaWin();
                sw.Show();
                sw.lstviewSchema.ItemsSource = db.GetOneBarnSchema(nr);
            }
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            db.Updatefrukost(true, 1);
        }

        private void BtnAway_Click(object sender, RoutedEventArgs e)
        {
            selectedBarn = (Barn)cmbBoxBarn.SelectedItem;

            int max = db.narvaroMax();
            max++;

            int vhID = 100;

            DateTime datetime;
            if (DateTime.TryParse(textBoxDate.Text, out datetime))
            {
                if (checkBoxSjuk.IsChecked == true)
                {
                    db.AddSjukdag(max, selectedBarn.Id, vhID, datetime);
                    MessageBox.Show("Sjukdag registrerad " + datetime.ToShortDateString() + " för " + selectedBarn.FirstName.ToUpper());
                }
                else if (checkBoxLedig.IsChecked == true)
                {
                    db.AddLedigdag(max, selectedBarn.Id, vhID, datetime);
                    MessageBox.Show("Ledigdag registrerad " + datetime.ToShortDateString() + " för " + selectedBarn.FirstName.ToUpper());
                }
                else if (checkBoxNarvaro.IsChecked == true)
                {
                    db.AddNarvaroDag(max, selectedBarn.Id, vhID, datetime);
                    MessageBox.Show("Närvarodag registrerad " + datetime.ToShortDateString() + " för " + selectedBarn.FirstName.ToUpper());
                }
                else if (checkBoxLedig.IsChecked == true && checkBoxSjuk.IsChecked == true || checkBoxLedig.IsChecked == true && checkBoxNarvaro.IsChecked == true || checkBoxSjuk.IsChecked == true && checkBoxNarvaro.IsChecked == true || checkBoxSjuk.IsChecked == true && checkBoxNarvaro.IsChecked == true && checkBoxLedig.IsChecked == true)
                {
                    MessageBox.Show("Du kan endast välja att antingen registrera en närvarodag, sjukdag eller ledigdag åt gången!");
                }
                else
                {
                    MessageBox.Show("Du måste kryssa för sjukdag, ledigdag eller närvarodag");
                }
            }
            else
            {
                MessageBox.Show("Datum måste vara i formatet åååå-mm-dd");
            }
        }

        private void BtnFrukost_Click(object sender, RoutedEventArgs e) // registrerar frukost
        {
            selectedBarn = (Barn)cmbBoxBarn.SelectedItem;

            int nr = selectedBarn.Id;
            bool ja, nej;
            if (rdBtnFrukostJa.IsChecked == true)
            {
                ja = true;
                db.Updatefrukost(ja, nr);
                MessageBox.Show("Ditt barn kommer att serveras frukost");
            }
            else if (rdBtnFrukostNej.IsChecked == true)
            {
                nej = false;
                db.Updatefrukost(nej, nr);
                MessageBox.Show("Ditt barn kommer inte att serveras frukost");
            }
        }

        private void BtnFarHamta_Click(object sender, RoutedEventArgs e)
        {
            selectedBarn = (Barn)cmbBoxBarn.SelectedItem;
            string farhamta = txtFarHamta.Text;
            if (selectedBarn == null)
            {
                MessageBox.Show("Du måste välja ett barn i listan");
            }
            else
            {
                db.UpdateFarHamta(farhamta, selectedBarn.Id);
                MessageBox.Show("Din kommentar är registrerad!");
            }     
        }

        private void ListBox1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selectedVardnadshavare = (Vardnadshavare)listBox1.SelectedItem;

            cmbBoxBarn.ItemsSource = null;
            cmbBoxBarn.ItemsSource = db.GetBarnByVh(selectedVardnadshavare.Id);
        }

        private void CmbBoxBarn_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selectedBarn = (Barn)cmbBoxBarn.SelectedItem;
            if (selectedBarn != null)
            {
                lblBarnNamn.Content = selectedBarn.FirstName.ToUpper() + " " + selectedBarn.LastName.ToUpper();
            }
            else
            {
                cmbBoxBarn.ItemsSource = null;
            }
        }
    }
}
