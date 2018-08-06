using System;
using System.Collections.Generic;
using System.Text;
using Elastic.Xunit.XunitPlumbing;
using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using Nest.JsonNetSerializer;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Tests.Framework;

namespace Tests.Reproduce
{
	// from https://stackoverflow.com/questions/49224866/elasticsearch-nest-6-storing-enums-as-string
	public class JsonNetSerializerConverters
	{
		[U] public void JsonConvertersInJsonSerializerSettingsAreHonoured()
		{
			var pool = new SingleNodeConnectionPool(new Uri("http://localhost:9200"));
			var connectionSettings = new ConnectionSettings(pool, new InMemoryConnection(), (builtin, settings) =>
				new JsonNetSerializer(builtin, settings,
					() => new JsonSerializerSettings
					{
						Converters = new List<JsonConverter> { new StringEnumConverter() }
					})).DisableDirectStreaming();

			var client = new ElasticClient(connectionSettings);

			var indexResponse = client.Index(new Product { ProductType = ProductType.Example }, i => i.Index("examples"));
			Encoding.UTF8.GetString(indexResponse.ApiCall.RequestBodyInBytes).Should().Contain("\"productType\":\"Example\"");
		}

		public class Product
		{
			public ProductType ProductType { get; set; }
		}

		public enum ProductType
		{
			Example
		}
	}
}
