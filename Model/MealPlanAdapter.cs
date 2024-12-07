using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ZTP_projekt.Model
{
	internal class MealPlanAdapter(MealPlan mealPlan)
	{
		MealPlan _mealPlan = mealPlan;

		void adapt_to_json()
		{
			string json = JsonSerializer.Serialize(_mealPlan, new JsonSerializerOptions { WriteIndented = true });

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