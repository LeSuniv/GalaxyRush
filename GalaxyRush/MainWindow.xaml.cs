﻿using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;

namespace GalaxyRush
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

        private void CanvasKeyIsDown(object sender, KeyEventArgs e)
        {
            // on gère les booléens gauche et droite en fonction de l’appui de la touche
            if (e.Key == Key.Space && goUp == true)
            {
                goUp = false;
            }
            if (e.Key == Key.Space && goUp == false)
            {
                goUp = true;
            }
        }
        private void CanvasKeyIsUp(object sender, KeyEventArgs e)
        {
            // on gère les booléens gauche et droite en fonction de l’appui de la touche
            if (e.Key == Key.Space && goUp == true)
            {
                goDown = true;
            }
            if (e.Key == Key.Space && goUp == false)
            {
                goUp = false;
            }
        }
        private void GameEngine(object sender, EventArgs e)
        {

        }
        private bool goUp, goRight = true;
        private bool goDown = false;
        // crée une nouvelle instance de la classe dispatch timer
        private DispatcherTimer dispatcherTimer = new DispatcherTimer();
    }
}
