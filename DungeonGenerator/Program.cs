using DungeonGenerator.Maze2D;
using DungeonGenerator.Maze2D.Cells;
using DungeonGenerator.Maze2D.Converters;
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

            for (int i = 0; i < m.Width * 2 + 1; i++)
                Console.Write('x');
            Console.WriteLine();

            for (int i = 0; i < maze.GetLength(1); i++)
            {
                Console.Write('x');
                for (int j = 0; j < maze.GetLength(0); j++)
                {
                    if (path.FindIndex(p => p.X == j / 2 && p.Y == i /2) >= 0)
                    {
                        if(maze[j,i])
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.Write('^');
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.White;
                            Console.Write('x');
                        }
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.Write((maze[j, i]) ? ' ' : 'x');
                    }
                }
                Console.WriteLine();
            }
        }
    }
}
