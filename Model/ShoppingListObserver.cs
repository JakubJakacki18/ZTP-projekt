using System;
using System.Collections.Generic;
using System.Linq;
using ZTP_projekt.Data.Enum;
using ZTP_projekt.Interface;

namespace ZTP_projekt.Model
{
	//to nie powinien być singleton?
	internal class ShoppingListObserver : IObserver
	{
		private readonly MealPlan _mealPlan;
		public Dictionary<CategoryIngredientEnum, List<Ingredient>> Ingredients { get; private set; } = [];
		public List<string> ShoppingList { get; private set; } = [];

		public ShoppingListObserver(MealPlan mealPlan)
		{
			_mealPlan = mealPlan;
			_mealPlan.Attach(this);
		}

		public void Update()
		{
			GenerateShoppingList();
		}
		public void ClearShoppingList()
		{
			ShoppingList.Clear();
		}
		public void DeleteShoppingListItem(int id)
		{
			try
			{
				ShoppingList.RemoveAt(id);
			}
			catch (ArgumentOutOfRangeException e)
			{
				Console.WriteLine("Index out of range: "+e.Message);
			}
		}

		private void GenerateShoppingList()
		{
			//ShoppingList.Clear();

			var shoppingList = _mealPlan.MealDays
			 .SelectMany(mealDay => mealDay.Meals)
			 .SelectMany(meal => meal.Recipes)
			 .SelectMany(recipe => recipe.Ingredients)
			 .Select(ingredient => ingredient.Name)
			 .Distinct()
			 .ToList();
			// do sprawdzenia działanie except
			ShoppingList.AddRange(shoppingList.Except(ShoppingList));
		}
	}
}
