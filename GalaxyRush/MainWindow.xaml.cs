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
        private int score = 7;
        private List<Rectangle> enlever = new List<Rectangle>();
        private ImageBrush fond = new ImageBrush();
        private ImageBrush fusee = new ImageBrush();
        private int limiteAsteroide = 1;
        private int nb_asteroide = 0;
        private int nb_ovni = 0;
        private int limiteOvni = 1;
        private int repere_vitesse = 1;
        Random aleatoire = new Random();
        private bool enPause = false;
        private int declencheur = 300;
        private double vitesseAsteroide = 10;
        private double vitesseOvni = 6;
        private double changement_vitesse = 10;
        private double change_qnt_asteroide = 4;
        private double change_qnt_ovni = 7;
        private int delai = 1;
        private int temps_apparition = 20;
        ImageBrush backgroundImg = new ImageBrush();

        private double vitesseDefilement = 5;

        #endregion

        public MainWindow()
        {
            InitializeComponent();
            myCanvas.Focus();

            Menu main = new Menu();
            main.ShowDialog();

            //if ((Application.Current.MainWindow is Menu menu))
            //{
            //    menu.Hide();
            //    Menu newMenu = new Menu();
            //    newMenu.ShowDialog();
            //}




            backgroundImg.ImageSource = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "\\images\\fond_espace_jeu.png"));
            background.Fill = backgroundImg;
            background2.Fill = backgroundImg;
            // configure le Timer et les événements
            // lie le timer du répartiteur à un événement appelé moteur de jeu gameengine

            // rafraissement toutes les 16 milliseconds
            dispatcherTimer.Interval = TimeSpan.FromMilliseconds(16);
            dispatcherTimer.Tick += Jeu;
            dispatcherTimer.Start();
            // chargement de l’image du joueur 
            SkinJoueur.ImageSource = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "\\images\\fusee.png"));
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
            if (e.Key == Key.O)
            {
                QuitterPartie();
            }
        }


        private void QuitterPartie()
        {
            dispatcherTimer.Stop();
            timeTimer.Stop();
            Quitter.Visibility = Visibility.Visible;
            //Rejouer.Visibility = Visibility.Visible;
            //Menu.Visibility = Visibility.Visible;
            //perduText.Visibility = Visibility.Visible;
        }
        private void QuitterBoutton(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
        //private void MenuBoutton(object sender, RoutedEventArgs e)
        //{
        //    Menu menu = new Menu();
        //    menu.Show();
        //}

        private void CleeCanvasRelachee(object sender, KeyEventArgs e)
        {
            // on gère les booléens espace en fonction de l’appui de la touche
            if (e.Key == Key.Space && goUp == true)
            {
                goDown = true;
            }
        }

        /*private void FinDuJeu()
        {
            dispatcherTimer.Stop();
            timeTimer.Stop();
            perduText.Visibility = Visibility.Visible;
        }*/
        private void CreeObstacles()
        {
            int right = 0;
            int y = 0;
            for (int i = nb_asteroide; i < limiteAsteroide; i++)
            {
                delai -= 1;
                nbrObstacle += 1;
                y = aleatoire.Next(0, 350);
                if (i < limiteAsteroide && delai == 0)
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
                    nb_asteroide += 1;
                    delai = temps_apparition;
                }
                #endregion
            }
            if (score >= 5)
            {

                for (int i = nb_ovni; i < limiteOvni; i++)
                {
                    delai -= 1;
                    nbrObstacle += 1;
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
                    nb_ovni += 1;
                    delai = temps_apparition;
                }
            }
        }

        private void MouvementObstacle()
        {
            foreach (Rectangle asteroide in myCanvas.Children.OfType<Rectangle>())
            {
                if (asteroide is Rectangle && (string)asteroide.Tag == "asteroide")
                {
                    Canvas.SetRight(asteroide, Canvas.GetRight(asteroide) + vitesseAsteroide);
                }

                if (Canvas.GetRight(asteroide) > ActualWidth)
                {
                    score += 1;
                    enlever.Add(asteroide);
                    nb_asteroide = 0;
                }

            }
            foreach (Rectangle asteroide in enlever)
            {
                myCanvas.Children.Remove(asteroide);
                score = nbrObstacle - 1;
            }
            foreach (Rectangle ovni in myCanvas.Children.OfType<Rectangle>())
            {
                if (ovni is Rectangle && (string)ovni.Tag == "ovni")
                {
                    Canvas.SetRight(ovni, Canvas.GetRight(ovni) + vitesseOvni);
                    if (Canvas.GetLeft(ovni) / 2 == declencheur)
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
                if (Canvas.GetRight(ovni) > ActualWidth)
                {
                    declencheur = aleatoire.Next(50, (int)ActualWidth / 2);
                    enlever.Add(ovni);
                    nb_ovni = 0;
                }
            }
            foreach (Rectangle ovni in enlever)
            {
                myCanvas.Children.Remove(ovni);
                score = nbrObstacle - 1;
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
        private void Vitesse_Et_Quantite()
        {
            /*if (score >= 10 * repere_vitesse)
            {
                repere_vitesse = repere_vitesse + 1;
                vitesseAsteroide += 0.25;
                vitesseOvni += 0.25;
            }
            if (score >= (change_qnt_asteroide * limiteAsteroide))
            {
                change_qnt_asteroide = change_qnt_asteroide * 2;
                limiteAsteroide = limiteAsteroide + 1;
            }
            if (score >= (change_qnt_ovni * limiteOvni))
            {
                change_qnt_ovni = change_qnt_ovni * 2;                 
                limiteOvni = limiteOvni + 1;
            }*/
        }


        private void Collision(Rect rect_fusee)
        {
            foreach (var y in myCanvas.Children.OfType<Rectangle>())
            {
                // si le rectangle est un ennemi
                if (y is Rectangle && (string)y.Tag == "asteroide")
                {
                    Rect boite_asteroide = new Rect(Canvas.GetLeft(y), Canvas.GetTop(y), y.Width, y.Height);
                    if (rect_fusee.IntersectsWith(boite_asteroide))
                    {
                        dispatcherTimer.Stop();
                        MessageBox.Show("Vous avez été touché par un asteroide", "la mission est un échec", MessageBoxButton.OK, MessageBoxImage.Stop);
                    }
                }
                else if (y is Rectangle && (string)y.Tag == "ovni")
                {
                    Rect boite_ovni = new Rect(Canvas.GetLeft(y), Canvas.GetTop(y), y.Width, y.Height);
                    if (rect_fusee.IntersectsWith(boite_ovni))
                    {
                        dispatcherTimer.Stop();
                        MessageBox.Show("Vous avez été touché par un ovni", "la mission est un échec", MessageBoxButton.OK, MessageBoxImage.Stop);
                    }
                }
            }
        }


        private void AnimerFond()
        {
            double newPos = Canvas.GetLeft(background) - vitesseDefilement;
            Canvas.SetLeft(background, newPos);
        }

        private void MouvementFusee()
        {
            Rect rect_fusee = new Rect(Canvas.GetLeft(joueur), Canvas.GetTop(joueur), joueur.Width, joueur.Height);
            scoreText.Content = "Score: " + score;
            if (goDown && Canvas.GetTop(joueur) > 0)
            {
                Canvas.SetTop(joueur, Canvas.GetTop(joueur) - vitesseJoueur);
            }
            else if (goUp && Canvas.GetTop(joueur) + joueur.Height < Application.Current.MainWindow.Height)
            {
                Canvas.SetTop(joueur, Canvas.GetTop(joueur) + vitesseJoueur);
            }
            else
            {
                joueur.RenderTransform = rotation3;
            }
        }
        private void Jeu(object sender, EventArgs e)
        {
            Canvas.SetLeft(background, Canvas.GetLeft(background) - 9);
            Canvas.SetLeft(background2, Canvas.GetLeft(background2) - 9);
            //MouvementFusee();
            CreeObstacles();
            Vitesse_Et_Quantite();
            //Collision(rect_fusee);
            MouvementObstacle();
            mooveGroundAndBackground();
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

            string tempsFormat = minutes.ToString("D2") + ":" + secondes.ToString("D2");
            temps.Text = tempsFormat;
        }
        #endregion


        private void mooveGroundAndBackground()
        {
            if (Canvas.GetLeft(background) <= -800)
            {
                Canvas.SetLeft(background, Canvas.GetLeft(background2) + background2.Width);
            }
            if (Canvas.GetLeft(background2) <= -800)
            {
                Canvas.SetLeft(background2, Canvas.GetLeft(background) + background.Width);
            }
        }
        private void Rejouer_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
