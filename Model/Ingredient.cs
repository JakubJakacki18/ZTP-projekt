using System;
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
		public int Id { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        CategoryIngredientEnum categoryEnym { get; set; }

		public Ingredient(int id, string name, int quantity)
        {
            Id = id;
            Name = name;
            Quantity = quantity;
        }
		public void Display()
		{
			throw new NotImplementedException();
		}
	}
}
