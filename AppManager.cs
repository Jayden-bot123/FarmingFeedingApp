using System;
namespace FarmingFeedingApp
{
	public class AppManager
	{
		//Constants - cost per gram of each food
		private const double GRASS_SILAGE = 0.0003;
        private const double MAIZE_SILAGE = 0.0004;
        private const double GRAIN_MIX = 0.0006;
        private const double ALFALFA_HAY = 0.0005;
        private const double PASTURE_GRASS = 0.0002;
        private const double GRASS_HAY = 0.00035;
        private const double CORN_SILAGE = 0.00038;
        private const double GRAIN_SUPPLEMENT = 0.00055;
        private const double SILAGE = 0.0003;
        private const double CORN = 0.00028;
        private const double SOYBEAN_MEAL = 0.00045;
        private const double BARLEY = 0.00032;
        private const double WHEAT = 0.0003;
        private const double STARTER_LAYER_FEED = 0.0004;
        private const double OATS = 0.00031;

        //Global lists and dictionaries

        // Stores all Animal objects added during the session
        private List<Animal> animals = new List<Animal>();

        //Puts each species into a list of breeds
        private Dictionary<string, List<string>> speciesBreed = new Dictionary<string, List<string>>()
        {
            {"Dairy Cow", new List<string>() {"Holestein-Friesian", "Jersey"} },
            {"Beef Cow", new List<string>() {"Angus", "Hereford"} },
            {"Sheep", new List<string>() { "Merino", "Border Leicester", "Charollais", "Awassi" } },
            {"Pig", new List<string>() {"Large White", "Duroc", "Landrace"} },
            {"Chicken", new List<string>() { "Broiler", "Leghorn", "Rhode Island Red" } }
        };

        //Puts each species to its list of food types
        private Dictionary<string, List<string>> speciesFoods = new Dictionary<string, List<string>>()
        {
            {"Dairy Cow", new List<string>() {"Grass Silage", "Maiza Silage", "Grain Mix", "Alfalfa Hay" } },
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
            { "Holstein-Friesian", (126000, 147000) },
            { "Jersey", (105000, 126000) },
            { "Angus", (77000,  98000) },
            { "Hereford", (84000,  105000) },
            { "Merino", (4900,   6300) },
            { "Border Leicester", (6300,   8400) },
            { "Charollais", (5600,   7700)   },
            { "Awassi", (5600,   7000) },
            { "Large White", (14000,  19600)  },
            { "Duroc", (15400,  20300) },
            { "Landrace", (13300,  18900) },
            { "Broiler", (735, 1050) },
            { "Leghorn", (490, 630) },
            { "Rhode Island Red", (805, 980) }

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

        public AppManager()
		{


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
    }
}

