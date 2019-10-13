using DungeonGenerator.Maze2D.Cells;

namespace DungeonGenerator.Maze2D.Utils
{
    public static class Converter
    {
        public static bool[,] ToBoolArray<T>(this Maze<T> maze) where T : Cell, new()
        {
            bool[,] ret = new bool[maze.Width * 2, maze.Height * 2];

            for (int i = 0; i < maze.Width * 2; i += 2)
            {
                for (int j = 0; j < maze.Height * 2; j += 2)
                {
                    if (!maze[i / 2, j / 2].Visited) continue;
                    if (maze[i / 2, j / 2].Bottom && maze[i / 2, j / 2].Right)
                    {
                        ret[i, j] = true;
                        ret[i + 1, j] = true;
                        ret[i, j + 1] = true;
                    }
                    else if (maze[i / 2, j / 2].Bottom)
                    {
                        ret[i, j] = true;
                        ret[i, j + 1] = true;
                    }
                    else if (maze[i / 2, j / 2].Right)
                    {
                        ret[i, j] = true;
                        ret[i + 1, j] = true;
                    }
                    else if (maze[i / 2, j / 2].Locked)
                        continue;
                    else
                        ret[i, j] = true;
                }
            }

            return ret;
        }
    }
}
