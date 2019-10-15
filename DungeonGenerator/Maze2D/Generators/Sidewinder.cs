using DungeonGenerator.Maze2D.Cells;
using System;
using System.Collections.Generic;

namespace DungeonGenerator.Maze2D.Generators
{
    public class Sidewinder : IGenerator<Cell>
    {
        private Maze<Cell> maze;
        private Random random;

        public Maze<Cell> Generate(int x, int y)
        {
            maze = new Maze<Cell>(x, y);
            random = new Random();

            for(int i = maze.Height - 1; i >= 0; i--)
            {
                List<Cell> run = new List<Cell>();

                for(int j = 0; j < maze.Width; j++)
                {
                    maze[j, i].Visited = true;
                    run.Add(maze[j, i]);

                    bool east_end = maze[j, i].GetRightNeighbor(maze.Width) == null;
                    bool north_end = maze[j, i].GetTopNeighbor() == null;

                    if (east_end || (!north_end && random.Next(2) == 0)) //end left-right run and create link to top cell
                    {
                        if(!north_end)
                        {
                            Cell cell = run[random.Next(run.Count)];
                            cell.Connect(maze[cell.GetTopNeighbor()]);
                            run.Clear();
                        }
                    }
                    else
                        maze[j, i].Connect(maze[maze[j, i].GetRightNeighbor(maze.Width)]);
                }
            }

            return maze;
        }
    }
}
