// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Collections.Generic;
using System.Text;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Elastic.Transport;
using FluentAssertions;
using Nest;
using Nest.JsonNetSerializer;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;


namespace Tests.Reproduce
{
	// from https://stackoverflow.com/questions/49224866/elasticsearch-nest-6-storing-enums-as-string
	public class JsonNetSerializerConverters
	{
		public enum ProductType
		{
			Example
		}

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
	}
}
