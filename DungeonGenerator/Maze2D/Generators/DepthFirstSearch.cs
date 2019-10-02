using DungeonGenerator.Maze2D.Cells;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DungeonGenerator.Maze2D.Generators
{
    public sealed class DepthFirstSearch : IGenerator<Cell>, IMaskGenerator<Cell>
    {
        private Maze<Cell> maze;
        private Random random;

        public Maze<Cell> Generate(int x, int y, Maze<Cell> mask)
        {
            maze = mask;
            random = new Random();

            Position start = null;
            do
            {
                start = new Position(random.Next(x), random.Next(y));
            }
            while (maze[start].Locked);

            Stack<Position> stack = new Stack<Position>();
            maze[start].Visited = true;
            stack.Push(start);

            while(stack.Count > 0)
            {
                Position position = stack.Pop();

                var neighbor = maze[position].GetAllNeighbors(maze.Width, maze.Height).Where(n=> !maze[n].Visited).OrderBy(n => random.Next()).ToList();

                if (neighbor.Count == 0) continue;

                stack.Push(position);

                maze[position].Connect(maze[neighbor[0]]);
                maze[neighbor[0]].Visited = true;
                stack.Push(neighbor[0]);
            }

            return maze;
        }

        public Maze<Cell> Generate(int x, int y)
        {
            return Generate(x, y, new Maze<Cell>(x, y));
        }
    }
}
