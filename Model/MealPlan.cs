using System;
using System.Collections.Generic;
using System.ComponentModel;
using ZTP_projekt.Interface;

namespace ZTP_projekt.Model
{
    internal class MealPlan : IMealComposite, ISubject
    {
        private static Dictionary<int, MealPlan> instances = new();
        private readonly List<IObserver> observers = new();
		private List<MealDay> mealDays = new();
		private int id;
        private readonly DateOnly startDate;
        private readonly DateOnly endDate;

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
            if (mealDays.Count() > daysDifference) 
            {
                throw new ArgumentException("MealPlan is full");
			}
			mealDays.Add(mealDay);

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

	}
}
