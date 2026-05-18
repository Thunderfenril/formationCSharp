using System;
using Or.Business;
using System.Xml.Serialization;

namespace Or.Models
{
    // get et set utilisés correctement
    public class Transaction
    {
        [XmlElement(ElementName = "Identifiant", Order = 1)]
        public int IdTransaction { get; set; }

        [XmlIgnore]
        public DateTime? Horodatage { get; set; }

        [XmlElement(ElementName = "Date", Order = 2)]
        public string HorodatageString
        {
            get { return Horodatage.HasValue ? Horodatage.Value.ToString("dd/MM/yyyy HH:mm:ss") : null; }
            set { Horodatage = string.IsNullOrEmpty(value) ? (DateTime?)null : DateTime.Parse(value); }
        }
        [XmlIgnore]
        public decimal Montant { get; set; }

        [XmlElement(ElementName = "Montant", Order = 3)]
        public string SoldeSerializable
        {
            // Bien, utilisation des formats standards
            get => Montant.ToString("C2");
            set
            {
                decimal.TryParse(
                value,
                System.Globalization.NumberStyles.Currency,
                System.Globalization.CultureInfo.CurrentCulture,
                out _montant);
            }
        }

        private decimal _montant;

        [XmlElement(ElementName = "CompteExpediteur", Order = 4)]
        public int Expediteur { get; set; }
        [XmlElement(ElementName = "CompteDestinataire", Order = 5)]
        public int Destinataire { get; set; }

        [XmlIgnore]
        public Operation Type { get { return Tools.TypeTransaction(Expediteur, Destinataire); } }
        [XmlElement(ElementName = "Operation", Order = 6)]
        public string TypeString { get { return _converter.ConvertString(Type); } set { } }

        private TypeTransacConverter _converter;

        public Transaction(int idTransaction, DateTime horodatage, decimal montant, int expediteur, int destinataire)
        {
            IdTransaction = idTransaction;
            Horodatage = horodatage;
            Montant = montant;
            Expediteur = expediteur;
            Destinataire = destinataire;
            _converter = new TypeTransacConverter();
        }

        private Transaction()
        {

        }

        public bool ShouldSerializeExpediteur()
        {
            return Expediteur != 0;
        }

        public bool ShouldSerializeDestinataire()
        {
            return Destinataire != 0;
        }
    }
}
