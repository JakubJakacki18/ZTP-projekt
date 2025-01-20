using System;
using System.Collections.Generic;
using ZTP_projekt.Model;

namespace ZTP_projekt.Model
{
    // Klasa buildera do tworzenia obiektów Recipe.
    internal class RecipeBuilder
    {
        private int id;
        private string name;
        private List<Ingredient> ingredients = new List<Ingredient>();
        private int calories;

        public RecipeBuilder SetId(int id)
        {
            this.id = id;
            return this;
        }

        public RecipeBuilder SetName(string name)
        {
            this.name = name;
            return this;
        }

        public RecipeBuilder AddIngredient(Ingredient ingredient)
        {
            this.ingredients.Add(ingredient);
            return this;
        }

        public RecipeBuilder AddIngredients(IEnumerable<Ingredient> ingredients)
        {
            this.ingredients.AddRange(ingredients);
            return this;
        }

        public RecipeBuilder SetCalories(int calories)
        {
            this.calories = calories;
            return this;
        }

        // Tworzy obiekt Recipe na podstawie zdefiniowanych właściwości.
        public Recipe Build()
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new InvalidOperationException("Recipe must have a name.");
            }

            if (!ingredients.Any())
            {
                throw new InvalidOperationException("Recipe must have at least one ingredient.");
            }

            return new Recipe(id, name, ingredients, calories);
        }
    }
}
