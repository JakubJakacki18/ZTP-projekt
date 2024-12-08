using System;
using System.Collections.Generic;
using ZTP_projekt.Interface;

namespace ZTP_projekt.Model
{
    internal class Recipe : IMealComposite
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Ingredient> Ingredients { get; set; }
        public int Calories { get; private set; }

        public Recipe(int id, string name, List<Ingredient> ingredients, int calories)
        {
            Id = id;
            Name = name;
            Ingredients = new List<Ingredient>();
            Calories = calories;
        }

        public void AddIngredient(Ingredient ingredient)
        {
            Ingredients.Add(ingredient);
        }

        public void Display()
        {
            throw new NotImplementedException();
        }
    }
}
