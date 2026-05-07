using System;
using System.Collections.Generic;
using System.IO;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banque
{
    public class Entree
    {
        /// <summary>
        /// Fonction de lecture du fichier d'entrée pour les comptes
        /// </summary>
        /// <param name="input">Le chemin où se trouve le fichier</param>
        /// <param name="dictCarte">Le dictionnaire contenant les cartes</param>
        /// <returns>Un dictionnaire contenant l'id du compte ainsi que le compte</returns>
        public Dictionary<int, Compte> EntreeCompte(string input, Dictionary<long, Carte> dictCarte)
        {
            Dictionary<int, Compte> res = new Dictionary<int, Compte>();

            using(FileStream file = new FileStream(input, FileMode.Open, FileAccess.Read))
            {
                using(StreamReader reader= new StreamReader(file))
                {
                    string line;
                    string[] data;
                    int id;
                    long idCarte;
                    string type;
                    decimal soldeInit;

                    while((line = reader.ReadLine()) != null)
                    {
                        data = line.Split(';');

                        

                        if(data.Length > 2) // Vérification que l'on a assez de données
                        {

                            id = int.Parse(data[0]);
                            idCarte = long.Parse(data[1]);
                            type = data[2];

                            if (!res.ContainsKey(id) && idCarte.ToString().Length == 16) // Vérification que l'on n'a pas encore le compte et que son id soit assez long
                            {
                                if(type.ToLower() == "livret" || type.ToLower() == "courant") // Vérification que le type de comtpe soit correct.
                                {

                                    if (data.Length > 3) // Mise en place de la variable soldeInit
                                    {
                                        if (data[3].Contains(',')) //Vérification que le montant initial si il existe, n'a pas de ','
                                        {
                                            continue;
                                        }

                                        if(data[3] != "")
                                        {
                                            soldeInit = decimal.Parse(data[3], CultureInfo.InvariantCulture);
                                        } else
                                        {
                                            soldeInit = 0;
                                        }
                                    }
                                    else
                                    {
                                        soldeInit = 0;
                                    }


                                    Compte compte = new Compte(id, idCarte, type, soldeInit);
                                    dictCarte[idCarte].CompteListe.Add(compte); // Ajout du compte dans la liste des comptes de la carte
                                    res.Add(id, compte);
                                }
                            }
                        }
                    }


                }
            }

            return res;
        }

        /// <summary>
        /// Fonction de lecture du fichier d'entrée des cartes
        /// </summary>
        /// <param name="input">Chemin vers le fichier d'entrée</param>
        /// <returns>Un dictionnaire contenant l'id de la carte et la carte</returns>
        public Dictionary<long, Carte> EntreeCarte(string input)
        {
            Dictionary<long, Carte> res = new Dictionary<long, Carte>();

            using (FileStream file = new FileStream(input, FileMode.Open, FileAccess.Read))
            {
                using (StreamReader reader = new StreamReader(file))
                {
                    string line;
                    string[] data;
                    long id;
                    int plafond;
                    List<Compte> compteListe = new List<Compte>();

                    while ((line = reader.ReadLine()) != null)
                    {
                        data = line.Split(';');



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
                                            continue;
                                        }
                                    }
                                }
                                else
                                {
                                    plafond = 500;
                                }


                                compteListe = new List<Compte>();
                                Carte carte = new Carte(id, plafond, compteListe);
                                res.Add(id, carte);
                            }
                        }
                    }


                }
            }

            return res;
        }

        /// <summary>
        /// Fonction de lecture du ficheir d'entrée de transaction
        /// </summary>
        /// <param name="input">Chemin ers le fichier d'entrée</param>
        /// <returns>Un dictionnaire contenant l'id de la transaction et la transaction</returns>
        public Dictionary<int, Transaction> EntreeTransaction(string input)
        {
            Dictionary<int, Transaction> res = new Dictionary<int, Transaction>();

            using (FileStream file = new FileStream(input, FileMode.Open, FileAccess.Read))
            {
                using (StreamReader reader = new StreamReader(file))
                {
                    string line;
                    string[] data;
                    int id;
                    DateTime date;
                    decimal montant;
                    int expediteur;
                    int recepteur;

                    while ((line = reader.ReadLine()) != null)
                    {
                        data = line.Split(';');



                        if (data.Length == 5) // Vérification que l'on a assez de données
                        {

                            id = int.Parse(data[0]);
                            if(DateTime.TryParseExact(data[1], "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime dateNormalise))
                            {
                                date = dateNormalise;
                            } else
                            {
                                continue;
                            }

                            if (data[2].Contains(','))  // Vérification que le montant initial si il existe, n'a pas de ','
                            {
                                continue;
                            }

                            montant = decimal.Parse(data[2], CultureInfo.InvariantCulture);
                            expediteur = int.Parse(data[3]);
                            recepteur = int.Parse(data[4]);

                            if (!res.ContainsKey(id)) // Vérification que l'on n'a pas encore le compte et que son id soit assez long
                            {
                                if(montant > 0)
                                {
                                    Transaction transac = new Transaction(id, date, montant, expediteur, recepteur);
                                    res.Add(id, transac);

                                }
                            }
                        }
                    }


                }
            }

            return res;
        }
    }
}
