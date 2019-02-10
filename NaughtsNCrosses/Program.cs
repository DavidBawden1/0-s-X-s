using NaughtsNCrosses.Players;
using NaughtsNCrosses.Rules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NaughtsNCrosses
{
    class Program
    {
        public static List<string> ErrorMessages { get; set; } = new List<string>();
        public static List<Player> Players { get; set; } = new List<Player>();
        public static Player Player1 { get; set; } = new Player();
        public static Player Player2 { get; set; } = new Player();
        public static Player CurrentPlayer { get; set; } = new Player();
        public static Board Board { get; set; } = new Board();
        public static Referee Referee { get; set; } = new Referee();

        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Naughts and Crosses!");
            InitialisePlayers();
            GameIntroduction();

            Board board = new Board();
            var CurrentPlayer = Players.FirstOrDefault(x => x.IsTurn == true);
            board.DrawBoard(board.PossiblePlays);

            int turnNumber = 0;
            while (turnNumber < board.MaxNumberOfPlays)
            {
                Console.WriteLine($"Current Turn is {CurrentPlayer.Name}, plot your {CurrentPlayer.NaughtOrCross}");
                turnNumber++;
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
            CheckMarkerTaken(playIndex); // bug here. the second go overwrites the first go. 
            CurrentPlayer = Players.FirstOrDefault(x => x.IsTurn == true);
            UpdatePlays(playIndex, CurrentPlayer);
            if(CheckWinner())
            {
                Console.WriteLine($"{CurrentPlayer.Name} WINS!!!");
            }
            CurrentPlayer = Referee.DetermineCurrentPlayersTurn(Players).FirstOrDefault(x => x.IsTurn == true);

            return CurrentPlayer;
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
            if(Board.PossiblePlays[0] == Board.PossiblePlays[4] && Board.PossiblePlays[4] == Board.PossiblePlays[8])
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
