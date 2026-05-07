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


        public void ExecTransaction(Dictionary<int, Compte> dictCompte, Dictionary<long, Carte> dictCarte)
        {
            long idCarte;
            decimal sommeTransacExpe;

            if(_id == 14)
            {
                Console.WriteLine("14");
            }

            if(!dictCompte.ContainsKey(_expediteur) && _expediteur != 0)
            {
                _statut = "KO";
                return;
            }

            if(_expediteur != 0)
            {

                idCarte = dictCompte[_expediteur].IdCarte;
                sommeTransacExpe = dictCarte[idCarte].TransactionListe.Sum(transac => transac.Montant);

                if (_expediteur != 0 && (dictCompte[_expediteur].VerificationSolde(_montant) && ((sommeTransacExpe + _montant) < dictCarte[idCarte].Plafond || dictCarte[idCarte].Plafond == 0))) // On considère que plafond = 0 => Pas de plafond
                {
                    dictCompte[_expediteur].Solde -= _montant;
                }
                else
                {
                    _statut = "KO";
                    return;
                }
            }

            if (_recepteur == 0 || dictCompte.ContainsKey(_recepteur))
            {
                if(_recepteur != 0)
                {
                    dictCompte[_recepteur].Solde += _montant;
                }

                _statut = "OK";

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
