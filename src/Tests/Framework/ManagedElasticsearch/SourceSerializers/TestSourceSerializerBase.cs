using System.Collections.Generic;
using Elasticsearch.Net;
using Nest.JsonNetSerializer;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Tests.Framework.MockData;

namespace Tests.Framework.ManagedElasticsearch.SourceSerializers
{
	public class TestSourceSerializerBase : JsonNetSourceSerializerBase
	{
		public TestSourceSerializerBase(IElasticsearchSerializer builtinSerializer) : base(builtinSerializer) { }

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

		protected override IContractResolver CreateContractResolver() =>
			new DefaultContractResolver {NamingStrategy = new CamelCaseNamingStrategy()};
	}
}
