using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banque
{
    internal class Sortie
    {
        public void SortieImpression(string output, Dictionary<int, Compte> comptes, Dictionary<int, Transaction> transactions)
        {
            using (FileStream file = new FileStream(output, FileMode.Append, FileAccess.Write))
            {
                using(StreamWriter writer = new StreamWriter(file))
                {
                    StringBuilder line = new StringBuilder();
                    Dictionary<int, int> soldeComptes = new Dictionary<int, int>();

                    //Création des headers
                    line.Append("Sortie;");

                    foreach(KeyValuePair<int, Compte> compte in comptes)
                    {
                        line.Append($"Solde Compte {compte.Key};"); ;
                    }
                    line.Remove(line.Length - 1, 1); //Retire le dernier ';'

                    writer.WriteLine(line);


                    //Ligne Solde initiale
                    line.Clear();
                    line.Append(";");

                    foreach (KeyValuePair<int, Compte> compte in comptes)
                    {
                        line.Append($"{compte.Value.SoldeInit};");
                        soldeComptes.Add(compte.Key, compte.Value.SoldeInit);
                    }
                    line.Remove(line.Length - 1, 1); //Retire le dernier ';'

                    writer.WriteLine(line);


                    // Partie transaction
                    line.Clear();
                    foreach (KeyValuePair<int, Transaction> transaction in transactions)
                    {
                        line.Append($"{transaction.Key},{transaction.Value.Statut};");
                        foreach(KeyValuePair<int, int> soldeCompte in soldeComptes)
                        {

                            /*
                             * Cas 1: Emetteur correspond à la clé: Solde - Montant
                             * Cas 2: Recepteur correspond à la clé: Solde + Montant
                             * Cas 3: Clé différent de emteur et recepteur: Solde
                             * Cas 4: KO: Affichage normal
                             */
                            if (transaction.Value.Statut == "OK" && transaction.Value.Recepteur == soldeCompte.Key)
                            {

                            }
                            else
                            {
                                line.Append("");
                            }
                        }
                    }
                }
            }
        }
    }
}
