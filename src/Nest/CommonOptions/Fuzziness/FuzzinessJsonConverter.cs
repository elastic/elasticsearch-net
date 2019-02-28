using System;
using System.Text.RegularExpressions;
using Newtonsoft.Json;

namespace Nest
{
	internal class FuzzinessJsonConverter : JsonConverter
	{
		private static readonly Regex AutoLengthRegex = new Regex(@"^AUTO:(?<low>\+?\d+),(?<high>\+?\d+)$", RegexOptions.Compiled | RegexOptions.IgnoreCase);

		public override bool CanRead => true;
		public override bool CanWrite => true;

		public override bool CanConvert(Type objectType) => true;

		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			var v = (IFuzziness)value;
			if (v.Auto)
			{
				if (!v.Low.HasValue || !v.High.HasValue)
					writer.WriteValue("AUTO");
				else
					writer.WriteValue($"AUTO:{v.Low},{v.High}");
			}
			else if (v.EditDistance.HasValue) writer.WriteValue(v.EditDistance.Value);
			else if (v.Ratio.HasValue) writer.WriteValue(v.Ratio.Value);
			else writer.WriteNull();
		}

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			if (reader.TokenType == JsonToken.String)
			{
				var rawAuto = (string)reader.Value;
				var match = AutoLengthRegex.Match(rawAuto);
				if (!match.Success)
					return Fuzziness.Auto;

				var low = int.Parse(match.Groups["low"].Value);
				var high = int.Parse(match.Groups["high"].Value);
				return Fuzziness.AutoLength(low, high);
			}

			if (reader.TokenType == JsonToken.Integer)
			{
				var editDistance = Convert.ToInt32(reader.Value);
				return Fuzziness.EditDistance(editDistance);
			}
			if (reader.TokenType == JsonToken.Float)
			{
				var ratio = (reader.Value as double?).GetValueOrDefault(0);
				return Fuzziness.Ratio(ratio);
			}
			return null;
		}
	}
}
