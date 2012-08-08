using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Nest.DSL.Descriptors;


namespace Nest.Resolvers.Converters
{
	public class SortGeoDistanceDescriptorConverter : JsonConverter
	{
		public override bool CanConvert(Type objectType)
		{
			return typeof(SortGeoDistanceDescriptor).IsAssignableFrom(objectType);
		}
		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			var sort = value as SortGeoDistanceDescriptor;
			if (sort != null)
			{
				writer.WriteStartObject();
				if (sort._Missing != null)
				{
					writer.WritePropertyName("missing");
					writer.WriteValue(sort._Missing);
				}
				if (sort._Order != null)
				{
					writer.WritePropertyName("order");
					writer.WriteValue(sort._Order);
				} 
				if (sort._PinLocation != null)
				{
					writer.WritePropertyName(sort._Field);
					writer.WriteValue(sort._PinLocation);
				}
				if (sort._GeoUnit.HasValue)
				{
					writer.WritePropertyName("unit");
					var v = Enum.GetName(typeof(GeoUnit), sort._GeoUnit.Value);
					writer.WriteValue(v);
				}
				writer.WriteEndObject();
			}
			else
				writer.WriteNull();
		}
		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			return null;
		}

	}
}
