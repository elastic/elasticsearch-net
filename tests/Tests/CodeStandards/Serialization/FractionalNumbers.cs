using System;
using Elastic.Xunit.XunitPlumbing;
using Elasticsearch.Net;
using FluentAssertions;
using Nest;

namespace Tests.CodeStandards.Serialization
{
	public class FractionalNumbers
	{
		private readonly IElasticsearchSerializer _serializer;

		public FractionalNumbers()
		{
			var pool = new SingleNodeConnectionPool(new Uri("http://localhost:9200"));
			var settings = new ConnectionSettings(pool, new InMemoryConnection());
			var client = new ElasticClient(settings);
			_serializer = client.RequestResponseSerializer;
		}
		
		[U]
		public void SerializeDouble()
		{
			var poco = new
			{
				Whole = 1d,
				Fractional = 1.1d
			};

			var serialized = _serializer.SerializeToString(poco);
			serialized.Should().Be("{\"whole\":1.0,\"fractional\":1.1}");
		}

		[U]
		public void SerializeFloat()
		{
			var poco = new
			{
				Whole = 1f,
				Fractional = 1.1f
			};

			var serialized = _serializer.SerializeToString(poco);
			serialized.Should().Be("{\"whole\":1.0,\"fractional\":1.1}");
		}

		[U]
		public void SerializeDecimal()
		{
			var poco = new
			{
				Whole = 1m,
				Fractional = 1.1m
			};

			var serialized = _serializer.SerializeToString(poco);
			serialized.Should().Be("{\"whole\":1.0,\"fractional\":1.1}");
		}
	}
}
