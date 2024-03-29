﻿using DungeonGenerator.Maze2D.Cells;
using DungeonGenerator.Maze2D.Utils;
using System;
using System.Collections.Generic;

namespace DungeonGenerator.Maze2D.Generators
{
    public class Roguelike
    {
        private Maze<Cell> maze;
        private Random random;
        List<Room> rooms;

        public int MinRoomSize { get; private set; }
        public int MaxRoomSize { get; private set; }


        public Roguelike()
        {
            MinRoomSize = 4;
            MaxRoomSize = 8;
        }

        public void SetRoomSize(int min, int max)
        {
            MinRoomSize = min;
            MaxRoomSize = max;
        }

        public void Generate(int x, int y, int density = 500)
        {
            maze = new Maze<Cell>(x, y);
            random = new Random();
            DepthFirstSearch generator = new DepthFirstSearch();

            CreateRooms(density);
            maze = generator.Generate(x, y, maze);
        }

        private void CreateRooms(int density)
        {
            rooms = new List<Room>();
            Room room = null;

            int attemption = density;

            do
            {
                room = new Room(random.Next(MinRoomSize, MaxRoomSize), random.Next(MinRoomSize, MaxRoomSize));
                room.SetPosition(random.Next(1, maze.Width), random.Next(1, maze.Height));

                if (room.Position.X + room.Width >= (maze.Width) ||
                    room.Position.Y + room.Height >= (maze.Height))
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
                    AddRandomEntrance(room);
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

        private void AddRandomEntrance(Room room)
        {
            int minX = room.Position.X;
            int minY = room.Position.Y;
            int maxX = room.Position.X + room.Width - 1;
            int maxY = room.Position.Y + room.Height - 1;

            int count = random.Next(1, 4);
            while (count > 0)
            {
                Position pos = null;
                Position neighbor = null;
                switch (random.Next(4)) //0 - top, 1 - down, 2 - left, 3 - right
                {
                    case 0:
                        pos = new Position(random.Next(minX, maxX), minY);
                        neighbor = new Position(pos.X, pos.Y - 1);
                        break;
                    case 1:
                        pos = new Position(random.Next(minX, maxX), maxY);
                        neighbor = new Position(pos.X, pos.Y + 1);
                        break;
                    case 2:
                        pos = new Position(minX, random.Next(minY, maxY));
                        neighbor = new Position(pos.X - 1, pos.Y);
                        break;
                    case 3:
                        pos = new Position(maxX, random.Next(minY, maxY));
                        neighbor = new Position(pos.X + 1, pos.Y);
                        break;
                }
                maze[pos].Connect(maze[neighbor]);
                count--;
            }
        }

        public Maze<Cell> GetMaze()
        {
            return maze;
        }

        public bool[,] GetMazeAsBoolArray()
        {
            bool[,] ret = maze?.ToBoolArray();

            foreach (var room in rooms)
            {
                for (int i = room.Position.X * 2; i < room.Position.X * 2 + (room.Width - 1) * 2; i++)
                {
                    ret[i, room.Position.Y * 2 + (room.Height - 1) * 2] = true;
                    ret[i + 1, room.Position.Y * 2 + (room.Height - 1) * 2] = true;
                }

                for (int i = room.Position.Y * 2; i < room.Position.Y * 2 + (room.Height - 1) * 2; i++)
                {
                    ret[room.Position.X * 2 + (room.Width - 1) * 2, i] = true;
                    ret[room.Position.X * 2 + (room.Width - 1) * 2, i + 1] = true;
                }

                for (int i = room.Position.X * 2; i < room.Position.X * 2 + room.Width * 2 - 2; i++)
                {
                    for (int j = room.Position.Y * 2; j < room.Position.Y * 2 + room.Height * 2 - 2; j++)
                    {
                        ret[i, j] = true;
                    }
                }
            }
            return ret;
        }
    }
}
