using DungeonGenerator.Maze2D.Cells;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonGenerator.Maze2D.Generators
{
    public interface IMaskGenerator<T> where T : Cell, new()
    {
        Maze<T> Generate(int x, int y, Maze<T> mask);
    }
}
