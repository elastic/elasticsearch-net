using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Shared.Extensions;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nest.Resolvers.Converters.Filters
{
	public class GeoBoundingFilterConverter : JsonConverter
	{
		public override bool CanRead { get { return true; } }
		public override bool CanWrite { get { return true; } }

		public override bool CanConvert(Type objectType)
		{
			return true; //only to be used with attribute or contract registration.
		}
		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			var f = value as IGeoBoundingBoxFilter;
			if (f == null || (f.IsConditionless && !f.IsVerbatim)) return;
			
			var contract = serializer.ContractResolver as SettingsContractResolver;
			if (contract == null)
				return;
			
			var field = contract.Infer.PropertyPath(f.Field);
			if (field.IsNullOrEmpty())
				return;

			writer.WriteStartObject();
			{
				writer.WritePropertyName(field);
				writer.WriteStartObject();
				{
					WriteProperty(writer, f, "top_left", f.TopLeft);
					WriteProperty(writer, f, "bottom_right", f.BottomRight);
				}
				writer.WriteEndObject();
				SerializeProperty(writer, serializer, f, "type", f.GeoExecution);
				
				WriteProperty(writer, f, "_cache", f.Cache);
				WriteProperty(writer, f, "_cache_key", f.CacheKey);
				WriteProperty(writer, f, "_name", f.FilterName);
			}
			writer.WriteEndObject();
		}
		
		private static void SerializeProperty(JsonWriter writer, JsonSerializer serializer , IFilter filter, string field, object value)
		{
			if ((field.IsNullOrEmpty() || value == null))
				return;
			writer.WritePropertyName(field);
			serializer.Serialize(writer, value);
		}
		private static void WriteProperty(JsonWriter writer, IFilter filter, string field, object value)
		{
			if ((field.IsNullOrEmpty() || value == null))
				return;
			writer.WritePropertyName(field);
			writer.WriteValue(value);
		}
		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			var filter = new GeoBoundingBoxFilterDescriptor();
			IGeoBoundingBoxFilter f = filter;
			if (reader.TokenType != JsonToken.StartObject)
				return null;

			var depth = reader.Depth;
			while (reader.Read() && reader.Depth >= depth && reader.Value != null)
			{
				var property = reader.Value as string;
				switch(property)
				{
					case "_cache":
						reader.Read();
						f.Cache = reader.Value as bool?;
						break;
					case "_name":
						reader.Read();
						f.FilterName = reader.Value as string;
						break;
					case "_cache_key":
						reader.Read();
						f.CacheKey = reader.Value as string;
						break;
					case "type":
						reader.Read();
						f.GeoExecution = Enum.Parse(typeof(GeoExecution), reader.Value as string) as GeoExecution?;
						break;
					default:
						f.Field = property;
						ReadBox(f, reader);
						break;
				}
			}
			return filter;
		}
		
		private void ReadBox(IGeoBoundingBoxFilter filter, JsonReader reader)
		{
			reader.Read();
			if (reader.TokenType != JsonToken.StartObject)
				return;
			reader.Read();
			var firstProperty = reader.Value as string;
			if (firstProperty == "top_left")
			{
				reader.Read();
				if (reader.ValueType == typeof(string))
				{
					filter.TopLeft = reader.Value as string;
					reader.Read();
					reader.Read();
					filter.BottomRight = reader.Value as string;
				}
				else if (reader.TokenType == JsonToken.StartArray)
				{
					var values = JArray.Load(reader).Values<double>();
					filter.TopLeft = string.Join(", ", values);
					reader.Read();
					reader.Read();
					values = JArray.Load(reader).Values<double>();
					filter.BottomRight =string.Join(", ", values); 
				}
				else if (reader.TokenType == JsonToken.StartObject)
				{
					var latlon = JObject.Load(reader).ToObject<LatLon>();
					filter.TopLeft = "{0}, {1}".F(latlon.Lon, latlon.Lat); 
					reader.Read();
					reader.Read();
					latlon = JObject.Load(reader).ToObject<LatLon>();
					filter.BottomRight = "{0}, {1}".F(latlon.Lon, latlon.Lat); 
				}
			}
			//vertices
			else if (firstProperty == "top")
			{
				reader.Read();
				var top = reader.Value as double?;
				reader.Read();
				reader.Read();
				var left = reader.Value as double?;
				reader.Read();
				reader.Read();
				var bottom = reader.Value as double?;
				reader.Read();
				reader.Read();
				var right = reader.Value as double?;

				filter.TopLeft = "{0}, {1}".F(top, left);
				filter.BottomRight = "{0}, {1}".F(bottom, right);

			}
			reader.Read();
		}
	}
	
}
