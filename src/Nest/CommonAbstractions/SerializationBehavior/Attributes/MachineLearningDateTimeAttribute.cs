using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Nest
{
	/// <summary>
	/// Signals that this date time property is used in Machine learning API's some of which will always return the date as epoch.
	/// </summary>
	[AttributeUsage(AttributeTargets.Property)]
	public class MachineLearningDateTimeAttribute : Attribute { }

	internal class MachineLearningDateTimeConverter : IsoDateTimeConverter
	{
		private static readonly DateTimeOffset Epoch = new DateTimeOffset(1970, 1, 1, 0, 0, 0, 0, TimeSpan.Zero);

		public override bool CanWrite => false;

		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer) => throw new NotSupportedException();

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			if (reader.TokenType != JsonToken.Integer) return base.ReadJson(reader, objectType, existingValue, serializer);

			var millisecondsSinceEpoch = Convert.ToDouble(reader.Value);
			var dateTimeOffset = Epoch.AddMilliseconds(millisecondsSinceEpoch);

			if (objectType == typeof(DateTimeOffset) || objectType == typeof(DateTimeOffset?))
				return dateTimeOffset;

			return dateTimeOffset.DateTime;
		}
	}
}
