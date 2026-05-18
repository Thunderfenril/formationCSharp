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
        private long _idCarte { get; set; }
        private int _idCompte { get; set; }


        public Beneficiaire(int Id, long numCarte, int IdCompte) { 
            _id = Id;
            _idCarte = numCarte;
            _idCompte = IdCompte;
        }
    }
}
