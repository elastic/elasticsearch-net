using Nest.Resolvers;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nest
{
	internal class SortCollectionJsonConverter : JsonConverter
	{
		public override bool CanConvert(Type objectType) => typeof(IList<ISort>).IsAssignableFrom(objectType);

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			var sorts = new List<ISort>();
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
							sorts.Add(sort);
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
							sorts.Add(sort);
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
							sorts.Add(sort);
						}
					}
				}
				reader.Read();
			}
			return sorts;
		}

		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			var sorts = value as IList<ISort>;
			if (sorts == null) return;

			var contract = serializer.ContractResolver as SettingsContractResolver;
			if (contract == null) 
				throw new Exception("Can not serialize sort because the current json contract does not extend SettingsContractResolver");

			writer.WriteStartArray();
			foreach (var sort in sorts)
			{

				writer.WriteStartObject();
				var fieldName = contract.Infer.Field(sort.SortKey);
				writer.WritePropertyName(fieldName);
				serializer.Serialize(writer, sort);
				writer.WriteEndObject();
			}
			writer.WriteEndArray();
		}

		private void LoadGeoDistanceSortLocation(GeoDistanceSort sort, JObject j)
		{
			var field = j.Properties().FirstOrDefault(p => !GeoDistanceSort.Params.Contains(p.Name));

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
