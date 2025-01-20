using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Newtonsoft.Json;
using ZTP_projekt.Interface;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace ZTP_projekt.Model
{
    // Klasa odpowiedzialna za konwersję obiektów MealPlan na format JSON i odwrotnie
    internal class JsonConverter : DataConverter
	{
		protected override string Serialize(List<MealPlan> mealPlans)
		{
			return JsonSerializer.Serialize(mealPlans, new JsonSerializerOptions
			{
				WriteIndented = true
			});
		}

		protected override List<MealPlan> Deserialize(string content)
		{
			return JsonConvert.DeserializeObject<List<MealPlan>>(content) ?? [];
		}
	}
}
