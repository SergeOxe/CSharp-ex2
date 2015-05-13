using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace B15_Ex02_Serge_310881082_Tal_200348316
{
    class Player
    {
        private readonly string r_Name;
        private readonly Piece r_Color;

        public Player(string i_Name, Piece i_Color)
        {
            r_Name = i_Name;
            r_Color = i_Color;
        }
    }
}
