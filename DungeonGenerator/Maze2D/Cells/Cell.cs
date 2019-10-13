using System;
using System.Collections.Generic;
using System.Linq;

namespace DungeonGenerator.Maze2D.Cells
{
    public class Cell : ICell
    {
        public Position Position { get; set; }
        public bool Visited { get; set; }
        private bool locked;
        public bool Locked
        {
            get
            {
                return locked;
            }
            set
            {
                locked = value;
                Visited = locked;
            }
        }

        //connections
        public bool Left { get; protected set; }
        public bool Right { get; protected set; }
        public bool Top { get; protected set; }
        public bool Bottom { get; protected set; }

        public void Connect(Cell cell)
        {
            if (this.Position.X < cell.Position.X)
            {
                Right = true;
                cell.Left = true;
            }
            else if (this.Position.X > cell.Position.X)
            {
                Left = true;
                cell.Right = true;
            }
            else if (this.Position.Y < cell.Position.Y)
            {
                Bottom = true;
                cell.Top = true;
            }
            else if (this.Position.Y > cell.Position.Y)
            {
                Top = true;
                cell.Bottom = true;
            }
        }

        public void Disconnect(Cell cell)
        {
            if (this.Position.X < cell.Position.X)
            {
                Right = false;
                cell.Left = false;
            }
            else if (this.Position.X > cell.Position.X)
            {
                Left = false;
                cell.Right = false;
            }
            else if (this.Position.Y < cell.Position.Y)
            {
                Bottom = false;
                cell.Top = false;
            }
            else if (this.Position.Y > cell.Position.Y)
            {
                Top = false;
                cell.Bottom = false;
            }
        }

        public Position GetLeftNeighbor()
        {
            if (Position.X == 0 || Locked) return null;
            return new Position(Position.X - 1, Position.Y);
        }

        public Position GetRightNeighbor(int mazeWidth)
        {
            if (Position.X == mazeWidth - 1 || Locked) return null;
            return new Position(Position.X + 1, Position.Y);
        }

        public Position GetTopNeighbor()
        {
            if (Position.Y == 0 || Locked) return null;
            return new Position(Position.X, Position.Y - 1);
        }

        public Position GetBottomNeighbor(int mazeHeight)
        {
            if (Position.Y == mazeHeight - 1 || Locked) return null;
            return new Position(Position.X, Position.Y + 1);
        }

        public List<Position> GetAllNeighbors(int w, int h)
        {
            if (Locked) return new List<Position>();

            return new List<Position>
            {
                GetBottomNeighbor(h),
                GetTopNeighbor(),
                GetRightNeighbor(w),
                GetLeftNeighbor()
            }.Where(n => n != null).ToList();
        }
    }
}