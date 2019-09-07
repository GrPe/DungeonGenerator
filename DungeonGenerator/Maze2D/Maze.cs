using DungeonGenerator.Maze2D.Cells;

namespace DungeonGenerator.Maze2D
{
    public class Maze<T> where T : ICell, new()
    {
        private T[,] maze;
        public int Width { get; protected set; }
        public int Height { get; protected set; }

        public Maze(int width, int height)
        {
            Width = width;
            Height = height;
            maze = new T[width, height];

            for (int i = 0; i < maze.Length; i++)
                maze[i / Height, i % Height] = new T();
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
    }
}
