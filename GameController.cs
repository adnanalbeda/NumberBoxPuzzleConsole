using System;

namespace SimpleNumberBoxGameBase
{
    public class GameController
    {
        // Rows
        public Box[] Row0 { get; private set; }
        public Box[] Row1 { get; private set; }
        public Box[] Row2 { get; private set; }

        // the empty box, for declaring moving axis.
        public Box EmptyBox { get; private set; }
        public Box LastMovedBox { get; private set; }

        public bool Win { get; private set; }
        public bool Quit { get; private set; }

        #region Initialize Game
        public GameController()
        {
            Row0 = new Box[3]
            {
                new Box(1),
                new Box(2),
                new Box(3)
            };
            Row1 = new Box[3]
            {
                new Box(4),
                new Box(5),
                new Box(6)
            };
            Row2 = new Box[3]
            {
                new Box(7),
                new Box(8),
                EmptyBox = new Box()
            };
            ShuffleBoard();
            UpdateValues();
        }

        private void ShuffleBoard()
        {
            Random rnd = new Random();
            for (int i = 0; i < 300; i++) // make 500 random move
            {

                // moving empty box to new location
                if (rnd.NextDouble() > 0.5) // vertical shift
                {
                    double rndd = rnd.NextDouble();
                    try // on error, then there will not be enough room to move, so shift moving into Ys
                    {
                        if (rndd < 0.25)
                        {
                            MoveLeft();
                        }
                        else if (rndd < 0.5)
                        {
                            MoveRight();
                        }
                        else if (rndd < 0.75)
                        {
                            MoveLeft();
                            MoveLeft();
                        }
                        else
                        {
                            MoveRight();
                            MoveRight();
                        }
                    }
                    catch
                    {
                        if (rndd < 0.5)
                            MoveUp();
                        else
                            MoveDown();
                    }

                } // done moving vertically
                else // horizontal shift
                {
                    double rndd = rnd.NextDouble();
                    if (rndd < 0.25)
                    {
                        MoveUp();
                    }
                    else if (rndd < 0.5)
                    {
                        MoveDown();
                    }
                    else if (rndd < 0.75)
                    {
                        MoveUp();
                        MoveUp();
                    }
                    else
                    {
                        MoveDown();
                        MoveDown();
                    }
                } // done moving horizontally
            } // done shuffling
        }
        #endregion

        #region User Input
        public bool HandleInput(KeyPressed key)
        {
            bool moved = false;
            // checking move
            if (key == KeyPressed.Up)
            {
                moved = MoveUp();
            }
            if (key == KeyPressed.Down)
            {
                moved = MoveDown();
            }
            if (key == KeyPressed.Left)
            {
                if (EmptyBox.XInd == 0) return false; // move not allowed
                MoveLeft(); moved = true;
            }
            if (key == KeyPressed.Right)
            {
                if (EmptyBox.XInd == 2) return false; // move not allowed
                MoveRight(); moved = true;
            }

            // quit game
            if (key == KeyPressed.Esc)
                Quit = true;

            UpdateValues();

            return moved;
        }

        private void UpdateValues()
        {
            Win = true;
            // upadte each item in Row, and check if player should win by knowing all boxes are in right place. 
            for (int i = 0; i < 3; i++)
            {
                Row0[i].Update();
                Win &= Row0[i].IsInRightPlace();
            }
            for (int i = 0; i < 3; i++)
            {
                Row1[i].Update();
                Win &= Row1[i].IsInRightPlace();
            }
            for (int i = 0; i < 3; i++)
            {
                Row2[i].Update();
                Win &= Row2[i].IsInRightPlace();
            }

            // in case player has won, stop timer, and make the hidden box visible.
            if (Win)
            {
                Row2[2].IsHidden = false;
                Row2[2].Update();
                Quit = Win;
            }
        }
        #endregion

        #region Moving Empty Box
        private void MoveLeft()
        {
            int newInd = EmptyBox.XInd - 1;
            switch (EmptyBox.YInd)
            {
                case 0:
                    Box b01 = Row0[EmptyBox.XInd];
                    Box b02 = LastMovedBox = Row0[newInd];

                    b02.XInd = EmptyBox.XInd;
                    b01.XInd = newInd;

                    Row0[b02.XInd] = b02;
                    Row0[newInd] = b01;
                    break;
                case 1:
                    Box b11 = Row1[EmptyBox.XInd];
                    Box b12 = LastMovedBox = Row1[newInd];

                    b12.XInd = EmptyBox.XInd;
                    b11.XInd = newInd;

                    Row1[b12.XInd] = b12;
                    Row1[newInd] = b11;
                    break;
                case 2:
                    Box b21 = Row2[EmptyBox.XInd];
                    Box b22 = LastMovedBox = Row2[newInd];

                    b22.XInd = EmptyBox.XInd;
                    b21.XInd = newInd;

                    Row2[b22.XInd] = b22;
                    Row2[newInd] = b21;
                    break;
            }
        }

        private void MoveRight()
        {
            int newInd = EmptyBox.XInd + 1;
            switch (EmptyBox.YInd)
            {
                case 0:
                    Box b01 = Row0[EmptyBox.XInd];
                    Box b02 = LastMovedBox = Row0[newInd];

                    b02.XInd = EmptyBox.XInd;
                    b01.XInd = newInd;

                    Row0[b02.XInd] = b02;
                    Row0[newInd] = b01;
                    break;
                case 1:
                    Box b11 = Row1[EmptyBox.XInd];
                    Box b12 = LastMovedBox = Row1[newInd];

                    b12.XInd = EmptyBox.XInd;
                    b11.XInd = newInd;

                    Row1[b12.XInd] = b12;
                    Row1[newInd] = b11;
                    break;
                case 2:
                    Box b21 = Row2[EmptyBox.XInd];
                    Box b22 = LastMovedBox = Row2[newInd];

                    b22.XInd = EmptyBox.XInd;
                    b21.XInd = newInd;

                    Row2[b22.XInd] = b22;
                    Row2[newInd] = b21;
                    break;
            }
        }

        private bool MoveDown()
        {
            switch (EmptyBox.YInd)
            {
                case 0:
                    Box b01 = LastMovedBox = Row1[EmptyBox.XInd];
                    Box b02 = Row0[EmptyBox.XInd];

                    b01.YInd -= 1;
                    b02.YInd += 1;

                    Row1[EmptyBox.XInd] = b02;
                    Row0[EmptyBox.XInd] = b01;
                    return true;
                case 1:
                    Box b11 = LastMovedBox = Row2[EmptyBox.XInd];
                    Box b12 = Row1[EmptyBox.XInd];

                    b11.YInd -= 1;
                    b12.YInd += 1;

                    Row2[EmptyBox.XInd] = b12;
                    Row1[EmptyBox.XInd] = b11;
                    return true;
            }
            return false;
        }

        private bool MoveUp()
        {
            switch (EmptyBox.YInd)
            {
                case 2:
                    Box b21 = LastMovedBox = Row1[EmptyBox.XInd];
                    Box b22 = Row2[EmptyBox.XInd];

                    b21.YInd += 1;
                    b22.YInd -= 1;

                    Row1[EmptyBox.XInd] = b22;
                    Row2[EmptyBox.XInd] = b21;
                    return true;
                case 1:
                    Box b11 = LastMovedBox = Row0[EmptyBox.XInd];
                    Box b12 = Row1[EmptyBox.XInd];

                    b11.YInd += 1;
                    b12.YInd -= 1;

                    Row0[EmptyBox.XInd] = b12;
                    Row1[EmptyBox.XInd] = b11;
                    return true;
            }
            return false;
        }
        #endregion
    }

    public enum KeyPressed
    {
        Up,
        Down,
        Left,
        Right,
        Esc
    }
}
