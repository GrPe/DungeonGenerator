using DungeonGenerator.Maze2D.Cells;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonGenerator.Maze2D.Generators
{
    public sealed class BacteriaSpread : IGenerator<Cell>, IMaskGenerator<Cell>
    {
        private Maze<Cell> maze;

        public Maze<Cell> Generate(int x, int y, Maze<Cell> mask)
        {
            maze = mask;
            Random random = new Random();
            List<Position> currentLayer = new List<Position>();
            List<Position> nextLayer = new List<Position>();

            Position start = new Position(random.Next(maze.Width), random.Next(maze.Height));
            while (maze[start].Locked) start = new Position(random.Next(maze.Width), random.Next(maze.Height));
            maze[start].Visited = true;
            currentLayer.Add(start);

            while(currentLayer.Count > 0)
            {
                foreach(Position pos in currentLayer)
                {
                    var neighbors = maze[pos].GetAllNeighbors(maze.Width, maze.Height);

                    foreach(var n in neighbors)
                    {
                        if(!maze[n].Visited)
                        {
                            maze[pos].Connect(maze[n]);
                            maze[n].Visited = true;
                            nextLayer.Add(n);
                        }
                    }
                }
                currentLayer = nextLayer.OrderBy(p => random.Next()).ToList();
                nextLayer = new List<Position>();
            }

            return maze;
        }

        public Maze<Cell> Generate(int x, int y)
        {
            return Generate(x, y, new Maze<Cell>(x, y));
        }
    }
}
