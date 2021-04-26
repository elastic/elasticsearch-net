/*
 * Licensed to Elasticsearch B.V. under one or more contributor
 * license agreements. See the NOTICE file distributed with
 * this work for additional information regarding copyright
 * ownership. Elasticsearch B.V. licenses this file to you under
 * the Apache License, Version 2.0 (the "License"); you may
 * not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *    http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing,
 * software distributed under the License is distributed on an
 * "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY
 * KIND, either express or implied.  See the License for the
 * specific language governing permissions and limitations
 * under the License.
 */

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
