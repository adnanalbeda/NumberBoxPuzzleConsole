using System;
using System.Diagnostics;
using SimpleNumberBoxGameBase;

namespace NumberBoxPuzzleConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Clear();
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            // 1- say hi to user, be polite and tell him how to play.
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("Welcome to Order Numbers Puzzle!");
            Console.WriteLine("Instructions: use arrow keys (← → ↑ ↓)\n    to move number 9 into another box.");
            Console.WriteLine("Press \"Esc\" to Quit.");
            Console.WriteLine();

            // 2- starting the game
            GameController Controller = new GameController();
            DrawBoard(Controller); // TODO: random takes a lot of time.
            ConsoleKey keyPressed = ConsoleKey.Enter;
            int x = Controller.EmptyBox.XInd * 4 + 2;
            int y = Controller.EmptyBox.YInd * 2 + 6;
            Console.SetCursorPosition(x, y);
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            // 3- game started
            while (keyPressed != ConsoleKey.Escape && !Controller.Quit)
            {
                ConsoleKeyInfo keyInfo = Console.ReadKey(true); // read user input
                switch (keyInfo.Key)
                {
                    case ConsoleKey.UpArrow:
                        keyPressed = keyInfo.Key;
                        if (Controller.HandleInput(KeyPressed.Up))
                        {
                            TypeBox(Controller.LastMovedBox);
                            Console.CursorTop -= 2;
                            TypeBox(Controller.EmptyBox);
                        }
                        break;
                    case ConsoleKey.DownArrow:
                        keyPressed = keyInfo.Key;
                        if (Controller.HandleInput(KeyPressed.Down))
                        {
                            TypeBox(Controller.LastMovedBox);
                            Console.CursorTop += 2;
                            TypeBox(Controller.EmptyBox);
                        }
                        break;
                    case ConsoleKey.LeftArrow:
                        keyPressed = keyInfo.Key;
                        if (Controller.HandleInput(KeyPressed.Left))
                        {
                            TypeBox(Controller.LastMovedBox);
                            Console.CursorLeft -= 4;
                            TypeBox(Controller.EmptyBox);
                        }
                        break;
                    case ConsoleKey.RightArrow:
                        keyPressed = keyInfo.Key;
                        if (Controller.HandleInput(KeyPressed.Right))
                        {
                            TypeBox(Controller.LastMovedBox);
                            Console.CursorLeft += 4;
                            TypeBox(Controller.EmptyBox);
                        }
                        break;
                    case ConsoleKey.Escape:
                        keyPressed = keyInfo.Key;
                        Controller.HandleInput(KeyPressed.Esc);
                        break;
                    default:
                        Console.Beep();
                        keyPressed = ConsoleKey.Enter;
                        break;
                }
            }

            // 4- game done or quit
            stopwatch.Stop();
            Console.CursorTop += 3;
            if (Controller.Win)
            {
                Console.WriteLine("Enter your name: ");
                string name = Console.ReadLine();
                Console.WriteLine("You won, {0}!", name);
                Console.WriteLine("\tScore:\t" + stopwatch.ElapsedMilliseconds / 1000 + "\tseconds!");
            }
            else
            {
                Console.WriteLine("\n\n        Sorry, I thought we are having fun. :(");
            }
            Console.WriteLine("Press any key to quit...");
            Console.ReadKey(true);
        } // end of main method


        #region Drawing Board On Console
        private static void DrawBoard(GameController Controller)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("-------------");
            DrawRaw(Controller.Row0);
            Console.WriteLine("-------------");
            DrawRaw(Controller.Row1);
            Console.WriteLine("-------------");
            DrawRaw(Controller.Row2);
            Console.Write("-------------");
        }

        private static void DrawRaw(Box[] row)
        {
            Console.Write($"| ");
            Console.ForegroundColor = PickColor(row[0].BoxColor);
            Console.Write($"{ row[0].Number }");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write($" | ");
            Console.ForegroundColor = PickColor(row[1].BoxColor);
            Console.Write($"{ row[1].Number }");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write($" | ");
            Console.ForegroundColor = PickColor(row[2].BoxColor);
            Console.Write($"{ row[2].Number }");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($" |");
        }
        #endregion

        // while playing
        private static void TypeBox(Box box)
        {
            Console.ForegroundColor = PickColor(box.BoxColor);
            Console.Write(box.Number);
            Console.CursorLeft--;
        }

        private static ConsoleColor PickColor(GameColor boxColor)
        {
            switch (boxColor)
            {
                case GameColor.Blue:
                    return ConsoleColor.Blue;
                case GameColor.Green:
                    return ConsoleColor.Green;
                case GameColor.Red:
                    return ConsoleColor.Red;
            }
            return ConsoleColor.Yellow;
        }
    }
}
