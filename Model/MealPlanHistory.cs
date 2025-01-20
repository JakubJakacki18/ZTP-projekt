using System;
using System.Collections.Generic;

namespace ZTP_projekt.Model
{
    internal class MealPlanHistory
    {
        private List<MealPlan> mealPlans = new List<MealPlan>();
        private static readonly MealPlanHistory _instance = new MealPlanHistory();
        public static MealPlanHistory Instance => _instance;

        // Zwraca listę wszystkich zapisanych planów posiłków.
        public List<MealPlan> GetAllMealPlans()
        {
            return mealPlans;
        }

        // Dodaje nowy plan posiłków do historii.
        public void AddMealPlan(MealPlan mealPlan)
        {
            mealPlans.Add(mealPlan);
        }

        // Wyświetla plan posiłków na podstawie numeru planu.
        public void ShowMealPlan(int numberOfMealPlan)
        {
            var mealPlan = GetMealPlan(numberOfMealPlan);
            if (mealPlan != null)
            {
                mealPlan.Display();
            }
        }

        // Wyświetla plan posiłków oraz szczegóły dla danego dnia w planie.
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

        // Nadpisuje historię planów posiłków nową listą.
        public void OverrideMealPlanHistory(List<MealPlan> mealPlans)
        {
            this.mealPlans = mealPlans;
        }

        // Zwraca plan posiłków na podstawie numeru, lub `null` jeśli numer jest niepoprawny.
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

        // Czyści historię zapisanych planów posiłków.
        internal void ClearHistory()
        {
            mealPlans.Clear();
        }
    }
}
