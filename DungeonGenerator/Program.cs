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
using System.Drawing;

namespace DungeonGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            //Generator generator = new Generator();

            //generator.InitGenerator(new DepthFirstSearch());

            //bool[,] mask = LoadFromBitmap("bitmap.bmp");

            //var m = generator.CreateMazeWithMask(60, 60, mask);

            //var maze = m.ToBoolArray();
            //maze.Display(m.FindPath(new Position(0, 0), new Position(m.Width - 1, m.Height - 1)));

            Roguelike gen = new Roguelike();
            gen.Generate(40, 40, 2000);
            gen.GetMazeAsBoolArray().Display();
        }

        static bool[,] LoadFromFile(string fileName)
        {
            IEnumerable<string> lines = File.ReadLines(fileName);
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

        static bool[,] LoadFromBitmap(string fileName)
        {
            bool[,] mask = null;

            using (Bitmap bitmap = new Bitmap(fileName))
            {
                mask = new bool[bitmap.Height, bitmap.Width];
                for(int i = 0; i < bitmap.Height; i++)
                {
                    for(int j = 0; j < bitmap.Width; j++)
                    {
                        Color color = bitmap.GetPixel(j, i);
                        if (color.R == 0 && color.G == 0 && color.B == 0)
                            mask[j, i] = true;
                    }
                }
            }

            return mask;
        }
    }
}
