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

using System;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
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
