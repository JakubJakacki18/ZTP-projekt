using System;
using ZTP_projekt.Interface;

namespace ZTP_projekt.Model
{
    internal class SummaryObserver : IObserver
    {
        private readonly MealPlan _mealPlan;
        private ICalculate _calculate;

        public SummaryObserver(MealPlan mealPlan, ICalculate calculate)
        {
            _mealPlan = mealPlan;
            _mealPlan.Attach(this);
			_calculate = calculate;
		}

        public void Update()
        {
            CalculateTotalCalories();
        }

        private void CalculateTotalCalories()
        {
            _calculate.Calculate(_mealPlan);
        }
        public void ChangeCalculateStrategy(ICalculate calculate)
        {
            _calculate = calculate;
        }
        public void DisplayTotalCalories()
        {
            int totalCalories = 0;

            foreach (var mealDay in _mealPlan.MealDays)
            {
                foreach (var meal in mealDay.Meals)
                {
                    foreach (var recipe in meal.Recipes)
                    {
                        totalCalories += recipe.Calories;  // Sumowanie kalorii z każdego przepisu
                    }
                }
            }

            Console.WriteLine($"Total Calories for the Meal Plan: {totalCalories} kcal");
        }
    }
}
