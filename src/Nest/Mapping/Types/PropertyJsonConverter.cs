using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Nest
{
	internal class PropertyJsonConverter : JsonConverter
	{
		public override bool CanWrite
		{
			get { return false; }
		}

		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			throw new NotSupportedException();
		}

		private IProperty GetTypeFromJObject(JObject po, JsonSerializer serializer)
		{
			JToken typeToken;
			JToken propertiesToken;
			var type = string.Empty;

			var hasType = po.TryGetValue("type", out typeToken);
			if (hasType)
				type = typeToken.Value<string>().ToLowerInvariant();
			else if (po.TryGetValue("properties", out propertiesToken))
				type = "object";

			serializer.TypeNameHandling = TypeNameHandling.None;

			switch (type)
			{
				case "string":
					return serializer.Deserialize(po.CreateReader(), typeof(StringProperty)) as StringProperty;
				case "float":
				case "double":
				case "byte":
				case "short":
				case "integer":
				case "long":
					return serializer.Deserialize(po.CreateReader(), typeof(NumberProperty)) as NumberProperty;
				case "date":
					return serializer.Deserialize(po.CreateReader(), typeof(DateProperty)) as DateProperty;
				case "boolean":
					return serializer.Deserialize(po.CreateReader(), typeof(BooleanProperty)) as BooleanProperty;
				case "binary":
					return serializer.Deserialize(po.CreateReader(), typeof(BinaryProperty)) as BinaryProperty;
				case "object":
					return serializer.Deserialize(po.CreateReader(), typeof(ObjectProperty)) as ObjectProperty;
				case "nested":
					return serializer.Deserialize(po.CreateReader(), typeof(NestedProperty)) as NestedProperty;
				case "ip":
					return serializer.Deserialize(po.CreateReader(), typeof(IpProperty)) as IpProperty;
				case "geo_point":
					return serializer.Deserialize(po.CreateReader(), typeof(GeoPointProperty)) as GeoPointProperty;
				case "geo_shape":
					return serializer.Deserialize(po.CreateReader(), typeof(GeoShapeProperty)) as GeoShapeProperty;
				case "attachment":
					return serializer.Deserialize(po.CreateReader(), typeof(AttachmentProperty)) as AttachmentProperty;
			}

			return null;
		}
		public override object ReadJson(JsonReader reader, Type objectType, object existingValue,
										JsonSerializer serializer)
		{
			JObject o = JObject.Load(reader);

			var esType = this.GetTypeFromJObject(o, serializer);
			return esType;
		}

		public override bool CanConvert(Type objectType)
		{
			return objectType == typeof(IProperty);
		}

	}
	
}