using DungeonGenerator.Maze2D.Cells;
using System;
using System.Collections.Generic;

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
                Direction? neighbor = GetRandomNeighbor(position);

                if (neighbor == null) continue;

                stack.Push(position);

                switch (neighbor)
                {
                    case Direction.Left:
                        maze[position].InsertConnection(Direction.Left);
                        maze[position.X - 1, position.Y].Visited = true;
                        maze[position.X - 1, position.Y].InsertConnection(Direction.Right);
                        stack.Push(new Position(position.X - 1, position.Y));
                        break;

                    case Direction.Right:
                        maze[position].InsertConnection(Direction.Right);
                        maze[position.X + 1, position.Y].Visited = true;
                        maze[position.X + 1, position.Y].InsertConnection(Direction.Left);
                        stack.Push(new Position(position.X + 1, position.Y));
                        break;

                    case Direction.Top:
                        maze[position].InsertConnection(Direction.Top);
                        maze[position.X, position.Y - 1].Visited = true;
                        maze[position.X,  position.Y - 1].InsertConnection(Direction.Bottom);
                        stack.Push(new Position(position.X, position.Y - 1));
                        break;

                    case Direction.Bottom:
                        maze[position].InsertConnection(Direction.Bottom);
                        maze[position.X, position.Y + 1].Visited = true;
                        maze[position.X, position.Y + 1].InsertConnection(Direction.Top);
                        stack.Push(new Position(position.X, position.Y + 1));
                        break;
                }
            }

            return maze;
        }

        private Direction? GetRandomNeighbor(Position position)
        {
            List<Direction> neighbors = new List<Direction>();

            if (position.X - 1 >= 0)
                if (!maze[position.X - 1, position.Y].Visited)
                    neighbors.Add(Direction.Left);

            if (position.X + 1 < maze.Width)
                if (!maze[position.X + 1, position.Y].Visited)
                    neighbors.Add(Direction.Right);

            if (position.Y - 1 >= 0)
                if (!maze[position.X, position.Y - 1].Visited)
                    neighbors.Add(Direction.Top);

            if (position.Y + 1 < maze.Height)
                if (!maze[position.X, position.Y + 1].Visited)
                    neighbors.Add(Direction.Bottom);

            if (neighbors.Count > 0)
                return neighbors[random.Next(0, neighbors.Count)];
            return null;
        }
    }
}
