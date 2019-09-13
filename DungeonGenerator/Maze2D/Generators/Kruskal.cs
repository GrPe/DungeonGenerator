using DungeonGenerator.Maze2D.Cells;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DungeonGenerator.Maze2D.Generators
{
    public sealed class Kruskal : IGenerator<KruskalCell>
    {
        private Maze<KruskalCell> maze;
        private List<Pair> walls;

        public Maze<KruskalCell> Generate(int x, int y)
        {
            maze = new Maze<KruskalCell>(x, y);
            walls = new List<Pair>();

            //create list of connections
            int currentColor = 1;

            foreach(var cell in maze.Get())
            {
                cell.Visited = true;
                cell.Color = currentColor++;

                Position left = cell.GetRightNeighbor(maze.Width);
                Position bottom = cell.GetBottomNeighbor(maze.Height);
                if (left != null) walls.Add(new Pair { First = cell, Second = maze[left] });
                if (bottom != null) walls.Add(new Pair { First = cell, Second = maze[bottom] });
            }

            Random random = new Random();

            while(walls.Count > 0)
            {
                int selected = random.Next(0, walls.Count);
                Pair wall = walls[selected];
                walls.RemoveAt(selected);

                if (wall.First.Color == wall.Second.Color) continue;
                
                wall.First.Connect(wall.Second);
                RecolorMaze(wall);
            }

            return maze;
        }

        private void RecolorMaze(Pair connection)
        {
            Queue<KruskalCell> cells = new Queue<KruskalCell>();
            cells.Enqueue(connection.First);
            int color = connection.First.Color;

            while(cells.Count > 0)
            {
                KruskalCell cell = cells.Dequeue();
                cell.Color = color;

                if (cell.Left && maze[cell.GetLeftNeighbor()].Color != color)
                    cells.Enqueue(maze[cell.GetLeftNeighbor()]);
                if (cell.Right && maze[cell.GetRightNeighbor(maze.Width)].Color != color)
                    cells.Enqueue(maze[cell.GetRightNeighbor(maze.Width)]);
                if (cell.Top && maze[cell.GetTopNeighbor()].Color != color)
                    cells.Enqueue(maze[cell.GetTopNeighbor()]);
                if (cell.Bottom && maze[cell.GetBottomNeighbor(maze.Height)].Color != color)
                    cells.Enqueue(maze[cell.GetBottomNeighbor(maze.Height)]);
            }
        }
    }

    internal sealed class Pair
    {
        public KruskalCell First { get; set; }
        public KruskalCell Second { get; set; }
    }
}
