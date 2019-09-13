﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace DungeonGenerator.Maze2D.Cells
{
    public class Cell : ICell
    {
        public Position Position { get; set; }
        public bool Visited { get; set; }
        //connections
        public bool Left { get; protected set; }
        public bool Right { get; protected set; }
        public bool Top { get; protected set; }
        public bool Bottom { get; protected set; }

        public void Connect(Cell cell)
        {
            if(this.Position.X < cell.Position.X)
            {
                Right = true;
                cell.Left = true;
            }
            else if(this.Position.X > cell.Position.X)
            {
                Left = true;
                cell.Right = true;
            }
            else if(this.Position.Y < cell.Position.Y)
            {
                Bottom = true;
                cell.Top = true;
            }
            else if(this.Position.Y > cell.Position.Y)
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
            if (Position.X == 0) return null;
            return new Position(Position.X - 1, Position.Y);
        }

        public Position GetRightNeighbor(int mazeWidth)
        {
            if (Position.X == mazeWidth - 1) return null;
            return new Position(Position.X + 1, Position.Y);
        }

        public Position GetTopNeighbor()
        {
            if (Position.Y == 0) return null;
            return new Position(Position.X, Position.Y - 1);
        }

        public Position GetBottomNeighbor(int mazeHeight)
        {
            if (Position.Y == mazeHeight - 1) return null;
            return new Position(Position.X, Position.Y + 1);
        }

        public List<Position> GetAllNeighbors(int w, int h)
        {
            return new List<Position>
            {
                GetBottomNeighbor(h),
                GetTopNeighbor(),
                GetRightNeighbor(w),
                GetLeftNeighbor()
            }.Where(n => n != null).ToList();
        }

        [Obsolete()]
        public void InsertConnection(Direction direction)
        {
            switch(direction)
            {
                case Direction.Left:
                    Left = true;
                    break;
                case Direction.Right:
                    Right = true;
                    break;
                case Direction.Top:
                    Top = true;
                    break;
                case Direction.Bottom:
                    Bottom = true;
                    break;
            }
        }

        [Obsolete()]
        public void RemoveConnection(Direction direction)
        {
            switch (direction)
            {
                case Direction.Left:
                    Left = false;
                    break;
                case Direction.Right:
                    Right = false;
                    break;
                case Direction.Top:
                    Top = false;
                    break;
                case Direction.Bottom:
                    Bottom = false;
                    break;
            }
        }

        [Obsolete()]
        public void SetAllWallOpened()
        {
            Left = true;
            Right = true;
            Top = true;
            Bottom = true;
        }
    }
}