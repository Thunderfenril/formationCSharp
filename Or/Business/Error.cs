using System;
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
        MontantInf = 5,
        TypeCompte = 6,
        Possede = 7,
        CompteInconnu = 8,
        AjoutBenefExistant = 9
    }

    // Classe OK
    public static class Error
    {
        public static void TypeErreur(string origine, string message)
        {
            // Intéressant d'avoir centralisé la gestion des erreurs par code erreur et par écran
            switch(origine.ToLower())
            {
                case Tools.EcranAccueil:
                    if(message == "Carte")
                    {
                        Label(ErrorEnum.CarteInconnu, "accueil");
                    }
                    break;

                case Tools.EcranDepot:
                    if (message == "Montant")
                    {
                        Label(ErrorEnum.MontantInvalide, "dépôt");
                    } else if (message == "Montant0")
                    {
                        Label(ErrorEnum.MontantInf, "dépôt");
                    }
                    break;

                case Tools.EcranVirement:
                    if(message == "Solde")
                    {
                        Label(ErrorEnum.SoldeInsufissant, "virement");
                    } else if (message == "Plafond")
                    {
                        Label(ErrorEnum.MontantDepassePlafond, "virement");
                    } else if(message == "Montant")
                    {
                        Label(ErrorEnum.MontantInvalide, "virement");
                    } else if(message == "Transfert")
                    {
                        Label(ErrorEnum.TransfertInterdit, "virement");
                    }
                    else if (message == "Montant0")
                    {
                        Label(ErrorEnum.MontantInf, "dépôt");
                    }
                    break;

                case Tools.EcranAjoutBeneficiaire:
                    if(message == "Type")
                    {
                        Label(ErrorEnum.TypeCompte, "ajout benef");
                    } else if (message == "Possede")
                    {
                        Label(ErrorEnum.Possede, "ajout benef");
                    } else if(message == "Inconnu")
                    {
                        Label(ErrorEnum.CompteInconnu, "ajout benef");
                    } else if(message == "Pair")
                    {
                        Label(ErrorEnum.AjoutBenefExistant, "ajout benef");
                    }
                    break;

                case Tools.EcranRetrait:
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
                        Label(ErrorEnum.MontantDepassePlafond, "virement");
                    }
                    break;

                default:
                    throw new Exception("Cas inconnu");
            }

        }

        public static void Label(ErrorEnum code, string action)
        {
            string res = "";

            // Attention aux fautes d'orthographe
            switch(code)
            {
                case ErrorEnum.SoldeInsufissant:
                    res = "Il n'y a pas assez d'argent sur le compte pour effectuer ce " + action;
                    break;
                case ErrorEnum.MontantDepassePlafond:
                    res = "Ce " + action + " va dépasser le plafond. Cette action va donc être annulée.";
                    break;
                case ErrorEnum.MontantInvalide:
                    res = "Le montant que vous avez entré n'est pas valide.";
                    break;
                case ErrorEnum.TransfertInterdit:
                    res = "Virement depuis le livret impossible vers un autre compte";
                    break;
                case ErrorEnum.CarteInconnu:
                    res = "La carte entrée n'a pas été reconnue.";
                    break;
                case ErrorEnum.MontantInf:
                    res = "Le montant indiqué est inférieur à 0. Ceci est interdit";
                    break;
                case ErrorEnum.CompteInconnu:
                    res = "Ce compte n'existe pas dans la base de donnee";
                    break;
                case ErrorEnum.Possede:
                    res = "Vous ne pouvez pas ajouter votre propre compte en beneficiaire";
                    break;
                case ErrorEnum.TypeCompte:
                    res = "Vous ne pouvez pas sélectionner un compte de type \"Livret\"";
                    break;
                case ErrorEnum.AjoutBenefExistant:
                    res = "Ce compte a déjà été ajouté";
                    break;
            }


            MessageBox.Show(res);
        }
    }
}
