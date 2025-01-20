﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using ZTP_projekt.Interface;
using ICloneable = ZTP_projekt.Interface.ICloneable;


namespace ZTP_projekt.Model
{
	internal class MealPlan : IMealComposite, ISubject, ICloneable
	{
		
		private readonly List<IObserver> observers = [];
		public List<MealDay> MealDays { get; private set;} = []; 
		public int Id { get; private set; }
		public DateTime StartDate { get; private set; } = DateTime.Now;
		public DateTime EndDate { get; private set; } = DateTime.Now.AddDays(1);

        // Konstruktor przyjmujący datę początkową i końcową planu posiłków
        public MealPlan(DateTime startDate, DateTime endDate)
        {
            int daysDifference = (EndDate - StartDate).Days;
            if (daysDifference < 0)
            {
                throw new ArgumentException("End date must be greater than start date");
            }
            this.StartDate = startDate;
            this.EndDate = endDate;
        }
        //liczenie dni
        public int GetMealPlanLength()
		 => (EndDate - StartDate).Days;
		// Konstruktor domyślny oznaczony jako przestarzały
		[Obsolete("Do not use the parameterless constructor. MealPlan(DateTime startDate, DateTime endDate)", true)]
        public MealPlan() { }

        // Wyświetla szczegóły planu posiłków
        public void Display()
        {
            Console.WriteLine("\n--- Meal Plan Details ---");
            Console.WriteLine($"Meal Plan ID: {Id}");
            Console.WriteLine($"Start Date: {StartDate}");
            Console.WriteLine($"End Date: {EndDate}");
            Console.WriteLine($"Total Days: {(EndDate - StartDate).Days}");
            Console.WriteLine("Meal Days:");

            if (!MealDays.Any())
            {
                Console.WriteLine("No Meal Days available.");
                return;
            }

            foreach (var mealDay in MealDays)
            {
                mealDay.Display();
            }
        }

        // Dodaje dzień posiłków do planu
        public void AddMealDay(MealDay mealDay)
        {
            int daysDifference = (EndDate - StartDate).Days;
            if (MealDays.Count() > daysDifference)
            {
                throw new ArgumentException("MealPlan is full");
            }
            MealDays.Add(mealDay);
            Notify();

		}

        // Dodaje obserwatora
        public void Attach(IObserver observer)
        {
            if (!observers.Contains(observer))
            {
                observers.Add(observer);
            }
        }

        // Usuwa obserwatora
        public void Detach(IObserver observer)
        {
            if (observers.Contains(observer))
            {
                observers.Remove(observer);
            }
        }

        // Powiadamia obserwatorów o zmianach
        public void Notify()
        {
            foreach (var observer in observers)
            {
                observer.Update();
            }
        }

        // Tworzy kopię planu posiłków
        public object Clone()
        {
            var clonedMealPlan = new MealPlan(StartDate, EndDate)
            {
                MealDays = new List<MealDay>(MealDays)
            };

            return clonedMealPlan;
        }

		// Zmienia datę początkową planu posiłków
		public void ChangeStartDate(DateTime startDate)
		{
            var timeDifference = (EndDate- StartDate).Days;
			StartDate = startDate;
			EndDate = StartDate.AddDays(timeDifference);
		}
	}
}