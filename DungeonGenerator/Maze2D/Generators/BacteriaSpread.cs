using DungeonGenerator.Maze2D.Cells;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonGenerator.Maze2D.Generators
{
    public sealed class BacteriaSpread : IGenerator<Cell>
    {
        private Maze<Cell> maze;

        public Maze<Cell> Generate(int x, int y)
        {
            maze = new Maze<Cell>(x, y);
            Random random = new Random();
            List<Position> currentLayer = new List<Position>();
            List<Position> nextLayer = new List<Position>();

            Position start = new Position(random.Next(0, maze.Width), random.Next(0, maze.Height));
            maze[start].Visited = true;
            currentLayer.Add(start);

            while(currentLayer.Count > 0)
            {
                foreach(Position pos in currentLayer)
                {
                    if (pos.X > 0 && (!maze[pos.X - 1, pos.Y].Visited))
                    {
                        maze[pos].InsertConnection(Direction.Left);
                        var npos = new Position(pos.X - 1, pos.Y);
                        maze[npos].InsertConnection(Direction.Right);
                        maze[npos].Visited = true;
                        nextLayer.Add(npos);
                    }
                    if (pos.X < maze.Width - 1 && (!maze[pos.X + 1, pos.Y].Visited))
                    {
                        maze[pos].InsertConnection(Direction.Right);
                        var npos = new Position(pos.X + 1, pos.Y);
                        maze[npos].InsertConnection(Direction.Left);
                        maze[npos].Visited = true;
                        nextLayer.Add(npos);
                    }
                    if (pos.Y > 0 && (!maze[pos.X, pos.Y - 1].Visited))
                    {
                        maze[pos].InsertConnection(Direction.Top);
                        var npos = new Position(pos.X, pos.Y - 1);
                        maze[npos].InsertConnection(Direction.Bottom);
                        maze[npos].Visited = true;
                        nextLayer.Add(npos);
                    }
                    if (pos.Y < maze.Height - 1 && (!maze[pos.X, pos.Y + 1].Visited))
                    {
                        maze[pos].InsertConnection(Direction.Bottom);
                        var npos = new Position(pos.X, pos.Y + 1);
                        maze[npos].InsertConnection(Direction.Top);
                        maze[npos].Visited = true;
                        nextLayer.Add(npos);
                    }
                }
                currentLayer = nextLayer.OrderBy(p => random.Next()).ToList();
                nextLayer = new List<Position>();
            }

            return maze;
        }
    }
}
