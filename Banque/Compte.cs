using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banque
{
    public class Compte
    {
        private int _idCompte;
        private string _type;
        private decimal _soldeInit;
        private decimal _solde;
        private long _idCarte;

        public Compte(int idCompte, long idCarte, string type, decimal soldeInit)
        {
            _idCompte = idCompte;
            _type = type;
            _soldeInit = soldeInit;
            _solde = soldeInit;
            _idCarte = idCarte;
        }

        public long IdCarte { get => _idCarte;}
        public decimal SoldeInit { get => _soldeInit; set => _soldeInit = value; }
        public string Type { get => _type; }
        public int IdCompte { get => _idCompte; }
        public decimal Solde { get => _solde; set => _solde = value; }

        public bool VerificationSolde(decimal montant)
        {
            return _solde >= montant;
        }
    }
}
