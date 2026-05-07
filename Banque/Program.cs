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
            string inputCompte      = @"C:\Users\FORMATION\Documents\FormationCSharp\formationCSharp\Banque\Files\compte.csv";
            string inputCarte       = @"C:\Users\FORMATION\Documents\FormationCSharp\formationCSharp\Banque\Files\carte.csv";
            string inputTransaction = @"C:\Users\FORMATION\Documents\FormationCSharp\formationCSharp\Banque\Files\transaction.csv";
            string output           = @"C:\Users\FORMATION\Documents\FormationCSharp\formationCSharp\Banque\Files\output.csv";

            Dictionary<int, Compte> dictCompte          = new Dictionary<int, Compte>();
            Dictionary<long, Carte> dictCarte            = new Dictionary<long, Carte>();
            Dictionary<int, Transaction> dictTransac    = new Dictionary<int, Transaction>();

            Entree entree = new Entree();
            Sortie sortie = new Sortie();

            dictCarte = entree.EntreeCarte(inputCarte, dictCompte);
            dictCompte = entree.EntreeCompte(inputCompte, dictCarte);
            dictTransac = entree.EntreeTransaction(inputTransaction);

            foreach(KeyValuePair<int, Transaction> transaction in dictTransac)
            {
                transaction.Value.ExecTransaction(dictCompte, dictCarte);
            }

            sortie.SortieImpression(output, dictTransac);
        }
    }
}
