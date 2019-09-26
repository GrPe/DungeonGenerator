using DungeonGenerator.Maze2D.Cells;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Priority_Queue;

namespace DungeonGenerator.Maze2D.Travelsals
{
    public class DijkstraPathFinder
    {
        public List<Position> FindPath<T>(Maze<T> maze, Position start, Position end) where T : Cell, new()
        {
            if (maze[start].Visited == false) return null;
            if (maze[end].Visited == false) return null;

            int[,] distances = new int[maze.Width, maze.Height];
            bool[,] visited = new bool[maze.Width, maze.Height];
            Position[,] ancestors = new Position[maze.Width, maze.Height];

            for (int i = 0; i < maze.Width; i++)
                for (int j = 0; j < maze.Height; j++)
                    distances[i, j] = int.MaxValue;
            
            SimplePriorityQueue<Position, int> queue = new SimplePriorityQueue<Position, int>();

            queue.Enqueue(start, 0);
            distances[start.X, start.Y] = 0;

            while (queue.Count > 0)
            {
                Position position = queue.Dequeue();
                if (visited[position.X, position.Y]) continue;
                visited[position.X, position.Y] = true;

                List<Position> neighbors = new List<Position>();

                if (maze[position].Top)
                    neighbors.Add(maze[position].GetTopNeighbor());
                if (maze[position].Bottom)
                    neighbors.Add(maze[position].GetBottomNeighbor(maze.Height));
                if (maze[position].Left)
                    neighbors.Add(maze[position].GetLeftNeighbor());
                if (maze[position].Right)
                    neighbors.Add(maze[position].GetRightNeighbor(maze.Width));

                foreach(var neighbor in neighbors)
                {
                    if(distances[neighbor.X, neighbor.Y] > distances[position.X, position.Y] + 1)
                    {
                        distances[neighbor.X, neighbor.Y] = distances[position.X, position.Y] + 1;
                        ancestors[neighbor.X, neighbor.Y] = position;
                        queue.Enqueue(neighbor, distances[neighbor.X, neighbor.Y]);
                    }
                }

            }

            List<Position> path = new List<Position>();

            Position current = end;
            while(current != null)
            {
                path.Add(current);
                current = ancestors[current.X, current.Y];
            }

            return path;
        }
    }
}
