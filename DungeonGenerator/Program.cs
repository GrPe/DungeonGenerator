using DungeonGenerator.Maze2D;
using DungeonGenerator.Maze2D.Cells;
using DungeonGenerator.Maze2D.Converters;
using DungeonGenerator.Maze2D.Generators;
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

            var gen = new Roguelike();
            //gen.SetRoomSize(6, 12);

            var m = gen.Generate(40, 40);

            var maze = Converter.ToBoolArray(m);

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
