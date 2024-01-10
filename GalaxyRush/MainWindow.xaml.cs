using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;

namespace GalaxyRush
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ImageBrush fusée = new ImageBrush(); 

        public MainWindow()
        {
            InitializeComponent();
            Window1 debutjeu = new Window1();
            debutjeu.ShowDialog();
            if (debutjeu.DialogResult == false)
                Application.Current.Shutdown();

            //InitializeGame();

        }

        private void CanvasKeyIsDown(object sender, KeyEventArgs e)
        {
            bool truc = goUp;
            // on gère les booléens gauche et droite en fonction de l’appui de la touche
            if (e.Key == Key.Space && truc == true)
            {
                goUp = false;
                goRight = false;
            }
        }
        private void CanvasKeyIsUp(object sender, KeyEventArgs e)
        {
            // on gère les booléens gauche et droite en fonction de l’appui de la touche
            if (e.Key == Key.Space && goUp == true)
            {
                goUp = true;
                goRight = true;
            }
            if (e.Key == Key.Space && goUp == false)
            {
                goUp = false;
                goRight = false;
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
