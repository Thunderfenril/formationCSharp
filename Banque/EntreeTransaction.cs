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

            using (FileStream file = new FileStream(input, FileMode.Open, FileAccess.Read))
            {
                using (StreamReader reader = new StreamReader(file))
                {
                    string line;

                    while ((line = reader.ReadLine()) != null)
                    {
                        (bool, int, DateTime, decimal, int, int) verificationTrans = TransactionVerification(line, res);

                        if (verificationTrans.Item1)
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
                    return (false, 0, DateTime.MinValue, 0, 0, 0);
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
