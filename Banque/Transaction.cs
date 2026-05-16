using System;
using System.Collections.Generic;
using System.Linq;

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
        public void ExecTransaction(Dictionary<int, Compte> dictCompte, Dictionary<long, Carte> dictCarte, Dictionary<int, string> err)
        {
            long idCarte;
            decimal sommeTransacExpe;

            if (_expediteur != 0 && !dictCompte.ContainsKey(_expediteur))
            {
                err.Add(_id, $"L'expediteur n'est pas dans la liste des comptes. Expediteur: {_expediteur}");
                _statut = "Err"; //Cas spécifique où la transaction est invalide
                return;
            }

            if (_recepteur != 0 && !dictCompte.ContainsKey(_recepteur))
            {
                err.Add(_id, $"Le recepteur n'est pas dans la liste des comptes. Recepteur: {_recepteur}");
                _statut = "Err"; //Cas spécifique où la transaction est invalide
                return;
            }

            //Dans le cas où l'expéditeur est externe, on a n'a pas d'action où l'on va enlever de l'argent
            if (_expediteur != 0)
            {

                idCarte = dictCompte[_expediteur].IdCarte; //Récupération de l'id de la carte
                sommeTransacExpe = dictCarte[idCarte].TransactionListe.Sum(transac => transac.Montant); //Récupération des montants qui ont quitté le compte
                // Il manque la contrainte sur la plage de dates - 10 jours

                /**
                 * On va retirer de l'argent du compte que si:
                 * 1.L'expéditeur a assez d'argent sur son compte pour le faire.
                 * 2.L'expéditeur ne vas pas dépasser son plafond avec ce transfert.
                 * 3.L'expediteur et le recepteur, si différent, doivent être des comptes Courant
                 * 4.Si l'expéditeur et le récepteur sont les mêmes alors c'est ok
                 * 
                 * On considère qu'un plafond valant 0 signifie qu'il n'y a pas de plafond
                 */
                if (
                    dictCompte[_expediteur].VerificationSolde(_montant) &&
                    (
                        (sommeTransacExpe + _montant) < dictCarte[idCarte].Plafond || dictCarte[idCarte].Plafond == 0
                    )
                    )
                {

                    if (VerificationTransfert(dictCompte, _expediteur, _recepteur))
                    {
                        err.Add(_id, "Transfert entre compte interdis");
                        _statut = "KO";
                        return;
                    }
                    dictCompte[_expediteur].Solde -= _montant;
                }
                else
                {
                    err.Add(_id, $"Pas assez sur le compte, ou plafond dépassé.\nSolde: {dictCompte[_expediteur].Solde}\nMontant: {_montant}" +
                        $"\nPlafond: {dictCarte[idCarte].Plafond}\nDépense actuel: {sommeTransacExpe}");
                    _statut = "KO";
                    return;
                }
            }

            // Pour la récepteur on va mettre l'argent su son compte
            if (_recepteur == 0 || dictCompte.ContainsKey(_recepteur))
            {
                if (_recepteur != 0)
                {
                    dictCompte[_recepteur].Solde += _montant;
                }

                _statut = "OK";

                //Si l'expediteur n'est pas externe à la banque, on va rajouter cette transaction dans son historique
                if (_expediteur != 0)
                {
                    idCarte = dictCompte[_expediteur].IdCarte;
                    dictCarte[idCarte].TransactionListe.Add(this);
                }

                return;
            }
        }

        /// <summary>
        /// Fonction qui va vérifier si un transfert est ok ou non
        /// On va ignorer le cas où le recepteur est 0
        /// On va vérifier que:
        /// 1. L'expediteur est dans notre dictionnaire
        /// 2. Le recepteur est dans notre dictionnaire
        /// 3. Si ils sont differents on va verifier que le transfert se fasse de compte Courant a compte Courant
        /// </summary>
        /// <param name="dictCompte">Le dictionnaire de Compte</param>
        /// <param name="_expediteur">L'id de l'expediteur</param>
        /// <param name="_recepteur">L'id du receveur</param>
        /// <returns>Un booleen</returns>
        public bool VerificationTransfert(Dictionary<int, Compte> dictCompte, int _expediteur, int _recepteur)
        {
            bool res = false;

            if (_recepteur != 0 &&
                        dictCompte[_expediteur].IdCarte != dictCompte[_recepteur].IdCarte &&
                        (dictCompte[_expediteur].Type != "Courant" || dictCompte[_recepteur].Type != "Courant")
               )
            {
                res = true;
            }

            // je ne suis pas sûr que le nom ne soit pas source de confusion - on ne traite pas si true :/ 
            return res;
        }
    }
}
