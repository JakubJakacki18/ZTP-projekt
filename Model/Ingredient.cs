﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZTP_projekt.Data.Enum;
using ZTP_projekt.Interface;

namespace ZTP_projekt.Model
{
	internal class Ingredient : IMealComposite
	{
		int id;
		string name;
		CategoryIngredientEnum categoryEnym;
		int quantity;

		public void Display()
		{
			throw new NotImplementedException();
		}
	}
}
