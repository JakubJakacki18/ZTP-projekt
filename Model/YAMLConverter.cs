using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

using ZTP_projekt.Interface;


using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using SharpYaml.Serialization;

namespace ZTP_projekt.Model
{
	internal class YAMLConverter : DataConverter
	{
		
		protected override string Serialize(List<MealPlan> mealPlans)
		{
			//var serializer = new SerializerBuilder()
			// .WithNamingConvention(CamelCaseNamingConvention.Instance)
			// .Build();

			//return serializer.Serialize(mealPlans);
			var serializer = new Serializer();
			return serializer.Serialize(mealPlans);
		}

		protected override List<MealPlan> Deserialize(string data)
		{
			//var deserializer = new DeserializerBuilder()
			// .WithNamingConvention(CamelCaseNamingConvention.Instance) 
			// .Build();

			//return deserializer.Deserialize<List<MealPlan>>(data);
			var serializer = new Serializer();
			return serializer.Deserialize<List<MealPlan>>(data) ?? [];
		}
	}
}