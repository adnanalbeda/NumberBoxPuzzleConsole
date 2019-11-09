namespace SimpleNumberBoxGameBase
{
    public class Box
    {
        private int _CorrectXInd;
        private int _CorrectYInd;

        public int YInd { get; set; }
        public int XInd { get; set; }

        public int Number { get; set; }
        public bool IsHidden { get; set; }

        public GameColor BoxColor { get; private set; }

        public Box(int number)
        {
            Number = number;
            int x = number % 3 - 1;
            if (x == -1) x = 2;

            int y;
            if (number < 4) y = 0;
            else if (number < 7) y = 1;
            else y = 2;

            XInd = _CorrectXInd = x;
            YInd = _CorrectYInd = y;

            IsHidden = false;
        }

        public Box()
        {
            XInd = _CorrectXInd = 2;
            YInd = _CorrectYInd = 2;
            Number = 9;
            IsHidden = true;
        }

        public void Update()
        {
            if (IsHidden)
                BoxColor = GameColor.Blue;
            else if (IsInRightPlace())
                BoxColor = GameColor.Green;
            else
                BoxColor = GameColor.Red;
        }

        public bool IsInRightPlace()
        {
            return XInd == _CorrectXInd && YInd == _CorrectYInd;
        }
    }

    public enum GameColor
    {
        Blue = 9,
        Green,
        Red = 12
    }
}
