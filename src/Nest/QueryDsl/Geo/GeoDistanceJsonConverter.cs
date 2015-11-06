using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.IO;
using System.Text.RegularExpressions;

namespace Nest
{
	internal class GeoDistanceJsonConverter : JsonConverter
	{
		private static readonly Regex SplitRegex = new Regex(@"^(\d+(?:[.,]\d+)?)(\D+)$");
		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			var p = value as GeoDistance;
			if (p == null)
			{
				writer.WriteNull();
				return;
			}
			using (var sw = new StringWriter())
			using (var localWriter = new JsonTextWriter(sw))
			{
				serializer.Serialize(localWriter, p.Precision);
				localWriter.WriteRaw(p.Unit.GetStringValue());
				var s = sw.ToString();
				writer.WriteValue(s);
			}
		}

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			if (reader.TokenType != JsonToken.String) return null;
			var v = reader.Value as string;
			if (v == null) return null;
			var matches = SplitRegex.Matches(v);
			if (matches.Count < 0
				|| matches[0].Groups.Count < 3)
				return null;
			double p;
			var sp = matches[0].Groups[1].Captures[0].Value;
			if (!double.TryParse(sp, out p)) return null;
			var unit = matches[0].Groups[2].Captures[0].Value.ToEnum<GeoPrecision>();
			if (!unit.HasValue) return null;
			return new GeoDistance(p, unit.Value);
		}

		public override bool CanConvert(Type objectType)
		{
			return true;
		}
	}
}