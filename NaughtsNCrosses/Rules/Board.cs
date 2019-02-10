using System;

namespace NaughtsNCrosses.Rules
{
    public class Board
    {
        const int RowCount = 3;
        const int ColumnCount = 3;
        internal int MaxNumberOfPlays = 9;
        public char[] PossiblePlays = { '0', '1', '2', '3', '4', '5', '6', '7', '8' };

        public void DrawBoard(char[] choices)
        {
            int rowCounter = 0;
            int choice = 0;
            while (rowCounter < RowCount)
            {
                Console.WriteLine($"| {choices[choice]} | {choices[choice+=1]}  | {choices[choice+=1]}\n");
                rowCounter++;
                if(rowCounter == 1)
                {
                    choice = 3;
                }
                else if(rowCounter == 2)
                {
                    choice = 6;
                }
            }
          }
    }
}
