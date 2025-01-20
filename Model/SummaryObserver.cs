using System;
using ZTP_projekt.Interface;

namespace ZTP_projekt.Model
{
    internal class SummaryObserver : IObserver
    {
        private readonly MealPlan _mealPlan;
        private ICalculate _calculate;
        private decimal _totalCalories;

		// Konstruktor wiążący obserwatora z planem posiłków i strategią kalkulacji.
		public SummaryObserver(MealPlan mealPlan, ICalculate calculate)
        {
            _mealPlan = mealPlan;
            _mealPlan.Attach(this);
            _calculate = calculate;
            Update();
        }

        // Aktualizuje obserwatora i oblicza całkowite kalorie.
        public void Update()
        {
            CalculateTotalCalories();
        }

        // Oblicza całkowitą liczbę kalorii za pomocą strategii kalkulacji.
        private void CalculateTotalCalories()
        {
            _totalCalories = _calculate.Calculate(_mealPlan);
        }

        // Zmienia strategię kalkulacji kalorii.
        public void ChangeCalculateStrategy(ICalculate calculate)
        {
            _calculate = calculate;
        }

        // Wyświetla całkowitą liczbę kalorii dla planu posiłków.
        public void DisplayCalories()
        {
            Console.WriteLine($"{_calculate.TypeOfCalculation} calories for the Meal Plan: {_totalCalories} kcal");
        }
    }
}
