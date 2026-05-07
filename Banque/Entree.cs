using System;
using System.Collections.Generic;
using System.IO;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banque
{
    public struct EntreeCompteStruct {
        bool statut;
        int id;
        long idCarte;
        string type;
        decimal soldeInit;
    }
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

                    while((line = reader.ReadLine()) != null)
                    {

                        (bool, int, long, string, decimal) compteVerif = CompteVerification(line, res);

                        if (compteVerif.Item1 != false)
                        {
                            Compte compte = new Compte(compteVerif.Item2, compteVerif.Item3, compteVerif.Item4, compteVerif.Item5);
                            dictCarte[compteVerif.Item3].CompteListe.Add(compte); // Ajout du compte dans la liste des comptes de la carte
                            res.Add(compte.IdCompte, compte);
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

                    while ((line = reader.ReadLine()) != null)
                    {
                        (bool, long, int) verification = CarteVerification(line, res);

                        if(verification.Item1)
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

                    while ((line = reader.ReadLine()) != null)
                    {
                        (bool, int, DateTime, decimal, int, int) verificationTrans = TransactionVerification(line, res);

                        if(verificationTrans.Item1)
                        {
                            Transaction transac = new Transaction(verificationTrans.Item2, verificationTrans.Item3, verificationTrans.Item4, verificationTrans.Item5, verificationTrans.Item6);
                            res.Add(verificationTrans.Item2, transac);
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
                } else
                {

                    return (false, 0, 0);
                }
            } else
            {
                return (false, 0, 0);
            }
        }

        /// <summary>
        /// Fonction qui va vérifier si l'on peut créer une transaction ou non
        /// </summary>
        /// <param name="transactionData">Un string venant du csv</param>
        /// <param name="res">Le dictionaire des transactions</param>
        /// <returns>Un tuple avec les informations d'une transaction et si l'on peut la créer ou non</returns>
        public (bool, int, DateTime, decimal, int, int) TransactionVerification(string transactionData, Dictionary<int, Transaction> res)
        {
            string[] data;
            int id;
            DateTime date;
            decimal montant;
            int expediteur;
            int recepteur;

            data = transactionData.Split(';');



            if (data.Length == 5) // Vérification que l'on a assez de données
            {

                id = int.Parse(data[0]);
                if (DateTime.TryParseExact(data[1], "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime dateNormalise))
                {
                    date = dateNormalise;
                }
                else
                {
                    return (false, 0, DateTime.MinValue ,0,0,0);
                }

                if (data[2].Contains(','))  // Vérification que le montant initial si il existe, n'a pas de ','
                {
                    return (false, 0, DateTime.MinValue, 0, 0, 0);
                }

                montant = decimal.Parse(data[2], CultureInfo.InvariantCulture);
                expediteur = int.Parse(data[3]);
                recepteur = int.Parse(data[4]);

                if (!res.ContainsKey(id)) // Vérification que l'on n'a pas encore le compte et que son id soit assez long
                {
                    if (montant > 0)
                    {
                        return (true, id, date, montant, expediteur, recepteur);

                    }
                }
            }
            return (false, 0, DateTime.MinValue, 0, 0, 0);
        }
    }
}
