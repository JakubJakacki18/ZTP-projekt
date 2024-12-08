using System;
using ZTP_projekt.Interface;

namespace ZTP_projekt.Model
{
    internal class SummaryObserver : IObserver
    {
        private readonly MealPlan _mealPlan;
        public int TotalCalories { get; private set; }

        public SummaryObserver(MealPlan mealPlan)
        {
            _mealPlan = mealPlan;
            _mealPlan.Attach(this);
        }

        public void Update()
        {
            CalculateTotalCalories();
        }

        private void CalculateTotalCalories()
        {
            TotalCalories = 0;

            foreach (var mealDay in _mealPlan.MealDays)
            {
                foreach (var meal in mealDay.Meals)
                {
                    foreach (var recipe in meal.Recipes)
                    {
                        TotalCalories += recipe.Calories;
                    }
                }
            }
        }
    }
}
