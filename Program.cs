using System;
using System.Collections.Generic;
using System.Linq;
using ZTP_projekt.Data.Enum;
using ZTP_projekt.Model;

namespace ZTP_projekt
{
    class Program
    {
        private static int currentRecipeId = 1;
        private static int currentMealId = 1;
        private static List<Recipe> recipes = new List<Recipe>();
        private static List<Meal> meals = new List<Meal>();

        static void Main(string[] args)
        {
            Console.WriteLine("Recipe and Meal Builder Program\n");

            // Dodawanie przepisów
            AddRecipe("Pasta Carbonara", new List<Ingredient>
            {
                new Ingredient(1, "Pasta", 200),
                new Ingredient(2, "Eggs", 2),
                new Ingredient(3, "Parmesan Cheese", 50),
                new Ingredient(4, "Pancetta", 100)
            }, 800);

            AddRecipe("Salad Nicoise", new List<Ingredient>
            {
                new Ingredient(1, "Lettuce", 100),
                new Ingredient(2, "Tuna", 150),
                new Ingredient(3, "Eggs", 2),
                new Ingredient(4, "Olives", 50),
                new Ingredient(5, "Potatoes", 200)
            }, 350);

            AddRecipe("Chocolate Cake", new List<Ingredient>
            {
                new Ingredient(1, "Flour", 250),
                new Ingredient(2, "Cocoa Powder", 50),
                new Ingredient(3, "Sugar", 200),
                new Ingredient(4, "Butter", 100),
                new Ingredient(5, "Eggs", 3)
            }, 1200);
            Recipe.DisplayRecipes(recipes);
            // Tworzenie posiłków
            Meal meal1 = new Meal(currentMealId++, "Italian Dinner", CategoryMealEnum.DINNER);
            meal1.AddRecipe(recipes[0]);
            meal1.AddRecipe(recipes[1]);
            meals.Add(meal1);

            MealDay mealDay = new MealDay(1, DateTime.Now, new List<Meal> { meal1 });
            DateOnly startDate = DateOnly.FromDateTime(DateTime.Now);
            MealPlan mealPlan = new MealPlan(startDate, startDate.AddDays(6));

            mealPlan.AddMealDay(mealDay);
            MealPlanHistory.Instance.AddMealPlan(mealPlan);

            Console.WriteLine("\nSerializing Recipes to Json...");
            var jsonConverter = new JsonConverter();
            jsonConverter.Export("./plik.json");
            MealPlanHistory.Instance.ClearHistory();
            jsonConverter.Import("./plik.json");

            Meal meal2 = new Meal(currentMealId++, "Sweet Treats", CategoryMealEnum.DESSERT);
            meal2.AddRecipe(recipes[2]);
            meals.Add(meal2);

            // Wypisywanie posiłków przy użyciu Meal.DisplayMeals()
            Console.WriteLine("\nMeals After Adding Recipes:");
            Meal.DisplayMeals(meals);

            // Edycja przepisu (zmiana składu Pasta Carbonara)
            Console.WriteLine("\nEditing Recipe: Pasta Carbonara using Clone");
            EditRecipeUsingClone(1, "Whole Grain Pasta Carbonara", new List<Ingredient>
            {
                new Ingredient(1, "Whole Grain Pasta", 200),
                new Ingredient(2, "Eggs", 2),
                new Ingredient(3, "Parmesan Cheese", 50),
                new Ingredient(4, "Bacon", 120)
            }, 850);
            Recipe.DisplayRecipes(recipes);
            Console.WriteLine("\nMeals After Editing Recipe:");
            Meal.DisplayMeals(meals); // Wypisanie posiłków po edycji przepisu
        }

        static void AddRecipe(string name, List<Ingredient> ingredients, int calories)
        {
            var recipeBuilder = new RecipeBuilder();
            var recipe = recipeBuilder
                .SetId(currentRecipeId++)
                .SetName(name)
                .AddIngredients(ingredients)
                .SetCalories(calories)
                .Build();

            recipes.Add(recipe);
            Console.WriteLine($"Added recipe: {recipe.Name} (ID: {recipe.Id})");
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
