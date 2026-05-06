using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banque
{
    internal class Carte
    {
        private int _id;
        private int _plafond;
        private List<Compte> _compteListe;

        public Carte(int id, int plafond)
        {
            _id = id;
            _plafond = plafond;
        }

        public int Id { get => _id; set => _id = value; }
        public int Plafond { get => _plafond; set => _plafond = value; }
        internal List<Compte> CompteListe { get => _compteListe; set => _compteListe = value; }
    }
}
