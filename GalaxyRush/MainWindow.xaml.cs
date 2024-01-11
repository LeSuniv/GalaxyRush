using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace GalaxyRush
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        #region Constante

        private bool goUp = true;
        private bool goDown = false;
        // crée une nouvelle instance de la classe dispatch timer
        private DispatcherTimer dispatcherTimer = new DispatcherTimer();
        // classe de pinceau d'image que nous utiliserons comme image du joueur appelée skin du joueur
        private ImageBrush SkinJoueur = new ImageBrush();
        private int nbrObstacle = 0;
        private int score = 0;
        private List<Rectangle> enlever = new List<Rectangle>();
        private ImageBrush fond = new ImageBrush();
        private ImageBrush fusée = new ImageBrush();

        #endregion

        public MainWindow()
        {
            InitializeComponent();

            Window1 fenetreNiveau = new Window1();
            fenetreNiveau.ShowDialog();


            fond.ImageSource = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "\\Images\\fond_espace_jeu.png")); background.Fill = fond;

            //Dialog dialogNom = new Dialog();
            //dialogNom.ShowDialog();
            //dialogNom.Owner = this;
            //if (dialogNom.DialogResult == false)
            //    Application.Current.Shutdown();


            //InitializeGame();

            // configure le Timer et les événements
            // lie le timer du répartiteur à un événement appelé moteur de jeu gameengine
            dispatcherTimer.Tick += Jeu;
            // rafraissement toutes les 16 milliseconds
            dispatcherTimer.Interval = TimeSpan.FromMilliseconds(16);
            // lancement du timer
            dispatcherTimer.Start();
            // chargement de l’image du joueur 
            SkinJoueur.ImageSource = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "\\images\\fusée.png"));
            // assignement de skin du joueur au rectangle associé
            joueur.Fill = SkinJoueur;

            timeTimer.Tick += ComptageTemps;
            timeTimer.Interval = TimeSpan.FromSeconds(1);
            timeTimer.Start();
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
            nbrObstacle += 1;
            Random ordonne = new Random();

            int y = ordonne.Next(0, 200);
            int right = 0;

            #region Asteroide
            ImageBrush texturObstacle = new ImageBrush();

            Rectangle nouveauObstacle = new Rectangle
            {
                Tag = "asteroide",
                Height = 200,
                Width = 50,
                Fill = texturObstacle,
            };

            Canvas.SetRight(nouveauObstacle, right);

            Canvas.SetTop(nouveauObstacle, y);

            myCanvas.Children.Add(nouveauObstacle);

            right -= 60;
            texturObstacle.ImageSource = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "Images/asteroide.png"));
            #endregion

            #region Ovni
            if (nbrObstacle > 5) //j'ai mis 10 juste pour contextualiser
            {
                y = ordonne.Next(0, 200);
                ImageBrush textureOvni = new ImageBrush();
                Rectangle nouveauOvni = new Rectangle
                {
                    Tag = "ovni",
                    Height = 40,
                    Width = 50,
                    Fill = textureOvni,
                };
                Canvas.SetRight(nouveauOvni, right);
                Canvas.SetTop(nouveauOvni, y);
                myCanvas.Children.Add(nouveauOvni);
                right -= 60;
                textureOvni.ImageSource = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "Images/ovni.png"));
            }
            #endregion
        }


        private void Jeu(object sender, EventArgs e)
        {
            // création d’un rectangle joueur pour la détection de collision
            Rect player = new Rect(Canvas.GetLeft(joueur), Canvas.GetTop(joueur),
            joueur.Width, joueur.Height);
            scoreText.Content = "Score: " + score;
        }

        //if (x is Rectangle && (string)x.Tag == "enemy")
        //{
        //    // On le déplace vers la droite selon enemySpeed
        //    Canvas.SetLeft(x, Canvas.GetLeft(x) + enemySpeed);
        //}
        //// vérification de la collision avec le joueur
        //Rect enemy = new Rect(Canvas.GetLeft(x), Canvas.GetTop(x), x.Width, x.Height);
        //if (player.IntersectsWith(enemy))
        //{
        //    // collision avec le joueur et fin de la partie
        //    dispatcherTimer.Stop();
        //    lose.Visibility = Visibility.Visible;
        //}

        //fsd
        #region Temps
        // variable pour le temps
        private int minutes = 0;
        private int secondes = 0;
        private DispatcherTimer timeTimer = new DispatcherTimer();


        private void ComptageTemps(object sender, EventArgs e)
        {
            secondes++;
            if (secondes == 60)
            {
                minutes++;
                secondes = 0;
            }
            if (secondes == 0 && minutes == 0)
            {
                time.Text = "0:00";
            }
            else if (minutes == 0)
            {
                time.Text = "0:" + secondes.ToString();
            }
            else
            {
                time.Text = minutes.ToString() + ":" + secondes.ToString();
            }
        }
        #endregion
    }
}
