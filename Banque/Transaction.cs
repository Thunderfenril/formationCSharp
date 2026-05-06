using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banque
{
    internal class Transaction
    {
        private readonly int _id;
        private readonly DateTime _date;
        private readonly int _montant;
        private readonly int _expediteur;
        private readonly int _recepteur;
        private string _statut;

        public Transaction(int id, DateTime date, int montant, int expediteur, int recepteur)
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

        public int Montant => _montant;
    }
}
