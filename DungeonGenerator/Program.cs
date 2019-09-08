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
            //gen.SetRoomSize(4, 8);

            var m = gen.Generate(50, 50);

            var maze = Converter.ToBoolArray(m);

            for(int i = 0; i < maze.GetLength(1); i++)
            {
                for (int j = 0; j < maze.GetLength(0); j++)
                    Console.Write((maze[j, i]) ? ' ' : 'x');
                Console.WriteLine();
            }

        }
    }
}
