using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
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
            // configure le Timer et les événements
            // lie le timer du répartiteur à un événement appelé moteur de jeu gameengine
            dispatcherTimer.Tick += GameEngine;
            // rafraissement toutes les 16 milliseconds
            dispatcherTimer.Interval = TimeSpan.FromMilliseconds(16);
            // lancement du timer
            dispatcherTimer.Start();
        }
        private void CanvasKeyIsDown(object sender, KeyEventArgs e)
        {
            // on gère les booléens gauche et droite en fonction de l’appui de la touche
            if (e.Key == Key.Left)
            {
                goUp = true;
            }
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
