using System;
using System.IO;
using System.Linq;
using System.Text;
using Elasticsearch.Net;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Nest.JsonNetSerializer
{
	internal class RevertBackToBuiltinSerializer : JsonConverter
	{
		private readonly IElasticsearchSerializer _builtInSerializer;
		public override bool CanRead => true;
		public override bool CanWrite => true;

		public RevertBackToBuiltinSerializer(IElasticsearchSerializer builtInSerializer)
		{
			_builtInSerializer = builtInSerializer;
		}

		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			var v = _builtInSerializer.SerializeToString(value);
			var token = JToken.Parse(v);
			writer.WriteToken(token.CreateReader(), true);
		}

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			var token = JToken.ReadFrom(reader);
			using (var ms = new MemoryStream(Encoding.UTF8.GetBytes(token.ToString())))
				return _builtInSerializer.Deserialize(objectType, ms);
		}

		private static readonly Type[] NestTypesThatCanAppearInSource = {
			typeof(JoinField),
			typeof(QueryContainer),
			typeof(CompletionField),
			typeof(Attachment)
		};

		public override bool CanConvert(Type objectType) => NestTypesThatCanAppearInSource.Contains(objectType);

	}
}
