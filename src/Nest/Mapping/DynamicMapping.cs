using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace Nest
{
	/// <summary>
	/// Controls how elasticsearch handles dynamic mapping changes when a new document present new fields
	/// </summary>
	[JsonConverter(typeof(DynamicMappingJsonConverter))]
	public enum DynamicMapping
	{
		/// <summary>
		/// Default value, allows unmapped fields to be cause a mapping update 
		/// </summary>
		[EnumMember(Value = "allow")]
		Allow,
		/// <summary>
		/// New unmapped fields will be silently ignored
		/// </summary>
		[EnumMember(Value = "ignore")]
		Ignore,
		/// <summary>
		/// If new unmapped fields are passed, the whole document WON'T be added/updated
		/// </summary>
		[EnumMember(Value = "strict")]
		Strict
	}

	internal class DynamicMappingJsonConverter : JsonConverter
	{
		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			var v = value as DynamicMapping?;
			if (!v.HasValue)
			{
				writer.WriteValue(true);
				return;
			}
			switch (v.Value)
			{
				case DynamicMapping.Strict:
					writer.WriteValue("strict");
					break;
				case DynamicMapping.Ignore:
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
					return DynamicMapping.Ignore;
				case "strict":
					return DynamicMapping.Strict;
				default:
					return DynamicMapping.Allow;
			}
		}

		public override bool CanConvert(Type objectType) => objectType == typeof(DynamicMapping?);
	}
}
