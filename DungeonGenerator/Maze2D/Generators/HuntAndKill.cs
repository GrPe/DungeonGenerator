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
            while (mazeSize > 0)
            {
                var neighbors = maze[current].GetAllNeighbors(maze.Width, maze.Height).Where(n => !maze[n].Visited).ToList();

                if (neighbors.Count == 0)
                {
                    current = Hunt();
                    neighbors = maze[current].GetAllNeighbors(maze.Width, maze.Height).Where(n => !maze[n].Visited).ToList();
                }

                neighbors = neighbors.OrderBy(a => random.Next()).ToList();

                maze[current].Connect(maze[neighbors[0]]);
                current = neighbors[0];
                maze[current].Visited = true;

                mazeSize--;
            }

            return maze;
        }

        private Position Hunt()
        {
            foreach (var cell in maze.Get())
            {
                if (maze[cell.Position].Visited)
                {
                    if (maze[cell.Position].GetAllNeighbors(maze.Width, maze.Height).Where(n => !maze[n].Visited).Count() > 0)
                    {
                        return cell.Position;
                    }
                }
            }

            return null;
        }
    }
}
