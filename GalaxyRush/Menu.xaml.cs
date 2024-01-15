﻿using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace GalaxyRush
{
    public partial class Menu : Window
    {

        private ImageBrush fondMenu = new ImageBrush();
        private ImageBrush fondMenuLogo = new ImageBrush();

        public Menu()
        {
            InitializeComponent();

            fondMenu.ImageSource = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "\\images\\fond_espace.jpg")); Fond.Fill = fondMenu;
            fondMenuLogo.ImageSource = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "\\images\\LogoGalaxyRushR.png")); FondLogo.Fill = fondMenuLogo;
        }

        private void ButJouer_Click(object sender, RoutedEventArgs e)
        {
            //MainWindow gameWindow = new MainWindow();
            //gameWindow.ShowDialog();
            //MainWindow jeu = new MainWindow();
            //jeu.ShowDialog();
            //this.Hide();
            
            this.DialogResult = true;
            //this.Close();
        }

        private void ButQuitter_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void ButTuto_Click(object sender, RoutedEventArgs e)
        {
            CommentJouer Tuto = new CommentJouer();
            Tuto.Show();
        }
    }
}