using System;
using Elasticsearch.Net;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Nest
{
	[ContractJsonConverter(typeof(SqlValueJsonConverter))]
	public class SqlValue : LazyDocument
	{
		internal SqlValue(JToken token, IElasticsearchSerializer sourceSerializer, IElasticsearchSerializer requestResponseSerializer)
			: base(token, sourceSerializer, requestResponseSerializer) { }
	}

	internal class SqlValueJsonConverter : LazyDocumentJsonConverter
	{
		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			var sourceSerializer = serializer.GetConnectionSettings().SourceSerializer;
			var requestResponseSerializer = serializer.GetConnectionSettings().RequestResponseSerializer;
			var token = reader.ReadTokenWithDateParseHandlingNone();
			return new SqlValue(token, sourceSerializer, requestResponseSerializer);
		}
	}
}
