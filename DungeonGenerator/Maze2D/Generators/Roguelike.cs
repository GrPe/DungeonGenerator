using DungeonGenerator.Maze2D.Cells;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonGenerator.Maze2D.Generators
{
    public sealed class Roguelike : IGenerator<RoomCell>
    {
        private Maze<RoomCell> maze;
        private List<Room> rooms;
        private Random random;

        public int MinRoomSize { get; private set; }
        public int MaxRoomSize { get; private set; }
        public int Padding { get; private set; }
        public int MaxConnection { get; private set; }

        public Roguelike()
        {
            MinRoomSize = 4;
            MaxRoomSize = 8;
            Padding = 1;
            MaxConnection = 2;
        }

        public void SetRoomSize(int min, int max)
        {
            MinRoomSize = min;
            MaxRoomSize = max;
        }

        public void SetPadding(int padding)
        {
            Padding = padding;
        }

        public void SetMaxConnection(int max)
        {
            if (max < 1) return;
            MaxConnection = max;
        }

        public Maze<RoomCell> Generate(int x, int y) => Generate(x, y, 300);

        public Maze<RoomCell> Generate(int x, int y, int density)
        {
            maze = new Maze<RoomCell>(x, y);
            rooms = new List<Room>();
            random = new Random();

            InsertRooms(density);
            ConnectRooms();

            PrepareMaze();

            return maze;
        }

        private void InsertRooms(int density)
        {
            Room room = null;

            bool replaced = false;
            int attemption = density;

            while (attemption >= 0)
            {
                room = new Room(rooms.Count, random.Next(MinRoomSize, MaxRoomSize), random.Next(MinRoomSize, MaxRoomSize));
                room.SetPosition(random.Next(1, maze.Width), random.Next(1, maze.Height));

                if (room.Position.X + room.Width >= maze.Width ||
                    room.Position.Y + room.Height >= maze.Height)
                    continue;

                replaced = false;

                //collision detection
                foreach (var r in rooms)
                {
                    if (room.Position.X < r.Position.X + r.Width + 1 &&
                        room.Position.X + room.Width + 1 > r.Position.X &&
                        room.Position.Y < r.Position.Y + r.Height + 1 &&
                        room.Position.Y + room.Height + 1 > r.Position.Y)
                    {
                        replaced = true;
                        break;
                    }
                }

                attemption--;

                if (!replaced)
                {
                    rooms.Add(room);
                    attemption = density;
                }
            }
        }

        private void ConnectRooms()
        {
            List<Pair<Room>> connections = new List<Pair<Room>>();

            for (int i = 0; i < rooms.Count; i++)
            {
                var neighbors = GetNearestNeighbors(rooms[i], i + 1);
                connections.AddRange(neighbors);
            }

            foreach (var c in connections)
            {
                c.First.Connections.Add(c.Second.Id);
            }
        }

        private List<Pair<Room>> GetNearestNeighbors(Room room, int start)
        {
            var neighbors = new List<KeyValuePair<double, Room>>();

            for (int i = start; i < rooms.Count; i++)
            {
                double dist = GetDistance(room, rooms[i]);
                neighbors.Add(new KeyValuePair<double, Room>(dist, rooms[i]));
            }

            var list = (from r in neighbors orderby r.Key ascending select r.Value).Take(random.Next(1, MaxConnection)).ToList();

            List<Pair<Room>> ret = new List<Pair<Room>>();
            foreach (var n in list)
                ret.Add(new Pair<Room> { First = room, Second = n });

            return ret;
        }

        private double GetDistance(Room r1, Room r2)
        {
            return Math.Sqrt(Math.Pow(r1.Center.X - r2.Center.X, 2) + Math.Pow(r1.Center.Y - r2.Center.Y, 2));
        }

        private void PrepareMaze()
        {
            //draw rooms
            foreach (var r in rooms)
            {
                for (int i = r.Position.X; i < r.Position.X + r.Width; i++)
                {
                    for (int j = r.Position.Y; j < r.Position.Y + r.Height; j++)
                    {
                        maze[i, j].Visited = true;
                        if (i != (r.Position.X + r.Width - 1) && j != (r.Position.Y + r.Height - 1))
                        {
                            maze[i, j].IsRoom = true;
                            maze[i, j].SetAllWallOpened();
                            continue;
                        }
                        if (i == r.Position.X + r.Width - 1 && j == r.Position.Y + r.Height - 1) continue;
                        if (i == r.Position.X + r.Width - 1) maze[i, j].InsertConnection(Direction.Bottom);
                        if (j == r.Position.Y + r.Height - 1) maze[i, j].InsertConnection(Direction.Right);
                    }
                }
            }

            //draw paths
            foreach (var r in rooms)
            {
                foreach (var id in r.Connections)
                {
                    Position direction = rooms[id].Center - r.Center;

                    while(direction.X != 0 || direction.Y != 0)
                    {
                        if(direction.X < 0)
                        {
                            maze[r.Center + direction].InsertConnection(Direction.Right);
                            direction.X++;
                            maze[r.Center + direction].InsertConnection(Direction.Left);
                        }
                        else if(direction.X > 0)
                        {
                            maze[r.Center + direction].InsertConnection(Direction.Left);
                            direction.X--;
                            maze[r.Center + direction].InsertConnection(Direction.Right);
                        }
                        else if(direction.Y < 0)
                        {
                            maze[r.Center + direction].InsertConnection(Direction.Bottom);
                            direction.Y++;
                            maze[r.Center + direction].InsertConnection(Direction.Top);
                        }
                        else if(direction.Y > 0)
                        {
                            maze[r.Center + direction].InsertConnection(Direction.Top);
                            direction.Y--;
                            maze[r.Center + direction].InsertConnection(Direction.Bottom);
                        }
                        maze[r.Center + direction].Visited = true;
                    }
                }
            }
        }
    }

    internal sealed class Pair<T>
    {
        public T First { get; set; }
        public T Second { get; set; }
    }
}
