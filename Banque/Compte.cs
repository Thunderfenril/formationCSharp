namespace Banque
{
    public class Compte
    {
        // attributs privés - bonne pratique
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

        // Propriétés OK
        public long IdCarte { get => _idCarte;}
        public decimal SoldeInit { get => _soldeInit; set => _soldeInit = value; }
        public string Type { get => _type; }
        public int IdCompte { get => _idCompte; }
        // Le solde peut être modifié en dehors de la classe Compte - pas sécurisé...
        public decimal Solde { get => _solde; set => _solde = value; }

        /// <summary>
        /// Fonction pour vérifier que le compte ait assez d'argent sur son compte pour un transfert
        /// </summary>
        /// <param name="montant"></param>
        /// <returns>Un booleen qui confirme ou infirme le fait d'avoir assez d'argent sur son compte pour le transfert</returns>
        public bool VerificationSolde(decimal montant)
        {
            return _solde >= montant;
        }
    }
}
