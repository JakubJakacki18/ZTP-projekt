using System;
using System.Collections.Generic;
using ZTP_projekt.Model;

namespace ZTP
{
    class Program
    {
        private static int currentId = 1; // Static field to keep track of recipe IDs
        private static List<Recipe> recipes = new List<Recipe>(); // List to store recipes

        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("Welcome to the Recipe Builder!");
                Console.WriteLine("1. Add a new recipe");
                Console.WriteLine("2. List all recipes");
                Console.WriteLine("3. Exit");
                Console.Write("Choose an option: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        AddRecipe();
                        break;
                    case "2":
                        ListRecipes();
                        break;
                    case "3":
                        Console.WriteLine("Goodbye!");
                        return;
                    default:
                        Console.WriteLine("Invalid choice, please try again.");
                        break;
                }
            }
        }

        static void AddRecipe()
        {
            Console.Write("Enter recipe name: ");
            string name = Console.ReadLine();

            Console.WriteLine("Enter ingredients (format: name:quantity (no metric units), separate by commas): ");
            string ingredientInput = Console.ReadLine();
            var ingredients = ParseIngredients(ingredientInput);

            Console.Write("Enter total calories: ");
            if (!int.TryParse(Console.ReadLine(), out int calories))
            {
                Console.WriteLine("Invalid calorie input. Recipe not added.");
                return;
            }

            // Build the recipe
            var recipeBuilder = new RecipeBuilder();
            var recipe = recipeBuilder
                .SetId(currentId++)
                .SetName(name)
                .AddIngredients(ingredients)
                .SetCalories(calories)
                .Build();

            recipes.Add(recipe);
            Console.WriteLine($"Recipe '{recipe.Name}' added successfully!");
        }

        static List<Ingredient> ParseIngredients(string input)
        {
            var ingredients = new List<Ingredient>();
            var parts = input.Split(',');

            foreach (var part in parts)
            {
                var details = part.Split(':');
                if (details.Length != 2 || !int.TryParse(details[1], out int quantity))
                {
                    Console.WriteLine($"Invalid ingredient format: '{part}'. Skipping.");
                    continue;
                }

                var ingredient = new Ingredient(0, details[0].Trim(), quantity);
                ingredients.Add(ingredient);
            }

            return ingredients;
        }

        static void ListRecipes()
        {
            if (recipes.Count == 0)
            {
                Console.WriteLine("No recipes added yet.");
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
    }
}
