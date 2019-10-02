using DungeonGenerator.Maze2D;
using DungeonGenerator.Maze2D.Cells;
using DungeonGenerator.Maze2D.Utils;
using DungeonGenerator.Maze2D.Generators;
using DungeonGenerator.Maze2D.Travelsals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            Generator generator = new Generator();

            generator.InitGenerator(new HuntAndKill());

            bool[,] mask = new bool[5, 5];
            mask[1, 0] = true;
            mask[2, 0] = true;
            mask[3, 0] = true;

            var m = generator.CreateMazeWithMask(5, 5, mask);

            var dijkstra = new DijkstraPathFinder();
            var path = dijkstra.FindPath(m, new Position(0, 0), new Position(m.Width - 1, m.Height - 1));

            var maze = m.ToBoolArray();
            maze.Display(path);
        }
    }
}
