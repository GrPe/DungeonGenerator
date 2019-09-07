using DungeonGenerator.Maze2D.Cells;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonGenerator.Maze2D.Generators
{
    public class RecursiveDivision : IGenerator<Cell>
    {
        Maze<Cell> maze;
        Random random;

        public Maze<Cell> Generate(int x, int y)
        {
            maze = new Maze<Cell>(x, y);
            random = new Random();

            for (int i = 1; i < maze.Width - 1; i++)
            {
                for (int j = 1; j < maze.Height - 1; j++)
                {
                    maze[i, j].Visited = true;
                    maze[i, j].SetAllWallOpened();
                }
            }

            Divide(1, maze.Width - 1, 1, maze.Height - 1);

            return maze;
        }

        private void Divide(int minX, int maxX, int minY, int maxY)
        {
            //vertical > horizontal
            if (maxY - minY > maxX - minX)
            {
                if (maxY - minY < 2) return;

                int split = random.Next(minY, maxY);

                for (int i = minX; i < maxX; i++)
                {
                    maze[i, split].Visited = true;
                    maze[i, split].RemoveConnection(Direction.Top);
                    maze[i, split].RemoveConnection(Direction.Bottom);
                }

                int path = random.Next(minX, maxX);
                maze[path, split].InsertConnection(Direction.Top);
                maze[path, split].InsertConnection(Direction.Bottom);

                Divide(minX, maxX, minY, split);
                Divide(minX, maxX, split + 1, maxY);
            }
            else
            {
                if (maxX - minX < 2) return;

                int split = random.Next(minX, maxX);

                for (int i = minY; i < maxY; i++)
                {
                    maze[split, i].Visited = true;
                    maze[split, i].RemoveConnection(Direction.Left);
                    maze[split, i].RemoveConnection(Direction.Right);
                }

                int path = random.Next(minY, maxY);
                maze[split, path].InsertConnection(Direction.Left);
                maze[split, path].InsertConnection(Direction.Right);

                Divide(minX, split, minY, maxY);
                Divide(split + 1, maxX, minY, maxY);
            }
        }
    }
}
