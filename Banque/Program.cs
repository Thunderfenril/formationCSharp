using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banque
{
    public class Banque
    {
        static void Main(string[] args)
        {
            string inputCompte      = @"C:\Users\FORMATION\Documents\FormationCSharp\formationCSharp\Banque\Files\compteIA.csv";
            string inputCarte       = @"C:\Users\FORMATION\Documents\FormationCSharp\formationCSharp\Banque\Files\carteIA.csv";
            string inputTransaction = @"C:\Users\FORMATION\Documents\FormationCSharp\formationCSharp\Banque\Files\transactionIA.csv";
            string output           = @"C:\Users\FORMATION\Documents\FormationCSharp\formationCSharp\Banque\Files\outputIA.csv";

            Dictionary<int, Compte> dictCompte          = new Dictionary<int, Compte>();
            Dictionary<long, Carte> dictCarte            = new Dictionary<long, Carte>();
            Dictionary<int, Transaction> dictTransac    = new Dictionary<int, Transaction>();

            Entree entree = new Entree();
            Sortie sortie = new Sortie();

            dictCarte = entree.EntreeCarteCall(inputCarte);
            dictCompte = entree.EntreeCompteCall(inputCompte, dictCarte);
            dictTransac = entree.EntreeTransactionCall(inputTransaction);

            foreach(KeyValuePair<int, Transaction> transaction in dictTransac)
            {
                transaction.Value.ExecTransaction(dictCompte, dictCarte);
            }

            sortie.SortieImpression(output, dictTransac);
        }
    }
}
