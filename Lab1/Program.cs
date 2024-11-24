namespace Lab1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Task1 task = new Task1();
            int result = task.TaskSolution();
            File.WriteAllText(@"Lab1\output.txt", $"{result}");
        }
    }
}
