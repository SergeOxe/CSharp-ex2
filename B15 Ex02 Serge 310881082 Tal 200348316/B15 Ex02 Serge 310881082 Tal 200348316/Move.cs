﻿using System;
using System.Collections.Generic;
using System.Text;

namespace B15_Ex02_Serge_310881082_Tal_200348316
{

    struct Direction
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

    class Move
    {
        private int m_X;
        private int m_Y;
        private List<Direction> m_Directions;

        public int X { get { return m_X; } }
        public int Y { get { return m_Y; } }
        public List<Direction> Directions { get { return m_Directions;} } 

        public Move(int i_X, int i_Y, List<Direction> i_Directions)
        {
            m_X = i_X;
            m_Y = i_Y;
            m_Directions = i_Directions;
        }
    }
}
