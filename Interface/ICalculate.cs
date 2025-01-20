using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZTP_projekt.Model;

namespace ZTP_projekt.Interface
{
    // Interfejs definiujący metodę obliczania wartości dla planu posiłków
    internal interface ICalculate
	{
		decimal Calculate(MealPlan mealPlan);
	}
}
