using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

            using (FileStream file = new FileStream(input, FileMode.Open, FileAccess.Read))
            {
                using (StreamReader reader = new StreamReader(file))
                {
                    string line;

                    while ((line = reader.ReadLine()) != null)
                    {

                        (bool, int, long, string, decimal) compteVerif = CompteVerification(line, res);

                        if (compteVerif.Item1 != false)
                        {
                            Compte compte = new Compte(compteVerif.Item2, compteVerif.Item3, compteVerif.Item4, compteVerif.Item5);
                            if (dictCarte.ContainsKey(compteVerif.Item3))
                            {
                                dictCarte[compteVerif.Item3].CompteListe.Add(compte); // Ajout du compte dans la liste des comptes de la carte
                                res.Add(compte.IdCompte, compte);
                            }
                        }
                    }
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
        public (bool, int, long, string, decimal) CompteVerification(string compteData, Dictionary<int, Compte> res)
        {
            string[] data;
            int id;
            long idCarte;
            string type;
            decimal soldeInit;

            data = compteData.Split(';');

            if (data.Length > 2) // Vérification que l'on a assez de données
            {

                id = int.Parse(data[0]);
                idCarte = long.Parse(data[1]);
                type = data[2];

                if (!res.ContainsKey(id) && idCarte.ToString().Length == 16) // Vérification que l'on n'a pas encore le compte et que son id soit assez long
                {
                    if (type.ToLower() == "livret" || type.ToLower() == "courant") // Vérification que le type de comtpe soit correct.
                    {

                        if (data.Length > 3) // Mise en place de la variable soldeInit
                        {
                            if (data[3].Contains(',')) //Vérification que le montant initial si il existe, n'a pas de ','
                            {

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
            }

            return (false, 0, 0, "", 0);
        }
    }
}
