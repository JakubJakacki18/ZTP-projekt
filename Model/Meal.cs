using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZTP_projekt.Data.Enum;
using ZTP_projekt.Interface;

namespace ZTP_projekt.Model
{
internal class Meal : IMealComposite
    {
        public int Id { get; set; }             
        public string Name { get; set; }     
        public List<Recipe> Recipes { get; set; }  
        public CategoryMealEnum CategoryMeal { get; set; }

        public Meal(int id, string name, CategoryMealEnum categoryMeal)
        {
            Id = id;
            Name = name;
            CategoryMeal = categoryMeal;
            Recipes = new List<Recipe>(); 
        }

        public void AddRecipe(Recipe recipe)
        {
            Recipes.Add(recipe);
        }
        public void Display()
		{
			throw new NotImplementedException();
		}
	}
}
