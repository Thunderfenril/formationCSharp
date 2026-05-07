using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banque
{
    public class EntreeCarte
    {
        /// <summary>
        /// Fonction de lecture du fichier d'entrée des cartes
        /// </summary>
        /// <param name="input">Chemin vers le fichier d'entrée</param>
        /// <returns>Un dictionnaire contenant l'id de la carte et la carte</returns>
        public Dictionary<long, Carte> EntreeCarteCSV(string input)
        {
            Dictionary<long, Carte> res = new Dictionary<long, Carte>();

            using (FileStream file = new FileStream(input, FileMode.Open, FileAccess.Read))
            {
                using (StreamReader reader = new StreamReader(file))
                {
                    string line;

                    while ((line = reader.ReadLine()) != null)
                    {
                        (bool, long, int) verification = CarteVerification(line, res);

                        if (verification.Item1)
                        {
                            List<Compte> compteListe = new List<Compte>();
                            compteListe = new List<Compte>();
                            Carte carte = new Carte(verification.Item2, verification.Item3, compteListe);
                            res.Add(verification.Item2, carte);
                        }
                    }
                }
            }

            return res;
        }

        /// <summary>
        /// Fonction qui va vérifier si l'on peut créer une carte ou non
        /// </summary>
        /// <param name="carteData">Un string venant du csv</param>
        /// <param name="res">Le dictionaire des cartes</param>
        /// <returns>Un tuple avec les informations d'une carte et si l'on peut la créer ou non</returns>
        public (bool, long, int) CarteVerification(string carteData, Dictionary<long, Carte> res)
        {
            long id;
            int plafond;
            string[] data;

            data = carteData.Split(';');

            if (data.Length > 0) // Vérification que l'on a assez de données
            {

                id = long.Parse(data[0]);

                if (!res.ContainsKey(id) && id.ToString().Length == 16) // Vérification que l'on n'a pas encore le compte et que son id soit assez long
                {

                    if (data.Length > 1) // Mise en place de la variable soldeInit
                    {
                        if (data[1] == "")
                        {
                            plafond = 0;
                        }
                        else
                        {
                            plafond = int.Parse(data[1]);
                            if (plafond < 500 || plafond > 3000)
                            {
                                return (false, 0, 0);
                            }
                        }
                    }
                    else
                    {
                        plafond = 500;
                    }

                    return (true, id, plafond);
                }
                else
                {

                    return (false, 0, 0);
                }
            }
            else
            {
                return (false, 0, 0);
            }
        }
    }
}
