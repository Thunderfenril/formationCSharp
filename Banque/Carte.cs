using System.Collections.Generic;

namespace Banque
{
    public class Carte
    {
        // En vrai, pourquoi ne pas utiliser des struct ? Le fonctionnel n'est pas présent de ce côté (comme Compte)

        private long _id;
        private int _plafond;
        private List<Compte> _compteListe;
        private List<Transaction> _transactionListe;

        public Carte(long id, int plafond, List<Compte> compteListe)
        {
            _id = id;
            _plafond = plafond;
            _compteListe = compteListe;
            _transactionListe = new List<Transaction>();
        }

        public long Id { get => _id; set => _id = value; }
        public int Plafond { get => _plafond; set => _plafond = value; }
        public List<Transaction> TransactionListe { get => _transactionListe; set => _transactionListe = value; }
        public List<Compte> CompteListe { get => _compteListe; set => _compteListe = value; }
    }
}
