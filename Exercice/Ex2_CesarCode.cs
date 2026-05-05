using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Serie3
{
    /*
     * Question 1: Utilisation en tant que "dictionnaire" pour faire la "traduction"
     */
    public class Cesar
    {
        private readonly char[,] _cesarTable;

        public Cesar()
        {
            _cesarTable = new char[,]
            {
                { 'A', 'D' },
                { 'B', 'E' },
                { 'C', 'F' },
                { 'D', 'G' },
                { 'E', 'H' },
                { 'F', 'I' },
                { 'G', 'J' },
                { 'H', 'K' },
                { 'I', 'L' },
                { 'J', 'M' },
                { 'K', 'N' },
                { 'L', 'O' },
                { 'M', 'P' },
                { 'N', 'Q' },
                { 'O', 'R' },
                { 'P', 'S' },
                { 'Q', 'T' },
                { 'R', 'U' },
                { 'S', 'V' },
                { 'T', 'W' },
                { 'U', 'X' },
                { 'V', 'Y' },
                { 'W', 'Z' },
                { 'X', 'A' },
                { 'Y', 'B' },
                { 'Z', 'C' }
            };
        }

        public string CesarCode(string line)
        {
            StringBuilder res = new StringBuilder();

            foreach(char c in line)
            {
                if(c == ' ')
                {
                    res.Append(" ");
                    continue;
                }

                for(int i = 0; i < _cesarTable.Length; i++)
                {
                    if (_cesarTable[i, 0] == c)
                    {
                        res.Append(_cesarTable[i, 1]);
                    }
                }
            }

            return res.ToString(); 
        }

        public string DecryptCesarCode(string line)
        {
            StringBuilder res = new StringBuilder();

            foreach (char c in line)
            {
                if (c == ' ')
                {
                    res.Append(" ");
                    continue;
                }

                for (int i = 0; i < _cesarTable.Length; i++)
                {
                    if (_cesarTable[i, 1] == c)
                    {
                        res.Append(_cesarTable[i, 0]);
                    }
                }
            }

            return res.ToString();
        }

        public string GeneralCesarCode(string line, int x)
        {
            StringBuilder res = new StringBuilder();

            foreach (char c in line)
            {
                if (c == ' ')
                {
                    res.Append(" ");
                    continue;
                }

                int shift = c + x;
                if(shift > 90)
                {
                    shift -= 26;
                }
                res.Append(shift);
            }

            return res.ToString();
        }

        public string GeneralDecryptCesarCode(string line, int x)
        {
            StringBuilder res = new StringBuilder();

            foreach (char c in line)
            {
                if (c == ' ')
                {
                    res.Append(" ");
                    continue;
                }

                int shift = c - x;
                if (shift < 65)
                {
                    shift += 26;
                }
                res.Append(shift);
            }

            return res.ToString();
        }
    }
}
