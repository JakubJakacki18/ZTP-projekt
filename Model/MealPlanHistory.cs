using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZTP_projekt.Model
{
	internal class MealPlanHistory
	{
		private List<MealPlan> mealPlans = [];
		private static readonly MealPlanHistory _instance = new();
		public static MealPlanHistory Instance => _instance;
		public List<MealPlan> GetAllMealPlans()
		{
			return mealPlans;
		}
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
		public void OverrideMealPlanHistory(List<MealPlan> mealPlans) 
		{
			this.mealPlans = mealPlans;
		}
		public MealPlan? GetMealPlan(int numberOfMealPlan)
		{
			try
			{
				return mealPlans[numberOfMealPlan];
			}
			catch (ArgumentOutOfRangeException)
			{
				Console.WriteLine("Meal plan with this number does not exist");
				return null;
			
			}
		}

		internal void ClearHistory()
		{
			mealPlans.Clear();
		}
	}
}
