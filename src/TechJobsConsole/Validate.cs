using System;
using System.Collections.Generic;
using System.Text;

namespace TechJobsConsole
{
    public static class Validate
    {
        public static string CheckIfEmpty(string input)
        {
            while (!Int32.TryParse(input, out int result))
            {
                Console.WriteLine("Please type and enter an integer to make your selection");
                input = Console.ReadLine();
            }
            return input;
        }
    }
}
