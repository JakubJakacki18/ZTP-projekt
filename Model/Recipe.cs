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
        public object Clone()
        {
            Recipe clonedRecipe = (Recipe)this.MemberwiseClone();

            clonedRecipe.Ingredients = new List<Ingredient>();
            foreach (var ingredient in this.Ingredients)
            {
                clonedRecipe.Ingredients.Add(new Ingredient(ingredient.Id, ingredient.Name, ingredient.Quantity));
            }

            return clonedRecipe;
        }
    }
}
