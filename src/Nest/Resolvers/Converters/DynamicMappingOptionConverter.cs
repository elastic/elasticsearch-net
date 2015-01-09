using System;
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
				case DynamicMappingOption.Strict:
					writer.WriteValue("strict");
					break;
				case DynamicMappingOption.Ignore:
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

			var sv = v.ToString().ToLowerInvariant();
			switch (sv)
			{
				case "false":
					return DynamicMappingOption.Ignore;
				case "strict":
					return DynamicMappingOption.Strict;
				default:
					return DynamicMappingOption.Allow;

			}
		}

		public override bool CanConvert(Type objectType)
		{
			return objectType == typeof(DynamicMappingOption?);
		}
	}
}
