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
            fuseeChoisi = basiqueFusee.getSkin();

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
            //increment difficulty choice on each click
            skinNum++;

            switch (skinNum)

            {
                case 1:
                    skinFusee basiqueFusee = new fusee_vert(new DefaultFusee());
                    fuseeChoisi = basiqueFusee.getSkin();
                    ImageSource? skin = new ImageSourceConverter().ConvertFromString(fuseeChoisi) as ImageSource;
                    fusee.Source = skin;
                    break;
                case 2:
                    skinFusee basiqueFusee1 = new Fusee_missile(new DefaultFusee());
                    fuseeChoisi = basiqueFusee1.getSkin();
                    ImageSource? skin1 = new ImageSourceConverter().ConvertFromString(fuseeChoisi) as ImageSource;
                    fusee.Source = skin1;
                    break;
                case 3:
                    skinFusee basiqueFusee2 = new Fusee_violet(new DefaultFusee());
                    fuseeChoisi = basiqueFusee2.getSkin();
                    ImageSource? skin2 = new ImageSourceConverter().ConvertFromString(fuseeChoisi) as ImageSource;
                    fusee.Source = skin2;
                    break;
                case 4:
                    skinFusee basiqueFusee3 = new Fusee_rouge(new DefaultFusee());
                    fuseeChoisi = basiqueFusee3.getSkin();
                    ImageSource? skin3 = new ImageSourceConverter().ConvertFromString(fuseeChoisi) as ImageSource;
                    fusee.Source = skin3;
                    break;
                case 5:
                    skinFusee basiqueFusee4 = new Fusee_noir(new DefaultFusee());
                    fuseeChoisi = basiqueFusee4.getSkin();
                    ImageSource? skin4 = new ImageSourceConverter().ConvertFromString(fuseeChoisi) as ImageSource;
                    fusee.Source = skin4;
                    break;
                default:
                    skinFusee basicLlama2 = new DefaultFusee();
                    fuseeChoisi = basicLlama2.getSkin();
                    ImageSource? skin6 = new ImageSourceConverter().ConvertFromString(fuseeChoisi) as ImageSource;
                    fusee.Source = skin6;

                    skinNum = 0;
                    break;
            }
        }
    }
}