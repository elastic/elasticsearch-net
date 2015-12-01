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
		public override bool CanConvert(Type objectType) => typeof(ISort).IsAssignableFrom(objectType);

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			ISort sort = null;
			reader.Read();
			var field = reader.Value as string;
			reader.Read();

			if (field == "_geo_distance")
			{
				var j = JObject.Load(reader);
				if (j != null)
				{
					var s = j.ToObject<GeoDistanceSort>(serializer);
					if (s != null)
					{
						LoadGeoDistanceSortLocation(s, j);
						sort = s;
					}
				}
			}
			else if (field == "_script")
			{
				var j = JObject.Load(reader);
				if (j != null)
				{
					sort = j.ToObject<ScriptSort>(serializer);
				}
			}
			else
			{
				var j = JObject.Load(reader);
				if (j != null)
				{
					var s = j.ToObject<SortField>(serializer);
					s.Field = field;
					sort = s;
				}
			}
			reader.Read();
			return sort;
		}

		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			var sorts = value as IList<ISort>;
			if (sorts == null) return;

			var settings = serializer.GetConnectionSettings();
			writer.WriteStartArray();
			foreach (var sort in sorts)
			{

				writer.WriteStartObject();
				var fieldName = settings.Inferrer.Field(sort.SortKey);
				writer.WritePropertyName(fieldName);
				serializer.Serialize(writer, sort);
				writer.WriteEndObject();
			}
			writer.WriteEndArray();
		}

		private void LoadGeoDistanceSortLocation(GeoDistanceSort sort, JObject j)
		{
			//var field = j.Properties().FirstOrDefault(p => !GeoDistanceSort.Params.Contains(p.Name));

			//if (field != null)
			//{
			//	sort.Field = field.Name;
			//	// TODO use field.Value.Type instead of try catch

			//	try
			//	{
			//		sort.PinLocation = field.Value.Value<string>();
			//	}
			//	catch { }

			//	try
			//	{
			//		sort.Points = field.Value.Value<IEnumerable<string>>();
			//	}
			//	catch { }
			//}
		}
	}
}
