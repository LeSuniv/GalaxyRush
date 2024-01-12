using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.DirectoryServices;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
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
        private int limiteAsteroide = 1;
        private int asteroide = 0;
        private int ovni = 0;
        private int limiteOvni = 1;
        Random aleatoire = new Random();
        private bool enPause = false;


        #endregion

        public MainWindow()
        {
            InitializeComponent();
            myCanvas.Focus();

            Window1 fenetreNiveau = new Window1();
            fenetreNiveau.ShowDialog();


            fond.ImageSource = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "\\images\\fond_espace_jeu.png")); background.Fill = fond;

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
            SkinJoueur.ImageSource = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "\\images\\fusee.png"));
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

            if (e.Key == Key.P)
            {
                MettrePause();
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


        private void CreeObstacles()
        {
            int right = 0;
            int y = 0;
            for (int i = asteroide; i < limiteAsteroide; i++)
            {
                 y = aleatoire.Next(0, 350);
                if (i < limiteAsteroide)
                {
                    score += 1;
                    #region Asteroide
                    ImageBrush texturObstacle = new ImageBrush();


                    Rectangle nouveauObstacle = new Rectangle
                    {
                        Tag = "asteroide",
                        Height = 100,
                        Width = 50,
                        Fill = texturObstacle,
                    };

                    Canvas.SetRight(nouveauObstacle,right);

                    Canvas.SetTop(nouveauObstacle, y);

                    myCanvas.Children.Add(nouveauObstacle);

                    texturObstacle.ImageSource = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "Images/asteroide.png"));
                    asteroide += 1;
                }
            }
            
            #endregion

            #region Ovni
            #endregion
        }
        private void CreeOvni()
        {
            
            int right = 0;
            int y = 0;
            for (int i = ovni; i < limiteOvni; i++)
            {
                if (score > 0 )
                {
                    y = aleatoire.Next(0, 350);
                    ImageBrush textureOvni = new ImageBrush();
                    Rectangle nouveauOvni = new Rectangle
                    {
                        Tag = "ovni",
                        Height = 60,
                        Width = 125,
                        Fill = textureOvni,
                    };
                    Canvas.SetRight(nouveauOvni, right);
                    Canvas.SetTop(nouveauOvni, y);
                    myCanvas.Children.Add(nouveauOvni);
                    textureOvni.ImageSource = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "Images/ovni.png"));
                    ovni += 1;
                }
            }
        }


        private void MouvementObstacle()
        {
            foreach (Rectangle x in myCanvas.Children.OfType<Rectangle>())
            {
                if (x is Rectangle && (string)x.Tag == "asteroide")
                {
                    Canvas.SetRight(x, Canvas.GetRight(x) + vitesseObstacle);
                }

                if (Canvas.GetRight(x) > 802)
                {
                    enlever.Add(x);
                    asteroide = 0;
                }
            }
            foreach (Rectangle x in enlever)
            {
                myCanvas.Children.Remove(x);
            }
            foreach (Rectangle z in myCanvas.Children.OfType<Rectangle>())
            {
                if (z is Rectangle && (string)z.Tag == "ovni")
                {
                    Canvas.SetRight(z, Canvas.GetRight(z) + vitesseOvni);
                    if (Canvas.GetRight(z) == 500)
                    {
                        foreach (Rectangle y in myCanvas.Children.OfType<Rectangle>())
                        {
                            if (y is Rectangle && (string)y.Tag == "ovni")
                            {
                                for (int i = 0; i < 4; i++)
                                {
                                    Random numero = new Random();
                                    int oba = 0;
                                    if (oba == 0)
                                    {
                                        Canvas.SetTop(y, Canvas.GetTop(y) - vitesseOvni);
                                    }

                                }
                            }
                        }
                    }

                }
                if (Canvas.GetRight(z) > 802)
                {
                    enlever.Add(z);
                    ovni = 0;
                }
            }
            foreach( Rectangle z in enlever )
            {
                myCanvas.Children.Remove(z);
            }
           
        }


        private void Jeu(object sender, EventArgs e)
        {
            // création d’un rectangle joueur pour la détection de collision
            Rect player = new Rect(Canvas.GetLeft(joueur), Canvas.GetTop(joueur),
            joueur.Width, joueur.Height);
            scoreText.Content = "Score: " + score;
            CreeObstacles();
            MouvementObstacle();
            CreeOvni();

        }


        private void MettrePause()
        {
            enPause = !enPause;

            if (enPause)
            {
                // Arrêter les timers pour mettre le jeu en pause
                dispatcherTimer.Stop();
                timeTimer.Stop();
                pauseText.Visibility = Visibility.Visible;
                // Autres timers si nécessaire
            }
            else
            {
                // Redémarrer les timers pour reprendre le jeu
                dispatcherTimer.Start();
                timeTimer.Start();
                pauseText.Visibility = Visibility.Collapsed;
                // Autres timers si nécessaire
            }
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


        #region Temps
        // variable pour le temps
        private int minutes = 0;
        private int secondes = 0;
        private DispatcherTimer timeTimer = new DispatcherTimer();
        private double vitesseObstacle = 5;
        private double vitesseOvni = 10;

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



//Pour plus tard mettre sur pause
//private void OnSpaceDownHandler(object sender, KeyEventArgs e)
//{
//    if (e.Key == Key.Space)
//    {
//        if (velocity > 0)
//            velocity = -leapDist;
//        else
//            velocity -= leapDist;
//        //velocity -= leapDist;
//        //llama.Margin = new Thickness(llama.Margin.Left, llama.Margin.Top - 50, llama.Margin.Right, llama.Margin.Bottom + 50);
//    }
//    else if (e.Key == Key.P)
//    {
//        if (timeTimer.IsEnabled)
//        {
//            gravTimer.Stop();
//            timeTimer.Stop();
//            genTimer.Stop();
//        }
//        else
//        {
//            gravTimer.Start();
//            timeTimer.Start();
//            genTimer.Start();
//        }
//    }
//    else if (e.Key == Key.Escape)
//    {
//        gravTimer.Stop();
//        timeTimer.Stop();
//        genTimer.Stop();
//        //add to open a popup to retry or go to new window
//        GameOver(allFences.score);
//    }
//}
