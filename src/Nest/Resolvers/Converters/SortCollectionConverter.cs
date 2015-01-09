using Nest.DSL.Descriptors;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Nest.Resolvers.Converters
{
	public class SortCollectionConverter : JsonConverter
	{
		public override bool CanConvert(Type objectType)
		{
			return typeof(IList<KeyValuePair<PropertyPathMarker, ISort>>).IsAssignableFrom(objectType);
		}

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			var sorts = new List<KeyValuePair<PropertyPathMarker, ISort>>();
			while (reader.TokenType != JsonToken.EndArray)
			{
				reader.Read();
				if (reader.TokenType == JsonToken.EndArray)
					break;
				reader.Read();
				var field = reader.Value as string;
				reader.Read();

				if (field == "_geo_distance")
				{
					var j = JObject.Load(reader);
					if (j != null)
					{
						var sort = j.ToObject<GeoDistanceSort>(serializer);
						if (sort != null)
						{
							LoadGeoDistanceSortLocation(sort, j);
							sorts.Add(new KeyValuePair<PropertyPathMarker, ISort>("_geo_distance", sort));
						}
					}
				}
				else if (field == "_script")
				{
					var j = JObject.Load(reader);
					if (j != null)
					{
						var sort = j.ToObject<ScriptSort>(serializer);
						if (sort != null)
						{
							sorts.Add(new KeyValuePair<PropertyPathMarker, ISort>("_script", sort));
						}
					}
				}
				else
				{
					var j = JObject.Load(reader);
					if (j != null)
					{
						var sort = j.ToObject<Sort>(serializer);
						if (sort != null)
						{
							sort.Field = field;
							sorts.Add(new KeyValuePair<PropertyPathMarker, ISort>(sort.Field, sort));
						}
					}
				}
				reader.Read();
			}
			return sorts;
		}

		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			writer.WriteStartArray();
			var sortItems = value as IList<KeyValuePair<PropertyPathMarker, ISort>>;
			foreach (var item in sortItems)
			{
				writer.WriteStartObject();
				var contract = serializer.ContractResolver as SettingsContractResolver;
				var fieldName = contract.Infer.PropertyPath(item.Key);
				writer.WritePropertyName(fieldName);
				serializer.Serialize(writer, item.Value);
				writer.WriteEndObject();
			}
			writer.WriteEndArray();
		}

		private void LoadGeoDistanceSortLocation(GeoDistanceSort sort, JObject j)
		{
			var field = j.Properties().Where(p => !GeoDistanceSort.Params.Contains(p.Name)).FirstOrDefault();

			if (field != null)
			{
				sort.Field = field.Name;

				try
				{
					sort.PinLocation = field.Value.Value<string>();
				}
				catch { }

				try
				{
					sort.Points = field.Value.Value<IEnumerable<string>>();
				}
				catch { }
			}
		}
	}
}
