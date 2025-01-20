using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZTP_projekt.Model
{
	//Wzorzec projektowy: Tamplate Method
	internal abstract class DataConverter
	{
		//Odpowiada za export danych z MealPlanHistory do pliku
		public void Export(string filePath)
		{
			List<MealPlan> data = MealPlanHistory.Instance.GetAllMealPlans();
			string formattedData = Serialize(data);
			SaveToFile(formattedData, filePath);
		}
		//Odpoiwada za import danych z pliku do MealPlanHistory
		public void Import(string filePath)
		{
			string content = File.ReadAllText(filePath);
			var data = Deserialize(content);
			MealPlanHistory.Instance.OverrideMealPlanHistory(data);
		}
		//Serializuje dane do napisu
		protected abstract string Serialize(List<MealPlan> data);
		//Deserializuje dane z napisu
		protected abstract List<MealPlan> Deserialize(string content);
		
		private void SaveToFile(string content, string filePath)
		{
			File.WriteAllText(filePath, content);
		}
	}
}
