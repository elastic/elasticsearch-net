using System.Collections.Generic;
using Elasticsearch.Net;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Tests.Framework.MockData;

namespace Tests.Framework.ManagedElasticsearch.SourceSerializers
{
	public class TestSourceSerializer : CustomJsonNetSourceSerializer
	{
		public TestSourceSerializer(IElasticsearchSerializer builtinSerializer)
			: base(builtinSerializer) { }

		protected override JsonSerializerSettings CreateJsonSerializerSettings()
		{
			return new JsonSerializerSettings
			{
				NullValueHandling = NullValueHandling.Ignore,
				DefaultValueHandling = DefaultValueHandling.Include
			};
		}

		protected override IEnumerable<JsonConverter> CreateJsonConverters()
		{
			yield return new SourceOnlyUsingBuiltInConverter();
		}

		protected override IContractResolver CreateContractResolver()
		{
			return new DefaultContractResolver {NamingStrategy = new CamelCaseNamingStrategy()};
		}
	}
}
