using DungeonGenerator.Maze2D.Cells;
using System;
using System.Collections.Generic;

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
            for(int i = 0; i < maze.Width; i++)
            {
                for(int j = 0; j < maze.Height; j++)
                {
                    maze[i, j].Visited = true;
                    maze[i, j].Color = currentColor;
                    currentColor++;
                    if (i < maze.Width - 1) walls.Add(new Pair { First = new Position(i, j), Second = new Position(i + 1, j) });
                    if (j < maze.Height - 1) walls.Add(new Pair { First = new Position(i, j), Second = new Position(i, j + 1) });
                }
            }

            Random random = new Random();

            while(walls.Count > 0)
            {
                int selected = random.Next(0, walls.Count);
                Pair wall = walls[selected];
                walls.RemoveAt(selected);

                if (maze[wall.First].Color == maze[wall.Second].Color) continue;

                //left-right
                if(wall.First.X < wall.Second.X)
                {
                    maze[wall.First].InsertConnection(Direction.Right);
                    maze[wall.Second].InsertConnection(Direction.Left);
                    RecolorMaze(wall);
                }
                else //top - down
                {
                    maze[wall.First].InsertConnection(Direction.Bottom);
                    maze[wall.Second].InsertConnection(Direction.Top);
                    RecolorMaze(wall);
                }
            }

            return maze;
        }

        private void RecolorMaze(Pair connection)
        {
            Queue<Position> cells = new Queue<Position>();
            cells.Enqueue(connection.First);
            int color = maze[connection.First].Color;

            while(cells.Count > 0)
            {
                Position position = cells.Dequeue();
                maze[position].Color = color;

                if(maze[position].Left && maze[position.X - 1, position.Y].Color != color)
                    cells.Enqueue(new Position(position.X - 1, position.Y));
                if (maze[position].Right && maze[position.X + 1, position.Y].Color != color)
                    cells.Enqueue(new Position(position.X + 1, position.Y));
                if (maze[position].Top && maze[position.X, position.Y - 1].Color != color)
                    cells.Enqueue(new Position(position.X, position.Y - 1));
                if (maze[position].Bottom && maze[position.X, position.Y + 1].Color != color)
                    cells.Enqueue(new Position(position.X, position.Y + 1));
            }
        }
    }

    internal sealed class Pair
    {
        public Position First { get; set; }
        public Position Second { get; set; }
    }
}
