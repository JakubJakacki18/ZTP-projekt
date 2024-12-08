﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using ZTP_projekt.Interface;
using ICloneable = ZTP_projekt.Interface.ICloneable;


namespace ZTP_projekt.Model
{
	internal class MealPlan : IMealComposite, ISubject, ICloneable
	{
		private static Dictionary<int, MealPlan> instances = new();
		private readonly List<IObserver> observers = new();
		public List<MealDay> MealDays { get; private set;} = new (); 
		public int id { get; private set; }
		public readonly DateOnly startDate;
		public readonly DateOnly endDate;

		public MealPlan(DateOnly startDate, DateOnly endDate)
		{
			int id = instances.Count > 0 ? instances.Keys.Max() + 1 : 0;
			int daysDifference = (endDate.ToDateTime(TimeOnly.MinValue) - startDate.ToDateTime(TimeOnly.MinValue)).Days;
			if (daysDifference < 0)
			{
				throw new ArgumentException("End date must be greater than start date");
			}
			this.startDate = startDate;
			this.endDate = endDate;
		}



		public void Display()
		{
			throw new NotImplementedException();
		}

		public void AddMealDay(MealDay mealDay)
		{
			int daysDifference = (endDate.ToDateTime(TimeOnly.MinValue) - startDate.ToDateTime(TimeOnly.MinValue)).Days;
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
		public void SaveToFile()
		{

		}
		public void LoadFromFile()
		{

		}
		public object Clone()
		{
			var clonedMealPlan = new MealPlan(startDate, endDate)
			{
				MealDays = new List<MealDay>(this.MealDays)
			};

			return clonedMealPlan;
		}
	}
}