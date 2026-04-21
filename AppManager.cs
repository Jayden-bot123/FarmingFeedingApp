using System;
namespace FarmingFeedingApp
{
	public class AppManager
	{
        //Constants - cost per gram of each food
        private const double GRASS_SILAGE = 0.00033;
        private const double MAIZE_SILAGE = 0.00034;
        private const double GRAIN_MIX = 0.00059;
        private const double ALFALFA_HAY = 0.00051;
        private const double PASTURE_GRASS = 0.00010;
        private const double GRASS_HAY = 0.00031;
        private const double CORN_SILAGE = 0.00034;
        private const double GRAIN_SUPPLEMENT = 0.00089;
        private const double SILAGE = 0.00054;
        private const double CORN = 0.00059;
        private const double SOYBEAN_MEAL = 0.00085;
        private const double BARLEY = 0.00052;
        private const double WHEAT = 0.00055;
        private const double STARTER_LAYER_FEED = 0.00089;
        private const double OATS = 0.00051;

        //Global lists and dictionaries

        // Tracks the animal with the highest weekly cost
        private Animal highestCostAnimal = null;

        // Tracks the animal with the highest weekly food consumption
        private Animal highestConsumptionAnimal = null;

        // Stores all Animal objects added during the session
        private List<Animal> animals = new List<Animal>();

        //Puts each species into a list of breeds
        private Dictionary<string, List<string>> speciesBreed = new Dictionary<string, List<string>>()
        {
            {"Dairy Cow", new List<string>() {"Holstein-Friesian", "Jersey"} },
            {"Beef Cow", new List<string>() {"Angus", "Hereford"} },
            {"Sheep", new List<string>() { "Merino", "Border Leicester", "Charollais", "Awassi" } },
            {"Pig", new List<string>() {"Large White", "Duroc", "Landrace"} },
            {"Chicken", new List<string>() { "Broiler", "Leghorn", "Rhode Island Red" } }
        };

        //Puts each species to its list of food types
        private Dictionary<string, List<string>> speciesFoods = new Dictionary<string, List<string>>()
        {
            {"Dairy Cow", new List<string>() {"Grass Silage", "Maize Silage", "Grain Mix", "Alfalfa Hay" } },
            {"Beef Cow", new List<string>() {"Pasture Grass", "Grass Hay", "Corn Silage", "Grain Mix"} },
            {"Sheep", new List<string>() {"Pasture Grass", "Grass Hay", "Grain Supplement", "Silage"} },
            {"Pig", new List<string>() {"Corn", "Soybean Meal", "Barley", "Wheat",} },
            {"Chicken", new List<string>() {"Starter/Layer Feed", "Corn", "Oats", "Grain Mix"} }
        };

        //Puts each food type to its preset cost per gram using constants
        private Dictionary<string, double> feedCosts = new Dictionary<string, double>()
        {
            { "Grass Silage", GRASS_SILAGE },
            { "Maize Silage", MAIZE_SILAGE },
            { "Grain Mix", GRAIN_MIX },
            { "Alfalfa Hay", ALFALFA_HAY },
            { "Pasture Grass", PASTURE_GRASS },
            { "Grass Hay", GRASS_HAY },
            { "Corn Silage", CORN_SILAGE },
            { "Grain Supplement", GRAIN_SUPPLEMENT },
            { "Silage", SILAGE },
            { "Corn", CORN },
            { "Soybean Meal", SOYBEAN_MEAL },
            { "Barley", BARLEY },
            { "Wheat", WHEAT },
            { "Starter/Layer Feed", STARTER_LAYER_FEED },
            { "Oats", OATS }
        };

        //Puts each breed to its healthy weekly food range in grams (min, max)
        private Dictionary<string, (double min, double max)> feedingRanges = new Dictionary<string, (double, double)>()
        {
            { "Holstein-Friesian", (140000, 182000)},
            { "Jersey", (126000, 157000) },
            { "Angus", (84000, 133000) },
            { "Hereford", (84000, 133000)},
            { "Merino", (7000, 10500) },
            { "Border Leicester", (7000, 11000) },
            { "Charollais", (7000, 10000)},
            { "Awassi", (7000, 11200) },
            { "Large White", (14000, 24500)},
            { "Duroc", (14000, 24500) },
            { "Landrace", (14000, 24500)},
            { "Broiler", (1050, 1750) },
            { "Leghorn", (490, 630)},
            { "Rhode Island Red", (560, 840)}

        };

        //  each breed to its undereating and overeating consequence messages
        private Dictionary<string, (string under, string over)> consequences = new Dictionary<string, (string, string)>()
        {
            { "Holstein-Friesian", ("Reduced milk production, weight loss, risk of ketosis, reproductive problems.",
                "Obesity, fat deposits in udder reducing milk production, digestive disorders.")
            },
            { "Jersey", ("Reduced milk production, weight loss, poor body condition, delayed return to heat.",
                "Prone to obesity, fat deposits in udder, reduced future milk production.")
            },
            { "Angus", ("Weight loss, weakened immunity, delayed rebreeding, reduced calf birth weights.",
                "Obesity, excess internal fat causing calving difficulty, ruminal acidosis.")
            },
            { "Hereford", ("Weight loss, poor body condition, weakened immunity, reduced reproductive performance.",
                "Obesity, calving difficulty, ruminal acidosis, wasted feed costs.")
            },
            { "Merino", ("Reduced wool growth, weight loss, lower lamb survival rates, increased parasite susceptibility.",
                "Rumen acidosis, fatty liver syndrome, excessive weight gain reducing mobility.")
            },
            { "Border Leicester", ("Reduced wool and meat production, weight loss, poor lamb survival rates.",
                "Rumen acidosis, fatty liver syndrome, reduced grazing efficiency.")
            },
            { "Charollais", ("Reduced meat production, weight loss, poor body condition, lower lamb survival.",
                "Rumen acidosis, obesity, reduced mobility and grazing efficiency.")
            },
            { "Awassi", ("Reduced milk and wool production, weight loss, poor reproductive performance.",
                "Rumen acidosis, fatty liver, obesity reducing milk yield.")
            },
            { "Large White", ("Stunted growth, reduced daily weight gain, weakened immune system.",
                "Obesity reducing meat quality, constipation, digestive problems.")
            },
            { "Duroc", ("Stunted growth, reduced weight gain, increased disease susceptibility.",
                "Excess fat reducing carcass quality, digestive disorders, wasted feed costs.")
            },
            { "Landrace", ("Stunted growth, low birth weight in piglets, weakened immunity.",
                "Obesity, congested udders reducing milk yield, constipation.")
            },
            { "Broiler", ("Stunted growth, slower time to market weight, weakened immune system.",
                "Excess fat in carcass reducing meat quality, lethargy, increased disease susceptibility.")
            },
            { "Leghorn", ("Weight loss, decline in egg production, pale combs and wattles.",
                "Obesity, reduced egg production, lethargy, increased disease susceptibility.")
            },
            { "Rhode Island Red", ("Weight loss, reduced egg production, poor feather quality, weakened immunity.",
                "Obesity, reduced egg production, lethargy, wasted feed costs.")
            }
        };

        //Constructor
        public AppManager()
		{


		}

        //Methods

        // Adds a new Animal object to the animals list
        public void AddAnimal(Animal animal)
        {
            animals.Add(animal);

            // Check if this animal has the highest cost so far
            if (highestCostAnimal == null || animal.TotalWeeklyCost() > highestCostAnimal.TotalWeeklyCost())
            {
                highestCostAnimal = animal;
            }

            // Check if this animal has the highest food consumption so far
            if (highestConsumptionAnimal == null || animal.TotalWeeklyFood() > highestConsumptionAnimal.TotalWeeklyFood())
            {
                highestConsumptionAnimal = animal;
            }
        }

        // Returns the list of breeds for a given species
        public List<string> GetBreeds(string species)
        {
            return speciesBreed[species];
        }

        // Returns the list of food types for a given species
        public List<string> GetFoods(string species)
        {
            return speciesFoods[species];
        }

        // Looks up and returns the preset cost per gram for a given food type
        public double GetCostPerGram(string foods)
        {
            return feedCosts[foods];
        }

        // Looks up and returns the (min, max) feeding range in grams for a given breed
        public (double min, double max) GetFeedingRangeByBreed(string breed)
        {
            return feedingRanges[breed];
        }

        // Returns the consequence message for a given breed and status
        public string GetConsequence(string breed, string status)
        {
            if (status == "Undereating")
            {
                return consequences[breed].under;
            }
            else if (status == "Overeating")
            {
                return consequences[breed].over;
            }
            return "";
        }

        // Adds up the weekly cost of every animal and returns the grand total
        public double CalculateTotalFarmCost()
        {
            double total = 0;
            //loops through every animal in the main animals list
            foreach (Animal animal in animals)
            {
                total += animal.TotalWeeklyCost();
            }
            return total;
        }

        //creates list of undereating animals
        public List<Animal> GetAnimalsUndereating()
        {
            List<Animal> undereating = new List<Animal>();
            foreach (Animal animal in animals)
            {
                var (min, max) = GetFeedingRangeByBreed(animal.GetBreed());
                if (animal.CheckFeedingStatus(min, max) == "Undereating")
                {
                    undereating.Add(animal);
                }
            }
            return undereating;
        }

        //Creates list of overeating animals
        public List<Animal> GetAnimalsOvereating()
        {
            List<Animal> overeating = new List<Animal>();
            foreach (Animal animal in animals)
            {
                var (min, max) = GetFeedingRangeByBreed(animal.GetBreed());
                if (animal.CheckFeedingStatus(min, max) == "Overeating")
                {
                    overeating.Add(animal);
                }
            }
            return overeating;
        }

        // Returns the animal with the highest weekly cost
        public Animal GetHighestCostAnimal()
        {
            return highestCostAnimal;
        }

        // Returns the animal with the highest weekly food consumption
        public Animal GetHighestConsumptionAnimal()
        {
            return highestConsumptionAnimal;
        }

        // Counts and displays the number of animals per species and per breed
        public void CountAnimalsPerSpecies()
        {
            //create an empty dictionary to count how many animals per species and per breed
            Dictionary<string, int> speciesCount = new Dictionary<string, int>();
            Dictionary<string, int> breedCount = new Dictionary<string, int>();

            foreach (Animal animal in animals)
            {
                // Check if this species has already been added to the dictionary
                if (speciesCount.ContainsKey(animal.GetSpecies()))
                {
                    // If yes, add 1 to the existing count for that species (e.g. "Sheep" already exists in the dictionary), add 1 to its count
                    speciesCount[animal.GetSpecies()]++;
                }
                else
                {
                    // If no, add this species to the dictionary and set its count to 1 (e.g. "Chicken" has not been seen before), 
                    speciesCount[animal.GetSpecies()] = 1;
                }

                if (breedCount.ContainsKey(animal.GetBreed()))
                {
                    breedCount[animal.GetBreed()]++;
                }
                else
                {
                    breedCount[animal.GetBreed()] = 1;
                }
            }

            Console.WriteLine("\nAnimals Per Species:");
            foreach (var entry in speciesCount)
            {
                // entry.Key is the species name, entry.Value is the count
                Console.WriteLine($"{entry.Key}: {entry.Value}");
            }

            Console.WriteLine("\nAnimals Per Breed:");
            foreach (var entry in breedCount)
            {
                // entry.Key is the breed name, entry.Value is the count
                Console.WriteLine($"{entry.Key}: {entry.Value}");
            }
        }

        // Displays the full farm summary when user stops adding animals
        public void FinalFarmSummary()
        {
            Console.WriteLine("--------------------FARM SUMMARY--------------------");

            CountAnimalsPerSpecies();

            Console.WriteLine("\nAnimal Overview:\n");
            foreach (Animal animal in animals)
            {
                // Print a short summary for each animal - ID, breed, total food, total cost
                Console.WriteLine($"Animal Name: {animal.GetAnimalID()}\nAnimal Breed: {animal.GetBreed()}\nAnimal Weekly Food Consumption: {animal.TotalWeeklyFood():F0}g\nAnimal Weekly Food Cost: ${animal.TotalWeeklyCost():F2}\n");
                Console.WriteLine("----------------------------------------------");
            }

            //Total farm cost, highest feeding costs and highest animal consumption being returned to user

            Console.WriteLine($"Total Weekly Cost to Feed All Animals: ${CalculateTotalFarmCost()}");
            Console.WriteLine($"\nHighest Feeding Cost: {GetHighestCostAnimal().GetAnimalID()} ({GetHighestCostAnimal().GetBreed()}) - ${GetHighestCostAnimal().TotalWeeklyCost()} per week\n");
            Console.WriteLine($"Highest Consumption: {GetHighestConsumptionAnimal().GetAnimalID()} ({GetHighestConsumptionAnimal().GetBreed()}) - {GetHighestConsumptionAnimal().TotalWeeklyFood()}g per week\n");

            //Returns a list of all undereating animals
            List<Animal> undereating = GetAnimalsUndereating();

            Console.WriteLine("\n------------------------------------------------------------");

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Animals Undereating:");
            Console.ForegroundColor = ConsoleColor.White;

            if (undereating.Count == 0)
            {
                Console.WriteLine("  None");
            }
            else
            {
                foreach (Animal animal in undereating)
                {

                    Console.WriteLine($"  {animal.GetAnimalID()} ({animal.GetBreed()}) - {animal.TotalWeeklyFood():F0}g/week");

                }
            }


            //Returns a list of all overeating animals
            List<Animal> overeating = GetAnimalsOvereating();

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\nAnimals Overeating:");
            Console.ForegroundColor = ConsoleColor.White;

            if (overeating.Count == 0)
            {
                Console.WriteLine("  None");
            }
            else
            {
                foreach (Animal animal in overeating)
                {

                    Console.WriteLine($"  {animal.GetAnimalID()} ({animal.GetBreed()}) - {animal.TotalWeeklyFood():F0}g/week");

                }
            }

        }
    }
}
