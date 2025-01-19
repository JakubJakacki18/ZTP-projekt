using System;
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
            //Meals.meal.CategoryMeal

		}
        public object Clone()
        {
			throw new NotImplementedException();
		}
		public void Display()
		{
			throw new NotImplementedException();
		}
	}
}
