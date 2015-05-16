using System.Collections.Generic;

namespace B15_Ex02_Serge_310881082_Tal_200348316.Othello
{
    internal struct Direction
    {
        private int m_XDirection;
        private int m_YDirection;

        public int X
        {
            get { return m_XDirection; }
        }

        public int Y
        {
            get { return m_YDirection; }
        }

        public Direction(int i_X, int i_Y)
        {
            m_XDirection = i_X;
            m_YDirection = i_Y;
        }
    }

    internal class Move
    {
        private int m_X;
        private int m_Y;
        private int m_Value;
        private List<Direction> m_Directions;

        public int X
        {
            get
            {
                return m_X;
            }
        }

        public int Y
        {
            get
            {
                return m_Y;
            }
        }

        public List<Direction> Directions
        {
            get
            {
                return m_Directions;
            }
        }

        public int Value
        {
            get
            {
                return m_Value;
            }
        }

        public Move(int i_X, int i_Y, List<Direction> i_Directions, int i_Value)
        {
            m_X = i_X;
            m_Y = i_Y;
            m_Directions = i_Directions;
            m_Value = i_Value;
        }
    }
}
