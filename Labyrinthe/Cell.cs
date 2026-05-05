using System;

namespace Exercices.Labyrinthe
{
    public struct Cell
    {
		// 0 : Haut, 1 : bas, 2 : gauche, 3 : droite
		// true : mur present, false: pas de mur
        public bool[] Walls { get;  set; }
		
		public bool IsVisited { get; set; }
		
		// Définir système d'état de la cellule
		// 0 : Simple, 1: Entree, 2: Sortie
		public int Statut { get; set; }
    }
}
