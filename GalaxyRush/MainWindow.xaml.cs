using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;
using System.Xml.Linq;

namespace GalaxyRush
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        #region Constante

        private bool goUp = false;
        private bool goDown = true;
        RotateTransform rotation1 = new RotateTransform(135);
        RotateTransform rotation2 = new RotateTransform(45);
        RotateTransform rotation3 = new RotateTransform(90);
        // crée une nouvelle instance de la classe dispatch timer
        private DispatcherTimer dispatcherTimer = new DispatcherTimer();
        // classe de pinceau d'image que nous utiliserons comme image du joueur appelée skin du joueur
        private ImageBrush SkinJoueur = new ImageBrush();
        // vitesse du joueur
        private int vitesseJoueur = 5;
        // liste des éléments rectangles
        private int nbrObstacle = 0;
        private int score = 0;
        private List<Rectangle> enlever = new List<Rectangle>();
        private ImageBrush fond = new ImageBrush();
        private ImageBrush fusée = new ImageBrush();
        private int limiteAsteroide = 1;
        private int asteroide = 0;
        private int ovni = 0;
        private int limiteOvni = 1;
        private int repere = 1;
        Random aleatoire = new Random();
        private bool enPause = false;
        private int declencheur = 300;


        #endregion

        public MainWindow()
        {
            InitializeComponent();
            myCanvas.Focus();

            Menu fenetreNiveau = new Menu();
            fenetreNiveau.ShowDialog();

            fond.ImageSource = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "\\images\\fond_espace_jeu.png")); background.Fill = fond;


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
            if (e.Key == Key.Space)
            {
                if (goUp == false)
                {
                    goUp = true;
                    goDown = false;
                    joueur.RenderTransform = rotation1;
                }
                else
                {
                    goDown = true;
                    goUp = false;
                    joueur.RenderTransform = rotation2;
                }
            }
        }

        private void QuitterPartie()
        {
            //Stope les temps
            dispatcherTimer.Stop();
            timeTimer.Stop();

            //Menu menuWindow = new Menu();
            //menuWindow.Show();
            //this.Close();
            this.DialogResult = false;
        }

        private void CleeCanvasRelachee(object sender, KeyEventArgs e)
        {
            // on gère les booléens espace en fonction de l’appui de la touche
            if (e.Key == Key.Space && goUp == true)
            {
                goDown = true;
            }
            if (e.Key == Key.Space && goUp == false)
            {
                MettrePause();
            }
        }


        private void CreeObstacles()
        {
            int right = 0;
            int y = 0;
            for (int i = asteroide; i < limiteAsteroide; i++)
            {
                score += 1;
                y = aleatoire.Next(0, 350);
                if (i < limiteAsteroide)
                {
                    #region Asteroide
                    ImageBrush texturObstacle = new ImageBrush();

                    Rectangle nouveauObstacle = new Rectangle
                    {
                        Tag = "asteroide",
                        Height = 100,
                        Width = 50,
                        Fill = texturObstacle,
                    };

                    Canvas.SetRight(nouveauObstacle, right);

                    Canvas.SetTop(nouveauObstacle, y);

                    myCanvas.Children.Add(nouveauObstacle);

                    texturObstacle.ImageSource = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "Images/asteroide.png"));
                    asteroide += 1;
                }
                #endregion

            }
            if (score >= 5)
            {
                for (int i = ovni; i < limiteOvni; i++)
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

        private void MouvementObstacle(int declencheur)
        {
            foreach (Rectangle asteroide in myCanvas.Children.OfType<Rectangle>())
            {
                if (asteroide is Rectangle && (string)asteroide.Tag == "asteroide")
                {
                    Canvas.SetRight(asteroide, Canvas.GetRight(asteroide) + vitesseObstacle);
                }

                if (Canvas.GetRight(asteroide) > 802)
                {
                    enlever.Add(asteroide);
                    this.asteroide = 0;
                }
            }
            foreach (Rectangle x in enlever)
            {
                myCanvas.Children.Remove(x);
            }
            foreach (Rectangle ovni in myCanvas.Children.OfType<Rectangle>())
            {
                if (ovni is Rectangle && (string)ovni.Tag == "ovni")
                {
                    Canvas.SetRight(ovni, Canvas.GetRight(ovni) + vitesseOvni);
                    if (Canvas.GetRight(ovni) == declencheur)
                    {
                        for (int i = 0; i < 4; i++)
                        {

                            int oba = aleatoire.Next(0, 2);
                            if (oba == 1)
                            {
                                Canvas.SetTop(ovni, Canvas.GetTop(ovni) - vitesseOvni);
                            }
                            else if (oba == 0)
                            {
                                Canvas.SetTop(ovni, Canvas.GetTop(ovni) + vitesseOvni);
                            }
                        }
                    }
                }
               
                if (Canvas.GetRight(ovni) > 802)
                {
                    declencheur = aleatoire.Next(50, 400);
                    enlever.Add(ovni);
                    this.ovni = 0;
                }
            }
            foreach (Rectangle z in enlever)
            {
                myCanvas.Children.Remove(z);
            }
        }
        private void ComptagePoint()
        {
            foreach (Rectangle asteroide in myCanvas.Children.OfType<Rectangle>())
            {
                    if (asteroide is Rectangle && (string)asteroide.Tag == "asteroide" )
                    {
                        if (Canvas.GetTop(asteroide) < Canvas.GetTop(joueur)) 
                        {
                            score += 1;
                        }
                    }
            }

        }
        private void Vitesse() 
        { 
            if (score >= 10 * repere) 
            {
                repere = repere + 1;
                vitesseObstacle += 1;
                vitesseOvni += 1;
            }
        }

        private void Jeu(object sender, EventArgs e)
        {
            // création d’un rectangle joueur pour la détection de collision
            Rect player = new Rect(Canvas.GetLeft(joueur), Canvas.GetTop(joueur), joueur.Width, joueur.Height);
            scoreText.Content = "Score: " + score;
            if (goDown && Canvas.GetTop(joueur) > 0)
            {
                Canvas.SetTop(joueur, Canvas.GetTop(joueur) - vitesseJoueur);
            }
            else if (goUp && Canvas.GetTop(joueur) + joueur.Height  < Application.Current.MainWindow.Height)
            {
                Canvas.SetTop(joueur, Canvas.GetTop(joueur) + vitesseJoueur);
            }
            else
            {
                joueur.RenderTransform = rotation3;
            }
            CreeObstacles();
            Vitesse();
            MouvementObstacle(declencheur);
        }

        private void RetireObjet(object sender, EventArgs e)
        {

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
            }
            else
            {
                // Redémarrer les timers pour reprendre le jeu
                dispatcherTimer.Start();
                timeTimer.Start();
                pauseText.Visibility = Visibility.Collapsed;
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
        private double vitesseObstacle = 3;
        private double vitesseOvni = 6;

        private void ComptageTemps(object sender, EventArgs e)
        {
            secondes++;
            if (secondes == 60)
            {
                minutes++;
                secondes = 0;
            }

            string tempsFormat = minutes.ToString("D2") + ":" + secondes.ToString("D2");
            temps.Text = tempsFormat;
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
//        //llama.Margin = new Thickness(llama.Margin.Left, llama.Margin.Top - 50, llama.Margin.Right, llama.Margin.Top + 50);
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
