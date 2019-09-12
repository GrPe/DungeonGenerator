using DungeonGenerator.Maze2D.Cells;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

            for(int i = 0; i < maze.Height; i++)
            {
                for(int j = 0; j < maze.Width; j++)
                {
                    maze[j, i].Visited = true;
                    if (i == maze.Width - 1 && j == maze.Height - 1)
                        continue;
                    else if (i == maze.Width - 1)
                        maze[j, i].InsertConnection(Direction.Right);
                    else if (j == maze.Height - 1)
                        maze[j, i].InsertConnection(Direction.Bottom);
                    else
                    {
                        if (random.Next(0, 2) == 0)
                            maze[j, i].InsertConnection(Direction.Right);
                        else
                            maze[j, i].InsertConnection(Direction.Bottom);
                    }
                }
            }

            return maze;
        }
    }
}
