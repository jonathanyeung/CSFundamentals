using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interviews.OODesign
{
    public class Othello
    {
        private SpaceType[,] _grid;

        public void NewGame(int length)
        {
            if (length % 2 != 0)
            {
                throw new ArgumentException("Must be even");
            }
            _grid = new SpaceType[length, length];

            for (int i = 0; i < length; i++)
            {
                for (int j = 0; j < length; j++)
                {
                    if ((i == (length / 2) && (j == length / 2)) ||
                        (i == 1 + (length / 2) && (j == 1 + length / 2)))
                    {
                        _grid[i, j] = SpaceType.White;
                    }
                    else if ((i == (length / 2) && (j == 1 + length / 2)) ||
                        (i == 1 + (length / 2) && (j == length / 2)))
                    _grid[i, j] = SpaceType.Black;
                }
            }
        }

        public bool PlayMove(int i, int j)
        {
            if (i >= _grid.Length || j >= _grid.Length)
            {
                return false;
            }

            if (_grid[i,j] != SpaceType.Unoccupied)
            {
                return false;
            }

            throw new NotImplementedException();

        }
    }

    enum SpaceType
    {
        Unoccupied,
        Black,
        White
    }
}
