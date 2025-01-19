using System;
using System.Collections.Generic;
using System.Linq;
using ZTP_projekt.Model;

namespace ZTP_projekt
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Recipe Builder Program\n");

            Recipe.AddRecipe("Pasta Carbonara", new List<Ingredient>
            {
                new Ingredient(1, "Pasta", 200),
                new Ingredient(2, "Eggs", 2),
                new Ingredient(3, "Parmesan Cheese", 50),
                new Ingredient(4, "Pancetta", 100)
            }, 800);

            Recipe.AddRecipe("Salad Nicoise", new List<Ingredient>
            {
                new Ingredient(1, "Lettuce", 100),
                new Ingredient(2, "Tuna", 150),
                new Ingredient(3, "Eggs", 2),
                new Ingredient(4, "Olives", 50),
                new Ingredient(5, "Potatoes", 200)
            }, 350);

            Recipe.AddRecipe("Chocolate Cake", new List<Ingredient>
            {
                new Ingredient(1, "Flour", 250),
                new Ingredient(2, "Cocoa Powder", 50),
                new Ingredient(3, "Sugar", 200),
                new Ingredient(4, "Butter", 100),
                new Ingredient(5, "Eggs", 3)
            }, 1200);

            Console.WriteLine("Original Recipes:");
            Recipe.ListRecipes();

            Console.WriteLine("\nEditing Recipe: Pasta Carbonara using Clone");
            Recipe.EditRecipeUsingClone(1, "Pasta Carbonara (Updated)", new List<Ingredient>
            {
                new Ingredient(1, "Whole Grain Pasta", 200),
                new Ingredient(2, "Eggs", 2),
                new Ingredient(3, "Parmesan Cheese", 50),
                new Ingredient(4, "Bacon", 120)
            }, 850);

            Console.WriteLine("\nRecipes After Editing:");
            Recipe.ListRecipes();
        }

        
    }
}
