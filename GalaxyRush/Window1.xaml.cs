﻿using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace GalaxyRush
{
    /// <summary>
    /// Logique d'interaction pour Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        private ImageBrush fondMenu = new ImageBrush();
        private ImageBrush fondMenuLogo = new ImageBrush();

        public Window1()
        {
            InitializeComponent();
            fondMenu.ImageSource = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "\\Images\\fond_espace.jpg")); Fond.Fill = fondMenu;
            fondMenuLogo.ImageSource = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "\\images\\LogoGalaxyRushR.png")); FondLogo.Fill = fondMenuLogo;
        }

        private void ButJouer_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }

        private void ButQuitter_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void ShowRules_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void butTuto_Click(object sender, RoutedEventArgs e)
        {
            CommentJouer rulesWindow = new CommentJouer();
                rulesWindow.Show();
        }
    }
}