using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Or.Business
{
    public enum ErrorEnum
    {
        MontantInvalide = 0,
        MontantDepassePlafond = 1,
        SoldeInsufissant = 2,
        TransfertInterdit = 3,
        CarteInconnu = 4,
        MontantInf = 5
    }

    public static class Error
    {
        public static void TypeErreur(string origine, string message)
        {
            switch(origine.ToLower())
            {
                case "accueil":
                    if(message == "Carte")
                    {
                        Label(ErrorEnum.CarteInconnu, "accueil");
                    }
                    break;

                case "depot":
                    if (message == "Montant")
                    {
                        Label(ErrorEnum.MontantInvalide, "dépôt");
                    } else if (message == "Montant0")
                    {
                        Label(ErrorEnum.MontantInf, "dépôt");
                    }
                    break;

                case "virement":
                    if(message == "Solde")
                    {
                        Label(ErrorEnum.SoldeInsufissant, "viremment");
                    } else if (message == "Plafond")
                    {
                        Label(ErrorEnum.MontantDepassePlafond, "viremment");
                    } else if(message == "Montant")
                    {
                        Label(ErrorEnum.MontantInvalide, "viremment");
                    } else if(message == "Transfert")
                    {
                        Label(ErrorEnum.TransfertInterdit, "viremment");
                    }
                    else if (message == "Montant0")
                    {
                        Label(ErrorEnum.MontantInf, "dépôt");
                    }
                    break;

                case "retrait":
                    if (message == "Solde")
                    {
                        Label(ErrorEnum.SoldeInsufissant, "retrait");
                    } else if (message == "Montant")
                    {
                        Label(ErrorEnum.MontantInvalide, "retrait");
                    }
                    else if (message == "Montant0")
                    {
                        Label(ErrorEnum.MontantInf, "dépôt");
                    }
                    if (message == "Plafond")
                    {
                        Label(ErrorEnum.MontantDepassePlafond, "viremment");
                    }
                    break;

                default:
                    throw new Exception("Cas inconnu");
            }

        }

        public static void Label(ErrorEnum code, string action)
        {
            string res = "";

            switch(code)
            {
                case ErrorEnum.SoldeInsufissant:
                    res = "Il n'y a pas assez d'argent sur le compte pour effectuer ce " + action;
                    break;
                case ErrorEnum.MontantDepassePlafond:
                    res = "Ce " + action + " va dépasser le plafond. Cette action va donc être annulé.";
                    break;
                case ErrorEnum.MontantInvalide:
                    res = "Le montant que vous avez entré n'est pas valide.";
                    break;
                case ErrorEnum.TransfertInterdit:
                    res = "Virement depuis le livret impossible vers un autre compte";
                    break;
                case ErrorEnum.CarteInconnu:
                    res = "La carte entrée n'a pas été reconnus.";
                    break;
                case ErrorEnum.MontantInf:
                    res = "Le montant indiqué est inférieur à 0. Ceci est interdis";
                    break;
            }


            MessageBox.Show(res);
        }
    }
}
