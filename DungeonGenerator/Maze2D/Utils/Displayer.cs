using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonGenerator.Maze2D.Utils
{
    public static class Displayer
    {
        public static void Display(this bool[,] maze, List<Position> path)
        {
            for (int i = 0; i < maze.GetLength(0) + 1; i++)
                Console.Write('x');
            Console.WriteLine();

            for (int i = 0; i < maze.GetLength(1); i++)
            {
                Console.Write('x');
                for (int j = 0; j < maze.GetLength(0); j++)
                {
                    if (path.FindIndex(p => p.X == j / 2 && p.Y == i / 2) >= 0)
                    {
                        if (maze[j, i])
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

        public static void Display(this bool[,] maze)
        {
            Console.ForegroundColor = ConsoleColor.White;
            for (int i = 0; i < maze.GetLength(0) + 1; i++)
                Console.Write('x');
            Console.WriteLine();

            for (int i = 0; i < maze.GetLength(1); i++)
            {
                Console.Write('x');
                for (int j = 0; j < maze.GetLength(0); j++)
                    Console.Write((maze[j, i]) ? ' ' : 'x');
                Console.WriteLine();
            }
        }
    }
}
