using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZTP_projekt.Interface;

namespace ZTP_projekt.Model
{
	internal class Analitics(ICalculate calculate, MealPlan mealPlan)
	{
		private ICalculate _calculate=calculate;
		private MealPlan _mealPlan=mealPlan;

		public void SetMealPlan (MealPlan mealPlan)
		{
			_mealPlan = mealPlan;
		}
		public void ExecuteCalculate()
		{
			_calculate.calculate(_mealPlan);
		}
	}
}
