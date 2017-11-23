using System;
using System.IO;
using System.Reflection;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using static Elasticsearch.Net.SerializationFormatting;

namespace Nest
{
	internal abstract class GenericProxyRequestConverterBase : JsonConverter
	{
		private readonly Type _genericRequestType;

		protected GenericProxyRequestConverterBase(Type genericRequestType)
		{
			_genericRequestType = genericRequestType;
		}

		public override bool CanRead => true;
		public override bool CanWrite => true;
		public override bool CanConvert(Type objectType) => true;

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			var token = JToken.ReadFrom(reader);
			using (var ms = new MemoryStream(Encoding.UTF8.GetBytes(token.ToString())))
			{
                //not optimized but deserializing create requests is far from common practice
                var genericType = objectType.GetTypeInfo().GenericTypeArguments[0];
				var o = serializer.GetConnectionSettings().SourceSerializer.Deserialize(genericType, ms);
				var path = typeof(DocumentPath<>).CreateGenericInstance(genericType, o);
                // index, type and id are optional parameters on _genericRequestType but need to be passed to construct through reflection
                var x = _genericRequestType.CreateGenericInstance(genericType, path, null, null, null);
                return x;
			}
		}

		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			var untypedDocumentRequest = (IProxyRequest)value;
			var f = writer.Formatting == Formatting.Indented ? Indented : None;
			using (var ms = new MemoryStream())
			{
				untypedDocumentRequest.WriteJson(serializer.GetConnectionSettings().SourceSerializer, ms, f);
				var v = Encoding.UTF8.GetString(ms.ToArray());
				writer.WriteRawValue(v);
			}
		}
	}
}
