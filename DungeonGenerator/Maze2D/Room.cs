using System.Collections.Generic;

namespace DungeonGenerator.Maze2D
{
    public class Room
    {
        public int Id { get; private set; }
        public Position Position { get; private set; }
        public Position Center { get; private set; }
        public int Width { get; private set; }
        public int Height { get; private set; }
        public List<int> Connections { get; set; }

        public Room(int id, int width, int height)
        {
            Position = new Position(0, 0);
            Center = new Position(0, 0);
            Width = width;
            Height = height;
            Id = id;
            Connections = new List<int>();
        }

        public Room(int id, int x, int y, int width, int height) : this(id, width, height)
        {
            Position = new Position(x, y);
            Center = new Position((x + x + Width) / 2, (y + y + Height) / 2);
        }

        public void SetPosition(int x, int y)
        {
            Position = new Position(x, y);
            Center = new Position((x + x + Width) / 2, (y + y + Height) / 2);
        }
    }
}
