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
        private bool bonus = false;
        private bool protege = false;

        private int vitesseJoueur = 5;
        private int nbrObstacle;
        private int score = 0;
        private int limiteAsteroide = 1;
        private int nbAsteroide = 0;
        private int limiteOvni = 1;
        private int nbOvni = 0;
        private int repereVitesse = 1;
        private int declencheur = 300;
        private int delai = 1;
        private int tempsApparition = 20;
        private int minutes = 0;
        private int secondes = 0;

        private double vitesseAsteroide = 7;
        private double vitesseOvni = 5;
        private double changeVitesse = 8;
        private double changeQteAsteroide = 2;
        private double changeQteOvni = 1;
        private double vitesseDefilement = 5;

        private RotateTransform rotation1 = new RotateTransform(135);
        private RotateTransform rotation2 = new RotateTransform(45);
        private RotateTransform rotation3 = new RotateTransform(90);

        private DispatcherTimer dispatcherTimer = new DispatcherTimer();
        private DispatcherTimer tempsJeu = new DispatcherTimer();

        private ImageBrush SkinJoueur = new ImageBrush();
        private ImageBrush fond = new ImageBrush();
        private ImageBrush fusee = new ImageBrush();
        private ImageBrush backgroundImg = new ImageBrush();

        private List<Rectangle> enlever = new List<Rectangle>();
        private List<Rectangle> listeAsteroide = new List<Rectangle>();

        Random aleatoire = new Random();

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
            dispatcherTimer.Interval = TimeSpan.FromMilliseconds(5);
            dispatcherTimer.Tick += Jeu;
            dispatcherTimer.Start();
            SkinJoueur.ImageSource = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "\\images\\fusee.png"));
            joueur.Fill = SkinJoueur;

            tempsJeu.Tick += ComptageTemps;
            tempsJeu.Interval = TimeSpan.FromSeconds(1);
            tempsJeu.Start();

        }


        private void Rejouer()
        {
            MainWindow menu = new();
            menu.Show();
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
            this.Close();
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
            dispatcherTimer.Stop();
            tempsJeu.Stop();
            perduText.Visibility = Visibility.Visible;
            Rejouer1.Visibility = Visibility.Visible;
            Quitter.Visibility = Visibility.Visible;
        }


        private void CreeObstacles()
        {
            int right = 0;
            int y;
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
                        Height = 80,
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
            Rect rect_fusee = new Rect(Canvas.GetLeft(joueur) + joueur.Width/4, Canvas.GetTop(joueur) + joueur.Height/4, joueur.Width/2, joueur.Height/2);
            Rectangle fuseeHitbox = new Rectangle
            {
                Height = joueur.Height/2,
                Width = joueur.Width/2,
                Stroke = Brushes.Red,

            };
            Canvas.SetLeft(fuseeHitbox, Canvas.GetLeft(joueur) + joueur.Width/4);
            Canvas.SetTop(fuseeHitbox, Canvas.GetTop(joueur) + joueur.Height/4);
            myCanvas.Children.Add(fuseeHitbox);
            Panel.SetZIndex(fuseeHitbox, 99);
            foreach (Rectangle x in listeAsteroide)
            {
                Rect asteroideBox = new Rect(800 - Canvas.GetRight(x), Canvas.GetTop(x), x.Width, x.Height);

                if (rect_fusee.IntersectsWith(asteroideBox))
                {
#if DEBUG
                    Console.WriteLine("explosion");
#endif
                    FinDuJeu();
                        //MessageBox.Show("Vous avez été touché par un asteroide", "la mission est un échec", MessageBoxButton.OK, MessageBoxImage.Stop);

                }
            }
            myCanvas.Children.Remove(fuseeHitbox);
#if DEBUG
            Console.WriteLine("retire");
#endif
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
            MouvementFusee();
            CreeObstacles();
            Bouclier();

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
            if (finDePartie)
            {
                return;
            }
            enPause = !enPause;

            if (enPause)
            {
                dispatcherTimer.Stop();
                tempsJeu.Stop();
                pauseText.Visibility = Visibility.Visible;
                MusiqueJeu.Visibility = Visibility.Visible;
                Rejouer1.Visibility = Visibility.Visible;
                Quitter.Visibility = Visibility.Visible;
            }
            else
            {
                pauseText.Visibility = Visibility.Collapsed;
                dispatcherTimer.Start();
                tempsJeu.Start();
                MusiqueJeu.Visibility = Visibility.Collapsed;
                Rejouer1.Visibility = Visibility.Collapsed;
                Quitter.Visibility = Visibility.Collapsed;
            }
        }


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
