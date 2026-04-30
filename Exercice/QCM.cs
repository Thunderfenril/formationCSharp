using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exercice
{
    public class QCM
    {
        string question;
        List<string> answer;
        int solution;
        int weight;

        public QCM(string question, List<string> answer, int solution, int weight)
        {
            this.Question = question;
            this.Answer = answer;
            this.Solution = solution;
            this.Weight = weight;
        }

        public int Weight { get => weight; set => weight = value; }
        public int Solution { get => solution; set => solution = value; }
        public List<string> Answer { get => answer; set => answer = value; }
        public string Question { get => question; set => question = value; }
    }
}
