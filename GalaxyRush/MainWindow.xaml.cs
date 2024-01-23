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
        private bool finDePartie = false;
        private bool enPause = false;

        private int vitesseJoueur = 5;
        private int score;
        private int limiteAsteroide = 1;
        private int nbAsteroide;
        private int limiteOvni = 2;
        private int nbOvni;
        private int repereVitesse = 1;
        private int declencheur = 300;
        private int delai_asteroide = 1;
        private int delai_ovni = 1;
        private int tempsApparition = 20;
        private int bonus;
        private int protege;
        private int totemImmunite;
        private int limite_max = 4;
        private int invincibilite;
        private int limiteBouclier = 1;

        private double vitesseAsteroide = 7;
        private double vitesseOvni = 5;
        private double changeVitesse = 8;
        private double changeQteAsteroide = 2;
        private double vitesseBouclier = 5;
        List<Rectangle> listeObstacle= new List<Rectangle>();

        private RotateTransform rotation1 = new RotateTransform(135);
        private RotateTransform rotation2 = new RotateTransform(45);
        private RotateTransform rotation3 = new RotateTransform(90);

        private DispatcherTimer dispatcherTimer = new DispatcherTimer();

        private ImageBrush SkinJoueur = new ImageBrush();
        private ImageBrush backgroundImg = new ImageBrush();

        private List<Rectangle> enlever = new List<Rectangle>();
        private List<Rectangle> listeBouclier = new List<Rectangle>();

        Random aleatoire = new Random();

        #endregion


        public MainWindow()
        {
            InitializeComponent();
            myCanvas.Focus();
            MusiqueJeu.Play();

            Menu main = new Menu();
            main.ShowDialog();

            backgroundImg.ImageSource = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "\\Images\\fond_espace_jeu.png"));
            background.Fill = backgroundImg;
            background2.Fill = backgroundImg;
            dispatcherTimer.Interval = TimeSpan.FromMilliseconds(15);
            dispatcherTimer.Tick += Jeu;
            dispatcherTimer.Start();
            joueur.Fill = SkinJoueur;

        }


        private void Rejouer()
        {
#if DEBUG
            Console.WriteLine("rejouer");
#endif

            MainWindow menu = new();
            menu.Show();
            this.Close();
        }


        private void CleeCanvasAppuyee(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
            {
#if DEBUG
                Console.WriteLine("espace pressé");
#endif

                if (allerHaut == false)
                {
#if DEBUG

                    Console.WriteLine("fusée vers le bas");
#endif
                    allerHaut = true;
                    allerBas = false;
                    joueur.RenderTransform = rotation1;
                }
                else
                {
#if DEBUG
                    Console.WriteLine("fusée vers le haut");
#endif

                    allerBas = true;
                    allerHaut = false;
                    joueur.RenderTransform = rotation2;
                }
            }
            if (e.Key == Key.C)
            {
                if (totemImmunite == 0)
                {
#if DEBUG
                    Console.WriteLine("Immortel");
#endif

                    totemImmunite = 1;
                }
                else 
                {
#if DEBUG
                    Console.WriteLine("plus Immortel");
#endif
                    totemImmunite = 0;
                }
            }
            if (e.Key == Key.P)
            {
#if DEBUG
                Console.WriteLine("pause");
#endif

                MettrePause();
            }
            if (e.Key == Key.Escape)
            {
                Rejouer();
            }
        }


        private void QuitterBoutton(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();        
        }


        private void CleeCanvasRelachee(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space && allerHaut == true)
            {
                allerBas = true;
            }
        }


        private void FinDuJeu()
        {
#if DEBUG
            Console.WriteLine("Fin du jeu");
#endif

            dispatcherTimer.Stop();
            perduText.Visibility = Visibility.Visible;
            Rejouer1.Visibility = Visibility.Visible;
            Quitter.Visibility = Visibility.Visible;
            finDePartie = true;
        }


        private void CreeObstacles()
        {
            Obstacles asteroide = new Obstacles();
            Obstacles ovni = new Obstacles();
            int right = 0;
            int y;
            for (int i = nbAsteroide; i < limiteAsteroide; i++)
            {
                delai_asteroide -= 1;
                y = aleatoire.Next(0, 350);
                if (i < limiteAsteroide && delai_asteroide == 0)
                {
                    #region Asteroide
                    ImageBrush texturObstacle = new ImageBrush();
                    Rectangle nouveauAsteroide = new Rectangle
                    {
                        Tag = "asteroide",
                        Height = 100,
                        Width = 50,
                        Fill = texturObstacle,
                    };
                    Canvas.SetRight(nouveauAsteroide, right);
                    Canvas.SetTop(nouveauAsteroide, y);
                    listeObstacle.Add(nouveauAsteroide);
                    myCanvas.Children.Add(nouveauAsteroide);
                    texturObstacle.ImageSource = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "Images/asteroide.png"));
                    nbAsteroide += 1;
                    asteroide.Asteroide = nouveauAsteroide;
                    delai_asteroide = tempsApparition;
                }
                #endregion
            }
            #region Ovni
            if (score >= 5)
            {
                for (int i = nbOvni; i < limiteOvni; i++)
                {
                    delai_ovni -= 1;
                    y = aleatoire.Next(0, 350);
                    if (i < limiteOvni && delai_ovni == 0)
                    {
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
                        listeObstacle.Add(nouveauOvni);
                        myCanvas.Children.Add(nouveauOvni);
                        textureOvni.ImageSource = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "Images/ovni.png"));
                        nbOvni += 1;
                        ovni.Ovni = nouveauOvni;
                        delai_ovni = tempsApparition;
                    }
                }
            }
            #endregion
        }


        private void Bouclier()
        {
            if (bonus == 0 && score >= 6)
            {
                for (int i = 0; i < limiteBouclier; i++)
                {
                    bonus = 1;
                    int right = 0;
                    int y = aleatoire.Next(0, 350);
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
                    listeBouclier.Add(bouclier);
                    myCanvas.Children.Add(bouclier);
                    apparence.ImageSource = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "Images/bouclier.png"));
                }
            }
        }


        private void Protege()
        {
            if (protege == 0)
            {
                SkinJoueur.ImageSource = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "\\images\\fusee.png"));
            }
            else if (protege == 1)
            {
                SkinJoueur.ImageSource = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "\\images\\fusee_protege.png"));
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
                        for (int i = 0; i < 1; i++)
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
                    nbOvni -= 1;
                }
            }
            foreach (Rectangle ovni in enlever)
            {
                myCanvas.Children.Remove(ovni);
            }
        }


        private void Mouvement_Bouclier()
        {
            foreach (Rectangle bouclier in myCanvas.Children.OfType<Rectangle>())
            {
                if (bouclier is Rectangle && (string)bouclier.Tag == "bouclier")
                {
                    Canvas.SetRight(bouclier, Canvas.GetRight(bouclier) + vitesseBouclier);

                    if ((Canvas.GetRight(bouclier) > ActualWidth) || protege == 1)
                    {
                        enlever.Add(bouclier);
                        bonus = 0;

                    }
                }
            }
            foreach (Rectangle bouclier in enlever)
            {
                myCanvas.Children.Remove(bouclier);
            }
        }


        private void Vitesse_Et_Quantite()
        {
            if (score > changeVitesse * repereVitesse)
            {
                repereVitesse = repereVitesse + 1;
                vitesseAsteroide += 0.10;
                vitesseOvni += 0.10;
#if DEBUG
                Console.WriteLine("augmentation vitesse");
#endif
            }
            if (score > (changeQteAsteroide * limiteAsteroide))
            {
                if (limiteAsteroide <= limite_max) 
                {
                    changeQteAsteroide = changeQteAsteroide + changeQteAsteroide;
                    limiteAsteroide += 1;
#if DEBUG
                    Console.WriteLine("augmentation limite astéroïdes");
#endif
                }
            }
        }


        private void Collision()
        {
            invincibilite -= 1;
            Rect rect_fusee = new Rect(Canvas.GetLeft(joueur) + joueur.Width / 4, Canvas.GetTop(joueur) + joueur.Height / 4, joueur.Width / 2, joueur.Height / 2);
            Rectangle fuseeHitbox = new Rectangle
            {
                Height = joueur.Height / 2,
                Width = joueur.Width / 2,
            };
            Canvas.SetLeft(fuseeHitbox, Canvas.GetLeft(joueur) + joueur.Width / 4);
            Canvas.SetTop(fuseeHitbox, Canvas.GetTop(joueur) + joueur.Height / 4);
            myCanvas.Children.Add(fuseeHitbox);
            Panel.SetZIndex(fuseeHitbox, 99);
            foreach (Rectangle x in listeObstacle)
            {
                Rect obstacleBox = new Rect(800 - Canvas.GetRight(x)-x.ActualWidth, Canvas.GetTop(x), x.Width , x.Height);

                if (rect_fusee.IntersectsWith(obstacleBox))
                {
                    if ( protege == 0 && invincibilite <= 0 && totemImmunite == 0) 
                    {
#if DEBUG

                        Console.WriteLine("touché");
#endif

                        FinDuJeu();
                    }
                    else if ( protege == 1 && totemImmunite == 0)
                    {
#if DEBUG
                        Console.WriteLine("protégé");
#endif

                        protege = 0;
                        bonus = 0;
                        invincibilite = 290;
                    }
                }
            }
            foreach (Rectangle y in listeBouclier)
            {
                Rect bouclierBox = new Rect(800 - Canvas.GetRight(y)-y.ActualWidth, Canvas.GetTop(y), y.Width, y.Height);

                if (rect_fusee.IntersectsWith(bouclierBox))
                {
                    protege = 1;
                    bonus = 1;
                    if (y is Rectangle && (string)y.Tag == "bouclier")
                    {
                        myCanvas.Children.Remove(y);
                    }
                }
            }
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
            Canvas.SetLeft(background, Canvas.GetLeft(background) - 9);
            Canvas.SetLeft(background2, Canvas.GetLeft(background2) - 9);
            Protege();
            MouvementFusee();
            CreeObstacles();
            Bouclier();
            Mouvement_Bouclier();
            Vitesse_Et_Quantite();
            Collision();
            MouvementObstacle();
            BougerFond();
        }


        private void MettrePause()
        {
            if (finDePartie)
            {
                return;
            }
            else
            {
                enPause = !enPause;

                if (enPause)
                {
                    dispatcherTimer.Stop();
                    pauseText.Visibility = Visibility.Visible;
                    MusiqueJeu.Visibility = Visibility.Visible;
                    Rejouer1.Visibility = Visibility.Visible;
                    Quitter.Visibility = Visibility.Visible;
                }
                else
                {
                    pauseText.Visibility = Visibility.Collapsed;
                    dispatcherTimer.Start();
                    MusiqueJeu.Visibility = Visibility.Collapsed;
                    Rejouer1.Visibility = Visibility.Collapsed;
                    Quitter.Visibility = Visibility.Collapsed;
                }
            }
        }


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
            this.Close();
        }


        private void MenuBoutton(object sender, RoutedEventArgs e)
        {
            Menu retourMenu = new();
            retourMenu.Show();
            MusiqueJeu.Stop();
            this.Close();
        }
    }
}
