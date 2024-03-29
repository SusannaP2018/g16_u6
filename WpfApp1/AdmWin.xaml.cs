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
    /// Interaction logic for AdmWin.xaml
    /// </summary>
    public partial class AdmWin : Window
    {
        public AdmWin()
        {
            InitializeComponent();

            DbOperations db = new DbOperations();
            List<Vardnadshavare> vardnadshavares = db.GetAllVardnadshavare();
            listVH.ItemsSource = vardnadshavares;
        }

        private void btnAddVH_Click(object sender, RoutedEventArgs e)
        {
            DbOperations db = new DbOperations();
            db.AddNewVH(txtVHfornamn.Text, txtVHefternamn.Text, txtTel.Text); // Lägger till ny VH

            listVH.ItemsSource = null;
            List<Vardnadshavare> vardnadshavares = db.GetAllVardnadshavare(); // refreshar VH-listan 
            listVH.ItemsSource = vardnadshavares;
            MessageBox.Show("Vårdnadshavare registrerad!");
        }

        private void btnNyttBarnWin_Click(object sender, RoutedEventArgs e)
        {
            AdmWinBarn w = new AdmWinBarn();
            w.Show();
            this.Close();
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mw = new MainWindow();
            mw.Show();
            this.Close();
        }
    }
}
