using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using Elasticsearch.Net;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Nest.JsonNetSerializer.Converters
{
	public class HandleNestTypesOnSourceJsonConverter : JsonConverter
	{
		private readonly IElasticsearchSerializer _builtInSerializer;
		public override bool CanRead => true;
		public override bool CanWrite => true;

		public HandleNestTypesOnSourceJsonConverter(IElasticsearchSerializer builtInSerializer)
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
			//in place because JsonConverter.Deserialize() only works on full json objects.
			//even though we pass type JSON.NET won't try the registered converter for that type
			//even if it can handle string tokens :(
			if (objectType == typeof(JoinField) && token.Type == JTokenType.String)
				return JoinField.Root(token.ToString());

			using (var ms = new MemoryStream(Encoding.UTF8.GetBytes(token.ToString())))
				return _builtInSerializer.Deserialize(objectType, ms);
		}

		private static readonly Type[] NestTypesThatCanAppearInSource = {
			typeof(JoinField),
			typeof(QueryContainer),
			typeof(CompletionField),
			typeof(Attachment),
			typeof(ILazyDocument)
		};

		public override bool CanConvert(Type objectType) => NestTypesThatCanAppearInSource.Contains(objectType);
	}
}
