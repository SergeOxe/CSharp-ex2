using System.Collections.Generic;

namespace B15_Ex02_Serge_310881082_Tal_200348316.Othello
{
    internal struct Direction
    {
        private readonly int r_XDirection;
        private readonly int r_YDirection;

        public int X
        {
            get { return r_XDirection; }
        }

        public int Y
        {
            get { return r_YDirection; }
        }

        public Direction(int i_X, int i_Y)
        {
            r_XDirection = i_X;
            r_YDirection = i_Y;
        }
    }

    internal class Move
    {
        private readonly int r_X;
        private readonly int r_Y;
        private readonly int r_Value;
        private readonly List<Direction> r_Directions;

        public int X
        {
            get
            {
                return r_X;
            }
        }

        public int Y
        {
            get
            {
                return r_Y;
            }
        }

        public List<Direction> Directions
        {
            get
            {
                return r_Directions;
            }
        }

        public int Value
        {
            get
            {
                return r_Value;
            }
        }

        public Move(int i_X, int i_Y, List<Direction> i_Directions, int i_Value)
        {
            r_X = i_X;
            r_Y = i_Y;
            r_Directions = i_Directions;
            r_Value = i_Value;
        }
    }
}
