using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZTP_projekt.Model
{
	internal class File
	{
		const string filePath = "mealplan.json";
		static async void Write(string json) 
		{
			try
			{
				await System.IO.File.WriteAllTextAsync(filePath, json);

				Console.WriteLine($"Dane zapisane do pliku: {filePath}");
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Wystąpił błąd podczas zapisywania pliku: {ex.Message}");
			}
		}
		static async Task<string?> Read()
		{
			try
			{
				string json = await System.IO.File.ReadAllTextAsync(filePath);

				Console.WriteLine($"Dane odczytane z pliku: {filePath}");

				return json;
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Wystąpił błąd podczas odczytywania pliku: {ex.Message}");

				return null;
			}
		}
	}
}
