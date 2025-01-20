using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZTP_projekt.Interface;
using ZTP_projekt.Model;

namespace ZTP_projekt.Calculate
{
    // Oblicza średnią liczbę kalorii na dzień w planie posiłków
    internal class CalculateMeanPerDayCaloriesStrategy : ICalculate
	{
		public decimal Calculate(MealPlan mealPlan)
		{
			decimal totalCalories = mealPlan.MealDays
				.SelectMany(mealDay => mealDay.Meals)
				.SelectMany(meal => meal.Recipes)
				.Sum(recipe => recipe.Calories);
			totalCalories /= mealPlan.MealDays.Count;
			return totalCalories;
		}
	}
}
