using Microsoft.VisualBasic;
using System.Collections.Generic;
using System.Collections.Immutable;


/*      
*--------------------------------------------------------------------
* 	        File name: VideoGames
*           Project name: VideoGames
*--------------------------------------------------------------------
*           Author’s name and email: Brooke Acevedo acevedob@etsu.edu			
*           Course-Section: Server Side Web Programming
*           Creation Date: 09-06-2023
* -------------------------------------------------------------------
*/

// A lot of this code was referenced from the pokemon code reference provided to us as an example

namespace VideoGames

{
    internal class Program
    {

        static void Main(string[] args)
        {

            // String that allows us to have access to the file we wanna use
            string projectFolder = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.ToString();
            string filePath = $"{projectFolder}{Path.DirectorySeparatorChar}videogames.csv";

            // Creates a new list
            List<VideoGames> gamingList = new List<VideoGames>();

            // ---- DICTIONARY ---- //
            Dictionary<string, VideoGames> genreDictionary = new Dictionary<string, VideoGames>();
            Dictionary<string, VideoGames> platformDictionary = new Dictionary<string, VideoGames>();

            // ---- QUEUE ---- //
            Queue<VideoGames> gamingQueue = new Queue<VideoGames>();

            // ---- STACK ---- //
            Stack<VideoGames> gamingStack = new Stack<VideoGames>();



            using (var sr  = new StreamReader(filePath))
            {
                // Cuts off the header
                sr.ReadLine();

                // Essentially reads all the data
                while (!sr.EndOfStream)
                {
                    // Puts the line into a variable and splits them up w/ commas
                    string? line = sr.ReadLine();
                    string[] lineSplitting = line.Split(',');

                    // Allows us to adjust our file for the information we desire and allows easy access to it
                    VideoGames videoGames = new VideoGames()
                    {
                        Name = lineSplitting[0].ToLower(),
                        Platform = lineSplitting[1].ToLower()

                    };
                        
                    // Parsing is used to convert strings of numbers into numbers
                    // This code was referenced by ChatGPT
                    if (int.TryParse(lineSplitting[2], out int year))
                    {
                        videoGames.Year = year;

                    }
                    else
                    {   
                        videoGames.Year = 0000;

                    }

                    videoGames.Genre = lineSplitting[3].ToLower();
                    videoGames.Publisher = lineSplitting[4].ToLower();

                    // This code was referenced from ChatGPT
                    if (decimal.TryParse(lineSplitting[9], out decimal globalSales))
                    {
                        videoGames.GlobalSales = globalSales;

                    }
                    else
                    {
                        videoGames.GlobalSales = 0.00m;

                    }

                    // Adds it all
                    gamingList.Add(videoGames);

                    // Puts information into the dictionary
                    genreDictionary[videoGames.Genre] = videoGames;
                    platformDictionary[videoGames.Platform] = videoGames;

                    // Puts information into the queue
                    gamingQueue.Enqueue(videoGames);

                    // Puts information into the stack 
                    gamingStack.Push(videoGames);

                }

                // Sorts the list in alphabetical order
                gamingList.Sort();

                // -- DICTIONARY STUFF -- //
                // Prints out all the genre types


                Console.WriteLine("-- GENRE TYPES --");
                foreach (var kvp in genreDictionary)
                {
                    Console.WriteLine(kvp.Key);
                }

                Console.WriteLine("\n");

                // Prints out all the platform types
                Console.WriteLine("-- PLATFORM TYPES --");
                foreach (var kvp in platformDictionary)
                {
                    Console.WriteLine(kvp.Key);
                }

                Console.WriteLine("\n");

                // -- QUEUE STUFF -- //
                // Creates the queue for the prefered video games
                Console.WriteLine("This program creates a queue of video games of your desire!");
                Console.Write("Enter your prefered platform: ");
                string preferedPlatform = ExceptionHandling(Console.ReadLine());

                Console.Write("Enter your prefered genre: ");
                string preferedGenre = ExceptionHandling(Console.ReadLine());


                VideoGameQueue(gamingQueue, preferedPlatform, preferedGenre);

                // -- STACK STUFF -- //
                Console.Write("\n Enter a year between 1900 and 2023: ");
                int gamesAfterDate = Convert.ToInt32(Console.ReadLine());

                VideoGameStack(gamingStack, gamesAfterDate);

                // ---------------------- LAB 1 ---------------------- //
                /* Console.WriteLine("Which publisher would you like to see?");

                // Takes user input for publisher
                string uI = Console.ReadLine().Trim().ToLower();
                string userInputPub = char.ToUpper(uI[0]) + uI.Substring(1);

                // Uses the two methods below to get the important information
                PublisherData(gamingList, userInputPub);
                PublisherGameAmount(gamingList, userInputPub);


                Console.WriteLine("Which genre would you like to see?");

                // Takes user input for genre
                string uIG = Console.ReadLine().Trim().ToLower();
                string userInputGenre = char.ToUpper(uIG[0]) + uIG.Substring(1);

                GenreData(gamingList, userInputGenre);
                GenreAmount(gamingList, userInputGenre);
                */

                // Takes the user input and creates a stack of all the games made after a certain date 
                static void VideoGameStack(Stack<VideoGames> gamingStack, int releaseDate)
                {
                    // Gathers all the games that released after a certain game and shows the first in the stack (LINQ)
                    var desiredGame = gamingStack.Where(videoGames => videoGames.Year > releaseDate);
                    var firstValue = desiredGame.FirstOrDefault();
                    Console.WriteLine($"\nFirst game in the stack\n{firstValue}\n");

                    float numDesiredGames = desiredGame.Count();
                    Console.WriteLine($"There were {numDesiredGames} games released after {releaseDate}.");

                    // Lists all in the stack
                    /*
                    Console.WriteLine($"-- Games created after {releaseDate} --");
                    foreach (var videoGame in desiredGame)
                    {
                        Console.WriteLine($"\nName: {videoGame.Name}");
                        Console.WriteLine($"Publisher: {videoGame.Year}");

                    }
                    */

                }

                // Takes the user input and creates a queue of games that fit their preferences
                static void VideoGameQueue(Queue<VideoGames> gamingQueue, string platform, string genre)
                {
                    // Gathers all the prefered games and shows the first in the queue to the user (LINQ)
                    var desiredGame = gamingQueue.Where(videoGames => videoGames.Platform == platform && videoGames.Genre == genre);
                    var firstValue = desiredGame.FirstOrDefault();
                    Console.WriteLine($"\nYour first game to play!\n{firstValue}\n");

                    // Lists all the queue
                    Console.WriteLine("-- QUEUED GAMES --");
                    foreach (var videoGame in desiredGame)
                    {
                        Console.WriteLine($"\nName: {videoGame.Name}");
                        Console.WriteLine($"Publisher: {videoGame.Publisher}");

                    }
                    

                }


                // Gathers all the info for each game under a specific publisher
                static void PublisherData(List<VideoGames> gamingList, string userInput)
                {
                    // Locates all the published games from the chosen publisher and prints them to the console
                    var publisherGames = gamingList.Where(videoGames => videoGames.Publisher == userInput);


                    foreach (var videoGames in publisherGames)
                    {
                        Console.WriteLine(videoGames);

                    }

                    Console.WriteLine($"\n");


                }

                // Calculates the amount of games a certain publisher has within the list
                static void PublisherGameAmount(List<VideoGames> gamingList, string publisher)
                {
                    float numGames = gamingList.Count;
                    var numPublisher = new List<VideoGames>();


                    foreach (VideoGames videoGames in gamingList) 
                    { 
                        if (videoGames.Publisher == publisher)
                        {
                            numPublisher.Add(videoGames);


                        }

                    }

                    float numPublisherGames = numPublisher.Count;
                    var percentage = Math.Round(numPublisherGames / numGames * 100, 2);

                    // Prints it to the console 
                    Console.WriteLine($"Out of {numGames} games, {numPublisherGames} games are developed by {publisher}, which is about {percentage}%.\n");

                }


                // Does the same as publisher but for genre
                static void GenreData(List<VideoGames> gamingList, string userInput)
                {
                    // Locates all the games from the chosen genre and prints them to the console
                    var genreGames = gamingList.Where(videoGames => videoGames.Genre == userInput);


                    foreach (var videoGames in genreGames)
                    {
                        Console.WriteLine(videoGames);

                    }

                    Console.WriteLine($"\n");


                }

                // Does the same as publisher but for genre
                static void GenreAmount(List<VideoGames> gamingList, string genre)
                {
                    float numGames = gamingList.Count;
                    var numGenre = new List<VideoGames>();


                    foreach (VideoGames videoGames in gamingList)
                    {
                        if (videoGames.Genre == genre)
                        {
                            numGenre.Add(videoGames);


                        }

                    }

                    float numGenreGames = numGenre.Count;
                    var percentage = Math.Round(numGenreGames / numGames * 100, 2);

                    // Prints it to the console 
                    Console.WriteLine($"Out of {numGames} games, {numGenreGames} games are {genre} games, which is about {percentage}%.\n");

                }

                // Does some exception handling, ensuring it's all lowercase
                static string ExceptionHandling(string userInput)
                {
                    if (!string.IsNullOrEmpty(userInput))
                    {
                        userInput = userInput.ToLower();

                    }
                    else
                    {
                        Console.WriteLine("This is an invalid input.");

                    }

                    return userInput;

                }


            }
        }
    }
}