using System;
using System.Collections.Generic;
using ZTP_projekt.Data.Enum;
using ZTP_projekt.Interface;
using ICloneable = ZTP_projekt.Interface.ICloneable;

namespace ZTP_projekt.Model
{
internal class Meal : IMealComposite,  ICloneable
	{
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public List<Recipe> Recipes { get; set; } = [];  
        public CategoryMealEnum CategoryMeal { get; set; }

        public Meal(int id, string name, CategoryMealEnum categoryMeal)
        {
            Id = id;
            Name = name;
            CategoryMeal = categoryMeal;
            Recipes = new List<Recipe>();
        }
		[Obsolete("Do not use the parameterless constructor. Meal(int id, string name, CategoryMealEnum categoryMeal)", true)]
		public Meal() { }

		public void AddRecipe(Recipe recipe)
        {
            Recipes.Add(recipe);
        }

        public void Display()
        {
            Console.WriteLine($"\nMeal ID: {Id}");
            Console.WriteLine($"Meal Name: {Name}");
            Console.WriteLine($"Category: {CategoryMeal}");
            Console.WriteLine("Recipes:");
            foreach (var recipe in Recipes)
            {
                Console.WriteLine($"- {recipe.Name}");
            }
        }

        public object Clone()
        {
            Meal clonedMeal = (Meal)this.MemberwiseClone();
            clonedMeal.Recipes = new List<Recipe>();

            foreach (var recipe in this.Recipes)
            {
                clonedMeal.Recipes.Add((Recipe)recipe.Clone());
            }

            return clonedMeal;
        }

        public static void DisplayMeals(List<Meal> meals)
        {
            if (meals == null || meals.Count == 0)
            {
                Console.WriteLine("No meals available.");
                return;
            }

            Console.WriteLine("Meals:");
            foreach (var meal in meals)
            {
                Console.WriteLine($"Meal ID: {meal.Id}");
                Console.WriteLine($"Name: {meal.Name}");
                Console.WriteLine($"Category: {meal.CategoryMeal}");
                Console.WriteLine("Recipes:");
                foreach (var recipe in meal.Recipes)
                {
                    Console.WriteLine($"- {recipe.Name}");
                }
                Console.WriteLine();
            }
        }

    }
}
