using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZTP_projekt.Interface;
using ZTP_projekt.Model;

namespace ZTP_projekt.Calculate
{
    // Klasa implementująca strategię obliczania średniej liczby kalorii na przepis w planie posiłków.
    internal class CalculateMeanPerRecipeCaloriesStrategy : ICalculate
	{
		public string TypeOfCalculation => "Mean per recipe";
		public decimal Calculate(MealPlan mealPlan)
		{
			decimal totalCalories = mealPlan.MealDays
					.SelectMany(mealDay => mealDay.Meals)
					.SelectMany(meal => meal.Recipes)
					.Sum(recipe => recipe.Calories);
			int totalRecipes = mealPlan.MealDays
				.SelectMany(mealDay => mealDay.Meals)
				.SelectMany(meal => meal.Recipes)
				.Count();
			totalCalories /= totalRecipes;
			return totalCalories;
		}
	}
}
