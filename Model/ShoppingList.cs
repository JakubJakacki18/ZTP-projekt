using System;
using System.Collections.Generic;
using ZTP_projekt.Data.Enum;

namespace ZTP_projekt.Model
{
    // Klasa reprezentująca listę zakupów z podziałem na kategorie składników
    internal class ShoppingList
    {
        public int Id { get; set; }
        public Dictionary<CategoryIngredientEnum, List<Ingredient>> Ingredients { get; private set; }

        public ShoppingList(int id)
        {
            Id = id;
            Ingredients = new Dictionary<CategoryIngredientEnum, List<Ingredient>>();
        }

        // Dodaje składniki do listy zakupów
        public void AddIngredients(Dictionary<CategoryIngredientEnum, List<Ingredient>> ingredientsToAdd)
        {
            foreach (var category in ingredientsToAdd)
            {
                if (!Ingredients.ContainsKey(category.Key))
                {
                    Ingredients[category.Key] = new List<Ingredient>();
                }

                foreach (var ingredient in category.Value)
                {
                    var existingIngredient = Ingredients[category.Key]
                        .FirstOrDefault(i => i.Name == ingredient.Name);

                    if (existingIngredient != null)
                    {
                        existingIngredient.Quantity += ingredient.Quantity;
                    }
                    else
                    {
                        Ingredients[category.Key].Add((Ingredient)ingredient.Clone());
                    }
                }
            }
        }

        // Wyświetla szczegóły listy zakupów
        public void ShowShoppingList()
        {
            Console.WriteLine("\nShopping List:");
            foreach (var category in Ingredients)
            {
                Console.WriteLine($"Category: {category.Key}");
                foreach (var ingredient in category.Value)
                {
                    Console.WriteLine($" - {ingredient.Name}: {ingredient.Quantity}g");
                }
            }
        }
    }
}
