using System;
using System.Collections.Generic;
using System.Linq;

namespace TechJobsConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            // Create two Dictionary vars to hold info for menu and data

            // Top-level menu options
            Dictionary<string, string> actionChoices = new Dictionary<string, string>();
            actionChoices.Add("search", "Search");
            actionChoices.Add("list", "List");

            // Column options
            Dictionary<string, string> columnChoices = new Dictionary<string, string>();
            columnChoices.Add("core competency", "Skill");
            columnChoices.Add("employer", "Employer");
            columnChoices.Add("location", "Location");
            columnChoices.Add("position type", "Position Type");
            columnChoices.Add("all", "All");

            Console.WriteLine("Welcome to LaunchCode's TechJobs App!");

            // Allow user to search/list until they manually quit with ctrl+c
            while (true)
            {

                string actionChoice = GetUserSelection("View Jobs", actionChoices);

                if (actionChoice.Equals("list"))
                {
                    string columnChoice = GetUserSelection("List", columnChoices);

                    if (columnChoice.Equals("all"))
                    {
                        PrintJobs(JobData.FindAll());
                    }
                    else
                    {
                        List<string> results = JobData.FindAll(columnChoice);

                        Console.WriteLine("\n*** All " + columnChoices[columnChoice] + " Values ***");
                        foreach (string item in results)
                        {
                            Console.WriteLine(item);
                        }
                    }
                }
                else // choice is "search"
                {
                    // How does the user want to search (e.g. by skill or employer)
                    string columnChoice = GetUserSelection("Search", columnChoices);

                    // What is their search term?
                    Console.WriteLine("\nSearch term: ");
                    string searchTerm = Console.ReadLine();

                    List<Dictionary<string, string>> searchResults;

                    // Fetch results
                    if (columnChoice.Equals("all"))
                    {
                        searchResults = JobData.FindByValue(searchTerm).OrderBy(x => x.ContainsKey("name")).ToList();

                        for (int i = 0; i < searchResults.Count; i++)
                        {
                            Console.WriteLine("*****");

                            foreach (var job in searchResults[i])
                            {
                                Console.WriteLine("{0}: {1}",job.Key, job.Value);
                            }
                            Console.WriteLine("*****\n\n");
                        }
                    }
                    else
                    {
                        searchResults = JobData.FindByColumnAndValue(columnChoice, searchTerm).OrderBy(x => x.ContainsKey("name")).ToList();
                        PrintJobs(searchResults);
                    }
                }
            }
        }

        /*
         * Returns the key of the selected item from the choices Dictionary
         */
        private static string GetUserSelection(string choiceHeader, Dictionary<string, string> choices)
        {
            int choiceIdx;
            bool isValidChoice = false;
            string[] choiceKeys = new string[choices.Count];//Gets the count from input params and makes a string array of that length

            int i = 0;
            foreach (KeyValuePair<string, string> choice in choices)
            {
                choiceKeys[i] = choice.Key;
                i++;
            }

            do
            {
                Console.WriteLine("\n" + choiceHeader + " by:");

                for (int j = 0; j < choiceKeys.Length; j++)
                {
                    Console.WriteLine(j + " - " + choices[choiceKeys[j]]);
                }

                
                choiceIdx = int.Parse(Validate.CheckIfEmpty(Console.ReadLine()));

                if (choiceIdx < 0 || choiceIdx >= choiceKeys.Length)
                {
                    Console.WriteLine("Invalid choices. Try again.");
                }
                else
                {
                    isValidChoice = true;
                }

            } while (!isValidChoice);

            return choiceKeys[choiceIdx];
        }

        private static void PrintJobs(List<Dictionary<string, string>> someJobs)
        {
            if (someJobs.Count == 0)
            {
                Console.WriteLine("No results!");
                return;
            }

            var orderedJobs = someJobs.OrderBy(x => x.ContainsKey("name"));
            List<Dictionary<string, string>> printList = new List<Dictionary<string, string>>(orderedJobs);

            for (int i = 0; i < printList.Count; i++)
            {
                Console.WriteLine("*****");
                foreach (var job in printList[i])
                {
                    Console.WriteLine("{0}: {1}", job.Key, job.Value);
                }
                Console.WriteLine("*****\n\n");
            }
        }
    }
}
