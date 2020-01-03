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
