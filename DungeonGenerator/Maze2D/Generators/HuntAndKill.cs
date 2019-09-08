using DungeonGenerator.Maze2D.Cells;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonGenerator.Maze2D.Generators
{
    public sealed class HuntAndKill : IGenerator<Cell>
    {
        private Maze<Cell> maze;

        public Maze<Cell> Generate(int x, int y)
        {
            maze = new Maze<Cell>(x, y);
            Random random = new Random();

            Position current = new Position(random.Next(0, x), random.Next(0, y));
            maze[current].Visited = true;

            int mazeSize = x * y - 1;
            while(mazeSize > 0)
            {
                var neighbors = GetUnvisitedNeighbors(current);

                if (neighbors.Count == 0)
                {
                    Position pos = Hunt();
                    if (pos != null)
                    {
                        current = pos;
                        neighbors = GetUnvisitedNeighbors(current);
                    }
                    else
                        break;
                }

                neighbors = neighbors.OrderBy(a => random.Next()).ToList();

                switch (neighbors[0])
                {
                    case Direction.Bottom:
                        maze[current].InsertConnection(Direction.Bottom);
                        current.Y++;
                        maze[current].InsertConnection(Direction.Top);
                        maze[current].Visited = true;
                        break;
                    case Direction.Top:
                        maze[current].InsertConnection(Direction.Top);
                        current.Y--;
                        maze[current].InsertConnection(Direction.Bottom);
                        maze[current].Visited = true;
                        break;
                    case Direction.Left:
                        maze[current].InsertConnection(Direction.Left);
                        current.X--;
                        maze[current].InsertConnection(Direction.Right);
                        maze[current].Visited = true;
                        break;
                    case Direction.Right:
                        maze[current].InsertConnection(Direction.Right);
                        current.X++;
                        maze[current].InsertConnection(Direction.Left);
                        maze[current].Visited = true;
                        break;
                }
                mazeSize--;
            }

            return maze;
        }

        private List<Direction> GetUnvisitedNeighbors(Position position)
        {
            List<Direction> neighbors = new List<Direction>();

            if (!maze[position].Left && position.X > 0
                && !maze[position.X - 1, position.Y].Visited) neighbors.Add(Direction.Left);
            if (!maze[position].Right && position.X < maze.Width - 1
                && !maze[position.X + 1, position.Y].Visited) neighbors.Add(Direction.Right);
            if (!maze[position].Top && position.Y > 0
                && !maze[position.X, position.Y - 1].Visited) neighbors.Add(Direction.Top);
            if (!maze[position].Bottom && position.Y < maze.Height - 1
                && !maze[position.X, position.Y + 1].Visited) neighbors.Add(Direction.Bottom);

            return neighbors;
        }

        private Position Hunt()
        {

            for (int i = 0; i < maze.Width; i++)
            {
                for (int j = 0; j < maze.Height; j++)
                {
                    if (!maze[i, j].Visited) continue;
                    if (GetUnvisitedNeighbors(new Position(i, j)).Count > 0)
                    {
                        return new Position(i, j);
                    }
                }
            }
            return null;
        }
    }
}
