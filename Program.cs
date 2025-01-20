using System;
using System.Collections.Generic;
using System.Linq;
using ZTP_projekt.Model;
using ZTP_projekt.Data.Enum;
using ZTP_projekt.Calculate;
using System.Threading.Channels;

namespace ZTP_projekt
{
    class Program
    {
        private static List<Recipe> recipes = new List<Recipe>();
        private static List<Meal> meals = new List<Meal>();
        private static int currentMealId = 1;
		private static DateTime startDate = DateTime.Now;
		private static MealPlan mealPlan = new MealPlan(startDate, startDate.AddDays(6));

		static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("Recipe and Meal Builder Program\n");
                List<Action> tests = [Test1, Test2, Test3,Test4];
				foreach (var test in tests)
				{
					ExecuteWithReadKey(test);
				}





				
                // Test 3: Serialize/Deserialize Recipes
                // MealPlanHistory.Instance.AddMealPlan(mealPlan);
                Console.WriteLine("\nSerializing Recipes to Json...");
                var jsonConverter = new JsonConverter();
				//jsonConverter.Export("./plik.json");
			 //   MealPlanHistory.Instance.ClearHistory();
                //jsonConverter.Import("./plik.json");
				//MealPlanHistory.Instance.ShowMealPlans();
                Console.WriteLine("\nDisplaying Imported Meal Plans:");

                MealPlanHistory.Instance.ShowMealPlan(0);

                // Meal.DisplayMeals(meals);
                // Test 5: Summarize Calories
				MealPlanHistory.Instance.ShowMealPlan(0);
                Console.WriteLine("\nSummary of Calories in Meal Plan:");
                SummaryObserver summaryObserver = new SummaryObserver(mealPlan, new CalculateOverallCaloriesStrategy());
                summaryObserver.DisplayCalories();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }
		public static void ExecuteWithReadKey(Action method)
		{
			method();
            Console.WriteLine("\nPress button to continue\n");
            Console.ReadKey();
		}

		static void Test1() 
        {
			// Test 1: Adding Recipes
			Console.WriteLine("\n\n\nTest 1: Adding Recipes\n\n\n");
			AddRecipe("Pasta Carbonara", new List<Ingredient>
				{
					new Ingredient(1, "Pasta", 200, CategoryIngredientEnum.BAKERY),
					new Ingredient(2, "Eggs", 2, CategoryIngredientEnum.DAIRY),
					new Ingredient(3, "Parmesan Cheese", 50, CategoryIngredientEnum.DAIRY),
					new Ingredient(4, "Pancetta", 100, CategoryIngredientEnum.MEAT)
				}, 800);

			AddRecipe("Salad Nicoise", new List<Ingredient>
				{
					new Ingredient(1, "Lettuce", 100, CategoryIngredientEnum.VEGETABLE),
					new Ingredient(2, "Tuna", 150, CategoryIngredientEnum.MEAT),
					new Ingredient(3, "Eggs", 2, CategoryIngredientEnum.DAIRY),
					new Ingredient(4, "Olives", 50, CategoryIngredientEnum.VEGETABLE),
					new Ingredient(5, "Potatoes", 200, CategoryIngredientEnum.VEGETABLE)
				}, 350);

			AddRecipe("Chocolate Cake", new List<Ingredient>
				{
					new Ingredient(1, "Flour", 250, CategoryIngredientEnum.BAKERY),
					new Ingredient(2, "Cocoa Powder", 50, CategoryIngredientEnum.SPICE),
					new Ingredient(3, "Sugar", 200, CategoryIngredientEnum.OTHER),
					new Ingredient(4, "Butter", 100, CategoryIngredientEnum.DAIRY),
					new Ingredient(5, "Eggs", 3, CategoryIngredientEnum.DAIRY)
				}, 1200);

			Console.WriteLine("Recipes Added:");
			Recipe.DisplayRecipes(recipes);
		}
        static void Test2() 
        {
			// Test 2: Adding Meal Plan
			Console.WriteLine("\n\n\nTest 2: Adding Meal Plan and ShoppingListObserver\n\n\n");
			var startDate = DateTime.Now;
			


			ShoppingList shoppingList = new ShoppingList(1);
			ShoppingListObserver shoppingListObserver = new ShoppingListObserver(mealPlan, shoppingList);

			Meal meal1 = new Meal(currentMealId++, "Italian Dinner", CategoryMealEnum.DINNER);
			meal1.AddRecipe(recipes[0]);
			meal1.AddRecipe(recipes[1]);
			meals.Add(meal1);

			MealDay mealDay = new MealDay(1, DateTime.Now, new List<Meal> { meal1 });
			mealPlan.AddMealDay(mealDay);

			Console.WriteLine("\nDisplaying Shopping List After Adding Meals:");
			shoppingListObserver.Update(); // Wywołujemy aktualizację
			shoppingList.ShowShoppingList();



			MealPlanHistory.Instance.AddMealPlan(mealPlan);
			MealPlanHistory.Instance.ShowMealPlans();
		}
		static void Test3()
		{
			// Test 3: Editing Recipe
			Console.WriteLine("\n\n\n Test 3: Editing Recipe\n\n\n");
			Console.WriteLine("\nEditing Recipe: Pasta Carbonara using Clone");
			EditRecipeUsingClone(1, "Whole Grain Pasta Carbonara", new List<Ingredient>
				{
					new Ingredient(1, "Whole Grain Pasta", 200, CategoryIngredientEnum.BAKERY),
					new Ingredient(2, "Eggs", 2, CategoryIngredientEnum.DAIRY),
					new Ingredient(3, "Parmesan Cheese", 50, CategoryIngredientEnum.DAIRY),
					new Ingredient(4, "Bacon", 100, CategoryIngredientEnum.MEAT)
				}, 800);

			Console.WriteLine("\nRecipes After Editing:");
			Recipe.DisplayRecipes(recipes);

			Console.WriteLine("\nMeals After Editing Recipe:");
			// Meal.DisplayMeals(meals);
		}
		static void Test4()
		{
			// Test 4: Summarize Calories
			Console.WriteLine("\n\n\nTest 4: Summarize Calories\n\n\n");
			MealPlanHistory.Instance.ShowMealPlan(0);
			Console.WriteLine("\nSummary of Calories in Meal Plan:");
			SummaryObserver summaryObserver = new SummaryObserver(mealPlan, new CalculateOverallCaloriesStrategy());
			summaryObserver.DisplayCalories();
			summaryObserver.ChangeCalculateStrategy(new CalculateMeanPerDayCaloriesStrategy());
			summaryObserver.DisplayCalories();

			
			MealPlanHistory.Instance.GetMealPlan(0)?.AddMealDay(MealPlanHistory.Instance.GetMealPlan(0)?.MealDays[0] ?? new MealDay(1,DateTime.Now,[]));

			summaryObserver.DisplayCalories();
		}



		static void AddRecipe(string name, List<Ingredient> ingredients, int calories)
        {
            Recipe.AddRecipe(recipes, name, ingredients, calories);
        }

        static void EditRecipeUsingClone(int id, string newName, List<Ingredient> newIngredients, int newCalories)
        {
            var recipeToEdit = recipes.FirstOrDefault(r => r.Id == id);
            if (recipeToEdit == null)
            {
                Console.WriteLine($"Recipe with ID {id} not found.");
                return;
            }

            var clonedRecipe = (Recipe)recipeToEdit.Clone();
            clonedRecipe.Name = newName;
            clonedRecipe.Ingredients = newIngredients;
            clonedRecipe.SetCalories(newCalories);

            var index = recipes.IndexOf(recipeToEdit);
            recipes[index] = clonedRecipe;

            Console.WriteLine($"Recipe '{recipeToEdit.Name}' updated successfully to '{clonedRecipe.Name}' using clone.");

            foreach (var meal in meals)
            {
                var recipeInMeal = meal.Recipes.FirstOrDefault(r => r.Id == id);
                if (recipeInMeal != null)
                {
                    var recipeIndex = meal.Recipes.IndexOf(recipeInMeal);
                    meal.Recipes[recipeIndex] = clonedRecipe;
                }
            }
        }
    }
}
