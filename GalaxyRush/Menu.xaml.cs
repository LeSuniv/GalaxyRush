using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Input;


namespace GalaxyRush
{
    public partial class Menu : Window
    {
        private ImageBrush fondMenu = new ImageBrush();
        private ImageBrush fondMenuLogo = new ImageBrush();
        private bool fenetreRegle = false;
        bool imageAffiche = true;

        public Menu()
        {
            InitializeComponent();
            MusiqueMenu.Play();

            fondMenu.ImageSource = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "\\images\\fond_espace.jpg"));
            Fond.Fill = fondMenu;
            fondMenuLogo.ImageSource = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "\\images\\LogoGalaxyRushR.png"));
            FondLogo.Fill = fondMenuLogo;
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
            if (!fenetreRegle)
            {
                CommentJouer commentJouer = new();
                commentJouer.Closed += (s, args) => fenetreRegle = false;
                commentJouer.Show();
                fenetreRegle = true;
            }
        }


        private void ButSon_Click(object sender, RoutedEventArgs e)
        {
            MusiqueMenu.IsMuted = !MusiqueMenu.IsMuted;
        }

        private void SonStatut_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (imageAffiche)
                sonStatut.Source = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "/Images/son.png"));
            else
                sonStatut.Source = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "/Images/son_couper.png"));

            imageAffiche = !imageAffiche;
            butSon.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
        }
    }
}