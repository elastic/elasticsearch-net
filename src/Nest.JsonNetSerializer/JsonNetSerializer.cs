// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Collections.Generic;
using Elasticsearch.Net;
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
