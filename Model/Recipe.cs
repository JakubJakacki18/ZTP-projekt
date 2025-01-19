using System;
using System.Collections.Generic;
using ZTP_projekt.Interface;
using ICloneable = ZTP_projekt.Interface.ICloneable;

namespace ZTP_projekt.Model
{
    internal class Recipe : IMealComposite, ICloneable
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Ingredient> Ingredients { get; set; }
        public int Calories { get; private set; }
        private static List<Recipe> recipes = new List<Recipe>();
        private static int currentId = 1;

        public Recipe(int id, string name, List<Ingredient> ingredients, int calories)
        {
            Id = id;
            Name = name;
            Ingredients = new List<Ingredient>();
            Calories = calories;
        }

        public void AddIngredient(Ingredient ingredient, int calories)
        {
            Ingredients.Add(ingredient);
        }

        public void ChangeIngredient(int numberOfIngredient, Ingredient ingredient, int calories)
        {
            throw new NotImplementedException();
        }

        public void RemoveIngredient(int numberOfIngredient)
        {
            throw new NotImplementedException();
        }

        public void Display()
        {
            throw new NotImplementedException();
        }

        public void SetCalories(int calories)
        {
            Calories = calories;
        }

        public object Clone()
        {
            Recipe clonedRecipe = (Recipe)this.MemberwiseClone();

            clonedRecipe.Ingredients = new List<Ingredient>();
            foreach (var ingredient in this.Ingredients)
            {
                clonedRecipe.Ingredients.Add(new Ingredient(ingredient.Id, ingredient.Name, ingredient.Quantity));
            }
            clonedRecipe.Calories = this.Calories;

            return clonedRecipe;
        }
        public static void AddRecipe(string name, List<Ingredient> ingredients, int calories)
        {
            var recipeBuilder = new RecipeBuilder();
            var recipe = recipeBuilder
                .SetId(currentId++)
                .SetName(name)
                .AddIngredients(ingredients)
                .SetCalories(calories)
                .Build();

            recipes.Add(recipe);
        }

        public static void EditRecipeUsingClone(int id, string newName, List<Ingredient> newIngredients, int newCalories)
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

        public static void ListRecipes()
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
    }
}
