using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Nest
{
	internal class GetWarmerResponseConverter : JsonConverter
	{
		public override bool CanConvert(Type objectType) => true;
		public override bool CanWrite => false;
		public override bool CanRead => true;
			
		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			var response = new GetWarmerResponse();
			var warmersByIndex = JObject.Load(reader).Properties().ToDictionary(p => p.Name, p => p.Value);
			if (!warmersByIndex.HasAny()) return response;
			response.Indices = new Dictionary<string, Warmers>();
			foreach (var warmerByIndex in warmersByIndex)
			{
				var index = warmerByIndex.Key;
				var warmersObject = JObject.FromObject(warmerByIndex.Value).Properties()
					.Where(p => p.Name == "warmers")
					.Select(p => p.Value)
					.FirstOrDefault();
				var warmers = new Warmers();
				if (warmersObject != null)
				{
					var warmersByName = JObject.FromObject(warmersObject).Properties()
						.ToDictionary(p => p.Name, p => p.Value);
					foreach (var warmerByName in warmersByName)
					{
						var name = warmerByName.Key;
						var warmer = warmerByName.Value.ToObject<Warmer>();
						warmers.Add(name, warmer);
					}
				}
				response.Indices.Add(warmerByIndex.Key, warmers);
			}
			return response;
		}

		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			throw new NotSupportedException();
		}
	}
}
