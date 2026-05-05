using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Exercices.Labyrinthe;

namespace Labyrinthe
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Maze maze = new Maze(5, 3);

            maze.Display();
        }
    }
}
