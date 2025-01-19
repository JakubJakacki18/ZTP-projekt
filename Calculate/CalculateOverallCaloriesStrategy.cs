using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZTP_projekt.Interface;
using ZTP_projekt.Model;

namespace ZTP_projekt.Calculate
{
	internal class CalculateOverallCaloriesStrategy : ICalculate
	{
		public decimal Calculate(MealPlan mealPlan)
			=> mealPlan.MealDays
				.SelectMany(mealDay => mealDay.Meals)
				.SelectMany(meal => meal.Recipes)
				.Sum(recipe => recipe.Calories);
		
	}
}
