using Or.Business;
using Or.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using System.Windows.Navigation;

namespace Or.Pages
{
    /// <summary>
    /// Logique d'interaction pour AddBenef.xaml
    /// </summary>
    public partial class AddBenef : PageFunction<long>
    {
        private long _id;
        public AddBenef(long id)
        {
            InitializeComponent();
            _id = id;
        }

        private void ValiderAjout_Click(object sender, RoutedEventArgs e)
        {
            int idCompte = int.Parse(NCompte.Text);

            /**
             * Condition d'ajout:
             * - La combinaison n'existe pas dans la table beneficiaire
             * - Le compte est un compte courant
             * - Le compte n'est pas présent sur la carte
             * 
             */

            /*if(!SqlRequests.EstBeneficiairePotentiel(idCompte))
            {
                Error.TypeErreur(Tools.EcranAjoutBeneficiaire, "Type"); //A faire en plus, dissociation entre si le compte existe et le type de compte
                return;
            }*/

            Compte c = SqlRequests.InfosCompte(idCompte);

            if(c == null)
            {
                Error.TypeErreur(Tools.EcranAjoutBeneficiaire, "Inconnu");
                return;
            } else if(c.IdentifiantCarte == _id)
            {
                Error.TypeErreur(Tools.EcranAjoutBeneficiaire, "Possede");
                return;
            } else if(c.TypeDuCompte == TypeCompte.Livret)
            {
                Error.TypeErreur(Tools.EcranAjoutBeneficiaire, "Type");
                return;
            }

            List<Beneficiaire> benefList = SqlRequests.ListeBeneficiaireAssocieClient(_id);

            if(benefList.Any(p => p.IdCompte == idCompte))
            {
                Error.TypeErreur(Tools.EcranAjoutBeneficiaire, "Pair");
                return;
            }

            SqlRequests.AjoutBeneficiaire(_id, idCompte);
        }

        private void Retour_Click(object sender, RoutedEventArgs e)
        {
            OnReturn(null);
        }
    }
}
