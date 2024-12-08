using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using ZTP_projekt.Interface;

namespace ZTP_projekt.Model
{
	internal class JsonConverter<T> : IDataConverter<T>
	{
		public string Serialize(T obj)
		{
			return JsonSerializer.Serialize(obj, new JsonSerializerOptions
			{
				WriteIndented = true
			});
		}

		public T? Deserialize(string json)
		{
			return JsonSerializer.Deserialize<T>(json);
		}
	}
}
//def adapt_to_json(self):
//        return json.dumps({
//	"name": self.recipe.name,
//            "description": self.recipe.description,
//            "ingredients": self.recipe.ingredients,
//            "category": self.recipe.category,
//            "calories": self.recipe.calories,
//        }, indent = 4)