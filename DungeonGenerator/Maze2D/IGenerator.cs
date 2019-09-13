using DungeonGenerator.Maze2D.Cells;

namespace DungeonGenerator.Maze2D
{
    public interface IGenerator<T> where T : Cell, new()
    {
        Maze<T> Generate(int x, int y);
    }
}
