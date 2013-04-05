using System;
using System.Linq;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Nest.Resolvers.Converters
{
	public class WarmerResponseConverter : JsonConverter
	{
		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{

		}

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue,
										JsonSerializer serializer)
		{
			// {"indexname":{"warmers":{"warmername": {...}}}}
			var dict = new Dictionary<string, Dictionary<string, Dictionary<string, WarmerMapping>>>();
			serializer.Populate(reader, dict);

			Dictionary<string, Dictionary<string, WarmerMapping>> indices = new Dictionary<string, Dictionary<string, WarmerMapping>>();
			foreach (var kv in dict)
			{
				var indexDict = kv.Value;
				Dictionary<string, WarmerMapping> warmers;
				if (!indexDict.TryGetValue("warmers", out warmers) || warmers == null)
					continue;
				foreach (var kvW in warmers)
				{
					kvW.Value.Name = kvW.Key;
				}
				indices.Add(kv.Key, warmers);
			}

			return new WarmerResponse
			{
				Indices = indices
			};
		}

		public override bool CanConvert(Type objectType)
		{
			return objectType == typeof(WarmerResponse);
		}
	}
}