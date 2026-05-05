using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Serie3
{
    public static class AdministrativeTasks
    {
        public static string EliminateSeditiousThoughts(string text, string[] prohibitedTerms)
        {

            if (string.IsNullOrEmpty(text) || prohibitedTerms.Length == 0)
            {
                return text;
            }

            string res = text;

            foreach(string term in prohibitedTerms)
            {
                if(!text.Contains(term))
                {
                    continue;
                }

                StringBuilder rep = new StringBuilder();

                for(int i = 0; i < term.Length; i++)
                {
                    rep.Append('X');
                }

                res = res.Replace(term, rep.ToString());
            }

            return res;
        }

        public static bool ControlFormat(string line)
        {
            if(line.Length == 0)
            {
                return false;
            }

            bool res = false;

            string[] split = line.Split(' ');

            if (!(split[1].Contains("Mr.") || split[1].Contains("Mme") || split[1].Contains("Mlle")))
            {
                return res;
            }

            if (split[1].Length > 12 && split[1].Any(char.IsDigit))
            {
                return res;
            }

            if (split[2].Length > 12 && split[2].Any(char.IsDigit))
            {
                return res;
            }

            if (split[3].Length != 2 && !split[2].All(char.IsDigit))
            {
                return res;
            }

            res = true; // Tout les check sont passé

            return res;
        }

        public static string ChangeDate(string report)
        {
            if(report.Length == 0 )
            {
                return null;
            }

            string res = "";
            int idx = report.IndexOf("-");
            if(idx == 0)
            {
                idx = report.IndexOf("-", 1);
            }
            string dateCheck = report.Substring(idx - 4, 4);

            if (dateCheck.All(char.IsDigit))
            {
                string date = report.Substring(idx - 4, 10);
                string[] dateArray = date.Split('-');
                string newDate = dateArray[2] + "." + dateArray[1] + "." + dateArray[0];

                res = report.Replace(date, newDate);
            }



            return res;
        }
    }
}
