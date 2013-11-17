using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace Nest.Resolvers.Converters
{
	public class DynamicMappingOptionConverter : JsonConverter
	{
		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			var v = value as DynamicMappingOption?;
			if (!v.HasValue)
			{
				writer.WriteValue(true);
				return;
			}
			switch (v.Value)
			{
				case DynamicMappingOption.strict:
					writer.WriteValue("strict");
					break;
				case DynamicMappingOption.ignore:
					writer.WriteValue(false);
					break;
				default:
					writer.WriteValue(true);
					break;
			}
		}

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			var v = reader.Value;
			if (v == null)
				return null;

			var sv = v.ToString().ToLower();
			switch (sv)
			{
				case "false":
					return DynamicMappingOption.ignore;
				case "strict":
					return DynamicMappingOption.strict;
				default:
					return DynamicMappingOption.allow;

			}
		}

		public override bool CanConvert(Type objectType)
		{
			return objectType == typeof(DynamicMappingOption?);
		}
	}
}
