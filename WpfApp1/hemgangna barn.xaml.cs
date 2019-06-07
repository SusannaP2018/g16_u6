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
    /// Interaction logic for hemgangna_barn.xaml
    /// </summary>
    public partial class hemgangna_barn : Window
    {
        public hemgangna_barn()
        {
            InitializeComponent();
        }
        DbOperations db = new DbOperations();
    }
}
