using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Nest.DSL.Descriptors;


namespace Nest.Resolvers.Converters
{
	public class GeoDistanceFacetDescriptorConverter : JsonConverter
	{
		public override bool CanConvert(Type objectType)
		{
			return true;
		}
		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			var sort = value as GeoDistanceFacetDescriptor;
			if (sort != null)
			{
				writer.WriteStartObject();
				if (sort._PinLocation != null)
				{
					writer.WritePropertyName(sort._Field);
					writer.WriteValue(sort._PinLocation);
				}

				if (sort._ValueField != null)
				{
					writer.WritePropertyName("value_field");
					writer.WriteValue(sort._ValueField);
				}
				if (sort._ValueScript != null)
				{
					writer.WritePropertyName("value_script");
					writer.WriteValue(sort._ValueScript);
				}
				if (sort._Ranges != null)
				{
					writer.WritePropertyName("ranges");
					serializer.Serialize(writer,sort._Ranges);
				}
				if (sort._Params != null)
				{
					writer.WritePropertyName("params");
					serializer.Serialize(writer, sort._Params);
				} 
				if (sort._GeoUnit.HasValue)
				{
					writer.WritePropertyName("unit");
					var v = Enum.GetName(typeof(GeoUnit), sort._GeoUnit.Value);
					writer.WriteValue(v);
				}
				if (sort._GeoDistance.HasValue)
				{
					writer.WritePropertyName("distance_type");
					var v = Enum.GetName(typeof(GeoDistance), sort._GeoDistance.Value);
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
