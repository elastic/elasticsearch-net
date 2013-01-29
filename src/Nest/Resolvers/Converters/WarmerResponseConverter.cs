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
			var dict = new Dictionary<string, Dictionary<string, Dictionary<string, SearchDescriptor<dynamic>>>>();
			serializer.Populate(reader, dict);
			if (dict.Count == 0)
				throw new DslException("Could not deserialize WarmerMapping");
			var indexDict = dict.First().Value;
			if (indexDict.Count == 0)
				throw new DslException("Could not deserialize WarmerMapping");
			var warmersDict = indexDict.First().Value;
			if (warmersDict.Count == 0)
				throw new DslException("Could not deserialize WarmerMapping");


			return new WarmerResponse
			{
				Name = warmersDict.First().Key,
				Search = warmersDict.First().Value
			};
		}

		public override bool CanConvert(Type objectType)
		{
			return objectType == typeof(WarmerResponse);
		}
	}
}