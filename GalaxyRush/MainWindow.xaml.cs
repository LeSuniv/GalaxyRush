using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
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

            // configure le Timer et les événements
            // lie le timer du répartiteur à un événement appelé moteur de jeu gameengine
            dispatcherTimer.Tick += Jeu;
            // rafraissement toutes les 16 milliseconds
            dispatcherTimer.Interval = TimeSpan.FromMilliseconds(16);
            // lancement du timer
            dispatcherTimer.Start();
            // chargement de l’image du joueur 
            SkinJoueur.ImageSource = new BitmapImage(new
            Uri(AppDomain.CurrentDomain.BaseDirectory + "Images/fusée.png"));
            // assignement de skin du joueur au rectangle associé
            joueur.Fill = SkinJoueur;
        }

        private void CleeCanvasAppuyee(object sender, KeyEventArgs e)
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
        private void CleeCanvasRelachee(object sender, KeyEventArgs e)
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
        private void CreeObstacles(object sender, KeyboardEventArgs e)
        {
            Random ordonné = new Random();
            int y = ordonné.Next(0,200);
            ImageBrush texturobstacle = new ImageBrush();
            Rectangle nouveauobstacle = new Rectangle
            {
                Tag = "asteroide",
                Height = 200,
                Width = 50,
                Fill = texturobstacle,
            }
            Canvas.SetRight(nouveauobstacle, right);
            Canvas.SetTop(nouveauobstacle,y)
            myCanvas.Children.Add(nouveauobstacle);
            textureobstacle.ImageSource = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "Images/asteroide.png"));
        }
        private void Jeu(object sender, EventArgs e)
        {
            // création d’un rectangle joueur pour la détection de collision
            Rect player = new Rect(Canvas.GetLeft(joueur), Canvas.GetTop(joueur),
            joueur.Width, joueur.Height);
            scoreText.Content = scoreText + "points";
        }
        private bool goUp, goRight = true;
        private bool goDown = false;
        // crée une nouvelle instance de la classe dispatch timer
        private DispatcherTimer dispatcherTimer = new DispatcherTimer();
        // classe de pinceau d'image que nous utiliserons comme image du joueur appelée skin du joueur
        private ImageBrush SkinJoueur = new ImageBrush();
        private int score = 0;
    }
}
