using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using System.Collections;

namespace Serie4
{
    public static class ClassCouncil
    {
        public static void SchoolMeans(string input, string output)
        {
            List<(string etudiant, string matiere, double note)> etudiants = new List<(string etudiant, string matiere, double note)>();

            using(FileStream file = new FileStream(input, FileMode.Open, FileAccess.Read))
            {
                using(StreamReader reader = new StreamReader(file))
                {
                    string line;
                    bool isHeader = true;

                    while((line = reader.ReadLine()) != null)
                    {

                        if(isHeader)
                        {
                            isHeader = false;
                            continue;
                        }

                        string[] parts = line.Split(';');
                        double note = double.Parse(parts[2], CultureInfo.InvariantCulture);
                        etudiants.Add((parts[0], parts[1], note));
                        
                    }
                }
            }

            using(FileStream file = new FileStream(output, FileMode.Create, FileAccess.Write))
            {
                using (StreamWriter writer = new StreamWriter(file))
                {
                    writer.WriteLine("Matiere;Moyenne");

                    Dictionary<string, List<double>> dictMatiere = new Dictionary<string, List<double>>();

                    foreach ((string etudiant, string matiere, double note) etudiant in etudiants)
                    {
                        if (!dictMatiere.ContainsKey(etudiant.matiere))
                        {
                            dictMatiere[etudiant.matiere] = new List<double>();
                        }

                        dictMatiere[etudiant.matiere].Add(etudiant.note);
                    }

                    foreach (KeyValuePair<string, List<double>> dictMat in dictMatiere)
                    {
                        string matiere = dictMat.Key;

                        List<double> list = dictMat.Value;

                        double somme = 0;

                        foreach(double note in list)
                        {
                            somme += note;
                        }

                        double moyenne = somme / list.Count();

                        writer.WriteLine($"{matiere};{moyenne:F1}");
                    }
                }
            }

            

            return;
        }
    }
}
