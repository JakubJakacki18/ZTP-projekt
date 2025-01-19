using System;
using System.Collections.Generic;
using System.Linq;
using ZTP_projekt.Data.Enum;
using ZTP_projekt.Model;
using ZTP_projekt.Data.Enum;

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
            Meal meal = new Meal(1, "meal jakiś", CategoryMealEnum.LUNCH);
            meal.AddRecipe(recipes[0]);
            MealDay mealDay=new MealDay(1, DateTime.Now, [meal]);
            DateOnly startDate = DateOnly.FromDateTime(DateTime.Now);

			MealPlan mealPlan = new MealPlan(startDate, startDate.AddDays(6));
            mealPlan.AddMealDay(mealDay);
            MealPlanHistory.Instance.AddMealPlan(mealPlan);
			Console.WriteLine("\nSerializing Recipes to Json...");
			var jsonConverter = new JsonConverter();
			jsonConverter.Export("./plik.json");
            MealPlanHistory.Instance.ClearHistory();
            jsonConverter.Import("./plik.json");
			Console.WriteLine("\nEditing Recipe: Pasta Carbonara using Clone");
            EditRecipeUsingClone(1, "Pasta Carbonara (Updated)", new List<Ingredient>
            {
                new Ingredient(1, "Whole Grain Pasta", 200),
                new Ingredient(2, "Eggs", 2),
                new Ingredient(3, "Parmesan Cheese", 50),
                new Ingredient(4, "Bacon", 120)
            }, 850);

            Console.WriteLine("\nRecipes After Editing:");
            ListRecipes();

            Console.WriteLine("\nCreating Meals and Adding Recipes:");

            CreateMeal("Italian Dinner", CategoryMealEnum.DINNER);
            AddRecipeToMeal(1, "Pasta Carbonara");
            AddRecipeToMeal(1, "Salad Nicoise");

            CreateMeal("Sweet Treats", CategoryMealEnum.DESSERT);
            AddRecipeToMeal(2, "Chocolate Cake");

            Console.WriteLine("\nMeals After Adding Recipes:");
            ListMeals();
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
        }

        static void ListRecipes()
        {
            if (!recipes.Any())
            {
                Console.WriteLine("No recipes available.");
                return;
            }

            foreach (var recipe in recipes)
            {
                Console.WriteLine($"\nRecipe ID: {recipe.Id}");
                Console.WriteLine($"Recipe Name: {recipe.Name}");
                Console.WriteLine("Ingredients:");
                foreach (var ingredient in recipe.Ingredients)
                {
                    Console.WriteLine($"- {ingredient.Name} ({ingredient.Quantity}g)");
                }
                Console.WriteLine($"Calories: {recipe.Calories}");
            }
        }

        static void CreateMeal(string name, CategoryMealEnum categoryMeal)
        {
            var meal = new Meal(currentMealId++, name, categoryMeal);
            meals.Add(meal);
            Console.WriteLine($"Created meal: {meal.Name} (ID: {meal.Id})");
        }

        static void AddRecipeToMeal(int mealId, string recipeName)
        {
            var meal = meals.FirstOrDefault(m => m.Id == mealId);
            var recipe = recipes.FirstOrDefault(r => r.Name == recipeName);

            if (meal != null && recipe != null)
            {
                var clonedRecipe = (Recipe)recipe.Clone();
                foreach (var ingredient in clonedRecipe.Ingredients)
                {
                    var clonedIngredient = (Ingredient)ingredient.Clone();
                }
                    meal.AddRecipe(clonedRecipe);
                Console.WriteLine($"Added '{clonedRecipe.Name}' to meal '{meal.Name}'.");
            }
            else
            {
                Console.WriteLine($"Error: Meal or Recipe not found. (Meal ID: {mealId}, Recipe: {recipeName})");
            }
        }


        static void ListMeals()
        {
            if (!meals.Any())
            {
                Console.WriteLine("No meals available.");
                return;
            }

            foreach (var meal in meals)
            {
                Console.WriteLine($"\nMeal ID: {meal.Id}");
                Console.WriteLine($"Meal Name: {meal.Name}");
                Console.WriteLine($"Category: {meal.CategoryMeal}");
                Console.WriteLine("Recipes:");
                foreach (var recipe in meal.Recipes)
                {
                    Console.WriteLine($"- {recipe.Name}");
                }
            }
        }
    }
}
