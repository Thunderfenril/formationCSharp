using System;
using Or.Business;

namespace Or.Models
{
    public class Transaction
    {
        public int IdTransaction { get; set; }
        public DateTime Horodatage { get; set; }
        public decimal Montant { get; set; }
        public int Expediteur { get; set; }
        public int Destinataire { get; set; }
        public Operation Type { get { return Tools.TypeTransaction(Expediteur, Destinataire); } }

        public Transaction(int idTransaction, DateTime horodatage, decimal montant, int expediteur, int destinataire)
        {
            IdTransaction = idTransaction;
            Horodatage = horodatage;
            Montant = montant;
            Expediteur = expediteur;
            Destinataire = destinataire;
        }
    }
}
