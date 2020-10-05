// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Collections.Generic;
using Elastic.Transport;
using Nest;
using Nest.JsonNetSerializer;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Tests.Domain;

namespace Tests.Core.Client.Serializers
{
	public class TestSourceSerializerBase : ConnectionSettingsAwareSerializerBase
	{
		public TestSourceSerializerBase(ITransportSerializer builtinSerializer, IConnectionSettingsValues connectionSettings)
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
