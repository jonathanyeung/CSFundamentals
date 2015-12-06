using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interviews.OODesign
{
    public class JigsawPuzzle
    {
        public List<Piece> UnfittedPieces;

        public Piece[] puzzleGrid;

        public JigsawPuzzle(int length, int width)
        {

        }

        public void Solve()
        {

        }


    }

    public class Piece
    {
        public EdgeShape North;
        public EdgeShape South;
        public EdgeShape East;
        public EdgeShape West;

        public bool FitsWith(Piece compare)
        { return true; }
    }

    public class EdgeShape
    {
        public bool IsEdge;

    }
}
