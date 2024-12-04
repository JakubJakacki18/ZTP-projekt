using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZTP_projekt.Interface;

namespace ZTP_projekt.Model
{
	internal class MealPlan : IMealComposite
	{
		private static Dictionary<int, MealPlan> instances = new();
		int id;
		DateOnly startDate;
		DateOnly endDate;
		List<MealDay> mealDays = new();

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
	}
}
