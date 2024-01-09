using System.Windows;

namespace GalaxyRush
{
    /// <summary>
    /// Logique d'interaction pour Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {

        private void ButJouer_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }

        private void ButQuitter_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }
    }
}



