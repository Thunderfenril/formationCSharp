using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;

namespace Or.Models
{
    [XmlRoot("Comptes")]
    public class ExportDonnee
    {
        [XmlElement("Compte")]
        public List<ExportCompte> Comptes { get; set; }

        public ExportDonnee(List<Compte> compteList, List<Transaction> transactionList)
        {
            Comptes = new List<ExportCompte>();

            foreach(Compte compte in compteList)
            {
                //Gestion cas compte vide à développer
                List<Transaction> transac = transactionList //Récupération des 10 dernières transactions liés au compte
                                            .Where(t => t.Expediteur == compte.Id || t.Destinataire == compte.Id)
                                            .OrderByDescending(t => t.Horodatage)
                                            .Take(10)
                                            .ToList();

                Comptes.Add(new ExportCompte(compte, transac));
            }
        }

        private ExportDonnee()
        {

        }
    }

    public class ExportCompte
    {
        [XmlElement("Identifiant")]
        public int Id { get; set; }

        [XmlElement("Type")]
        public TypeCompte TypeDuCompte { get; set; }

        [XmlElement("Solde")]
        public string Solde { get; set; }

        [XmlArray("Transactions")]
        [XmlArrayItem("Transaction")]
        public List<Transaction> Transactions { get; set; }

        public ExportCompte()
        {
            Transactions = new List<Transaction>();
        }

        public ExportCompte(Compte compte, List<Transaction> transactions)
        {
            Id = compte.Id;
            TypeDuCompte = compte.TypeDuCompte;
            Solde = compte.SoldeSerializable;
            Transactions = transactions;
        }
    }
}
