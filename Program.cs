using System;
using System.IO;

namespace ToDoListConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            string filePath = "data.txt"; //The text file path that containst our Tasks
            string[] fileData = LoadData(filePath); //Call the loadData function and save it's result in fileData


            while (true) //Main Program Loop
            {
                Console.WriteLine(" What Would you like to do? \n Add | Remove | Quit");
                Console.Write(" >> ");
                string response = Console.ReadLine();
                Console.SetCursorPosition(0, Console.CursorTop - 1); //Sets the cursor to the previous line
                ClearCurrentConsoleLine(); //Calls the function ClearCurrentConsoleLine

                //Checks what the user typed in
                if (response.ToUpper() == "ADD")
                {
                    //Asks about the new task to add
                    Console.Write(" What is the task that you want to add? \n >> ");
                    string task = Console.ReadLine();
                    fileData = Add(filePath, task, fileData); //Calls the Add function and saves the new array
                }
                else if (response.ToUpper() == "REMOVE")
                {
                    //Asks about which task to remove
                    Console.Write(" What is the task's number that you want to remove? \n >> ");
                    string temp = Console.ReadLine();
                    //Check if the user typed in a number
                    bool test = Int32.TryParse(temp, out int taskNumber);
                    if (test)
                    {
                        //if the user input can be converted to an int it calls the Remove function
                        Remove(filePath, taskNumber - 1, fileData);
                    }
                }
                else if(response.ToUpper() == "QUIT")
                {
                    //Quits the Application
                    Environment.Exit(1);
                }

                //Resets the list with every new change
                Console.Clear();
                fileData = LoadData(filePath);
            }
            
        }

        static string[] Add(string filePath ,string Task, string[] fileData) //The Add functions that we use to add new tasks
        {
            if(fileData != null) //Checks if the data file is null
            {
                if(fileData.Length > 0) //Checks if the data file is empty
                {
                    Task = "\n" + Task;
                }
            }
            File.AppendAllText(filePath, Task); //Adds the new Task to the data file
            Console.Clear();
            return LoadData(filePath);
        }

        static string[] Remove(string filePath, int taskNumber, string[] fileData) //The Remove functions that we use to remove tasks
        { 
            if(taskNumber - 1 > fileData.Length || taskNumber < 0) //checks if the task number exits withing the task list and if its negative
            {
                return LoadData(filePath);
            }

            string[] newFileData = new string[fileData.Length - 1]; //making a new array but with 1 element shorter(since we are deleting an element from the old array)
            //we transfer the data in the original array into the new array
            //we use more than 1 index so we can skip the value that we want to delete
            for (int i = 0, j = 0; i < newFileData.Length; i++, j++)
            {
                if(i == taskNumber)
                {
                    j++;
                }

                newFileData[i] = fileData[j];
            }

            fileData = newFileData; //Copy the new array into the original array

            File.WriteAllLines(filePath, fileData); //Writes the new array into the data file
            //Refreshes the app
            Console.Clear();
            return LoadData(filePath);
        }


        static string[] LoadData(string filePath) //The LoadData function loads the data from the file into an array
        {
            Console.WriteLine("\n To Do List 1.0 by Mohamed Aymen Mhiri \n");
            if (File.Exists(filePath)) //Checks if the file exits
            {
                //if it does, it loads it in an array of strings called FileData
                string[] fileData = File.ReadAllLines(filePath);
                Console.WriteLine(" Your Current Tasks! \n");
                int listnumber = 0;

                if (fileData.Length == 0)//checks if the list is empty
                {
                    Console.WriteLine(" Your Task List Seems To be Empty");
                }
                else
                {
                    //if its not, it prints all the items
                    foreach (string line in fileData)
                    {
                        listnumber++;
                        Console.WriteLine(" " + listnumber.ToString() + "." + line);
                    }
                    Console.WriteLine("\n");
                }

                return fileData;

            }
            else
            {
                //if the file does not exist, it creates a new one
                Console.WriteLine(" Your List Seems To be Empty");
                using (StreamWriter sw = File.CreateText(filePath)) ;
            }

            return null;
        }

        public static void ClearCurrentConsoleLine() //Used to clear the current line in the console
        {
            int currentLineCursor = Console.CursorTop;
            Console.SetCursorPosition(0, Console.CursorTop);
            Console.Write(new string(' ', Console.WindowWidth));
            Console.SetCursorPosition(0, currentLineCursor);
        }
    }


    
}
