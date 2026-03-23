namespace FarmingFeedingApp;

class Program
{
    static AppManager appManager = new AppManager();

    // List of species names in numbered order for the menu
    static List<string> SPECIES = new List<string>() { "Dairy Cow", "Beef Cow", "Sheep", "Pig", "Chicken" };


    // List of days used for the 7-day food input loop
    static List<string> DAYS = new List<string>() {"Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "Sunday"};

    //Menu Methods---------------------

    // Displays the species menu and returns the selected species as a string
    static string DisplaySpeciesMenu()
    {
        Console.WriteLine("\nSelect a species:");
        for (int i = 0; i < SPECIES.Count; i++)
        {
            Console.WriteLine($"  {i + 1}. {SPECIES[i]}");
        }
        Console.Write("Enter number: ");

        int choice = Convert.ToInt32(Console.ReadLine());
        return SPECIES[choice - 1];
    }

    //Input Methods--------------------

    // Asks for and returns the animal name/ID
    static string GetAnimalID()
    {
        Console.Write("\nEnter animal name or ID: ");
        return Console.ReadLine().Trim().ToUpper();
    }



    static void Main(string[] args)
    {
        Console.WriteLine();
    } 
}

