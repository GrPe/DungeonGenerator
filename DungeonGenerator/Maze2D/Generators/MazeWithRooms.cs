using DungeonGenerator.Maze2D.Cells;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonGenerator.Maze2D.Generators
{
    public class MazeWithRooms
    {
        private Maze<Cell> maze;
        private Random random;

        public int MinRoomSize { get; private set; }
        public int MaxRoomSize { get; private set; }


        public MazeWithRooms()
        {
            MinRoomSize = 4;
            MaxRoomSize = 8;
        }

        public void SetRoomSize(int min, int max)
        {
            MinRoomSize = min;
            MaxRoomSize = max;
        }

        public Maze<Cell> Generate(int x, int y, IMaskGenerator<Cell> generator, int density = 500)
        {
            maze = new Maze<Cell>(x, y);
            random = new Random();

            CreateRooms(density);
            maze = generator.Generate(x, y, maze);

            return maze;
        }

        private void CreateRooms(int density)
        {
            List<Room> rooms = new List<Room>();
            Room room = null;

            int attemption = density;

            do
            {
                room = new Room(rooms.Count, random.Next(MinRoomSize, MaxRoomSize), random.Next(MinRoomSize, MaxRoomSize));
                room.SetPosition(random.Next(1, maze.Width), random.Next(1, maze.Height));

                if (room.Position.X + room.Width >= maze.Width ||
                    room.Position.Y + room.Height >= maze.Height)
                    continue;


                bool collided = false;
                //collision detection
                foreach (var r in rooms)
                {
                    if (room.Position.X < r.Position.X + r.Width + 1 &&
                        room.Position.X + room.Width + 1 > r.Position.X &&
                        room.Position.Y < r.Position.Y + r.Height + 1 &&
                        room.Position.Y + room.Height + 1 > r.Position.Y)
                    {
                        collided = true;
                        break;
                    }
                }

                if (!collided)
                {
                    rooms.Add(room);
                    InsertRoom(room);
                    attemption = density;
                }

                attemption--;

            } while (attemption >= 0);
        }

        private void InsertRoom(Room room)
        {
            for (int i = room.Position.X; i < room.Position.X + room.Width; i++)
            {
                for (int j = room.Position.Y; j < room.Position.Y + room.Height; j++)
                {
                    maze[i, j].Locked = true;
                }
            }
        }
    }
}
