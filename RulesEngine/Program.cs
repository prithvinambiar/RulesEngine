using System;
using System.IO;

namespace RulesEngine
{
    internal class Program
    {
        private static void Main()
        {
            Console.WriteLine("Please enter the file path for raw data");
            var rawDataFilePath = Console.ReadLine();
            var rawData = ReadFile(rawDataFilePath);

            Console.WriteLine("Please enter the file path for rules");
            var ruleFilePath = Console.ReadLine();
            var rules = ReadFile(ruleFilePath);

            var notification = Engine.Run(rawData, rules);

            Console.WriteLine("Results - ");
            foreach (var errorMessage in notification.GetErrorMessages())
            {
                Console.WriteLine(errorMessage);
            }
            Console.WriteLine("Press any key to exit");
            Console.ReadKey();
        }

        private static string ReadFile(string filePath)
        {
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException(filePath);
            }
            return File.ReadAllText(filePath);
        }
    }
}
