using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Or.Models
{
    public class Beneficiaire
    {
        private int _id { get; set; }
        public long _idCarte { get; }
        private int _idCompte;

        public string Nom { get; set; }
        public string Prenom { get; set; }

        public int IdCompte { get => _idCompte; set { _idCompte = value; } }


        public Beneficiaire(int Id, long numCarte, int IdCompte, string nom, string prenom) { 
            _id = Id;
            _idCarte = numCarte;
            _idCompte = IdCompte;
            Nom = nom;
            Prenom = prenom;
        }
    }
}
