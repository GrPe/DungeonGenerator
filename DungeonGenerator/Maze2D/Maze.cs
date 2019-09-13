using DungeonGenerator.Maze2D.Cells;
using System.Collections.Generic;

namespace DungeonGenerator.Maze2D
{
    public class Maze<T> where T : Cell, new()
    {
        private T[,] maze;
        public int Width { get; protected set; }
        public int Height { get; protected set; }

        public Maze(int width, int height)
        {
            Width = width;
            Height = height;
            maze = new T[width, height];

            for (int i = 0; i < Width; i++)
            {
                for (int j = 0; j < Height; j++)
                {
                    maze[i, j] = new T
                    {
                        Position = new Position(i, j)
                    };
                }
            }
        }

        public T this[Position position]
        {
            get => maze[position.X, position.Y];
            set => maze[position.X, position.Y] = value;
        }

        public T this[int x, int y]
        {
            get => maze[x, y];
            set => maze[x, y] = value;
        }

        public IEnumerable<T> Get()
        {
            for (int i = 0; i < Width; i++)
                for (int j = 0; j < Height; j++)
                    yield return maze[i, j];
        }
    }
}
