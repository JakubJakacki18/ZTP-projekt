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

        public MealDay(int id, DateTime date, List<Meal> meals)
        {
            Id = id;
            Date = date;
            Meals = meals;
        }
		[Obsolete("Do not use the parameterless constructor. MealDay(int id, DateTime date, List<Meal> meals)", true)]
		public MealDay() { }
		public void AddMeal(Meal meal) 
        {
            Meals.Add(meal);

        }
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
		public void Display()
		{
			throw new NotImplementedException();
		}
	}
}
