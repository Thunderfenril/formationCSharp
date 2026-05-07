using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banque
{
    public class Transaction
    {
        private readonly int _id;
        private readonly DateTime _date;
        private readonly decimal _montant;
        private readonly int _expediteur;
        private readonly int _recepteur;
        private string _statut;

        public Transaction(int id, DateTime date, decimal montant, int expediteur, int recepteur)
        {
            _id = id;
            _date = date;
            _montant = montant;
            _expediteur = expediteur;
            _recepteur = recepteur;
        }

        public string Statut { get => _statut; set => _statut = value; }

        public int Recepteur => _recepteur;

        public int Expediteur => _expediteur;

        public decimal Montant => _montant;


        /// <summary>
        /// Fonction qui va effectuer la transaction
        /// </summary>
        /// <param name="dictCompte">Dictionnaire des comptes</param>
        /// <param name="dictCarte">Dictionnaire des cartes</param>
        public void ExecTransaction(Dictionary<int, Compte> dictCompte, Dictionary<long, Carte> dictCarte)
        {
            long idCarte;
            decimal sommeTransacExpe;

            // On considère que si le dictionnaire de compte n'a pas l'id de l'expéditeur, on arrête
            if(!dictCompte.ContainsKey(_expediteur) && _expediteur != 0)
            {
                _statut = "KO";
                return;
            }

            //Dans le cas où l'expéditeur est externe, on a n'a pas d'action où l'on va enlever de l'argent
            if(_expediteur != 0)
            {

                idCarte = dictCompte[_expediteur].IdCarte; //Récupération de l'id de la carte
                sommeTransacExpe = dictCarte[idCarte].TransactionListe.Sum(transac => transac.Montant); //Récupération des montants qui ont quitté le compte

                /**
                 * On va retirer de l'argent du compte que si:
                 * 1.L'expéditeur a assez d'argent sur son compte pour le faire.
                 * 2.L'expéditeur ne vas pas dépasser son plafond avec ce transfert.
                 * 
                 * On considère qu'un plafond valant 0 signifie qu'il n'y a pas de plafond
                 */
                if (dictCompte[_expediteur].VerificationSolde(_montant) && ((sommeTransacExpe + _montant) < dictCarte[idCarte].Plafond || dictCarte[idCarte].Plafond == 0))
                {
                    dictCompte[_expediteur].Solde -= _montant;
                }
                else
                {
                    _statut = "KO";
                    return;
                }
            }

            // Pour la récepteur on va mettre l'argent su son compte
            if (_recepteur == 0 || dictCompte.ContainsKey(_recepteur))
            {
                if(_recepteur != 0)
                {
                    dictCompte[_recepteur].Solde += _montant;
                }

                _statut = "OK";

                //Si l'expediteur n'est pas externe à la banque, on va rajouter cette transaction dans son historique
                if(_expediteur != 0)
                {
                    idCarte = dictCompte[_expediteur].IdCarte;
                    dictCarte[idCarte].TransactionListe.Add(this);
                }

                return;
            }

            _statut = "Err"; //Cas spécifique où la transaction est invalide
            /*
                Cas transaction invalide:
                    - Expediteur = 0 && Recepteur inconnu
                    - Expediteur inconnu && Recepteur = 0
             */
        }
    }
}
