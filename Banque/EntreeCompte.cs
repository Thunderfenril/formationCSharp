using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace Banque
{
    public class EntreeCompte
    {

        /// <summary>
        /// Fonction de lecture du fichier d'entrée pour les comptes
        /// </summary>
        /// <param name="input">Le chemin où se trouve le fichier</param>
        /// <param name="dictCarte">Le dictionnaire contenant les cartes</param>
        /// <returns>Un dictionnaire contenant l'id du compte ainsi que le compte</returns>
        public Dictionary<int, Compte> EntreeCompteCSV(string input, Dictionary<long, Carte> dictCarte)
        {
            Dictionary<int, Compte> res = new Dictionary<int, Compte>();
            Dictionary<int, string> err = new Dictionary<int, string>();

            using (FileStream file = new FileStream(input, FileMode.Open, FileAccess.Read))
            {
                using (StreamReader reader = new StreamReader(file))
                {
                    string line;

                    while ((line = reader.ReadLine()) != null)
                    {

                        (bool, int, long, string, decimal) compteVerif = CompteVerification(line, res, err);

                        if (compteVerif.Item1 != false)
                        {
                            // Passer par des struct intermédiaires rendrait plus lisible le passage aux constructeurs (ItemX pas évident)
                            Compte compte = new Compte(compteVerif.Item2, compteVerif.Item3, compteVerif.Item4, compteVerif.Item5);
                            if (dictCarte.ContainsKey(compteVerif.Item3))
                            {
                                dictCarte[compteVerif.Item3].CompteListe.Add(compte); // Ajout du compte dans la liste des comptes de la carte
                                res.Add(compte.IdCompte, compte);
                            }
                        }
                    }

                    ImpressionErreur(err);
                }
            }

            return res;
        }

        /// <summary>
        /// Fonction qui va vérifier si l'on peut créer un compte ou non
        /// </summary>
        /// <param name="compteData">Un string venant du csv</param>
        /// <param name="res">Le dictionnaire des comptes</param>
        /// <returns>Un tuple avec les informations du compte et si l'on peut créer ou non le compte</returns>
        public (bool, int, long, string, decimal) CompteVerification(string compteData, Dictionary<int, Compte> res, Dictionary<int, string> err)
        {
            string[] data;
            int id;
            long idCarte;
            string type;
            decimal soldeInit;

            data = compteData.Split(';');

            if (data.Length > 2) // Vérification que l'on a assez de données
            {
                // Même chose int.TryParse
                id = int.Parse(data[0]);
                idCarte = long.Parse(data[1]);
                type = data[2];

                if (!res.ContainsKey(id) && idCarte.ToString().Length == 16) // Vérification que l'on n'a pas encore le compte et que son id soit assez long
                {
                    if (type.ToLower() == "livret" || type.ToLower() == "courant") // Vérification que le type de compte soit correct.
                    {

                        if (data.Length > 3) // Mise en place de la variable soldeInit
                        {
                            if (data[3].Contains(',')) //Vérification que le montant initial si il existe, n'a pas de ','
                            {
                                err.Add(id, "Montant avec une ','");
                                return (false, 0, 0, "", 0);
                            }

                            if (data[3] != "")
                            {
                                soldeInit = decimal.Parse(data[3], CultureInfo.InvariantCulture);
                            }
                            else
                            {
                                soldeInit = 0;
                            }
                        }
                        else
                        {
                            soldeInit = 0;
                        }

                        return (true, id, idCarte, type, soldeInit);
                    }
                }
                else
                {
                    err.Add(id, "Cet id existe déjà");
                }
            }

            return (false, 0, 0, "", 0);
        }

        /// <summary>
        /// Fonction pour imprimer les erreurs
        /// </summary>
        /// <param name="errDex">Un dictionnaire qui contient l'id de l'objet et le texte d'erreur attribué</param>
        public void ImpressionErreur(Dictionary<int, string> errDex)
        {
            using (FileStream file = new FileStream(@"C:\Users\FORMATION\Documents\FormationCSharp\formationCSharp\Banque\Files\err.txt", FileMode.Append, FileAccess.Write))
            {
                using (StreamWriter writer = new StreamWriter(file))
                {
                    foreach (KeyValuePair<int, string> err in errDex)
                    {
                        writer.WriteLine($"Erreur pour le compte {err.Key}: {err.Value}");
                    }
                }
            }
        }
    }
}
