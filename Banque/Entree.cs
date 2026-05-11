using System;
using System.Collections.Generic;
using System.IO;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banque
{
    /// <summary>
    /// Classe pour dispatcher les lecture dans les bonnes classes
    /// </summary>
    public class Entree
    {
        public Dictionary<int, Compte> EntreeCompteCall(string input, Dictionary<long, Carte> dictCarte)
        {
            Dictionary<int, Compte> res;
            EntreeCompte entree = new EntreeCompte();
            res = entree.EntreeCompteCSV(input, dictCarte);

            return res;
        }

        public Dictionary<long, Carte> EntreeCarteCall(string input)
        {
            Dictionary<long, Carte> res;
            EntreeCarte entree = new EntreeCarte();
            res = entree.EntreeCarteCSV(input);

            return res;
        }


        public Dictionary<int, Transaction> EntreeTransactionCall(string input)
        {
            Dictionary<int, Transaction> res;
            EntreeTransaction entree = new EntreeTransaction();
            res = entree.EntreeTransactionCSV(input);

            return res;
        }
    }
}
