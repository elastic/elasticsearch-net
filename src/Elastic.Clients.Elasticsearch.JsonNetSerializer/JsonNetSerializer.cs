// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
using Elastic.Transport;
using Elastic.Clients.Elasticsearch;
using Newtonsoft.Json;

namespace Elastic.Clients.JsonNetSerializer
{
	public class JsonNetSerializer : ConnectionSettingsAwareSerializer
	{
		public JsonNetSerializer(
			Serializer builtinSerializer,
			IElasticsearchClientSettings connectionSettings,
			Func<JsonSerializerSettings> jsonSerializerSettingsFactory = null,
			Action<ConnectionSettingsAwareContractResolver> modifyContractResolver = null,
			IEnumerable<JsonConverter> contractJsonConverters = null
		)
			: base(builtinSerializer, connectionSettings, jsonSerializerSettingsFactory, modifyContractResolver, contractJsonConverters) { }

		public static JsonNetSerializer Default(Serializer builtin, IElasticsearchClientSettings values) => new(builtin, values);
	}
}
