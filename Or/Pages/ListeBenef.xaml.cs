using Or.Business;
using Or.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Or.Pages
{
    /// <summary>
    /// Logique d'interaction pour ListeBenef.xaml
    /// </summary>
    public partial class ListeBenef : PageFunction<long>
    {
        public ListeBenef(long numCarte)
        {
            InitializeComponent();
            Carte carte = SqlRequests.InfosCarte(numCarte);
            List<Beneficiaire> beneficiaires = SqlRequests.ListeBeneficiaireAssocieClient(numCarte);

            NomCarte.Text = carte.NomClient;
            PrenomCarte.Text = carte.PrenomClient;

            listView.ItemsSource = beneficiaires;
        }

        private void Retour_Click(object sender, RoutedEventArgs e)
        {
            OnReturn(null);
        }


        private void deleteBenef(object sender, RoutedEventArgs e)
        {
            long numCarte = 0;
            int idCompte = 0;

            SqlRequests.SuppressionBeneficiaire(numCarte, idCompte);
        }
    }
}
