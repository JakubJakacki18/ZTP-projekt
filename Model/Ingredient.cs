using System;
using ZTP_projekt.Data.Enum;
using ZTP_projekt.Interface;
using ICloneable = ZTP_projekt.Interface.ICloneable;

namespace ZTP_projekt.Model
{
	internal class Ingredient : IMealComposite, ICloneable
	{
		public int Id { get; set; }
        public string Name { get; set; } = "";
		public int Quantity { get; set; }
        public CategoryIngredientEnum CategoryEnum { get; set; }


		public Ingredient(int id, string name, int quantity)
		{
			Id = id;
			Name = name;
			Quantity = quantity;
		}

		[Obsolete("Do not use the parameterless constructor. Ingredient(int id, string name, int quantity)", true)]
		public Ingredient() { }
		public void Display()
		{
			throw new NotImplementedException();
		}

		// Implementacja metody Clone
		public object Clone()
		{
			return new Ingredient(this.Id, this.Name, this.Quantity);
		}
	}
}
