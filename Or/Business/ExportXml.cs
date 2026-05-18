using Or.Models;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace Or.Business
{
    public class ExportXml
    {
        private readonly XmlSerializer serializer;
        private ExportDonnee _data;
        public ExportXml(long id)
        {
            serializer = new XmlSerializer(typeof(ExportDonnee));

            List<Compte> compteList = SqlRequests.ListeComptesAssociesCarte(id);
            List<Transaction> transacList = SqlRequests.ListeTransactionsAssociesCarte(id);

            _data = new ExportDonnee(compteList, transacList);
        }


        public void SerialiserComptesTransaction(string output)
        {
            using (FileStream file = File.OpenWrite(output))
            {
                serializer.Serialize(file, _data);
            }
        }

        public void ExportCompte(List<Compte> comptes)
        {

        }
    }
}
