using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using ZTP_projekt.Interface;

namespace ZTP_projekt.Model
{
	internal class CSVConverter<T> : IDataConverter<T> where T : new()
	{
		public string Serialize(T obj)
		{
			var properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
			var header = string.Join(",", properties.Select(p => p.Name));
			var values = string.Join(",", properties.Select(p => p.GetValue(obj)?.ToString() ?? string.Empty));

			return $"{header}\n{values}";
		}

		public T? Deserialize(string data)
		{
			var lines = data.Split(new[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);
			if (lines.Length < 2)
				throw new ArgumentException("Invalid CSV data.");

			var header = lines[0].Split(',');
			var values = lines[1].Split(',');

			var obj = new T();
			var properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

			for (int i = 0; i < header.Length; i++)
			{
				var property = properties.FirstOrDefault(p => p.Name == header[i]);
				if (property != null && i < values.Length)
				{
					var value = Convert.ChangeType(values[i], property.PropertyType);
					property.SetValue(obj, value);
				}
			}

			return obj;
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