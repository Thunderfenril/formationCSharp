using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;

namespace Or.Models
{
    public enum TypeCompte { Courant, Livret }

    [XmlRoot]
    public class Compte
    {
        [XmlElement("Identificant")]
        public int Id { get; set; }
        [XmlIgnore]
        public long IdentifiantCarte { get; set; }
        [XmlElement("Type")]
        public TypeCompte TypeDuCompte { get; set; }
        [XmlIgnore]
        public decimal Solde { get => _solde;}

        [XmlElement("Solde")]
        public string SoldeSerializable
        {
            get => _solde.ToString("C2");
            set {
                decimal.TryParse(
                value,
                System.Globalization.NumberStyles.Currency,
                System.Globalization.CultureInfo.CurrentCulture,
                out _solde);
            }
        }

        private decimal _solde;

        public Compte(int id, long identifiantCarte, TypeCompte type, decimal soldeInitial)
        {
            Id = id;
            IdentifiantCarte = identifiantCarte;
            TypeDuCompte = type;
            _solde = soldeInitial;
        }

        private Compte()
        {

        }

        /// <summary>
        /// Action de dépôt d'argent sur le compte bancaire
        /// </summary>
        /// <param name="transaction"></param>
        /// <returns>Statut du dépôt</returns>
        public bool EstDepotValide(Transaction transaction)
        {
            if (transaction.Montant > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Action de retrait d'argent sur le compte bancaire
        /// </summary>
        /// <param name="transaction"></param>
        /// <returns>Statut du retrait</returns>
        public bool EstRetraitValide(Transaction transaction)
        {
            if (EstRetraitAutorise(transaction.Montant))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool EstRetraitAutorise(decimal montant)
        {
            return Solde >= montant && montant > 0;
        }

    }
}
