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
using System.IO;

namespace DungeonGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            Generator generator = new Generator();

            generator.InitGenerator(new DepthFirstSearch());

            bool[,] mask = LoadFromFile();

            var m = generator.CreateMazeWithMask(10, 5, mask);

            var maze = m.ToBoolArray();
            maze.Display(m.FindPath(new Position(0, 0), new Position(m.Width - 1, m.Height - 1)));
        }

        static bool[,] LoadFromFile()
        {
            IEnumerable<string> lines = File.ReadLines("mask.txt");
            bool[,] mask = new bool[lines.FirstOrDefault().Length, lines.Count()];

            int i = 0;
            int j = 0;
            foreach (string line in lines)
            {
                foreach (char c in line)
                {
                    if (c == 'X') mask[j,i] = true;
                    j++;
                }
                j = 0;
                i++;
            }
            return mask;
        }
    }
}
