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
            res = Gcd(216, 152);
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


    }
}
