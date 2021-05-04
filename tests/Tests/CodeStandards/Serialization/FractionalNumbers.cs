// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Elastic.Transport;
using Elastic.Transport.Extensions;
using FluentAssertions;
using Nest;

namespace Tests.CodeStandards.Serialization
{
	public class FractionalNumbers
	{
		private readonly ITransportSerializer _serializer;

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
