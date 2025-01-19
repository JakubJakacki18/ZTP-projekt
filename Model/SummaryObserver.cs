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
	}
}
