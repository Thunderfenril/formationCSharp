using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Serie3
{
    public class Morse
    {
        private const string Taah = "===";
        private const string Ti = "=";
        private const string Point = ".";
        private const string PointLetter = "...";
        private const string PointWord = ".....";

        private readonly Dictionary<string, char> _alphabet;

        public Morse()
        {
            _alphabet = new Dictionary<string, char>()
            {
                {$"{Ti}.{Taah}", 'A'},
                {$"{Taah}.{Ti}.{Ti}.{Ti}", 'B'},
                {$"{Taah}.{Ti}.{Taah}.{Ti}", 'C'},
                {$"{Taah}.{Ti}.{Ti}", 'D'},
                {$"{Ti}", 'E'},
                {$"{Ti}.{Ti}.{Taah}.{Ti}", 'F'},
                {$"{Taah}.{Taah}.{Ti}", 'G'},
                {$"{Ti}.{Ti}.{Ti}.{Ti}", 'H'},
                {$"{Ti}.{Ti}", 'I'},
                {$"{Ti}.{Taah}.{Taah}.{Taah}", 'J'},
                {$"{Taah}.{Ti}.{Taah}", 'K'},
                {$"{Ti}.{Taah}.{Ti}.{Ti}", 'L'},
                {$"{Taah}.{Taah}", 'M'},
                {$"{Taah}.{Ti}", 'N'},
                {$"{Taah}.{Taah}.{Taah}", 'O'},
                {$"{Ti}.{Taah}.{Taah}.{Ti}", 'P'},
                {$"{Taah}.{Taah}.{Ti}.{Taah}", 'Q'},
                {$"{Ti}.{Taah}.{Ti}", 'R'},
                {$"{Ti}.{Ti}.{Ti}", 'S'},
                {$"{Taah}", 'T'},
                {$"{Ti}.{Ti}.{Taah}", 'U'},
                {$"{Ti}.{Ti}.{Ti}.{Taah}", 'V'},
                {$"{Ti}.{Taah}.{Taah}", 'W'},
                {$"{Taah}.{Ti}.{Ti}.{Taah}", 'X'},
                {$"{Taah}.{Ti}.{Taah}.{Taah}", 'Y'},
                {$"{Taah}.{Taah}.{Ti}.{Ti}", 'Z'},
            };
        }

        public int LettersCount(string code)
        {
            int res = code.Split(new[] { "=...=" }, StringSplitOptions.None).Length;
            return res;
        }

        public int WordsCount(string code)
        {
            int res = code.Split(new[] {"......"}, StringSplitOptions.None).Length;
            return res;
        }

        public string MorseTranslation(string code)
        {
            string[] word;
            string[] letter;

            StringBuilder res = new StringBuilder();

            word = code.Split(new[] { "......" }, StringSplitOptions.None);

            foreach(string word2 in word)
            {
                letter = word2.Split(new[] { "..." }, StringSplitOptions.None);

                foreach(string letter2 in letter)
                {
                    if(letter2 == "")
                    {
                        continue;
                    }
                    res.Append(_alphabet[letter2]);
                }

                res.Append(" ");
            }

            return res.ToString();
        }

        public string EfficientMorseTranslation(string code)
        {
            int idx = 0;
            int temp = 0;
            string stringTemp;
            string[] stringTemp2;
            string[] letters;
            StringBuilder res = new StringBuilder();

            while (idx < code.Length && idx != -1)
            {

                idx = code.IndexOf("......", idx);

                if (idx == -1)
                {
                    idx = code.Length;
                }

                stringTemp = code.Substring(temp, (idx - temp));

                stringTemp2 = stringTemp.Split(new[] { "......" }, StringSplitOptions.None);

                foreach (string word in stringTemp2)
                {
                    string normalized = word.Replace("....", "...");
                    letters = normalized.Split(new[] { "..." }, StringSplitOptions.None);

                    foreach(string letter in letters)
                    {
                        string letterNormalized = letter.Replace("..", ".");
                        if (_alphabet.ContainsKey(letterNormalized))
                        {
                            res.Append(_alphabet[letterNormalized]);
                        }
                    }

                    
                }

                if (idx != code.Length && code.Substring(idx, 6) == "......")
                {
                    res.Append(" ");
                }

                //Faire un idx of pour aller sur le premier charac =
                //Changer temp pour qu'il soit égale à ça aussi

                temp = code.IndexOf("=", idx);
                idx = code.IndexOf('=', idx);

            }

            return res.ToString();
        }

        public string MorseEncryption(string sentence)
        {
            StringBuilder res = new StringBuilder();
            StringBuilder key = new StringBuilder();

            foreach(char ch in sentence)
            {
                if(ch == ' ')
                {
                    for(int i = 0; i < 6; i++)
                    {
                        res.Append('.');
                    }
                }

                key.Clear();
                key.Append(_alphabet.FirstOrDefault(x => x.Value == ch).Key);

                if (_alphabet.ContainsKey(key.ToString()))
                {
                    res.Append(key.ToString());
                    for (int i = 0; i < 3; i++)
                    {
                        res.Append('.');
                    }
                }
            }

            return res.ToString();
        }
    }
}
