using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZTP_projekt.Model
{
	internal class MealPlanHistory
	{
		private List<MealPlan> mealPlans = new List<MealPlan>();
		private static readonly MealPlanHistory _instance = new();
		public static MealPlanHistory Instance => _instance;

		public void AddMealPlan(MealPlan mealPlan)
		{
			mealPlans.Add(mealPlan);
		}
		public void ShowMealPlan(int numberOfMealPlan) 
		{
		
		}
		public void ShowMealPlan(int numberOfMealPlan, int dayOfMealPlan)
		{

		}




	}
}
