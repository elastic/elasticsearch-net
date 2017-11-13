using System;
using System.IO;
using System.Reflection;
using System.Text;
using Newtonsoft.Json;
using Elasticsearch.Net;
using Newtonsoft.Json.Linq;
using static Elasticsearch.Net.SerializationFormatting;

namespace Nest
{
	internal abstract class DocumentJsonConverterBase<TRequest> : JsonConverter where TRequest : IUntypedDocumentRequest
	{
		private readonly Type _genericRequestType;

		protected DocumentJsonConverterBase(Type genericRequestType)
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
			var untypedDocumentRequest = (IUntypedDocumentRequest)value;
			var o = untypedDocumentRequest.UntypedDocument;
			var f = writer.Formatting == Formatting.Indented ? Indented : None;
			var v = serializer.GetConnectionSettings().SourceSerializer.SerializeToString(o, f);
			writer.WriteRawValue(v);

		}
	}
}
