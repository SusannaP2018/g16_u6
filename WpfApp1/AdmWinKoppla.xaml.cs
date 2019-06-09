﻿using System;
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
    /// Interaction logic for AdmWinKoppla.xaml
    /// </summary>
    public partial class AdmWinKoppla : Window
    {
        public AdmWinKoppla()
        {
            InitializeComponent();

            DbOperations db = new DbOperations();

            List<Vardnadshavare> vs = db.GetAllVardnadshavare();
            comboVH.ItemsSource = vs;

            List<Barn> bs = db.GetAllBarn();
            comboBarn.ItemsSource = bs;
        }
    }
}
