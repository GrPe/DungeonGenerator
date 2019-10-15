using DungeonGenerator.Maze2D.Cells;

namespace DungeonGenerator.Maze2D.Generators
{
    public interface IMaskGenerator<T> where T : Cell, new()
    {
        Maze<T> Generate(int x, int y, Maze<T> mask);
    }
}
