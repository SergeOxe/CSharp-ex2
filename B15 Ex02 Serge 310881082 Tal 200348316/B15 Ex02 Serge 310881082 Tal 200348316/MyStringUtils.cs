namespace B15_Ex02_Serge_310881082_Tal_200348316
{
    public class MyStringUtils
    {
        public static string AddOrdinal(int i_Num)
        {
            string oridnaledNum = i_Num.ToString();

            if (i_Num > 0)
            {
                int mod = i_Num % 100;

                if (mod >= 11 && mod <= 13)
                {
                    oridnaledNum = i_Num + "th";
                }
                else
                {
                    if (mod == 1)
                    {
                        oridnaledNum = i_Num + "st";
                    }
                    else if (mod == 2)
                    {
                        oridnaledNum = i_Num + "nd";
                    }
                    else if (mod == 3)
                    {
                        oridnaledNum = i_Num + "rd";
                    }
                    else
                    {
                        oridnaledNum = i_Num + "th";
                    }
                }
            }

            return oridnaledNum;
        }
    }
}
