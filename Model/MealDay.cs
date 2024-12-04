using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZTP_projekt.Interface;

namespace ZTP_projekt.Model
{
	internal class MealDay : IMealComposite
	{
		int id;
		DateTime date;
		List<Meal> meals;

		public void Display()
		{
			throw new NotImplementedException();
		}
	}
}
