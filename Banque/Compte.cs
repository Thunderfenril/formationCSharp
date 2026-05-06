using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banque
{
    internal class Compte
    {
        private int _idCompte;
        private string _type;
        private int _soldeInit;
        private int _idCarte;

        public Compte(int idCompte, string type, int soldeInit)
        {
            _idCompte = idCompte;
            _type = type;
            _soldeInit = soldeInit;
        }

        public int IdCarte { get => _idCarte;}
        public int SoldeInit { get => _soldeInit; set => _soldeInit = value; }
        public string Type { get => _type; }
        public int IdCompte { get => _idCompte; set => _idCompte = value; }
    }
}
