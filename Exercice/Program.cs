using System;
using System.Globalization;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exercice
{
    public enum PointCardinal: byte
    {
        Nord = 4,
        Est = 3,
        Sud = 2,
        Ouest = 1
    }

    public class Program
    {
        public static void Main(string[] args)
        {
            /*
            BasicOperation(3, 4, '+');
            BasicOperation(6, 2, '/');
            BasicOperation(3, 0, '/');
            BasicOperation(3, 4, 'L');
            */
            //PyramidConstruction(10, false);
            int res = 1;
            //res = FactorialRecu(5);
            //DisplayPrimes();
            //res = Gcd(216, 152);
            //Console.WriteLine(res);
            /*char[,] morTab = { { 'X', 'O', 'X'}, { '_', 'X', 'X'}, { 'O', 'X', 'O'} };
            char[,] morTab2 = { { 'X', 'O', 'X' }, { 'O', 'X', 'X' }, { 'O', 'O', 'O' } };
            char[,] morTab3 = { { 'X', 'O', 'X' }, { 'X', 'X', 'O' }, { 'X', 'O', 'O' } };
            char[,] morTab4 = { { 'X', 'O', 'X' }, { 'O', 'X', 'O' }, { 'X', 'O', 'O' } };
            DisplayMorpion(morTab);
            res = CheckMorpion(morTab);
            Console.WriteLine(res);
            res = CheckMorpion(morTab2);
            Console.WriteLine(res);
            res = CheckMorpion(morTab3);
            Console.WriteLine(res);
            res = CheckMorpion(morTab4);
            Console.WriteLine(res);*/

            int[] tab = { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            res = BinarySearch(tab, 7);
            Console.WriteLine(res);
            Console.ReadKey();
            /*
            PointCardinal point = PointCardinal.Est;

            switch(point)
            {
                case PointCardinal.Est:
                    Console.WriteLine("C'est l'Est");
                    break;
            }

            int a = 3;

            switch(a)
            {
                case int _ when a > 0 && a == 0:
                    Console.WriteLine("a positif");
                    break;
                case int n when n < 0:
                    Console.WriteLine("a est négatif");
                    break;
            }
            
            Console.WriteLine("Hello World");
            string entree = Console.ReadLine();
            Console.WriteLine("Valeur input: " + entree);
            int y = 12;
            int z = y + 6;
            CultureInfo culture = new CultureInfo("en-US");  //Utile pour faire de l'affichage selon les conventions d'un pays
            Console.OutputEncoding = Encoding.Unicode;
            Console.WriteLine($"La valeur de Z est: " + z.ToString("C2", culture));
            Console.WriteLine($"La valeur de z est: {z:000.00}");
            Console.ReadKey();
            */
        }

        public static void BasicOperation(int a, int b, char op)
        {
            int res = 0;

            switch(op)
            {
                case '+':
                    res = a + b;
                    break;
                case '-':
                    res = a - b;
                    break;
                case '*':
                    res = a * b;
                    break;
                case char _ when op == '/' && b != 0:
                    res = a / b;
                    break;
                default:
                    Console.WriteLine("Operation invalide");
                    break;

            }

            Console.WriteLine($"{a} {op} {b} = {res}");
        }

        public static void IntegerDivision(int a, int b)
        {
            int quotient = 0;
            int reste = 0;

            if(b != 0)
            {
                quotient = a / b;
                reste = a % b;

                Console.WriteLine($"{a} = {quotient} * {b} + {reste}");
            } else
            {
                Console.WriteLine($"{a} : {b} = Operation Invalide");
            }
        }

        public static void Power(int a, int b)
        {
            int res = a;

            if(b > 0 )
            {
                for(int i = b; i != 0; i--)
                {
                    res *= a;
                }
            } else if (b == 0)
            {
                res = 1;
            } else
            {
                Console.WriteLine("Opération invalide");
                return;
            }

            Console.WriteLine($"{a} ^ {b} = {res}");
        }

        public static string GoodDay(int heure)
        {
            string res = "";

            switch(heure)
            {
                case int _ when heure >= 0 && heure < 6:
                    res = "Merveuillese nuit";
                    break;

                case int _ when heure >= 6 && heure < 12:
                    res = "Bonne matinée";
                    break;

                case 12:
                    res = "Bon appétit";
                    break;

                case int _ when heure > 12 && heure < 18:
                    res = "Profitez de votre après-midi";
                    break;

                case int _ when heure >= 18 && heure < 24:
                    res = "Passez une bonne soirée";
                    break;

                default:
                    res = "Err. heure invalide";
                        break;
            }

            res = $"Il est {heure} heure, {res}";

            return res;
        }

        public static void PyramidConstruction(int n, bool isSmooth)
        {
            /*
             * Question 1.a: Au niveau j il y aura 2*(j-1)+1 blocs
             * Question 1.b:  N^2 blocs 
             * 
             * Question 2.a: au sommet, le bloc sera au centre
             * Question 2.b: 
             *  gauche(j): (n-j+1)
             *  droite(j): (2n - (n-j+1))
            */
            for(int i = 1; i < n; i++)
            {
                Console.WriteLine(new string(' ', (n-i)) + new string(isSmooth && i%2 == 0 ? '-' : '+', 2*(i-1)+1));
            }
        }

        /*
         * En terme d'efficacité, la version récursive est bien plus efficace, surtout si on utilise de grands nombre.
         * On évite des boucles qui sont trop longues.
         * 
         * Pour des petits nombres, les 2 versions sont équivalentes
         */
        public static int FactorialIte(int n)
        {
            int res = 1;

            for(int i = 1; i <= n; i++)
            {
                res *= i;
            }

            Console.WriteLine($"Factoriel de {n}: {res}");

            return res;
        }

        public static int FactorialRecu(int n)
        {
            if(n == 1)
            {
                return 1;
            }

            return n * FactorialRecu(n - 1);
        }

        public static bool IsPrime(int n)
        {
            bool res = true;

            if(n == 2)
            {
                return res;
            } else if( n < 2)
            {
                return false;
            }

            for(int i = 2; i < n; i++)
            {
                if(n % i == 0)
                {
                    res = false;
                    break;
                }
            }

            return res;
        }

        public static void DisplayPrimes()
        {
            for(int i = 2; i <=100; i++)
            {
                if(IsPrime(i))
                {
                    Console.WriteLine(i);
                }
            }
        }

        public static int Gcd(int a, int b)
        {
            int reste = 0;

            reste = a % b;

            if(reste == 0)
            {
                return b;
            } else
            {
                return Gcd(b, reste);
            }
        }

        public static int SumTab(int[] tab)
        {
            int res = 0;

            for(int i = 0; i < tab.Length; i++)
            {
                res += tab[i];
            }


            return res;
        }

        public static int[] OpeTab(int[] tab, char ope, int b)
        {
            int[] res = new int[tab.Length];

            if((ope != '+' && ope != '-' && ope != '*') || tab.Length == 0)
            {
                return res;
            }

            for(int i = 0; i < tab.Length; i++)
            {
                switch(ope)
                {
                    case '+':
                        res[i] = tab[i] + b;
                        break;
                    case '-':
                        res[i] = tab[i] - b;
                        break;
                    case '*':
                        res[i] = tab[i] * b;
                        break;
                }
            }


            return res;
        }


        public static int[] ConcatTab(int[] tab1, int[] tab2)
        {
            int[] res = new int[tab1.Length + tab2.Length];

            for(int i = 0; i < tab1.Length + tab2.Length; i++)
            {
                if(i < tab1.Length)
                {
                    res[i] = tab1[i];
                } else
                {
                    res[i] = tab2[i - tab1.Length];
                }
            }


            return res;
        }

        /*
         * Morpion:
         * Utilisation de List ou de tableau multidimensionnel
         * Simple à utiliser, on va avoir des données de taille fixe (zone de 3*3)
         * Avantage au tableau multidimensionnel, il sera plus simple de mon point de vue de faire les vérifications de victoire
         */
        public static void DisplayMorpion(char[,] tab)
        {
            for(int i = 0; i < tab.GetLength(0); i++)
            {
                for(int j = 0; j < tab.GetLength(1); j++)
                {
                    Console.Write(tab[i,j] + " ");
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

                    if(tab[i, j] == '_')
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

            for(int j = 0; j <tab.GetLength(1); j++)
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

            if(!ok)
            {
                statut = 0;
            }


            return statut;
        }

        public static int LinearSearch(int[] tab, int n)
        {
            // Dans le pire des cas tab.Length éléments doivent être lus
            int res = -1;

            if(tab.Length == 0)
            {
                return res;
            }

            for(int i = 0; i < tab.Length; i++)
            {
                if (tab[i] == n)
                {
                    return i;
                }
            }


            return res;
        }

        public static int BinarySearch(int[] tab, int n)
        {
            // Dans le pire des cas tab.Length/2 élément doivent être lus
            int res = -1;
            bool found = false;
            int mid = tab.Length / 2;
            int min = 0;
            int max = tab.Length;

            if (tab.Length == 0)
            {
                return res;
            }

            while (!found && min < max) {

                if (tab[mid] == n)
                {
                    found = true;
                    res = mid;
                    break;
                }

                if (tab[mid] > n)
                {
                    max = mid;
                } else
                {
                    min = mid;
                }

                mid = (min + max) / 2;
            }


            return res;
        }
    }
}