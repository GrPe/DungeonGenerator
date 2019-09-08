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
        public int MinRoomSize { get; private set; }
        public int MaxRoomSize { get; private set; }
        public int Padding { get; private set; }
        public int MaxConnection { get; private set; }

        public Roguelike()
        {
            MinRoomSize = 4;
            MaxRoomSize = 8;
            Padding = 1;
            MaxConnection = 4;
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

        public Maze<RoomCell> Generate(int x, int y) => Generate(x, y, 300);

        public Maze<RoomCell> Generate(int x, int y, int attemptions)
        {
            maze = new Maze<RoomCell>(x, y);
            rooms = new List<Room>();

            PlaceRooms(attemptions);
            ConnectRooms();

            foreach (var r in rooms)
                if (r.Connections.Count == 0)
                    Console.WriteLine(r.Id);

            PrepareMaze();

            return maze;
        }

        private void PlaceRooms(int attemptions)
        {
            Random random = new Random();
            Room room = null;

            bool replaced = false;

            Position maxPosition = new Position(maze.Width - MaxRoomSize - 1, maze.Height - MaxRoomSize - 1);
            int attemption = attemptions;

            while (attemption >= 0)
            {
                room = new Room(rooms.Count, random.Next(MinRoomSize, MaxRoomSize), random.Next(MinRoomSize, MaxRoomSize));
                room.SetPosition(random.Next(1, maze.Width), random.Next(1, maze.Height));

                if (room.Position.X + room.Width >= maze.Width ||
                    room.Position.Y + room.Height >= maze.Height)
                    continue;

                replaced = false;

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
                    attemption = attemptions;
                }
            }
        }

        private void ConnectRooms()
        {
            Random random = new Random();

            for (int i = 0; i < rooms.Count; i++)
            {
                var neighbors = GetNearestNeighbors(rooms[i], i + 1);

                int maxConnection = random.Next(1, MaxConnection);

                foreach (var r in neighbors)
                {
                    if (rooms[i].Connections.Count < maxConnection)
                    {
                        if (rooms[i].Connections.FindIndex(x => x == r.Id) < 0 && r.Connections.Count < MaxConnection)
                        {
                            rooms[i].Connections.Add(r.Id);
                            r.Connections.Add(rooms[i].Id);
                        }
                    }
                    else
                        break;
                }
            }
        }

        private List<Room> GetNearestNeighbors(Room room, int start)
        {
            var neighbors = new List<KeyValuePair<double, Room>>();

            for (int i = start; i < rooms.Count; i++)
            {
                double dist = GetDistance(room, rooms[i]);
                neighbors.Add(new KeyValuePair<double, Room>(dist, rooms[i]));
            }

            return (from r in neighbors orderby r.Key ascending select r.Value).Take(5).ToList();
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

            Random random = new Random();

            //draw paths
            foreach (var r in rooms)
            {
                foreach (var id in r.Connections)
                {
                    if (r.Id > id) continue;
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
}
