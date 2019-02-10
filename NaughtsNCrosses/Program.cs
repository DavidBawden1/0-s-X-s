using NaughtsNCrosses.Players;
using NaughtsNCrosses.Rules;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NaughtsNCrosses
{
    class Program
    {
        public static List<string> ErrorMessages { get; set; } = new List<string>();
        public static List<Player> Players { get; set; } = new List<Player>();
        public static Player Player1 { get; set; } = new Player();
        public static Player Player2 { get; set; } = new Player();
        public static Player CurrentPlayer { get; set; } = new Player();
        public static Player PreviousPlayer { get; set; } = new Player();
        public static Board Board { get; set; } = new Board();
        public static Referee Referee { get; set; } = new Referee();
        public static int TurnCount = 0;

        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Naughts and Crosses!");
            InitialisePlayers();
            GameIntroduction();

            var CurrentPlayer = Players.FirstOrDefault(x => x.IsTurn == true);
            Board.DrawBoard(Board.PossiblePlays);

            while (TurnCount < Board.MaxNumberOfPlays)
            {
                Console.WriteLine($"Current Turn is {CurrentPlayer.Name}, plot your {CurrentPlayer.NaughtOrCross}");
                TurnCount++;
                CurrentPlayer = TakeTurn(Players);
            }

            Console.ReadKey();
        }

        private static void GameIntroduction()
        {
            Referee referee = new Referee();
            Console.WriteLine("Player 1 enter your name!\n");
            Player1.Name = Console.ReadLine();
            Console.WriteLine($"Welcome {Player1.Name}");
            Console.WriteLine("Player 2 enter your name!\n");
            Player2.Name = Console.ReadLine();
            Console.WriteLine($"Welcome {Player2.Name}\n");
        }

        private static void InitialisePlayers()
        {
            Player1.IsTurn = true;
            Player1.Id = 1;
            Player1.NaughtOrCross = "O";
            Player2.IsTurn = false;
            Player2.Id = 2;
            Player2.NaughtOrCross = "X";
            Players.Add(Player1);
            Players.Add(Player2);
        }

        private static Player TakeTurn(List<Player> Players)
        {
            int playIndex = GetPlayerInput();
            CheckMarkerTaken(playIndex); 
            CurrentPlayer = Players.FirstOrDefault(x => x.IsTurn == true);
            UpdatePlays(playIndex, CurrentPlayer);
            if(CheckWinner())
            {
                CurrentPlayer.WinCounter++;
                PreviousPlayer = Players.FirstOrDefault(x => x.IsTurn == false);
                Console.WriteLine($"{CurrentPlayer.Name} WINS!!!");
                Console.WriteLine($"The score is: {CurrentPlayer.Name}: {CurrentPlayer.WinCounter} | {PreviousPlayer.Name}: {PreviousPlayer.WinCounter}");
                Console.WriteLine("Press any key to play again.");
                Console.ReadKey();
                ResetBoard();
            }
            CurrentPlayer = Referee.DetermineCurrentPlayersTurn(Players).FirstOrDefault(x => x.IsTurn == true);

            return CurrentPlayer;
        }

        private static void ResetBoard()
        {
            Board.PossiblePlays = new char[] { '0', '1', '2', '3', '4', '5', '6', '7', '8' };
            Board.DrawBoard(Board.PossiblePlays);
            TurnCount = 0;
        }

        private static void CheckMarkerTaken(int playIndex)
        {
            if (Board.PossiblePlays[playIndex] == 'O' || Board.PossiblePlays[playIndex] == 'X')
            {
                Console.WriteLine($"Invalid move {CurrentPlayer.Name}! Pick another number.");
                GetPlayerInput();
            }
        }

        private static void UpdatePlays(int playIndex, Player currentPlayer)
        {
            if (CurrentPlayer.NaughtOrCross == "O")
            {
                MarkBoard(playIndex, 'O');
            }
            else
            {
                MarkBoard(playIndex, 'X');
            }
            Board.DrawBoard(Board.PossiblePlays);
        }

        private static void MarkBoard(int playIndex, char marker)
        {
            Board.PossiblePlays[playIndex] = marker;
        }

        private static int GetPlayerInput()
        {
            int input = Int32.Parse(Console.ReadLine());
            string inRangeMessage =  $"Please provide a number from 0-9 {CurrentPlayer.Name}";
            if (input > 9)
            {
                Console.WriteLine(inRangeMessage);
                GetPlayerInput();
            }
            else if (input < 0)
            {
                Console.WriteLine(inRangeMessage);
                GetPlayerInput();
            }
          
            return input;
        }

        public static bool CheckWinner()
        {
            //diagonal checks
            if(Board.PossiblePlays[0] == Board.PossiblePlays[4] && Board.PossiblePlays[4] == Board.PossiblePlays[8])
            {
                return true;
            }
            if (Board.PossiblePlays[2] == Board.PossiblePlays[4] && Board.PossiblePlays[4] == Board.PossiblePlays[6])
            {
                return true;
            }

            //horizontal checks
            if (Board.PossiblePlays[0] == Board.PossiblePlays[1] && Board.PossiblePlays[1] == Board.PossiblePlays[2])
            {
                return true;
            }
            if (Board.PossiblePlays[3] == Board.PossiblePlays[4] && Board.PossiblePlays[4] == Board.PossiblePlays[5])
            {
                return true;
            }
            if (Board.PossiblePlays[6] == Board.PossiblePlays[7] && Board.PossiblePlays[7] == Board.PossiblePlays[8])
            {
                return true;
            }

            //vertical checks
            if (Board.PossiblePlays[0] == Board.PossiblePlays[3] && Board.PossiblePlays[3] == Board.PossiblePlays[6])
            {
                return true;
            }
            if (Board.PossiblePlays[1] == Board.PossiblePlays[4] && Board.PossiblePlays[4] == Board.PossiblePlays[7])
            {
                return true;
            }
            if (Board.PossiblePlays[2] == Board.PossiblePlays[5] && Board.PossiblePlays[5] == Board.PossiblePlays[8])
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
