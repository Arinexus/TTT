using System;
using static System.Console;
using System.Collections.Generic;
using System.Data.Common;

namespace BoardGame
{
    class Program
    {
        public static void Main()
        {
            Game.Welcome();
            ReadKey();

        }
    }


    public abstract class Game
    {
        Player p1 = new Player("Player One", true); //default player for using Odd numbers
        Player p2 = new Player("Player Two", false);

        public static void Welcome()
        {
            WriteLine("Welcome to Tic-Tac-Toe Game");
            WriteLine();
            WriteLine("Please choose game");
            WriteLine("1) Numerical Tic-Tac-Toe");
            WriteLine("2) Wild Tic-Tac-Toe");
            WriteLine("");
            Write("enter the number of the option >> ");

            int type;
            while (!int.TryParse(ReadLine(), out type) || type < 1 || type > 2)
            {
                Console.WriteLine("That is not a valid input. Please enter the number.");
            }

            if (type == 1)
            {
                NumericalTicTacToe game = new NumericalTicTacToe();
                while (true)
                {
                    WriteLine("");
                    WriteLine("You choosed Numerical Tic-Tac-Toe.");
                    WriteLine("Enter S to select the game mode (Human VS Human/ Human VS Computer)");
                    WriteLine("Enter H if you need help");

                    //**valid check added by sue
                    string? choice = ReadLine();
                    if (choice == null)
                    {
                        WriteLine("That is not valid. Try again.");
                        continue;
                    }
                    if (choice.Length > 1 || choice.Length == 0)
                    {
                        WriteLine("That is not valid. Try again.");
                        continue;
                    }

                    char input = Convert.ToChar(choice);
                    input = Char.ToUpper(input);

                    if (input == 'H')
                    {
                        OnlineHelp.DisplayHelp();
                        break;
                    }
                    else if (input == 'S')
                    {
                        game.GetGameMode();
                        break;
                    }
                    else
                    {
                        WriteLine("That is not valid. Try again.");
                        continue;
                    }
                }
                ReadKey();
            }
            else if (type == 2)
            {
                WildTicTacToe game = new WildTicTacToe();
                while (true)
                {
                    WriteLine("");
                    WriteLine("You choosed Wild Tic-Tac-Toe.");
                    WriteLine("Enter S to select the game mode (Human VS Human/ Human VS Computer)");
                    WriteLine("Enter H if you need help");

                    //**valid check added by sue
                    string? choice = ReadLine();
                    if (choice == null)
                    {
                        WriteLine("That is not valid. Try again.");
                        continue;
                    }
                    if (choice.Length > 1 || choice.Length == 0)
                    {
                        WriteLine("That is not valid. Try again.");
                        continue;
                    }

                    char input = Convert.ToChar(choice);
                    input = Char.ToUpper(input);

                    if (input == 'H')
                    {
                        OnlineHelp.DisplayHelp();
                        break;
                    }
                    else if (input == 'S')
                    {
                        game.GetGameMode();
                        break;
                    }
                    else
                    {
                        WriteLine("That is not valid. Try again.");
                        continue;
                    }
                }
                ReadKey();
            }
        }

        public void GetGameMode()
        {
            Write("What game mode do you want to play? enter the number: \n");
            WriteLine("(1) Human vs Human");
            WriteLine("(2) Human vs Computer");
            WriteLine("");
            Write("Your options >> ");

            //**add valid check by sue
            int mode;
            while (!int.TryParse(ReadLine(), out mode) || mode < 1 || mode > 2)
            {
                Console.WriteLine("That is not a valid input. Please enter the number.");
            }

            if (mode == 1)
            {
                WriteLine("");
                WriteLine("You choosed Human vs Human");
                StartGame();
            }
            else if (mode == 2)
            {
                WriteLine("");
                WriteLine("You choosed Human vs Computer");
                StartGame2();
            }
        }
        public abstract void StartGame(); //Human vs Human
        public abstract void StartGame2(); //Human vs Computer
    }



    public class NumericalTicTacToe : Game
    {
        public override void StartGame() //Game logic for Human vs Human
        {
            NumericalGameBoard b1 = new NumericalGameBoard();
            b1.display(b1);
            Player p1 = new HumanPlayer("Player One", true); //default player for using Odd numbers
            Player p2 = new HumanPlayer("Player Two", false);
            Player currentPlayer = p1;
            NumericalWin win = new NumericalWin();
            History history = new History(); //NEW

            while (true) //switch between players
            {
                OnlineHelp.DisplayCommendNum();
                WriteLine($"{currentPlayer.Name}'s turn");
                WriteLine(" ");
                Location moveSelection = currentPlayer.GetMove(b1);
                int numSelection = currentPlayer.GetNumber(b1);
                history.RecordMoves(currentPlayer, moveSelection, numSelection); //NEW 
                b1.SetValue(moveSelection, numSelection);

                if (win.CheckWin(b1, p1, p2))
                {
                    WriteLine($"{currentPlayer.Name} has collected 15 points.");
                    break;
                }

                b1.display(b1);

                history.PrintMoves(); //NEW need to delete afterwards

                if (currentPlayer == p1)
                {
                    currentPlayer = p2;
                }
                else
                {
                    currentPlayer = p1;
                }

            }
        }

        public override void StartGame2() //Game logic for Human vs Computer
        {
            NumericalGameBoard b1 = new NumericalGameBoard();
            b1.display(b1);
            Player p1 = new HumanPlayer("Player One", true); //default player for using Odd numbers
            Player p2 = new ComputerPlayer("Player Two", false);
            Player currentPlayer = p1;
            NumericalWin win = new NumericalWin();
            History history = new History(); //NEW

            while (true)
            {
                OnlineHelp.DisplayCommendNum();
                WriteLine($"{currentPlayer.Name}'s turn");
                WriteLine(" ");
                Location moveSelection = currentPlayer.GetMove(b1);
                int numSelection = currentPlayer.GetNumber(b1);
                history.RecordMoves(currentPlayer, moveSelection, numSelection); //NEW 
                b1.SetValue(moveSelection, numSelection);

                if (win.CheckWin(b1, p1, p2))
                {
                    WriteLine($"{currentPlayer.Name} has collected 15 points.");
                    break;
                }

                b1.display(b1);
                history.PrintMoves(); //NEW need to delete afterwards

                if (currentPlayer == p1)
                {
                    currentPlayer = p2;
                }
                else
                {
                    currentPlayer = p1;
                }

            }
        }
    }//NumericalTicTacToe class ends here

    //WildTicTacToe class starts here
    public class WildTicTacToe : Game
    {
        public override void StartGame() //Game logic for Human vs Human
        {
            WildGameBoard b1 = new WildGameBoard();
            b1.display(b1);
            Player p1 = new HumanPlayer("Player One", true); //default player for using Odd numbers
            Player p2 = new HumanPlayer("Player Two", false);
            Player currentPlayer = p1;
            WildWin win = new WildWin();
            History history = new History(); //NEW

            while (true)
            {
                OnlineHelp.DisplayCommendWild();
                WriteLine($"{currentPlayer.Name}'s turn");
                WriteLine(" ");
                Location moveSelection = currentPlayer.GetMove(b1);
                int symSelection = currentPlayer.GetSymbol(b1);
                history.RecordMoves(currentPlayer, moveSelection, symSelection); //NEW 
                b1.SetValue(moveSelection, symSelection);

                if (win.CheckWin(b1, p1, p2))
                {
                    WriteLine($"{currentPlayer.Name} has won.");
                    break;
                }

                b1.display(b1);
                history.PrintMoves(); //NEW need to delete afterwards

                if (currentPlayer == p1)
                {
                    currentPlayer = p2;
                }
                else
                {
                    currentPlayer = p1;
                }


                if (currentPlayer == p1)
                {
                    currentPlayer = p2;
                }
                else
                {
                    currentPlayer = p1;
                }

            }
        }

        public override void StartGame2() //Game logic for Human vs Computer
        {
            WildGameBoard b1 = new WildGameBoard();
            b1.display(b1);
            Player p1 = new HumanPlayer("Player One", true); //default player for using Odd numbers
            Player p2 = new ComputerPlayer("Player Two", false);
            Player currentPlayer = p1;
            WildWin win = new WildWin();
            History history = new History(); //NEW

            while (true)
            {
                OnlineHelp.DisplayCommendWild();
                WriteLine($"{currentPlayer.Name}'s turn");
                WriteLine(" ");
                Location moveSelection = currentPlayer.GetMove(b1);
                int symSelection = currentPlayer.GetSymbol(b1);
                history.RecordMoves(currentPlayer, moveSelection, symSelection); //NEW 
                b1.SetValue(moveSelection, symSelection);

                if (win.CheckWin(b1, p1, p2))
                {
                    WriteLine($"{currentPlayer.Name} has won.");
                    break;
                }

                b1.display(b1);
                history.PrintMoves(); //NEW need to delete afterwards

                if (currentPlayer == p1)
                {
                    currentPlayer = p2;
                }
                else
                {
                    currentPlayer = p1;
                }
            }
        }

    } //WildTicTacToe class ends here



    public abstract class WinChecker
    {
        protected static List<List<Location>> WinCombinations = new List<List<Location>>
        {
            //horizontal list
            new List<Location> { new Location(0, 0), new Location(0, 1), new Location(0, 2) },
            new List<Location> { new Location(1, 0), new Location(1, 1), new Location(1, 2) },
            new List<Location> { new Location(2, 0), new Location(2, 1), new Location(2, 2) },
            //vertical list
            new List<Location> { new Location(0, 0), new Location(1, 0), new Location(2, 0) },
            new List<Location> { new Location(0, 1), new Location(1, 1), new Location(2, 1) },
            new List<Location> { new Location(0, 2), new Location(1, 2), new Location(2, 2) },
            //diagonal list
            new List<Location> { new Location(0, 0), new Location(1, 1), new Location(2, 2) },
            new List<Location> { new Location(0, 2), new Location(1, 1), new Location(2, 0) },
        };
    } //WinChecker class ends here

    public class NumericalWin : WinChecker //calculate sum of 15
    {
        public bool CheckWin(GameBoard board, Player p1, Player p2)
        {
            bool hasWin = false;
            foreach (var combo in WinCombinations)
            {
                int score = 0;
                int value = 0;
                foreach (var item in combo)
                {
                    int row = item.Row;
                    int column = item.Column;
                    value = board.GetValue(new Location(row, column));
                    if (value == 0)
                    {
                        break;
                    }

                    if (value != 0)
                    {
                        score += value;
                    }
                }
                if (score == 15)
                {
                    board.display(board);
                    WriteLine("Game Over.");
                    hasWin = true;
                }
            }
            return hasWin;
        }
    } // NumericalWin class ends here

    public class WildWin : WinChecker
    {
        public bool CheckWin(GameBoard board, Player p1, Player p2)
        {

            bool hasWin = false;
            foreach (var combo in WinCombinations)
            {
                // Check for normal win
                bool sameTriple = true;
                for (int i = 0; i < combo.Count - 1; i++)
                {
                    if (board.GetValue(combo[i]) == 0)
                    {
                        sameTriple = false;
                        break;
                    }
                    else if (board.GetValue(combo[i]) != board.GetValue(combo[i + 1]))
                    {
                        sameTriple = false;
                        break;
                    }
                }
                if (sameTriple)
                {
                    board.display(board);
                    Console.WriteLine("Game over.");
                    hasWin = true;

                }
            }
            return hasWin;
        }

    } // WildWin class ends here

    public class Move //NEW
    {
        public Player Player { get; set; }
        public Location Selection { get; set; }
        public int Number { get; set; }

        public Move(Player player, Location selection, int number)
        {
            Player = player;
            Selection = selection;
            Number = number;
        }
    }

    public class History //NEW
    {
        public List<Move> MoveHistory = new List<Move>();

        public void RecordMoves(Player player, Location selection, int number)
        {
            Move move = new Move(player, selection, number);
            MoveHistory.Add(move);
        }

        public void PrintMoves() //FOR TESTING - delete afterwards
        {
            foreach (Move move in MoveHistory)
                WriteLine($"{move.Player.Name} chose {move.Selection} for {move.Number}.");
            WriteLine(" ");
        }
    }

    public class Player
    {
        public string Name { get; set; }
        public bool IsOdd { get; set; }
        public int Score { get; set; }
        public List<int> numbers = new List<int>(); //player's list of numbers

        public Player(string name, bool isOdd)
        {
            Name = name;
            IsOdd = isOdd;
        }
        public virtual Location GetMove(GameBoard board)
        {
            return new Location(0, 0);
        }


        public virtual int GetNumber(GameBoard board)
        {
            return 0;
        }

        public virtual int GetSymbol(GameBoard board)
        {
            return 0;
        }
        protected static Location setLocation(char locationChoice) //location represented by alphabets
        {
            return locationChoice switch
            {
                'A' => new Location(0, 0),
                'B' => new Location(0, 1),
                'C' => new Location(0, 2),
                'D' => new Location(1, 0),
                'E' => new Location(1, 1),
                'F' => new Location(1, 2),
                'G' => new Location(2, 0),
                'H' => new Location(2, 1),
                'I' => new Location(2, 2),
            };
        }
    }//Player class ends here

    //HumanPlayer class starts here
    public class HumanPlayer : Player
    {
        public HumanPlayer(string name, bool isOdd) : base(name, isOdd)
        {
            Name = name;
            IsOdd = isOdd;
        }
        public override Location GetMove(GameBoard board)
        {
            while (true)
            {
                WriteLine("Where would you like to place your move?");
                string? moveStr = ReadLine();

                //valid check by sue
                if (moveStr == null)
                {
                    WriteLine("That is not valid. Try again.");
                    WriteLine(" ");
                    continue;
                }

                if (moveStr.Length > 1 || moveStr.Length == 0) //more than 1 letter or less than 1 letter -> invalid
                {
                    WriteLine("That is not valid. Try again.");
                    WriteLine(" ");
                    continue;
                }
                char move = Convert.ToChar(moveStr);
                move = Char.ToUpper(move);

                //menu? by sue
                if (move == 'U')
                {
                }
                else if (move == 'R')
                {
                }
                else if (move == 'S')
                {
                }
                else if (move == 'L')
                {

                }
                else if (move == 'Q')
                {
                    ICommand exitGame = new QuitGame();
                    exitGame.Execute();
                }

                else
                {
                    bool inRange = false;
                    char[] locationarray = new[]
                    {'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I'};
                    for (int index = 0; index < locationarray.Length; index++)
                        if (move == locationarray[index])
                        {
                            inRange = true;
                            break;
                        }

                    if (inRange == false)
                    {
                        WriteLine("That is not valid. Try again.");
                        WriteLine(" ");
                        continue;
                    }
                    Location location = setLocation(move);

                    if (board.GetValue(location) == 0)
                    {
                        return location;
                    }
                    else
                    {
                        WriteLine("That spot is taken. Please try again");
                        WriteLine(" ");
                    }
                }
            }
        }

        public override int GetNumber(GameBoard board)
        {
            while (true)
            {
                WriteLine("What number would you like to use for your move?");
                string? numberChoice = ReadLine();

                //valid check
                if (numberChoice == null)
                {
                    WriteLine("That is not valid. Try again.");
                    WriteLine(" ");
                    continue;
                }

                if (numberChoice.Length > 1 || numberChoice.Length == 0) //more than 1 letter or less than 1 letter -> invalid
                {
                    WriteLine("That is not valid. Try again.");
                    WriteLine(" ");
                    continue;
                }

                int moveValue = Convert.ToInt32(numberChoice);

                if (numbers.Contains(moveValue) || moveValue == 0 || moveValue > 9) //check if number is within 1 to 9
                {
                    WriteLine("Invalid input. Please try a different number");
                    WriteLine(" ");
                }
                else if (IsOdd && moveValue % 2 == 0)
                {
                    WriteLine("Invalid input. Please use odd numbers within 1 to 9."); //check if player 1 uses Odd number
                    WriteLine(" ");
                }
                else if (!IsOdd && moveValue % 2 == 1)
                {
                    WriteLine("Invalid input. Please use even numbers within 1 to 9."); //check if player 2 uses Even number
                    WriteLine(" ");
                }

                else
                {
                    numbers.Add(moveValue);//no overlap check
                    return moveValue;
                }
            }
        }

        public override int GetSymbol(GameBoard board)//override Player Class' method
        {
            while (true)
            {
                WriteLine("What symbol would you like to use for your move? 'O' or 'X'");
                WriteLine("1) O  2) X");
                string? symbolChoice = ReadLine();

                //valid check
                if (symbolChoice == null)
                {
                    WriteLine("That is not valid. Try again.");
                    WriteLine(" ");
                    continue;
                }

                if (symbolChoice.Length > 1 || symbolChoice.Length == 0) //more than 1 letter or less than 1 letter -> invalid
                {
                    WriteLine("That is not valid. Try again.");
                    WriteLine(" ");
                    continue;
                }
                int moveValue = Convert.ToInt32(symbolChoice);

                if (moveValue != 1 && moveValue != 2) //check if number is 1 or 2
                {
                    WriteLine("Invalid input. Please put 1 or 2");
                    WriteLine(" ");
                }
                else
                    return moveValue;

            }
        }

    } //HumanPlayer class ends here

    //ComputerPlayer class starts here
    public class ComputerPlayer : Player
    {
        public ComputerPlayer(string name, bool isOdd) : base(name, isOdd)
        {
            Name = name;
            IsOdd = isOdd;
        }

        public override Location GetMove(GameBoard board)//override Player Class' method
        {
            // Make a random move
            while (true)
            {
                WriteLine("Computer player made a move.");
                Random random = new Random();
                int row, column;
                do
                {
                    row = random.Next(0, 3);
                    column = random.Next(0, 3);

                } while (board.GetValue(new Location(row, column)) != 0);
                Location location = new Location(row, column);
                return location;

            }
        }

        public override int GetNumber(GameBoard board)//override Player Class' method
        {
            //choose a random number
            while (true)
            {
                WriteLine("Computer player chose a number.");
                Random random = new Random();
                int moveValue;
                do
                {
                    moveValue = random.Next(1, 10);
                } while (!IsOdd && moveValue % 2 == 1);
                WriteLine(" ");
                if (!numbers.Contains(moveValue))//prevent duplicate values
                {
                    numbers.Add(moveValue);
                    return moveValue;

                }
            }
        }
        public override int GetSymbol(GameBoard board)//override Player Class' method
        {
            //choose a random number
            WriteLine("Computer player chose a symbol.");
            Random random = new Random();
            int moveValue = random.Next(1, 3);
            return moveValue;

        }
    } // ComputerPlayer class ends here

    public abstract class GameBoard
    {
        public abstract void display(GameBoard board);
        public abstract void SetValue(Location location, int value);
        public abstract int GetValue(Location location);
    } //gameBoard class ends here

    public class NumericalGameBoard : GameBoard
    {
        public override void display(GameBoard board)
        {
            string[,] cellState = new string[3, 3];
            for (int row = 0; row < 3; row++)
                for (int column = 0; column < 3; column++)
                {
                    cellState[row, column] = board.GetValue(new Location(row, column)).ToString();
                    if (board.GetValue(new Location(row, column)) == 0)
                        cellState[row, column] = " ";
                }
            WriteLine("");
            WriteLine("     |     |      ");
            WriteLine($"  {cellState[0, 0]}  |  {cellState[0, 1]}  |  {cellState[0, 2]}   ");
            WriteLine("_____|_____|_____ ");
            WriteLine("     |     |      ");
            WriteLine($"  {cellState[1, 0]}  |  {cellState[1, 1]}  |  {cellState[1, 2]}   ");
            WriteLine("_____|_____|_____ ");
            WriteLine("     |     |      ");
            WriteLine($"  {cellState[2, 0]}  |  {cellState[2, 1]}  |  {cellState[2, 2]}   ");
            WriteLine("     |     |      ");
            WriteLine(" ");
        }

        private int[,] board = new int[3, 3];

        public override void SetValue(Location location, int num) //method to set value of cell
        {
            board[location.Row, location.Column] = num;
        }

        public override int GetValue(Location location) //method to get value of cell based on location
        {
            return board[location.Row, location.Column];
        }
    } //NumericalGameBoard class ends here

    public class WildGameBoard : GameBoard
    {
        public override void display(GameBoard board)
        {
            string[,] cellState = new string[3, 3];

            for (int row = 0; row < 3; row++)
                for (int column = 0; column < 3; column++)
                {
                    if (board.GetValue(new Location(row, column)) == 1)
                        cellState[row, column] = "O";
                    else if (board.GetValue(new Location(row, column)) == 2)
                        cellState[row, column] = "X";
                    else
                        cellState[row, column] = " ";
                }

            WriteLine("     |     |      ");
            WriteLine($"  {cellState[0, 0]}  |  {cellState[0, 1]}  |  {cellState[0, 2]}   ");
            WriteLine("_____|_____|_____ ");
            WriteLine("     |     |      ");
            WriteLine($"  {cellState[1, 0]}  |  {cellState[1, 1]}  |  {cellState[1, 2]}   ");
            WriteLine("_____|_____|_____ ");
            WriteLine("     |     |      ");
            WriteLine($"  {cellState[2, 0]}  |  {cellState[2, 1]}  |  {cellState[2, 2]}   ");
            WriteLine("     |     |      ");
            WriteLine(" ");
        }

        private int[,] board = new int[3, 3];

        public override void SetValue(Location location, int value) //method to set value of cell
        {
            board[location.Row, location.Column] = value;
        }

        public override int GetValue(Location location) //method to get value of cell based on location
        {
            return board[location.Row, location.Column]; //REASON WHY GRID IS RETURNING 0
        }
    } //WildGameBoard class ends here

    public class Location //mark board location with row and column
    {
        public int Row { get; }
        public int Column { get; }

        public Location(int row, int column)
        {
            Row = row;
            Column = column;
        }
    } //Location class ends here


    public abstract class OnlineHelp
    {
        public static void DisplayCommendNum()
        {
            WriteLine("");
            WriteLine("------------------------");
            WriteLine("P1: odd number (1,3,5,7,9)  P2: even number (2,4,6,8) \n" +
                "Options: U-undo  smove, R-redo move, S-save game, L-load game");
            WriteLine("------------------------");
            WriteLine();
        }

        public static void DisplayCommendWild()//by sue
        {
            WriteLine("");
            WriteLine("------------------------");
            WriteLine("P1: 'O' or 'X'  P2: 'O' or 'X' \n" +
                "Options: U-undo  smove, R-redo move, S-save game, L-load game");
            WriteLine("------------------------");
            WriteLine();
        }

        public static void DisplayHelp()
        {
            Console.Clear();
            Console.WriteLine("Choose an option:");
            Console.WriteLine("1) Rule of the game");
            Console.WriteLine("2) Reset game");
            Console.WriteLine("3) Exit");
            Console.WriteLine("4) Quit game");

            int option = 0;
            while (option < 1 || option > 4)
            {
                Console.Write("\r\nSelect an option: ");
                if (!int.TryParse(Console.ReadLine(), out option))
                {
                    Console.WriteLine("Invalid input. Please try again.");
                    option = 0;
                }
            }

            switch (option)
            {
                case 1:
                    DisplayIns();
                    break;
                case 2:
                    {
                        Console.Clear();
                        Console.WriteLine("Resetting game...");
                        Program.Main();
                    }
                    break;
                case 3:
                    {
                        // TODO: Implement Exit help logic

                    }
                    break;
                case 4:
                    {
                        ICommand exitCommand = new QuitGame();
                        exitCommand.Execute();
                    }
                    break;
            }
        }
        public static void DisplayIns()
        {
            Console.WriteLine("");
            Console.WriteLine("Welcome to the Numerical Tic Tac Toe game. Here are the rules: ");
            Console.WriteLine("");
            Console.WriteLine("Press any key to return to the main menu.");
            Console.ReadKey(true);
            Console.Clear();
            Program.Main();
        }
    }
}

