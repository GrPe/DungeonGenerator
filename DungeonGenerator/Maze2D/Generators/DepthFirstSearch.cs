using DungeonGenerator.Maze2D.Cells;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DungeonGenerator.Maze2D.Generators
{
    public sealed class DepthFirstSearch : IGenerator<Cell>
    {
        private Maze<Cell> maze;
        private Random random;

        public Maze<Cell> Generate(int x, int y)
        {
            maze = new Maze<Cell>(x, y);
            random = new Random();
            Position start = new Position(random.Next(0, x), random.Next(0, y));

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
    }
}
