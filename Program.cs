using Microsoft.VisualBasic.FileIO;

namespace FarmingFeedingApp;

class Program
{
    // Single instance of AppManager - all animal data flows through this
    static AppManager appManager = new AppManager();

    // List of species names in numbered order for the menu
    static List<string> SPECIES = new List<string>() { "Dairy Cow", "Beef Cow", "Sheep", "Pig", "Chicken" };


    // List of days used for the 7-day food input loop
    static List<string> DAYS = new List<string>() {"Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "Sunday"};

    //Menu Methods

    // Displays the species menu and returns the selected species as a string
    static string DisplaySpeciesMenu()
    {
        Console.WriteLine("\nSelect a species:");
        for (int i = 0; i < SPECIES.Count; i++)
        {
            Console.WriteLine($"  {i + 1}. {SPECIES[i]}");
        }
        Console.Write("\nEnter number:\n");

        int choice = Convert.ToInt32(Console.ReadLine());
        return SPECIES[choice - 1];
    }

    // Displays the breed menu for the selected species and returns the selected breed
    static string DisplayBreedMenu(string species)
    {
        List<string> breeds = appManager.GetBreeds(species);

        Console.WriteLine($"\nSelect a breed for {species}:");
        for (int i = 0; i < breeds.Count; i++)
        {
            Console.WriteLine($"  {i + 1}. {breeds[i]}");
        }
        Console.Write("\nEnter number:\n");

        int choice = Convert.ToInt32(Console.ReadLine());
        return breeds[choice - 1];
    }

    static string DisplayFoodMenu(string species)
    {
        List<string> foods = appManager.GetFoods(species);
        Console.WriteLine($"\nSelect a food type for {species}\n");
        for (int i = 0; i < foods.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {foods[i]}");
        }
        Console.WriteLine("\nEnter number");

        int choice = Convert.ToInt32(Console.ReadLine());
        return foods[choice - 1];
            
    }


    //Input Methods

    // Asks for and returns the animal name/ID
    static string GetAnimalID()
    {
        Console.Write("Enter animal name or ID:\n");
        return Console.ReadLine().ToUpper();
    }

    // Collects 7 days of food intake in grams and returns as an array
    static double[] GetDailyFood()
    {
        double[] dailyFood = new double[7];
        int i = 0;

        Console.WriteLine("\nEnter the amount of food consumed each day (in grams):\n");

        //Loop through each day using global DAYS list
        foreach(var day in DAYS)
        {
            Console.WriteLine($"\nFood consumed on {day} (grams):\n");

            dailyFood[i] = Convert.ToDouble(Console.ReadLine());
            i++;
        }

        return dailyFood;
    }

    // Collects all input, creates and returns a new Animal object
    static Animal CreateAnimal()
    {
        //Get animal name/ID
        string animalID = GetAnimalID();

        //Select species
        string species = DisplaySpeciesMenu();

        //Select breed based on species
        string breeds = DisplayBreedMenu(species);

        //Select food type based on species
        string foods = DisplayFoodMenu(species);

        //Get daily food consumption for 7 days
        double[] dailyfood = GetDailyFood();

        //Look up preset cost per gram for selected food type
        double costPerGram = appManager.GetCostPerGram(foods);

        //Create and return the new Animal object
        return new Animal(animalID, species, breeds, foods, dailyfood, costPerGram);
    }

    static string CheckProceed()
    {
        string proceed;

        while (true)
        {
            Console.WriteLine("\nPress <Enter> to add another devices information or type 'Stop' to quit.\n");
            proceed = Console.ReadLine().ToUpper();

            if (proceed.Equals("") || proceed.Equals("STOP"))
            {
                return proceed;
            }

        }

    }


    // Collects all input, creates and returns a new Animal object

    static void Main(string[] args)
    {
        string proceed = "";
        while (proceed.Equals(""))
        {

            // Create a new animal from user input
            Animal newAnimal = CreateAnimal();

            //Add animal to the manager
            appManager.AddAnimal(newAnimal);

            //Get the feeding range for this breed
            var (min, max) = appManager.GetFeedingRangeByBreed(newAnimal.GetBreed());

            //Check feeding status
            string status = newAnimal.CheckFeedingStatus(min, max);

            // Colour reset before summary so it stays white
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(newAnimal.AnimalSummary());

            // Display the healthy feeding range for this breed
            Console.WriteLine($"Healthy Weekly Range for {newAnimal.GetBreed()}: {min:F0}g - {max:F0}g\n");

            // Display consequence message if under or overeating. Red means overeating, yellow means undereating and green means normal
            if (status == "Undereating")
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"Feeding Status: {status}");
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine($"Warning: {appManager.GetConsequence(newAnimal.GetBreed(), status)}");
            }
            else if (status == "Overeating")
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Feeding Status: {status}");
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine($"Warning: {appManager.GetConsequence(newAnimal.GetBreed(), status)}");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"Feeding Status: {status}");
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("This animal is being fed the correct amount.");
            }

            // Ask if user wants to add another animal
            proceed = CheckProceed();
        }

        // Display the final farm summary
        appManager.FinalFarmSummary();

    

    } 
}

