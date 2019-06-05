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
    /// Interaction logic for schema_personalvy.xaml
    /// </summary>
    public partial class schema_personalvy : Window
    {
        public schema_personalvy()
        {
            InitializeComponent();
        }
        DbOperations db = new DbOperations();
        
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            personalWindow pw = new personalWindow();
            pw.Show();
        }
    }
}
