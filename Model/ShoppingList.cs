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
        public int Id { get; set; }
        public Dictionary<CategoryIngredientEnum, List<Ingredient>> Ingredients { get; private set; }

        public ShoppingList(int id)
        {
            Id = id;
            Ingredients = new Dictionary<CategoryIngredientEnum, List<Ingredient>>();
        }
    }
}
