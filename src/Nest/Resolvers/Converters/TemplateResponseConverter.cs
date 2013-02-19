using System;
using System.Linq;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Nest.Resolvers.Converters
{
	public class TemplateResponseConverter : JsonConverter
	{
		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{

		}

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue,
										JsonSerializer serializer)
		{
			var dict = new Dictionary<string, TemplateMapping>();
			serializer.Populate(reader, dict);
			if (dict.Count == 0)
				throw new DslException("Could not deserialize TemplateMapping1");

			return new TemplateResponse
			{
				Name = dict.First().Key,
				TemplateMapping = dict.First().Value
			};
		}

		public override bool CanConvert(Type objectType)
		{
			return objectType == typeof(ShardsSegment);
		}
	}
}