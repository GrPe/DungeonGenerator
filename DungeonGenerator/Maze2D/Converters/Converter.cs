using DungeonGenerator.Maze2D.Cells;

namespace DungeonGenerator.Maze2D.Converters
{
    public static class Converter
    {
        /// <summary>
        /// Convert Maze to bool array
        /// true is path, false is wall
        /// Note: output maze is 4 times bigger
        /// </summary>
        /// <param name="maze"></param>
        /// <returns></returns>
        public static bool[,] ToBoolArray(Maze<Cell> maze)
        {
            bool[,] ret = new bool[maze.Width * 2, maze.Height * 2];

            for (int i = 0; i < maze.Width*  2; i+=2)
            {
                for (int j = 0; j < maze.Height * 2; j+=2)
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
                    else
                        ret[i, j] = true;
                }
            }

            return ret;
        }

        public static bool[,] ToBoolArray(Maze<RoomCell> maze)
        {
            bool[,] ret = new bool[maze.Width * 2, maze.Height * 2];

            for (int i = 0; i < maze.Width * 2; i += 2)
            {
                for (int j = 0; j < maze.Height * 2; j += 2)
                {
                    if (!maze[i / 2, j / 2].Visited) continue;

                    if(maze[i/2, j/2].IsRoom)
                    {
                        ret[i, j] = true;
                        ret[i + 1, j] = true;
                        ret[i, j + 1] = true;
                        ret[i + 1, j + 1] = true;

                        if (!maze[i / 2, j / 2].Right)
                        {
                            ret[i + 1, j] = false;
                            ret[i + 1, j + 1] = false;
                        }
                        if (!maze[i / 2, j / 2].Bottom)
                        {
                            ret[i , j + 1] = false;
                            ret[i + 1, j +1 ] = false;
                        }
                    }
                    else
                    {
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
                        else
                            ret[i, j] = true;
                    }
                   
                }
            }

            return ret;
        }
    }
}
