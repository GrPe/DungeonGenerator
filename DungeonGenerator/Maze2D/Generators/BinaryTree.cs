using DungeonGenerator.Maze2D.Cells;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DungeonGenerator.Maze2D.Generators
{
    public sealed class BinaryTree : IGenerator<Cell>
    {
        private Maze<Cell> maze;
        private Random random;

        public Maze<Cell> Generate(int x, int y)
        {
            maze = new Maze<Cell>(x, y);
            random = new Random();

            foreach(var cell in maze.Get())
            {
                cell.Visited = true;

                List<Position> cells = new List<Position>
                {
                    cell.GetRightNeighbor(maze.Width),
                    cell.GetBottomNeighbor(maze.Height)
                }.Where(n => n != null).ToList();

                if (cells.Count > 0)
                {
                    Position pos = cells[random.Next(0, cells.Count)];
                    cell.Connect(maze[pos]);
                }
            }

            return maze;
        }
    }
}
