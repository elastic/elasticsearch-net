using System;
using System.Collections.Generic;
using Elasticsearch.Net;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace Nest.JsonNetSerializer
{
	public class JsonNetSerializer : ConnectionSettingsAwareSerializerBase
	{
		public JsonNetSerializer(
			IElasticsearchSerializer builtinSerializer,
			IConnectionSettingsValues connectionSettings,
			Func<JsonSerializerSettings> jsonSerializerSettingsFactory = null,
			Action<ConnectionSettingsAwareContractResolver> modifyContractResolver = null,
			IEnumerable<JsonConverter> contractJsonConverters = null
		)
			: base(builtinSerializer, connectionSettings, jsonSerializerSettingsFactory, modifyContractResolver, contractJsonConverters) { }

		public static IElasticsearchSerializer Default(IElasticsearchSerializer builtin, IConnectionSettingsValues values)
			=> new JsonNetSerializer(builtin, values);
	}
}
