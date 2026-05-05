using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Serie4
{
    public static class Morpion
    {
        static int player;
        static char[,] morpions;
        static string input;
        static int statut;

        public static void MorpionGame()
        {
            Console.WriteLine("Début de la partie de Morpion:");
            init();

            while(statut != 1 && statut != 2)
            {
                Console.Write($"Coup du joueur {player}: ");
                input = Console.ReadLine();

                if(!CheckInput())
                {
                    Console.WriteLine("Coup incorrect, veuillez réessayer");
                } else
                {
                    UpdateMorpion();
                    DisplayMorpion(morpions);

                    statut = CheckMorpion(morpions);

                    switch(statut)
                    {
                        case 1:
                        case 2:
                            AffichageGagnant();
                            return;
                        case 0:
                            Console.WriteLine("Égalité");
                            return;
                        case -1:
                            player = player == 1 ? 2 : 1;
                            break;
                    }
                }
            }
        }

        public static void DisplayMorpion(/*typeGrille grille */)
        {
            //TODO
            return;
        }

        public static int CheckMorpion(/*typeGrille grille */)
        {
            //TODO
            return -1;
        }

        /*
        * Morpion:
        * Utilisation de List ou de tableau multidimensionnel
        * Simple à utiliser, on va avoir des données de taille fixe (zone de 3*3)
        * Avantage au tableau multidimensionnel, il sera plus simple de mon point de vue de faire les vérifications de victoire
        */
        public static void DisplayMorpion(char[,] tab)
        {
            for (int i = 0; i < tab.GetLength(0); i++)
            {
                for (int j = 0; j < tab.GetLength(1); j++)
                {
                    Console.Write(tab[i, j] + " ");
                }
                Console.WriteLine();
            }
        }

        public static int CheckMorpion(char[,] tab)
        {
            int statut = -1;
            bool ok = false;

            for (int i = 0; i < tab.GetLength(0); i++)
            {
                for (int j = 0; j < tab.GetLength(1); j++)
                {

                    if (tab[i, j] == '_')
                    {
                        return statut;
                    }

                }

                if (tab[i, 0] == tab[i, 1] && tab[i, 0] == tab[i, 2])
                {
                    statut = tab[i, 0] == 'X' ? 1 : 2;
                    ok = true;
                }


            }

            for (int j = 0; j < tab.GetLength(1); j++)
            {
                if (tab[0, j] == tab[1, j] && tab[0, j] == tab[2, j])
                {
                    statut = tab[0, j] == 'X' ? 1 : 2;
                    ok = true;
                }
            }

            if (tab[0, 0] == tab[1, 1] && tab[0, 0] == tab[2, 2])
            {
                statut = tab[0, 0] == 'X' ? 1 : 2;
                ok = true;
            }

            if (!ok)
            {
                statut = 0;
            }


            return statut;
        }

        public static void init()
        {
            player = 1;
            morpions = new char[3, 3];

            for(int i = 0; i < 3; i ++)
            {
                for(int j = 0; j < 3; j++)
                {
                    morpions[i, j] = '_';
                }
            }
        }

        public static bool CheckInput()
        {
            int row;
            int col;

            if(input.Length != 2)
            {
                return false;
            }

            if (input[0] != 'A' && input[0] != 'B' && input[0] != 'C')
            {
                return false;
            }

            if(input[1] != '1' && input[1] != '2' && input[1] != '3')
            {
                return false;
            }

            switch(input[0])
            {
                case 'A':
                    row = 0;
                    break;
                case 'B':
                    row = 1;
                    break;
                case 'C':
                    row = 2;
                    break;
                default:
                    return false;
            }

            col = (int)char.GetNumericValue(input[1]) - 1;

            if (morpions[row, col] != '_')
            {
                return false;
            }


            return true;

        }

        public static void UpdateMorpion()
        {
            int row;
            int col;

            switch (input[0])
            {
                case 'A':
                    row = 0;
                    break;
                case 'B':
                    row = 1;
                    break;
                case 'C':
                    row = 2;
                    break;
                default:
                    return;
            }

            col = (int)char.GetNumericValue(input[1]) - 1;

            morpions[row, col] = player == 1 ? 'X' : 'O';
        }

        public static void AffichageGagnant()
        {
            Console.WriteLine($"Le joueur ${statut} a gagné");
        }
    }
}
