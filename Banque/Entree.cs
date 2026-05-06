using System;
using System.Collections.Generic;
using System.IO;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banque
{
    internal class Entree
    {
        public Dictionary<int, Compte> EntreeCompte()
        {
            Dictionary<int, Compte> res = new Dictionary<int, Compte>();

            using(FileStream file = new FileStream("", FileMode.Open, FileAccess.Read))
            {
                using(StreamReader reader= new StreamReader(file))
                {
                    string line;
                    string[] data;
                    int id;
                    int idCompte;
                    string type;
                    int soldeInit;

                    while((line = reader.ReadLine()) != null)
                    {
                        data = line.Split(';');

                        

                        if(data.Length > 2) // Vérification que l'on a assez de données
                        {

                            id = int.Parse(data[0]);
                            idCompte = int.Parse(data[1]);
                            type = data[2];

                            if (!res.ContainsKey(id) && idCompte.ToString().Length == 16) // Vérification que l'on n'a pas encore le compte et que son id soit assez long
                            {
                                if(type.ToLower() == "livret" || type.ToLower() == "courant") // Vérification que le type de comtpe soit correct.
                                {

                                    if (data.Length > 3) // Mise en place de la variable soldeInit
                                    {
                                        soldeInit = int.Parse(data[3]);
                                    }
                                    else
                                    {
                                        soldeInit = 0;
                                    }


                                    Compte compte = new Compte(idCompte, type, soldeInit);
                                    res.Add(id, compte);
                                }
                            }
                        }
                    }


                }
            }

            return res;
        }

        public Dictionary<int, Carte> EntreeCarte()
        {
            Dictionary<int, Carte> res = new Dictionary<int, Carte>();

            using (FileStream file = new FileStream("", FileMode.Open, FileAccess.Read))
            {
                using (StreamReader reader = new StreamReader(file))
                {
                    string line;
                    string[] data;
                    int id;
                    int plafond;

                    while ((line = reader.ReadLine()) != null)
                    {
                        data = line.Split(';');



                        if (data.Length > 0) // Vérification que l'on a assez de données
                        {

                            id = int.Parse(data[0]);

                            if (!res.ContainsKey(id) && id.ToString().Length == 16) // Vérification que l'on n'a pas encore le compte et que son id soit assez long
                            {

                                    if (data.Length > 1) // Mise en place de la variable soldeInit
                                    {
                                        plafond = int.Parse(data[3]);
                                        if(plafond < 500 || plafond > 3000)
                                        {
                                            continue;
                                        }
                                    }
                                    else
                                    {
                                        plafond = 500;
                                    }


                                    Carte carte = new Carte(id, plafond);
                                    res.Add(id, carte);
                            }
                        }
                    }


                }
            }

            return res;
        }

        public Dictionary<int, Transaction> EntreeTransaction()
        {
            Dictionary<int, Transaction> res = new Dictionary<int, Transaction>();

            using (FileStream file = new FileStream("", FileMode.Open, FileAccess.Read))
            {
                using (StreamReader reader = new StreamReader(file))
                {
                    string line;
                    string[] data;
                    int id;
                    DateTime date;
                    int montant;
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

                            montant = int.Parse(data[2]);
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
