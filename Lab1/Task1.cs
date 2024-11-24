namespace Lab1
{
    public class Task1
    {
        public static CircularLinkedList<int> arr = new CircularLinkedList<int>();
        public static int n = 7;//n - кількість коробок
        public static int totalStepsCounter = 0;
        public int TaskSolution()
        {
            string[] input = File.ReadAllLines(@"Lab1\input.txt");

            n = int.Parse(input[0]);

            if (n < 1 || n > 1000)
            {
                throw new ArgumentException("The number of boxes should be within the limits 1 ≤ N ≤ 1000");
            }

            string[] balls = input[1].Split(' ');
 
            if (balls.Length != n)
            {
                throw new ArgumentException("The number of elements in the second line must be equal N");
            }

            int sum = 0;

            foreach (string ball in balls)
            {
                int ballCount = int.Parse(ball);

                if (ballCount < 0 || ballCount > n)
                {
                    throw new ArgumentException("The number of balls in each box must be within the limits 0 ≤ Ai ≤ N");
                }

                arr.Add(ballCount);
                sum += ballCount;
            }


            if (sum != n)
            {
                throw new ArgumentException("Sum of balls must = N");
            }

            bool balanced = false;
            while (!balanced)
            {
                balanced = true;

                for (int i = 0; i < n; i++)
                {
                    int currentBalls = arr.GetNodeData(i);

                    // Якщо в коробці більше 1 кулі, треба змістити кульки
                    if (currentBalls > 1)
                    {
                        balanced = false; // Треба переносити кульку
                        FindClosestEmptyNode(i);
                    }
                }
            }
            return totalStepsCounter;
            //File.WriteAllText(@"Lab1\output.txt", $"{totalStepsCounter}");
        }
        public void FindClosestEmptyNode(int currentIndex)
        {
            int nextSteps = FindNextEmptyNode(currentIndex);
            int prevSteps = FindPrevEmptyNode(currentIndex);
            if (nextSteps <= prevSteps)
            {
                Move(currentIndex, nextSteps, true);
            }
            else
            {
                Move(currentIndex, prevSteps, false);
            }
        }
        public static int FindNextEmptyNode(int currentIndex)
        {
            int nextSteps = 0;
            int index = currentIndex;
            do
            {
                index = (index + 1) % n;
                nextSteps++;
            }
            while (arr.GetNodeData(index) != 0 && nextSteps < arr.Count);

            return nextSteps;
        }
        public static int FindPrevEmptyNode(int currentIndex)
        {
            int prevSteps = 0;
            int index = currentIndex;
            do
            {
                index = (index - 1 + n) % n;
                prevSteps++;
            }
            while (arr.GetNodeData(index) != 0 && prevSteps < arr.Count);
            return prevSteps;
        }
        public void Move(int currentIndex, int steps, bool rotation)
        {

            arr.SetNodeData(currentIndex, arr.GetNodeData(currentIndex) - 1);
            if (rotation)
            {
                int newIndex = (currentIndex + steps) % n;
                arr.SetNodeData(newIndex, arr.GetNodeData(newIndex) + 1);
                totalStepsCounter = totalStepsCounter + steps;
            }
            else
            {
                int newIndex = (currentIndex - steps + n) % n;
                arr.SetNodeData(newIndex, arr.GetNodeData(newIndex) + 1);
                totalStepsCounter = totalStepsCounter + steps;
            }
        }
    }
}
