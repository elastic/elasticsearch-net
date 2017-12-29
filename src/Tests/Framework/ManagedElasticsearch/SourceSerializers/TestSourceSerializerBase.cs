using System.Collections.Generic;
using Elasticsearch.Net;
using Nest;
using Nest.JsonNetSerializer;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using Tests.Framework.MockData;

namespace Tests.Framework.ManagedElasticsearch.SourceSerializers
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
		}

		protected override void ModifyContractResolver(ConnectionSettingsAwareContractResolver resolver)
		{
			resolver.NamingStrategy = new CamelCaseNamingStrategy();
		}

	}
}
