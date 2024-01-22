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

        private bool allerHaut = false;
        private bool allerBas = true;
        bool imageAffiche = true;
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
        private ImageBrush fusee = new ImageBrush();
        private int limiteAsteroide = 1;
        private int nbAsteroide = 0;
        private int nbOvni = 0;
        private int limiteOvni = 1;
        private int repereVitesse = 1;
        Random aleatoire = new Random();
        private bool enPause = false;
        private int declencheur = 300;
        private double vitesseAsteroide = 8;
        private double vitesseOvni = 6;
        private double changeVitesse = 10;
        private double changeQteAsteroide = 2;
        private double changeQteOvni = 3;
        private int delai = 1;
        private int tempsApparition = 20;
        ImageBrush backgroundImg = new ImageBrush();
        private bool bonus = false;
        private bool protege = false;
        private double vitesseDefilement = 5;
        List<Rectangle> listeAsteroide = new List<Rectangle>();

        #endregion

        public MainWindow()
        {
            InitializeComponent();
            myCanvas.Focus();
            MusiqueJeu.Play();

            Menu main = new Menu();
            main.ShowDialog();

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

            tempsJeu.Tick += ComptageTemps;
            tempsJeu.Interval = TimeSpan.FromSeconds(1);
            tempsJeu.Start();

        }


        private void Rejouer()
        {
            MainWindow menu = new();
            menu.ShowDialog();
            this.Close();
        }


        private void CleeCanvasAppuyee(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
            {
                if (allerHaut == false)
                {
                    allerHaut = true;
                    allerBas = false;
                    joueur.RenderTransform = rotation1;
                }
                else
                {
                    allerBas = true;
                    allerHaut = false;
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
            if (e.Key == Key.Escape)
            {
                QuitterPartie();
            }
        }


        private void QuitterPartie()
        {
            dispatcherTimer.Stop();
            tempsJeu.Stop();
            Quitter.Visibility = Visibility.Visible;
            Rejouer1.Visibility = Visibility.Visible;
            perduText.Visibility = Visibility.Visible;
        }


        private void QuitterBoutton(object sender, RoutedEventArgs e)
        {
            Menu menu = new Menu();
            menu.ShowDialog();
            MusiqueJeu.Stop();
            //MusiqueMenu.Play();
            this.Close();
        }


        //private void MenuBoutton(object sender, RoutedEventArgs e)
        //{
        //    Menu menu = new Menu();
        //    menu.Show();
        //}


        private void CleeCanvasRelachee(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space && allerHaut == true)
            {
                allerBas = true;
            }
        }


        private void FinDuJeu()
        {
            dispatcherTimer.Stop();
            tempsJeu.Stop();
            perduText.Visibility = Visibility.Visible;
        }


        private void CreeObstacles()
        {
            int right = 0;
            int y = 0;
            for (int i = nbAsteroide; i < limiteAsteroide; i++)
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
                    listeAsteroide.Add(nouveauObstacle);
                    myCanvas.Children.Add(nouveauObstacle);
                    texturObstacle.ImageSource = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "Images/asteroide.png"));
                    nbAsteroide += 1;
                    delai = tempsApparition;
                }
                #endregion
            }
            #region Ovni
            if (score >= 5)
            {

                for (int i = nbOvni; i < limiteOvni; i++)
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
                    nbOvni += 1;
                    delai = tempsApparition;
                }
            }
            #endregion
        }


        private void Bouclier()
        {
                bonus = true;
                int right = 0;
                int y = 0;
                y = aleatoire.Next(0, 350);
                ImageBrush apparence = new ImageBrush();
                Rectangle bouclier = new Rectangle
                {
                    Tag = "bouclier",
                    Height = 100,
                    Width = 50,
                    Fill = apparence,
                };
                Canvas.SetRight(bouclier, right);
                Canvas.SetTop(bouclier, y);
                myCanvas.Children.Add(bouclier);
                //apparence.ImageSource = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "Images/bouclier.jpg"));
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
                    nbAsteroide -= 1;
                }

            }
            foreach (Rectangle asteroide in enlever)
            {
                myCanvas.Children.Remove(asteroide);
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
                if (Canvas.GetRight(ovni) > ActualWidth)
                {
                    score += 1;
                    declencheur = aleatoire.Next(50, (int)ActualWidth / 2);
                    enlever.Add(ovni);
                    nbOvni = 0;
                }
            }
            foreach (Rectangle ovni in enlever)
            {
                myCanvas.Children.Remove(ovni);
            }
        }


        private void Vitesse_Et_Quantite()
        {
            if (score > changeVitesse * repereVitesse)
            {
                repereVitesse = repereVitesse + 1;
                vitesseAsteroide += 0.10;
                vitesseOvni += 0.10;
            }
            if (score > (changeQteAsteroide * limiteAsteroide))
            {
                changeQteAsteroide *= 4;
                limiteAsteroide++;
            }
            if (score > (changeQteOvni * limiteOvni))
            {
                changeQteOvni *= 4;
                limiteOvni++;
            }
        }


        private void Collision()
        {
            Rect rect_fusee = new Rect(Canvas.GetLeft(joueur), Canvas.GetTop(joueur), joueur.Width, joueur.Height);
            foreach (Rectangle x in listeAsteroide)
            {

                // création d'un asteroide 
#if DEBUG
                Console.WriteLine("asteroide");
#endif
                //Rectangle tamere = new Rectangle
                //{
                //    Height = x.Height,
                //    Width = x.Width,
                //    Stroke = Brushes.Red,

                //};
                //Canvas.SetLeft(tamere, Canvas.GetLeft(x));
                //Canvas.SetTop(tamere, Canvas.GetTop(x));
                //myCanvas.Children.Add(tamere);
                //Panel.SetZIndex(tamere, 99);
                Rect asteroideBox = new Rect(800 - Canvas.GetRight(x), Canvas.GetTop(x), x.Width, x.Height);

                if (rect_fusee.IntersectsWith(asteroideBox))
                {
#if DEBUG
                    Console.WriteLine("explosion");
#endif
                    dispatcherTimer.Stop();
                    perduText.Visibility = Visibility.Visible;
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
            scoreText.Content = "Score: " + score;
            if (allerBas && Canvas.GetTop(joueur) > 0)
            {
                Canvas.SetTop(joueur, Canvas.GetTop(joueur) - vitesseJoueur);
            }
            else if (allerHaut && Canvas.GetTop(joueur) + joueur.Height < Application.Current.MainWindow.Height)
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
            /* Rect rect_fusee = new Rect(Canvas.GetLeft(joueur), Canvas.GetTop(joueur), joueur.Width, joueur.Height);
             foreach (Rectangle y in myCanvas.Children.OfType<Rectangle>())
             {
                 // si le rectangle est un ennemi
                 if (y is Rectangle && (string)y.Tag == "asteroide")
                 {
                     Canvas.SetTop(y, Canvas.GetTop(y) - vitesseAsteroide);
                     Rect boite_asteroide = new Rect(Canvas.GetLeft(y), Canvas.GetTop(y), y.Width, y.Height);
                     Rectangle asterRectangle = new Rectangle
                     {
                         Height = boite_asteroide.Height,
                         Width = boite_asteroide.Width,
                         Stroke = Brushes.Red
                     };
                     Canvas.SetRight(asterRectangle, Canvas.GetRight(y));
                     Canvas.SetTop(asterRectangle, Canvas.GetTop(y));
                     if (boite_asteroide.IntersectsWith(rect_fusee))
                     {
                         Console.WriteLine("collision ateriode");
                         dispatcherTimer.Stop();
                         MessageBox.Show("Vous avez été touché par un asteroide", "la mission est un échec", MessageBoxButton.OK, MessageBoxImage.Stop);
                     }
                 }*/
            Canvas.SetLeft(background, Canvas.GetLeft(background) - 9);
            Canvas.SetLeft(background2, Canvas.GetLeft(background2) - 9);
            MouvementFusee();
            CreeObstacles();
            Bouclier();
            /*Rect rect_fusee = new Rect(Canvas.GetLeft(joueur), Canvas.GetTop(joueur), joueur.Width, joueur.Height);
            foreach (Rectangle x in myCanvas.Children.OfType<Rectangle>()) 
            {
                if (x is Rectangle && (string)x.Tag == "bouclier") 
                {
                    Canvas.SetRight(x, Canvas.GetRight(x) + vitesseOvni);
                    Rect bullet = new Rect(Canvas.GetLeft(x), Canvas.GetTop(x), x.Width, x.Height);
                    if ( rect_fusee.IntersectsWith(bullet)) 
                    {
                        dispatcherTimer.Stop()
                    }
                } 
            }*/


            ////Rect rect_fusee = new Rect(Canvas.GetLeft(joueur), Canvas.GetTop(joueur), joueur.Width, joueur.Height);
            //foreach (Rectangle y in myCanvas.Children.OfType<Rectangle>())
            //{
            //    if (y is Rectangle && (string)y.Tag == "asteroide")
            //    {
            //        // Déplacer l'astéroïde
            //        Canvas.SetRight(y, Canvas.GetRight(y) + vitesseAsteroide);

            //        // Créer un rectangle pour la détection de collision
            //        Rect boite_asteroide = new Rect(Canvas.GetLeft(y), Canvas.GetTop(y), y.Width, y.Height);

            //        // Vérifier la collision avec la fusée
            //        if (rect_fusee.IntersectsWith(boite_asteroide))
            //        {
            //            // Collision détectée
            //            if (protege)
            //            {
            //                // Si la protection est activée, ne pas arrêter le jeu
            //                // Peut-être ajouter des effets visuels ou sonores pour indiquer la protection
            //                // Réduire le score ou appliquer d'autres règles selon le besoin
            //            }
            //            else
            //            {
            //                // Si la protection n'est pas activée, arrêter le jeu
            //                dispatcherTimer.Stop();
            //                tempsJeu.Stop();
            //                MessageBox.Show("Vous avez été touché par un astéroïde", "La mission est un échec", MessageBoxButton.OK, MessageBoxImage.Stop);
            //            }
            //        }

            //        // Si l'astéroïde sort de l'écran, le retirer et ajuster les compteurs
            //        if (Canvas.GetTop(y) > myCanvas.ActualHeight)
            //        {
            //            enlever.Add(y);
            //            nbAsteroide -= 1;
            //        }
            //    }
            //}


            // Retirer les astéroïdes sortis de l'écran
            foreach (Rectangle asteroide in enlever)
            {
                myCanvas.Children.Remove(asteroide);
            }
            Vitesse_Et_Quantite();
            Collision();
            MouvementObstacle();
            BougerFond();
        }


        private void MettrePause()
        {
            enPause = !enPause;

            if (enPause)
            {
                dispatcherTimer.Stop();
                tempsJeu.Stop();
                pauseText.Visibility = Visibility.Visible;
                MusiqueJeu.Visibility = Visibility.Visible;
                butSon.Visibility = Visibility.Visible;
                Rejouer1.Visibility = Visibility.Visible;
                Quitter.Visibility = Visibility.Visible;
            }
            else
            {
                pauseText.Visibility = Visibility.Collapsed;
                dispatcherTimer.Start();
                tempsJeu.Start();
                MusiqueJeu.Visibility = Visibility.Collapsed;
                butSon.Visibility = Visibility.Collapsed;
                Rejouer1.Visibility = Visibility.Collapsed;
                Quitter.Visibility = Visibility.Collapsed;
            }
        }


        #region Temps
        private int minutes = 0;
        private int secondes = 0;
        private DispatcherTimer tempsJeu = new DispatcherTimer();


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


        private void BougerFond()
        {
            if (Canvas.GetLeft(background) <= -800)
                Canvas.SetLeft(background, Canvas.GetLeft(background2) + background2.Width);

            if (Canvas.GetLeft(background2) <= -800)
                Canvas.SetLeft(background2, Canvas.GetLeft(background) + background.Width);      
        }


        private void Rejouer_Click(object sender, RoutedEventArgs e)
        {
            MainWindow rejouer = new();
            rejouer.Show();
            MusiqueJeu.Stop();
            //MusiqueMenu.Play();
            this.Close();
        }


        private void MenuBoutton(object sender, RoutedEventArgs e)
        {
            Menu retourMenu = new();
            retourMenu.Show();
            MusiqueJeu.Stop();
            this.Close();
        }


        private void ButSon_Click(object sender, RoutedEventArgs e)
        {
            MusiqueJeu.IsMuted = !MusiqueJeu.IsMuted;
            string imageUri;
            if (MusiqueJeu.IsMuted)           
                imageUri = "\\Images\\son_couper.png";            
            else           
                imageUri = "\\Images\\son.png";
            butSon.Background = new ImageBrush(new BitmapImage(new Uri($"pack://application:,,,/GalaxyRush;component/{imageUri}", UriKind.Absolute)));
        }
    }
}
