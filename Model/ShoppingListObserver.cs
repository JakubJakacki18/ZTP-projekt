using System;
using System.Collections.Generic;
using System.Linq;
using ZTP_projekt.Interface;

namespace ZTP_projekt.Model
{
    internal class ShoppingListObserver : IObserver
    {
        private readonly MealPlan _mealPlan;
        public List<string> ShoppingList { get; private set; } = new();

        public ShoppingListObserver(MealPlan mealPlan)
        {
            _mealPlan = mealPlan;
            _mealPlan.Attach(this);
        }

        public void Update()
        {
            GenerateShoppingList();
        }

        private void GenerateShoppingList()
        {
            ShoppingList.Clear();

            foreach (var mealDay in _mealPlan.MealDays)
            {
                foreach (var meal in mealDay.Meals)
                {
                    foreach (var recipe in meal.Recipes)
                    {
                        foreach (var ingredient in recipe.Ingredients)
                        {
                            if (!ShoppingList.Contains(ingredient.Name))
                            {
                                ShoppingList.Add(ingredient.Name);
                            }
                        }
                    }
                }
            }
        }
    }
}
