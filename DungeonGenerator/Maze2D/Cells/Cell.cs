namespace DungeonGenerator.Maze2D.Cells
{
    public class Cell : ICell
    {
        public bool Visited { get; set; }
        //connections
        public bool Left { get; protected set; }
        public bool Right { get; protected set; }
        public bool Top { get; protected set; }
        public bool Bottom { get; protected set; }

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
        public void SetAllWallOpened()
        {
            Left = true;
            Right = true;
            Top = true;
            Bottom = true;
        }
    }
}