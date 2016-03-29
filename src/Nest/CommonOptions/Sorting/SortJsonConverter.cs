using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Nest
{
	internal class SortJsonConverter : ReserializeJsonConverter<SortField, ISort>
	{
		public override bool CanRead => true;
		public override bool CanWrite => true;
		public override bool CanConvert(Type objectType) => typeof(ISort).IsAssignableFrom(objectType);

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			ISort sort = null;
			reader.Read();
			var field = reader.Value as string;

			if (field == "_geo_distance")
			{
				reader.Read();
				var jObject = JObject.Load(reader);
				var geoLocationProp = jObject.Properties().First(p => p.Value.Type == JTokenType.Array);
				using (var r = jObject.CreateReader())
				{
					var s = FromJson.ReadAs<GeoDistanceSort>(r, objectType, existingValue, serializer);
					s.Field = geoLocationProp.Name;
					using (var rr = geoLocationProp.Value.CreateReader())
						s.Points =FromJson.ReadAs<List<GeoLocation>>(rr, objectType, existingValue, serializer); 
					sort = s;
				}
			}
			else if (field == "_script")
			{
				reader.Read();
				var s = FromJson.ReadAs<ScriptSort>(reader, objectType, existingValue, serializer);
				sort = s;
			}
			else
			{
				reader.Read();
				var s = FromJson.ReadAs<SortField>(reader, objectType, existingValue, serializer);
				s.Field = field;
				sort = s;
			}
			reader.Read();
			return sort;
		}

		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			var s = value as ISort;
			if (s?.SortKey == null) return;

			writer.WriteStartObject();
			var settings = serializer.GetConnectionSettings();

			switch (s.SortKey.Name ?? string.Empty)
			{
				case "_script":
					writer.WritePropertyName("_script");
					base.Reserialize(writer, s, serializer);
					break;
				case "_geo_distance":
					var geo = s as IGeoDistanceSort;
					writer.WritePropertyName(geo.SortKey.Name);
					base.Reserialize(writer, s, serializer, w =>
					{
						writer.WritePropertyName(settings.Inferrer.Field(geo.Field));
						serializer.Serialize(writer, geo.Points);
					});
					break;
				default:
					writer.WritePropertyName(settings.Inferrer.Field(s.SortKey));
					base.Reserialize(writer, s, serializer);
					break;
			}
			writer.WriteEndObject();
		}
	}
}
