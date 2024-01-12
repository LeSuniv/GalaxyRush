using System;
using System.Windows;
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
        string fuseeChoisi;


        public Window1()
        {
            InitializeComponent();

            skinFusee basiqueFusee = new DefaultFusee();

            fondMenu.ImageSource = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "\\images\\fond_espace.jpg")); Fond.Fill = fondMenu;
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


        private void butTuto_Click(object sender, RoutedEventArgs e)
        {
            CommentJouer Tuto = new CommentJouer();
            Tuto.Show();
        }

        private void ChoisirSkin_Click(object sender, RoutedEventArgs e)
        {

            skinNum = (skinNum + 1) % 6;
            skinFusee basiqueFusee;

            switch (skinNum)

            {
                case 1:
                    basiqueFusee = new Fusee_vert(new DefaultFusee());
                    break;
                case 2:
                    basiqueFusee = new Fusee_missile(new DefaultFusee());
                    break;
                case 3:
                    basiqueFusee = new Fusee_violet(new DefaultFusee());
                    break;
                case 4:
                    basiqueFusee = new Fusee_rouge(new DefaultFusee());
                    break;
                case 5:
                    basiqueFusee = new Fusee_noir(new DefaultFusee());
                    break;
                default:
                    basiqueFusee = new DefaultFusee();
                    skinNum = 0;
                    break;
            }
            fuseeChoisi = basiqueFusee.getSkin();

            fuseeChoisi = basiqueFusee.getSkin();
            fusee.Source = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + fuseeChoisi));
        }
    }

}