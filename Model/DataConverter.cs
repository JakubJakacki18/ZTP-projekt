using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZTP_projekt.Model
{
	internal abstract class DataConverter
	{
		public void Export(string filePath)
		{
			List<MealPlan> data = MealPlanHistory.Instance.GetAllMealPlans();
			string formattedData = Serialize(data);
			SaveToFile(formattedData, filePath);
		}

		public void Import(string filePath)
		{
			string content = File.ReadAllText(filePath);
			var data = Deserialize(content);
			MealPlanHistory.Instance.OverrideMealPlanHistory(data);
		}

		protected abstract string Serialize(List<MealPlan> data);
		protected abstract List<MealPlan> Deserialize(string content);
		//żeby użycie tego wzorca miało sens to do saveToFile i ReadFromFile trzeba by dodać jeszcze jakąś logikę 
		private void SaveToFile(string content, string filePath)
		{
			File.WriteAllText(filePath, content);
		}
	}
}
