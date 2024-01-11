using System;
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
        int skinNum = 0;
        string selectedLlama;


        public Window1()
        {
            InitializeComponent();

            skinFusee basiqueFusee = new DefaultFusee();
            selectedLlama = basiqueFusee.getSkin();

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

        private void ChoisirSkin_Click(object sender, RoutedEventArgs e)
        {
            //increment difficulty choice on each click
            skinNum++;



            switch (skinNum)

            {
                case 1:
                    skinFusee basicLlama = new fusee_vert(new DefaultFusee());
                    selectedLlama = basicLlama.getSkin();
                    ImageSource skin = new ImageSourceConverter().ConvertFromString(selectedLlama) as ImageSource;
                    fusee.Source = skin;
                    break;
                case 2:
                    skinFusee basicLlama1 = new Fusee_violet(new DefaultFusee());
                    selectedLlama = basicLlama1.getSkin();
                    ImageSource skin1 = new ImageSourceConverter().ConvertFromString(selectedLlama) as ImageSource;
                    fusee.Source = skin1;
                    break;
                default:
                    skinFusee basicLlama2 = new DefaultFusee();
                    selectedLlama = basicLlama2.getSkin();
                    ImageSource skin2 = new ImageSourceConverter().ConvertFromString(selectedLlama) as ImageSource;
                    fusee.Source = skin2;

                    skinNum = 0;
                    break;

            }
        }
    }
}