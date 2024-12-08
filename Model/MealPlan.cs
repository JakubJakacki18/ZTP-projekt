using System;
using System.Collections.Generic;
using ZTP_projekt.Interface;

namespace ZTP_projekt.Model
{
    internal class MealPlan : IMealComposite, ISubject, System.ICloneable
    {
        private static Dictionary<int, MealPlan> instances = new();
        private readonly List<IObserver> observers = new();

        public int id { get; set; }
        public DateOnly startDate { get; set; }
		public DateOnly endDate { get; set; }
		private List<MealDay> mealDays = new();

        public List<MealDay> MealDays => mealDays;

        public void Display()
        {
            throw new NotImplementedException();
        }

        public static MealPlan GetInstance(int id)
        {
            if (!instances.ContainsKey(id))
            {
                throw new ArgumentOutOfRangeException("MealPlan with given id does not exist");
            }
            return instances[id];
        }

        public static MealPlan CreateInstance(DateOnly startDate, DateOnly endDate)
        {
            int newKey = instances.Count > 0 ? instances.Keys.Max() + 1 : 0;
            instances[newKey] = new MealPlan(startDate, endDate);
            return instances[newKey];
        }

        private MealPlan(DateOnly startDate, DateOnly endDate)
        {
            this.startDate = startDate;
            this.endDate = endDate;
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
                mealDays = new List<MealDay>(this.mealDays)
            };

            return clonedMealPlan;
        }
    }
}
