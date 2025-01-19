using System;
using System.Collections.Generic;
using System.Linq;
using ZTP_projekt.Interface;
using ICloneable = ZTP_projekt.Interface.ICloneable;

namespace ZTP_projekt.Model
{
    internal class Recipe : IMealComposite, ICloneable
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public List<Ingredient> Ingredients { get; set; } = [];
        public int Calories { get; private set; }
		public Recipe(int id, string name, List<Ingredient> ingredients, int calories)
        {
            Id = id;
            Name = name;
            Ingredients = ingredients ?? []; 
            Calories = calories;
        }
        [Obsolete("Do not use the parameterless constructor. Recipe(int id, string name, List<Ingredient> ingredients, int calories)", true)]
        public Recipe() { }

        // Dodanie składnika
        public void AddIngredient(Ingredient ingredient)
        {
            Ingredients.Add(ingredient);
        }

        // Zmiana składnika (niezaimplementowana w tym momencie)
        public void ChangeIngredient(int numberOfIngredient, Ingredient ingredient)
        {
            throw new NotImplementedException();
        }

        // Usunięcie składnika (niezaimplementowana w tym momencie)
        public void RemoveIngredient(int numberOfIngredient)
        {
            throw new NotImplementedException();
        }

        // Metoda wyświetlająca szczegóły przepisu
        public void Display()
        {
            Console.WriteLine($"\nRecipe ID: {Id}");
            Console.WriteLine($"Recipe Name: {Name}");
            Console.WriteLine("Ingredients:");
            foreach (var ingredient in Ingredients)
            {
                Console.WriteLine($"- {ingredient.Name} ({ingredient.Quantity}g)");
            }
            Console.WriteLine($"Calories: {Calories}");
        }

        // Statyczna metoda wyświetlająca listę przepisów
        public static void DisplayRecipes(List<Recipe> recipes)
        {
            if (!recipes.Any())
            {
                Console.WriteLine("No recipes available.");
                return;
            }

            foreach (var recipe in recipes)
            {
                recipe.Display();
            }
        }

        // Ustawienie kalorii przepisu
        public void SetCalories(int calories)
        {
            Calories = calories;
        }

        // Statyczna metoda dodająca przepis
        public static void AddRecipe(List<Recipe> recipes, string name, List<Ingredient> ingredients, int calories)
        {
            // Generowanie ID dla nowego przepisu
            int recipeId = recipes.Count > 0 ? recipes.Max(r => r.Id) + 1 : 1;

            // Tworzenie przepisu za pomocą RecipeBuilder
            var recipe = new RecipeBuilder()
                .SetId(recipeId)
                .SetName(name)
                .AddIngredients(ingredients)
                .SetCalories(calories)
                .Build();

            recipes.Add(recipe);
            Console.WriteLine($"Added recipe: {recipe.Name} (ID: {recipe.Id})");
        }


        // Klonowanie przepisu
        public object Clone()
        {
            Recipe clonedRecipe = (Recipe)this.MemberwiseClone();

            // Klonowanie składników przepisu
            clonedRecipe.Ingredients = new List<Ingredient>();
            foreach (var ingredient in this.Ingredients)
            {
                clonedRecipe.Ingredients.Add(new Ingredient(ingredient.Id, ingredient.Name, ingredient.Quantity, ingredient.CategoryEnum));
            }
            clonedRecipe.Calories = this.Calories;

            return clonedRecipe;
        }
    }
}
