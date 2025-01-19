﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Xml.Serialization;
using ZTP_projekt.Interface;
using ICloneable = ZTP_projekt.Interface.ICloneable;


namespace ZTP_projekt.Model
{
	internal class MealPlan : IMealComposite, ISubject, ICloneable
	{
		[XmlIgnore]
		private readonly List<IObserver> observers = [];
		public List<MealDay> MealDays { get; private set;} = []; 
		public int Id { get; private set; }
		public DateTime StartDate { get; private set; } = DateTime.Now;
		public DateTime EndDate { get; private set; } = DateTime.Now.AddDays(1);

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
		[Obsolete("Do not use the parameterless constructor. MealPlan(DateTime startDate, DateTime endDate)", true)]
		public MealPlan() { }


		public void Display()
		{
			throw new NotImplementedException();
		}

		public void AddMealDay(MealDay mealDay)
		{
			int daysDifference = (EndDate - StartDate).Days;
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