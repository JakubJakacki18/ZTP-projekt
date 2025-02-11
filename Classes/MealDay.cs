﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZTP_projekt.Interface;
using ICloneable = ZTP_projekt.Interface.ICloneable;

namespace ZTP_projekt.Model
{
	internal class MealDay : IMealComposite, ICloneable
	{
        public int Id { get; private set; }
        public DateTime Date { get; private set; } = DateTime.Now;
		public List<Meal> Meals { get; private set; } = [];

        // Konstruktor przyjmujący ID, datę oraz listę posiłków
        public MealDay(int id, DateTime date, List<Meal> meals)
        {
            Id = id;
            Date = date;
            Meals = meals;
        }

        // Konstruktor domyślny oznaczony jako przestarzały
        [Obsolete("Do not use the parameterless constructor. MealDay(int id, DateTime date, List<Meal> meals)", true)]
        public MealDay() { }

        // Dodaje posiłek do dnia
        public void AddMeal(Meal meal)
        {
			Meals.Add(meal);
        }

        // Klonowanie obiektu MealDay
        public object Clone()
        {
            var clonedMealDay = (MealDay)this.MemberwiseClone();

            clonedMealDay.Meals = new List<Meal>();
            foreach (var meal in this.Meals)
            {
                clonedMealDay.Meals.Add((Meal)meal.Clone());
            }

            return clonedMealDay;
        }

        // Wyświetla szczegóły dnia posiłku
        public void Display()
        {
            Console.WriteLine($"\n--- Meal Day Details ---");
            Console.WriteLine($"Meal Day ID: {Id}");
			Console.WriteLine($"Date: {Date.ToString("dddd")}");
			Console.WriteLine("Meals:");

            if (Meals == null || Meals.Count == 0)
            {
                Console.WriteLine("No meals available for this day.");
                return;
            }

            foreach (var meal in Meals)
            {
                Console.WriteLine($"- {meal.Name} (Category: {meal.CategoryMeal})");
                Console.WriteLine("  Recipes:");

                foreach (var recipe in meal.Recipes)
                {
                    Console.WriteLine($"    - {recipe.Name}");
                }
            }
        }

    }
}
