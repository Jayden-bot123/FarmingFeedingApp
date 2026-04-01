using System;
namespace FarmingFeedingApp
{
	public class Animal
	{
        //Properties
		private string animalID;
		private string species;
		private string breed;
		private string feedType;
		private double[] dailyFood;
		private double costPerGram;

        //Constructor
        public Animal(string animalID, string species, string breed, string feedType, double[] dailyFood, double costPerGram)
        {
            this.animalID = animalID;
            this.species = species;
            this.breed = breed;
            this.feedType = feedType;
            this.dailyFood = dailyFood;
            this.costPerGram = costPerGram;
        }

        //Getters 
        public string GetAnimalID()
        {
            return animalID;
        }
        public string GetSpecies()
        {
            return species;
        }
        public string GetBreed()
        {
            return breed;
        }
        public string GetFeedType()
        {
            return feedType;
        }
        public double[] GetDailyFood()
        {
            return dailyFood;
        }
        public double GetCostPerGram()
        {
            return costPerGram;
        }

        // Methods

        // Adds up all 7 daily food values and returns the total grams for the week
        public double TotalWeeklyFood()
        {
            double total = 0;
            foreach (double grams in dailyFood)
            {
                total += grams;
            }
            return total;
        }

        // Multiplies total weekly food by cost per gram to get weekly cost
        public double TotalWeeklyCost()
        {
            return TotalWeeklyFood() * costPerGram;
        }

        // Compares weekly total against breed min/max range and returns a message if the animal is overeating, undereating or normal.
        public string CheckFeedingStatus(double min, double max)
        {
            double weeklyTotal = TotalWeeklyFood();

            Console.ForegroundColor = ConsoleColor.White;
            if (weeklyTotal < min)
            {

                return "Undereating";
                Console.ForegroundColor = ConsoleColor.Yellow;

            }

            else if (weeklyTotal > max)
            {
   
                return "Overeating";
                Console.ForegroundColor = ConsoleColor.DarkRed;
            }
            else
            {

                return "Normal";
                Console.ForegroundColor = ConsoleColor.Green;

            }

        }
        public string AnimalSummary()
        {
            string[] days = { "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "Sunday" };

            string summary = "\n============================================================\n";
            summary += $"Animal ID: {animalID}\nSpecies: {species}\nBreed: {breed}\nFeed Type: {feedType}\nDaily Food Consumption:\n";

            for (int i = 0; i < 7; i++)
            {
                summary += $"{days[i], -10}: {dailyFood[i]}g\n";
            }

            summary += "------------------------------------------------------------\n";
            summary += $"Total Weekly Food: {TotalWeeklyFood():F0}g\nCost Per Gram for {feedType}: ${costPerGram:F6}\nTotal Weekly Cost : ${TotalWeeklyCost():F4}\n";
            summary += "============================================================\n";

            return summary;
        }
    }
}

