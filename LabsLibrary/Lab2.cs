namespace LabsLibrary
{
    internal class Lab2
    {
        private int[,] board;
        private int N;

        public Lab2(int size, int[] diagonalValues)
        {
            N = size;
            FillBoard(diagonalValues);
        }
        public Lab2() { }

        public int Start(string[] lines)
        {
            N = int.Parse(lines[0]); 
            int[] arr = Array.ConvertAll(lines[1].Split(), int.Parse); 

            FillBoard(arr);

            int result = Minimax(N - 1, 0, true);

            return result;

        }

        public void FillBoard(int[] a)
        {
            board = new int[N, N];
            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < N; j++)
                {
                    board[i, j] = 0;
                }
            }
            for (int i = 0; i < N; i++)
            {
                board[i, i] = a[i];
            }
        }

        public int Minimax(int x, int y, bool isFirstPlayer)
        {

            if (x < 0 || y >= N)
            {
                return 0;
            }

            if (x == y)
            {
                return board[x, y];
            }

            if (isFirstPlayer)
            {
                int moveUp = Minimax(x - 1, y, false);
                int moveRight = Minimax(x, y + 1, false);

                return Math.Max(moveUp, moveRight);
            }
            else
            {
                int moveUp = Minimax(x - 1, y, true);
                int moveRight = Minimax(x, y + 1, true);

                return Math.Min(moveUp, moveRight);
            }
        }
    }
}
