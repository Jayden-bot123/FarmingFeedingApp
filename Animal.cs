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
        public double TotalWeeklyFood()
        {
            return 0;
        }
        public double TotalWeeklyCost()
        {
            return 0;
        }
        public string CheckFeedingStatus(double min, double max)
        {
            return "";
        }
        public string AnimalSummary()
        {
            return "";
        }
    }
}

