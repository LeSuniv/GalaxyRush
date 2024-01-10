using System;
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
            Window1 debutjeu = new Window1();
            debutjeu.ShowDialog();
        }

        private void CanvasKeyIs(object sender, KeyEventArgs e)
        {
            // on gère les booléens gauche et droite en fonction de l’appui de la touche
            if (e.Key == Key.Left)
            {
                goUp = true;
            }
        }
        private void GameEngine(object sender, EventArgs e)
        {

        }
        private bool goUp = false;
        // crée une nouvelle instance de la classe dispatch timer
        private DispatcherTimer dispatcherTimer = new DispatcherTimer();
    }
}
