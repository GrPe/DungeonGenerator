using DungeonGenerator.Maze2D.Cells;
using DungeonGenerator.Maze2D.Generators;
using System;

namespace DungeonGenerator.Maze2D
{
    public class Generator
    {
        private IGenerator<Cell> currentGenerator;

        public void InitGenerator(IGenerator<Cell> generator)
        {
            currentGenerator = generator;
        }

        public Maze<Cell> CreateMaze(int x, int y)
        {
            if (currentGenerator == null) return null;
            return currentGenerator.Generate(x, y);
        }

        public Maze<Cell> CreateMazeWithMask(int x, int y, bool[,] mask)
        {
            if (mask.GetLength(0) != x || mask.GetLength(1) != y)
                throw new ArgumentException("Invalid mask size");

            if (currentGenerator == null) return null;

            IMaskGenerator<Cell> maskGenerator = currentGenerator as IMaskGenerator<Cell>;
            if (maskGenerator == null)
                throw new ArgumentException("Current Generator isn't a Mask Generator");

            Maze<Cell> maze = new Maze<Cell>(x, y);
            for (int i = 0; i < x; i++)
                for (int j = 0; j < y; j++)
                    if (mask[i, j]) maze[i, j].Locked = true;

            return maskGenerator.Generate(x, y, maze);
        }

    }
}
