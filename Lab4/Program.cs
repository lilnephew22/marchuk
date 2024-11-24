using LabsLibrary;
using McMaster.Extensions.CommandLineUtils;
using System.Runtime.InteropServices;

namespace Lab4
{
    [Command(Name = "RunLabApp", Description = "Console app to run lab projects")]
    [Subcommand(typeof(VersionCommand), typeof(RunCommand), typeof(SetPathCommand))]//вказує список підкоманд, які можна використовувати з цим додатком.
    class Program
    {
        public static int Main(string[] args) => CommandLineApplication.Execute<Program>(args);

        private void OnExecute()
        {
            Console.WriteLine("Please provide a command. Available commands: version, run, set-path");
        }
    }

    [Command(Name = "version", Description = "Displays application information")]
    class VersionCommand
    {
        public int OnExecute()
        {
            Console.WriteLine("Author: Igor Lugovoy, Version: 1.0.0");
            return 0;
        }
    }

    [Command(Name = "run", Description = "Executes the specified lab project")]
    class RunCommand
    {
        [Argument(0, Description = "Lab name: lab1, lab2, or lab3")]
        public string LabName { get; }

        [Option("-I|--input", Description = "Direct path to the input file")]
        public string InputPath { get; }

        [Option("-O|--output", Description = "Direct path to the output file")]
        public string OutputPath { get; }

        public int OnExecute()
        {
            try
            {
                var labRunner = new LabRunner();
                string inputPath = Path.Combine(GetDirectoryPath(InputPath, "LAB_PATH"), "input.txt");
                string outputPath = Path.Combine(GetDirectoryPath(OutputPath, "LAB_PATH"), "output.txt");

                if (!File.Exists(inputPath))
                {
                    throw new FileNotFoundException("Required files 'input.txt' not found in any specified path.");
                }

                switch (LabName.ToLower())
                {
                    case "lab1":
                        labRunner.RunLab1(inputPath, outputPath);
                        break;
                    case "lab2":
                        labRunner.RunLab2(inputPath, outputPath);
                        break;
                    case "lab3":
                        labRunner.RunLab3(inputPath, outputPath);
                        break;
                    default:
                        Console.WriteLine("Unknown lab. Available options: lab1, lab2, lab3.");
                        return 1;
                }
                Console.WriteLine($"Lab {LabName} executed.");
                return 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return 1;
            }
        }

        private string GetDirectoryPath(string? directPath, string envVariable)
        {
            //шлях вказали в консолі
            if (!string.IsNullOrEmpty(directPath) && Directory.Exists(directPath))
            {
                return directPath;
            }
            //через значення змінної  “LAB_PATH”
            string? envPath = Environment.GetEnvironmentVariable(envVariable);
            if (!string.IsNullOrEmpty(envPath) && Directory.Exists(envPath))
            {
                return envPath;
            }
            //через домашню директорію користувача
            string homePath = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
            return homePath;
        }
    }

    [Command(Name = "set-path", Description = "Sets the path for input and output files")]
    class SetPathCommand
    {
        [Option("-p|--path", Description = "Path to the directory containing input and output files", ShowInHelpText = true)]
        public string Path { get; }

        public int OnExecute()
        {
            if (Directory.Exists(Path))
            {
                Environment.SetEnvironmentVariable("LAB_PATH", Path);
                Console.WriteLine($"Environment variable 'LAB_PATH' set to {Path}");
                return 0;
            }
            else
            {
                Console.WriteLine("Invalid path. Directory does not exist.");
                return 1;
            }
        }
    }
}