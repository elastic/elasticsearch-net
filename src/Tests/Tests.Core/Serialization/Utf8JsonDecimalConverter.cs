using System;
using Newtonsoft.Json;
using static System.Decimal;

namespace Tests.Core.Serialization
{
	/// <summary>
	/// Serializes decimal, float and double with no decimal places
	/// when the contained value is a whole number
	/// </summary>
	/// <remarks>
	/// JSON.Net always appends .0 for floating points and decimal, so remove
	/// when serializing
	/// </remarks>
	public class Utf8JsonDecimalConverter : JsonConverter
	{
		public override bool CanRead => false;

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer) =>
			throw new NotSupportedException();

		public override bool CanConvert(Type objectType) =>
			objectType == typeof(decimal) || objectType == typeof(float) || objectType == typeof(double) ||
			objectType == typeof(decimal?) || objectType == typeof(float?) || objectType == typeof(double?);

		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			var stringValue = JsonConvert.ToString(value);
			writer.WriteRawValue(stringValue.EndsWith(".0")
				? stringValue.Substring(0, stringValue.IndexOf("."))
				: stringValue);
		}
	}
}
