using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nest
{
	internal class RegisterPercolatorJsonConverter : JsonConverter
	{
		public override bool CanConvert(Type objectType) => objectType == typeof(IRegisterPercolatorRequest);

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			var o = JObject.Load(reader);
			var request = new RegisterPercolatorRequest();

			var query = o.Property("query");
			if (query != null)
				request.Query = query.Value.ToObject<QueryContainer>(serializer);

			var metaProperties = o.Properties().Where(p => p.Name != "query");
			if (metaProperties.Count() > 0)
			{
				request.Metadata = new Dictionary<string, object>();
				foreach(var property in metaProperties)
					request.Metadata.Add(property.Name, property.Value.ToObject<object>());
			}

			return request;
		}

		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			var request = value as IRegisterPercolatorRequest;

			if (request == null) return;

			writer.WriteStartObject();

			if (request.Query != null)
			{
				writer.WritePropertyName("query");
				serializer.Serialize(writer, request.Query);
			}

			if (request.Metadata != null)
			{
				foreach(var kv in request.Metadata)
				{
					writer.WritePropertyName(kv.Key);
					writer.WriteValue(kv.Value);
				}
			}

			writer.WriteEndObject();
		}
	}
}
