using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banque
{
    public class EntreeTransaction
    {
        /// <summary>
        /// Fonction de lecture du ficheir d'entrée de transaction
        /// </summary>
        /// <param name="input">Chemin ers le fichier d'entrée</param>
        /// <returns>Un dictionnaire contenant l'id de la transaction et la transaction</returns>
        public Dictionary<int, Transaction> EntreeTransactionCSV(string input)
        {
            Dictionary<int, Transaction> res = new Dictionary<int, Transaction>();
            Dictionary<int, string> err = new Dictionary<int, string>();

            using (FileStream file = new FileStream(input, FileMode.Open, FileAccess.Read))
            {
                using (StreamReader reader = new StreamReader(file))
                {
                    string line;

                    while ((line = reader.ReadLine()) != null)
                    {
                        (bool, int, DateTime, decimal, int, int) verificationTrans = TransactionVerification(line, res, err);

                        if (verificationTrans.Item1)
                        {
                            Transaction transac = new Transaction(verificationTrans.Item2, verificationTrans.Item3, verificationTrans.Item4, verificationTrans.Item5, verificationTrans.Item6);
                            res.Add(verificationTrans.Item2, transac);
                        }
                    }

                    ImpressionErreur(err);
                }
            }

            return res;
        }




        /// <summary>
        /// Fonction qui va vérifier si l'on peut créer une transaction ou non
        /// </summary>
        /// <param name="transactionData">Un string venant du csv</param>
        /// <param name="res">Le dictionaire des transactions</param>
        /// <returns>Un tuple avec les informations d'une transaction et si l'on peut la créer ou non</returns>
        public (bool, int, DateTime, decimal, int, int) TransactionVerification(string transactionData, Dictionary<int, Transaction> res, Dictionary<int, string> err)
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
                    err.Add(id, $"Pas le bon format de date {data[1]}");
                    return (false, 0, DateTime.MinValue, 0, 0, 0);
                }

                if (data[2].Contains(','))  // Vérification que le montant initial si il existe, n'a pas de ','
                {
                    err.Add(id, $"Le montant contient une ',' {data[2]}");
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
                } else
                {
                    err.Add(id, $"id déjà présent");
                }
            }
            return (false, 0, DateTime.MinValue, 0, 0, 0);
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
                        writer.WriteLine($"Erreur pour la transaction {err.Key}: {err.Value}");
                    }
                }
            }
        }
    }
}
