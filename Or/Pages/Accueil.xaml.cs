using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Or.Business;


namespace Or.Pages
{
    /// <summary>
    /// Logique d'interaction pour Accueil.xaml
    /// </summary>
    public partial class Accueil : Page
    {
        public Accueil()
        {
            InitializeComponent();
        }

        public void GoConsultationCarte(object sender, RoutedEventArgs e)
        {
            bool estCarteValide = long.TryParse(NumeroCarte.Text, out long result);
            if (estCarteValide)
            {
                ConsultationCarte cc = new ConsultationCarte(result);

                if(cc.Prenom.Text == "")
                {
                    return;
                }

                NavigationService.Navigate(cc);
            }
            else
            {
                Error.TypeErreur("accueil", "Carte");
            }
        }

        public void GoMouse(object sender, RoutedEvent e)
        {

        }
    }
}
