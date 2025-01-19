using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using ZTP_projekt.Interface;

namespace ZTP_projekt.Model
{
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
			return JsonSerializer.Deserialize<List<MealPlan>>(content) ?? [];
		}
	}
}
