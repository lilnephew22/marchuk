using Lab1;
using Lab2;
using Lab3;
namespace LabsLibrary
{
    public class LabRunner
    {
        public void RunLab1(string inputFilePath, string outputFilePath)
        {

            Task1 task1 = new Task1();
            string result = task1.TaskSolution().ToString();

            File.WriteAllText(outputFilePath, result);

            Console.WriteLine("Data successfully written to the output file.");
        }

        public void RunLab2(string inputFilePath, string outputFilePath)
        {
            Task2 task2 = new Task2();

            string result = task2.Start().ToString();

            File.WriteAllText(outputFilePath, result);

            Console.WriteLine("Data successfully written to the output file.");
        }

        public void RunLab3(string inputFilePath, string outputFilePath)
        {
            Task3 task3 = new Task3();

            string result = task3.ExecuteTask().ToString();

            File.WriteAllText(outputFilePath, result);

            Console.WriteLine("Data successfully written to the output file.");
        }
        public string RunLab1(Stream inputFileStream)
        {
            List<string> input;
            using (var reader = new StreamReader(inputFileStream))
            {
                input = new List<string>();
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    input.Add(line);
                }
            }

            Lab1 lab1 = new Lab1();
            string result = lab1.TaskSolution(input.ToArray()).ToString();

            Console.WriteLine("Дані успішно оброблено.");
            return result;
        }
        public string RunLab2(Stream inputFileStream)
        {
            List<string> input;
            using (var reader = new StreamReader(inputFileStream))
            {
                input = new List<string>();
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    input.Add(line);
                }
            }

            Lab2 lab2 = new Lab2();
            string result = lab2.Start(input.ToArray()).ToString();

            Console.WriteLine("Дані успішно оброблено.");
            return result;
        }
        public string RunLab3(Stream inputFileStream)
        {
            List<string> input;
            using (var reader = new StreamReader(inputFileStream))
            {
                input = new List<string>();
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    input.Add(line);
                }
            }

            Lab3 lab3 = new Lab3();
            string result = lab3.ExecuteTask(input.ToArray()).ToString();

            Console.WriteLine("Дані успішно оброблено.");
            return result;
        }

    }
}