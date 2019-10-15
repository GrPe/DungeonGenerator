using DungeonGenerator.Maze2D.Cells;
using System.Collections.Generic;

namespace DungeonGenerator.Maze2D.Travelsals
{
    public static  class Travelsal
    {
        public static List<Position> FindPath<T>(this Maze<T> maze, Position start, Position end) where T : Cell, new()
        {
            var dijkstra = new DijkstraPathFinder();
            return dijkstra.FindPath(maze, start, end);
        }
    }
}
