namespace Lab2
{
    public class Task2
    {
        private int[,] board;
        private int N;

        public Task2(int size, int[] diagonalValues)
        {
            N = size;
            FillBoard(diagonalValues);
        }
        public Task2() { }

        public int Start()
        {
            string[] lines = File.ReadAllLines(@"Lab2\input.txt");
            N = int.Parse(lines[0]); // Розмір дошки
            int[] arr = Array.ConvertAll(lines[1].Split(), int.Parse); // Числа на діагоналі

            FillBoard(arr);

            // Початкова позиція пішака (лівий нижній кут)
            int result = Minimax(N - 1, 0, true);

            return result;
            //File.WriteAllText(@"Lab2\output.txt", result.ToString());
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

            // Якщо хід першого гравця, він хоче максимізувати виграш
            if (isFirstPlayer)
            {
                int moveUp = Minimax(x - 1, y, false);
                int moveRight = Minimax(x, y + 1, false); 

                return Math.Max(moveUp, moveRight);
            }
            // Якщо хід другого гравця, він хоче мінімізувати виграш першого
            else
            {
                int moveUp = Minimax(x - 1, y, true); 
                int moveRight = Minimax(x, y + 1, true);

                return Math.Min(moveUp, moveRight);
            }
        }
    }
}
