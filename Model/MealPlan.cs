﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using ZTP_projekt.Interface;
using ICloneable = ZTP_projekt.Interface.ICloneable;


namespace ZTP_projekt.Model
{
	internal class MealPlan : IMealComposite, ISubject, ICloneable
	{
		private static Dictionary<int, MealPlan> instances = [];
		private readonly List<IObserver> observers = [];
		public List<MealDay> MealDays { get; private set;} = []; 
		public int Id { get; private set; }
		public DateOnly StartDate { get; private set; }
		public DateOnly EndDate { get; private set; }

		public MealPlan(DateOnly startDate, DateOnly endDate)
		{
			int id = instances.Count > 0 ? instances.Keys.Max() + 1 : 0;
			int daysDifference = (endDate.ToDateTime(TimeOnly.MinValue) - startDate.ToDateTime(TimeOnly.MinValue)).Days;
			if (daysDifference < 0)
			{
				throw new ArgumentException("End date must be greater than start date");
			}
			this.StartDate = startDate;
			this.EndDate = endDate;
		}

        public void Display()
        {
            Console.WriteLine("\n--- Meal Plan Details ---");
            Console.WriteLine($"Meal Plan ID: {Id}");
            Console.WriteLine($"Start Date: {StartDate}");
            Console.WriteLine($"End Date: {EndDate}");
            Console.WriteLine($"Total Days: {(EndDate.ToDateTime(TimeOnly.MinValue) - StartDate.ToDateTime(TimeOnly.MinValue)).Days + 1}");
            Console.WriteLine("Meal Days:");

            if (!MealDays.Any())
            {
                Console.WriteLine("No Meal Days available.");
                return;
            }

            foreach (var mealDay in MealDays)
            {
                Console.WriteLine($"\nMeal Day ID: {mealDay.Id}");
                Console.WriteLine($"Date: {mealDay.Date}");
                Console.WriteLine("Meals:");
                foreach (var meal in mealDay.Meals)
                {
                    Console.WriteLine($"- {meal.Name} (Category: {meal.CategoryMeal})");
                }
            }
        }

        public void AddMealDay(MealDay mealDay)
		{
			int daysDifference = (EndDate.ToDateTime(TimeOnly.MinValue) - StartDate.ToDateTime(TimeOnly.MinValue)).Days;
			if (MealDays.Count() > daysDifference)
			{
				throw new ArgumentException("MealPlan is full");
			}
			MealDays.Add(mealDay);


        }
        public void Attach(IObserver observer)
		{
			if (!observers.Contains(observer))
			{
				observers.Add(observer);
			}
		}

		public void Detach(IObserver observer)
		{
			if (observers.Contains(observer))
			{
				observers.Remove(observer);
			}
		}

		public void Notify()
		{
			foreach (var observer in observers)
			{
				observer.Update();
			}
		}

		public object Clone()
		{
			var clonedMealPlan = new MealPlan(StartDate, EndDate)
			{
				MealDays = new List<MealDay>(MealDays)
			};

			return clonedMealPlan;
		}
	}
}