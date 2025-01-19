using System;
using ZTP_projekt.Data.Enum;
using ZTP_projekt.Interface;
using ICloneable = ZTP_projekt.Interface.ICloneable;

namespace ZTP_projekt.Model
{
    internal class Ingredient : IMealComposite, ICloneable
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public CategoryIngredientEnum CategoryEnum { get; set; }

        public Ingredient(int id, string name, int quantity, CategoryIngredientEnum categoryEnum)
        {
            Id = id;
            Name = name;
            Quantity = quantity;
            CategoryEnum = categoryEnum;

        }

        public void Display()
        {
                Console.WriteLine($"Ingredient ID: {Id}");
                Console.WriteLine($"Name: {Name}");
                Console.WriteLine($"Quantity: {Quantity}g");
                Console.WriteLine($"Category: {CategoryEnum}");
        }
        public static void DisplayIngredients(List<Ingredient> ingredients)
        {
            if (ingredients == null || ingredients.Count == 0)
            {
                Console.WriteLine("No ingredients available.");
                return;
            }

            Console.WriteLine("\nIngredients:");
            foreach (var ingredient in ingredients)
            {
                ingredient.Display();
                Console.WriteLine();
            }
        }
        // Implementacja metody Clone
        public object Clone()
        {
            return new Ingredient(this.Id, this.Name, this.Quantity, this.CategoryEnum);
        }
    }
}
