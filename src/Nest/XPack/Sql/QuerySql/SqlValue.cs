using System;
using Elasticsearch.Net;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Nest
{
	[ContractJsonConverter(typeof(SqlValueJsonConverter))]
	public class SqlValue : LazyDocument
	{
		internal SqlValue(JToken token, IElasticsearchSerializer serializer) : base(token, serializer) { }
	}

	internal class SqlValueJsonConverter : LazyDocumentJsonConverter
	{
		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			var sourceSerializer = serializer.GetConnectionSettings().SourceSerializer;
			var token = reader.ReadTokenWithDateParseHandlingNone();
			return new SqlValue(token, sourceSerializer);
		}
	}
}
