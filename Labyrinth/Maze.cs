using System;
using System.Collections.Generic;
using System.Linq;

namespace Exercices.Labyrinthe
{
    public class Maze
    {
        /// <summary>
        /// Grille permettant de représenter un matériau poreux
        /// Pour chaque élément, true case ouverte, false case bloquée
        /// </summary>
        private readonly Cell[,] _maze;

        private readonly int _lineSize;

        private readonly int _columnSize;

        /// <summary>
        /// Construction d'une grille de taille n * m
        /// </summary>
        /// <param name="size"></param>
        public Maze(int n, int m)
        {
            if (n <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(n), n, "le nombre de lignes de la grille négatif ou null.");
            }

            if (m <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(n), n, "le nombre de colonnes de la grille négatif ou null.");
            }

            _lineSize = n;
            _columnSize = m;
            _maze = new Cell[n, m];
        }

        public bool IsOpen(int i, int j, int w)
        {
            return !_maze[i, j].Walls[w];
        }

        public bool IsMazeStart(int i, int j)
        {
            return _maze[i, j].Statut == 1 ? true : false;
        }

        public bool IsMazeEnd(int i, int j)
        {
            return _maze[i, j].Statut == 2 ? true : false;
        }

        public void Open(int i, int j, int w)
        {
            _maze[i, j].Walls[w] = false;
            return;
        }

        private List<KeyValuePair<int, int>> CloseNeighbors(int i, int j)
        {
            List<KeyValuePair<int, int>> res = new List<KeyValuePair<int, int>>();

            int numbRow = _maze.GetLength(0);
            int numbCol = _maze.GetLength(1);

            int[] dRow = { 1, -1, 0, 0 }; 
            int[] dCol = { 0, 0, 1, -1 };

            for (int n = 0; n < 4; n++)
            {
                int checkRow = i + dRow[n];
                int checkCol = j + dCol[n];

                if(
                    checkRow >= 0 && checkCol >= 0 &&
                    checkRow < numbRow && checkCol < numbCol
                    )
                {
                    res.Add(new KeyValuePair<int, int>(i, j));
                }
            }


            return res;
        }

        /*
         * Raison de pourquoi l'algorithme va parcourir toute la grille:
         * On va utiliser à chaque fois un voisin qui n'a pas déjà été visité, et lorsqu'on arrive à la fin, on va revenir sur le précédent et regarder ceux non visité
         * Le choix du voisin est fait de façon aléatoire pour ne pas que le labyrinthe soit vide aussi.
         */
        public KeyValuePair<int, int> Generate()
        {
            Random r = new Random();

            for (int i = 0; i < _lineSize; i++)
            {
                for(int j = 0 ; j < _columnSize; j++)
                {
                    _maze[i][j] = new Cell();
                }
            }

            int randomRow = r.Next(0, _lineSize);
            int randomCol = r.Next(0, _columnSize);

            Creation(randomRow, randomCol, r);


            return new KeyValuePair<int, int>(randomRow, randomCol);
        }

        public void Creation(int row, int col, Random r)
        {

            int tempRow = 0;
            int tempCol = 0;
            int dRow = 0;
            int dCol = 0;
            int direction = 0;
            int directionNeighbor = 0;
            List<KeyValuePair<int, int>> neighborList;

            _maze[row][col].IsVisited = true;

            while (true)
            {
                List<KeyValuePair<int, int>> neighborUnvisited = new List<KeyValuePair<int, int>>();
                neighborList = CloseNeighbors(row, col);

                foreach (KeyValuePair<int, int> neighbor in neighborList)
                {
                    if (!_maze[neighbor.Key][neighbor.Value].IsVisited)
                    {
                        neighborUnvisited.Add(neighbor);
                    }
                }

                if(neighborUnvisited.Count == 0)
                {
                    return;
                }

                KeyValuePair<int, int> randomNeighbor = neighborUnvisited[r.Next(neighborUnvisited.Count)];

                tempRow = randomNeighbor.Key;
                tempCol = randomNeighbor.Value;
                dRow = tempRow - row;
                dCol = tempCol - col;

                if (dRow != 0)
                {
                    direction = dRow == -1 ? 2 : 3;
                    directionNeighbor = dRow == -1 ? 3 : 2;
                }
                else
                {
                    direction = dCol == -1 ? 1 : 0;
                    directionNeighbor = dCol == -1 ? 0 : 1;
                }

                Open(row, col, direction);
                Open(randomNeighbor.Key, randomNeighbor.Value, directionNeighbor);

                Creation(tempRow, tempCol, r);
            }
        }

        // Liste des intersections = [' ', '-', ',', '|', '┌', '┐', '└', '┘', '├', '┤', '┬', '┴', '┼']

        public string DisplayLine(int n)
        {
            return string.Empty;
        }

        public List<string> Display(int n)
        {
            return new List<string>();
        }
    }
}
