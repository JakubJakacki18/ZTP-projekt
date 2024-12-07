using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZTP_projekt.Model;

namespace ZTP_projekt.Interface
{
	internal interface ICalculate
	{
		decimal calculate(MealPlan mealPlan);
	}
}
