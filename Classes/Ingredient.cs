﻿using System;
using ZTP_projekt.Data.Enum;
using ZTP_projekt.Interface;
using ICloneable = ZTP_projekt.Interface.ICloneable;

namespace ZTP_projekt.Model
{
	internal class Ingredient : IMealComposite, ICloneable
	{
		public int Id { get; set; }
        public string Name { get; set; } = "";
		public int Quantity { get; set; }
        public CategoryIngredientEnum CategoryEnum { get; set; }

        [Obsolete("Do not use the parameterless constructor. Ingredient(int id, string name, int quantity)", true)]
        public Ingredient() { }

        // Konstruktor tworzący nowy składnik z podanymi właściwościami.
        public Ingredient(int id, string name, int quantity, CategoryIngredientEnum categoryEnum)
        {
            Id = id;
            Name = name;
            Quantity = quantity;
            CategoryEnum = categoryEnum;

        }

        // Wyświetla szczegóły składnika w konsoli.
        public void Display()
        {
            Console.WriteLine($"Ingredient ID: {Id}");
            Console.WriteLine($"Name: {Name}");
            Console.WriteLine($"Quantity: {Quantity}g");
            Console.WriteLine($"Category: {CategoryEnum}");
        }

        // Wyświetla listę składników. Informuje, jeśli lista jest pusta.
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
        // Tworzy klon bieżącego składnika. Kopiuje wszystkie jego właściwości.
        public object Clone()
        {
            return new Ingredient(this.Id, this.Name, this.Quantity, this.CategoryEnum);
        }
    }
}
