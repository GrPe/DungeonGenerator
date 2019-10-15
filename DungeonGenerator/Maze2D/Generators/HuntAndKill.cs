using DungeonGenerator.Maze2D.Cells;
using System;
using System.Linq;

namespace DungeonGenerator.Maze2D.Generators
{
    public sealed class HuntAndKill : IGenerator<Cell>, IMaskGenerator<Cell>
    {
        private Maze<Cell> maze;

        public Maze<Cell> Generate(int x, int y, Maze<Cell> mask)
        {
            maze = mask;
            Random random = new Random();

            Position current = null;
            do
            {
                current = new Position(random.Next(x), random.Next(y));
            }
            while (maze[current].Locked);
            maze[current].Visited = true;

            while (true)
            {
                var neighbors = maze[current].GetAllNeighbors(maze.Width, maze.Height).Where(n => !maze[n].Visited).ToList();

                if (neighbors.Count == 0)
                {
                    current = Hunt();
                    if (current == null) break;
                    neighbors = maze[current].GetAllNeighbors(maze.Width, maze.Height).Where(n => !maze[n].Visited).ToList();
                }

                neighbors = neighbors.OrderBy(a => random.Next()).ToList();

                maze[current].Connect(maze[neighbors[0]]);
                current = neighbors[0];
                maze[current].Visited = true;
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

        public Maze<Cell> Generate(int x, int y)
        {
            return Generate(x, y, new Maze<Cell>(x, y));
        }
    }
}
