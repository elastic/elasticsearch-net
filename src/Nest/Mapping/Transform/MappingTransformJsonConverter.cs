using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nest
{
	internal class MappingTransformCollectionJsonConverter : JsonConverter
	{
		public override bool CanWrite { get { return false; } }

		public override bool CanConvert(Type objectType) => objectType == typeof(IMappingTransform);

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			var o = JToken.Load(reader);

			if (o is JArray)
			{
				return o.ToObject<IList<IMappingTransform>>();
			}
			else if (o is JObject)
			{
				var transform = o.ToObject<IMappingTransform>();
				return new List<IMappingTransform> { transform };
			}

			return null;
		}

		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			throw new NotImplementedException();
		}
	}
}
