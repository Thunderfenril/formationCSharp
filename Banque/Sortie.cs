using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Globalization;
using System.Threading.Tasks;

namespace Banque
{
    public class Sortie
    {
        /// <summary>
        /// Fonction pour l'affichage en sortie
        /// </summary>
        /// <param name="output">Chemin vers le fichier de sortie</param>
        /// <param name="transactions">Un dictionnaire avec toute les transactions</param>
        public void SortieImpression(string output, Dictionary<int, Transaction> transactions)
        {
            using (FileStream file = new FileStream(output, FileMode.Create, FileAccess.Write))
            {
                using(StreamWriter writer = new StreamWriter(file))
                {
                    StringBuilder line = new StringBuilder();
                    Dictionary<int, decimal> soldeComptes = new Dictionary<int, decimal>();


                    // Partie transaction
                    foreach (KeyValuePair<int, Transaction> transaction in transactions)
                    {
                        if (transaction.Value.Statut == "Err") continue; // On passe une transaction invalide

                        line.Clear();
                        line.Append($"{transaction.Key};{transaction.Value.Statut}");
                        writer.WriteLine(line);
                    }
                }
            }
        }
    }
}
