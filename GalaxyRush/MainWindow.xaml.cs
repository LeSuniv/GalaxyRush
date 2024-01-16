using System;
using System.Collections.Generic;
using System.Linq;
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

        private double vitesseDefilement = 5;

        #endregion

        public MainWindow()
        {
            InitializeComponent();
            myCanvas.Focus();

            //MainWindow mainWindow = new MainWindow();
            //mainWindow.Hide();

            Menu main = new Menu();
            main.ShowDialog();
            //Rejouer();

            //if ((Application.Current.MainWindow is Menu menu))
            //{
            //    menu.Hide();
            //    Menu newMenu = new Menu();
            //    newMenu.ShowDialog();
            //}


            fond.ImageSource = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "\\images\\fond_espace_jeu.png")); background.Fill = fond; background2.Fill = fond;

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

        private void Rejouer()
        {
            Menu menu = new Menu();
            menu.ShowDialog();
            this.Close();
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
            if (e.Key == Key.P)
            {
                MettrePause();
            }
            if (e.Key == Key.Escape)
            {
                Rejouer();
            }
        }


        private void QuitterPartie()
        {
            //Stope les temps
            dispatcherTimer.Stop();
            timeTimer.Stop();

            Menu menu = new Menu();
            menu.ShowDialog();

            //Menu menuWindow = new Menu();
            //menuWindow.Show();
            this.Close();
            //DialogResult = false;
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
                if (asteroide is Rectangle && (string)asteroide.Tag == "asteroide")
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


        private void AnimerFond()
        {
            double newPos = Canvas.GetLeft(background) - vitesseDefilement;
            Canvas.SetLeft(background, newPos);

            newPos = Canvas.GetLeft(background2) - vitesseDefilement;
            Canvas.SetLeft(background2, newPos);

            if (Canvas.GetLeft(background) < -background.Width)
            {
                Canvas.SetLeft(background, Canvas.GetLeft(background2) + background2.Width);
            }

            if (Canvas.GetLeft(background2) < -background2.Width)
            {
                Canvas.SetLeft(background2, Canvas.GetLeft(background) + background.Width);
            }

            // on va avancer le background simultanement et infini
            // si le 1er background X position en dessous de -1262 pixels
            //if (Canvas.GetLeft(background) < -1262)
            //{
            //    // alors en met le 1er background derrière le 2ème background
            //    //on met les background à gauche (la position de X) à la largeur de background2
            //    Canvas.SetLeft(background, Canvas.GetLeft(background2) + background2.Width);
            //}
            //// meme procédé background 2 
            //// si background 2 X en dessous de -1262
            //if (Canvas.GetLeft(background2) < -1262)
            //{
            //    //2ème background derrière background 1
            //    // on met background 2 à gauche (la position de X) à la largeur de background
            //    Canvas.SetLeft(background2, Canvas.GetLeft(background) + background.Width);
            //}

        }


        private void Jeu(object sender, EventArgs e)
        {           
            CreeObstacles();
            Vitesse();
            MouvementObstacle(declencheur);
            AnimerFond();
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
                pauseText.Visibility = Visibility.Collapsed;
                dispatcherTimer.Start();
                timeTimer.Start();
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