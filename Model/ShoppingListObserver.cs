using System;
using System.Collections.Generic;
using System.Linq;
using ZTP_projekt.Data.Enum;
using ZTP_projekt.Interface;

namespace ZTP_projekt.Model
{
    internal class ShoppingListObserver : IObserver
    {
        private readonly MealPlan _mealPlan;
        private readonly ShoppingList _shoppingList;

        public ShoppingListObserver(MealPlan mealPlan, ShoppingList shoppingList)
        {
            _mealPlan = mealPlan;
            _shoppingList = shoppingList;
            _mealPlan.Attach(this);
        }

        public void Update()
        {
            UpdateShoppingList();
        }

        private void UpdateShoppingList()
        {
            var ingredientsToAdd = new Dictionary<CategoryIngredientEnum, List<Ingredient>>();

            foreach (var mealDay in _mealPlan.MealDays)
            {
                foreach (var meal in mealDay.Meals)
                {
                    foreach (var recipe in meal.Recipes)
                    {
                        foreach (var ingredient in recipe.Ingredients)
                        {
                            if (!ingredientsToAdd.ContainsKey(ingredient.CategoryEnum))
                            {
                                ingredientsToAdd[ingredient.CategoryEnum] = new List<Ingredient>();
                            }

                            var existingIngredient = ingredientsToAdd[ingredient.CategoryEnum]
                                .FirstOrDefault(i => i.Name == ingredient.Name);

                            if (existingIngredient != null)
                            {
                                existingIngredient.Quantity += ingredient.Quantity;
                            }
                            else
                            {
                                ingredientsToAdd[ingredient.CategoryEnum].Add((Ingredient)ingredient.Clone());
                            }
                        }
                    }
                }
            }

            _shoppingList.AddIngredients(ingredientsToAdd);
        }
    }
}
