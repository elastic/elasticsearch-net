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

using System.Collections.Generic;
using Elasticsearch.Net;
using Nest;
using Nest.JsonNetSerializer;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Tests.Domain;

namespace Tests.Core.Client.Serializers
{
	public class TestSourceSerializerBase : ConnectionSettingsAwareSerializerBase
	{
		public TestSourceSerializerBase(IElasticsearchSerializer builtinSerializer, IConnectionSettingsValues connectionSettings)
			: base(builtinSerializer, connectionSettings) { }

		protected override JsonSerializerSettings CreateJsonSerializerSettings() =>
			new JsonSerializerSettings
			{
				NullValueHandling = NullValueHandling.Ignore,
				DefaultValueHandling = DefaultValueHandling.Include
			};

		protected override IEnumerable<JsonConverter> CreateJsonConverters()
		{
			yield return new SourceOnlyUsingBuiltInConverter();
			yield return new Domain.JsonConverters.DateTimeConverter();
		}

		protected override void ModifyContractResolver(ConnectionSettingsAwareContractResolver resolver) =>
			resolver.NamingStrategy = new CamelCaseNamingStrategy();
	}
}
