using System;
using System.Collections.Generic;

namespace ZTP_projekt.Model
{
    internal class MealPlanHistory
    {
        private List<MealPlan> mealPlans = new List<MealPlan>();
        private static readonly MealPlanHistory _instance = new MealPlanHistory();
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
            var mealPlan = GetMealPlan(numberOfMealPlan);
            if (mealPlan != null)
            {
                mealPlan.Display();
            }
        }

        public void ShowMealPlan(int numberOfMealPlan, int dayOfMealPlan)
        {
            var mealPlan = GetMealPlan(numberOfMealPlan);
            if (mealPlan != null)
            {
                var mealDay = mealPlan.MealDays.FirstOrDefault(m => m.Id == dayOfMealPlan);
                if (mealDay != null)
                {
                    mealDay.Display();
                }
            }
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
