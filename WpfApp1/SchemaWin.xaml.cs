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
    /// Interaction logic for SchemaWin.xaml
    /// </summary>
    public partial class SchemaWin : Window
    {
        public SchemaWin()
        {
            InitializeComponent();
            UpdateListView();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mw = new MainWindow();
            mw.Show();
            this.Close();
        }
        private void UpdateListView()
        {
            DbOperations db = new DbOperations();
            listView.ItemsSource = null;
            listView.ItemsSource = db.GetSchemaBarn();

        }
    }
}
