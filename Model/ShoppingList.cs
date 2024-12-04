using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZTP_projekt.Data.Enum;

namespace ZTP_projekt.Model
{
	internal class ShoppingList
	{
		int id;
		Dictionary<CategoryIngredientEnum,List<Ingredient>> ingredients;
	}
}
