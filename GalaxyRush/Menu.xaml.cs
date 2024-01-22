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
            this.DialogResult = true;
            MusiqueMenu.Stop();
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

            string imageUri;
            if (MusiqueMenu.IsMuted)
            {
                imageUri = "\\Images\\son_couper.png";
            }
            else
            {
                imageUri = "\\Images\\son.png";
            }
            butSon.Background = new ImageBrush(new BitmapImage(new Uri($"pack://application:,,,/GalaxyRush;component/{imageUri}", UriKind.Absolute)));
        }
    }
}