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
            { "Broiler", (735,    1050) },
            { "Leghorn", (490,    630) },
            { "Rhode Island Red", (805,    840) }
        };

        public AppManager()
		{

		}
	}
}

