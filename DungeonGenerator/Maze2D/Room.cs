namespace DungeonGenerator.Maze2D
{
    public class Room
    {
        public Position Position { get; private set; }
        public int Width { get; private set; }
        public int Height { get; private set; }

        public Room(int width, int height)
        {
            Position = new Position(0, 0);
            Width = width;
            Height = height;
        }

        public Room(int x, int y, int width, int height) : this(width, height)
        {
            Position = new Position(x, y);
        }

        public void SetPosition(int x, int y)
        {
            Position = new Position(x, y);
        }
    }
}
