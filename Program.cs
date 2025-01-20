using System;
using System.Collections.Generic;
using System.Linq;
using ZTP_projekt.Model;
using ZTP_projekt.Data.Enum;
using ZTP_projekt.Calculate;

namespace ZTP_projekt
{
    class Program
    {
        private static List<Recipe> recipes = new List<Recipe>();
        private static List<Meal> meals = new List<Meal>();
        private static int currentMealId = 1;

        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("Recipe and Meal Builder Program\n");

                // Test 1: Adding Recipes
                Console.WriteLine("Test 1: Adding Recipes\n\n\n");
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

                // Test 2: Adding Meal Plan
				Console.WriteLine("Test 2: Adding Meal Plan and ShoppingListObserver\n\n\n");
                var startDate = DateTime.Now;
				MealPlan mealPlan = new MealPlan(startDate, startDate.AddDays(6));


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
                // Test 4: Editing Recipe
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
