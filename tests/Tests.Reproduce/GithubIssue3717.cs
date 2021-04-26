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
using System.Text;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Elasticsearch.Net;
using FluentAssertions;
using Nest;

namespace Tests.Reproduce
{
	public class GithubIssue3717
	{
		[U]
		public void CanDeserializeEmptyAggregation()
		{
			var json = @"{
  ""took"" : 2,
  ""timed_out"" : false,
  ""_shards"" : {
    ""total"" : 3,
    ""successful"" : 3,
    ""skipped"" : 0,
    ""failed"" : 0
  },
  ""hits"" : {
    ""total"" : {
      ""value"" : 10000,
      ""relation"" : ""gte""
    },
    ""max_score"" : null,
    ""hits"" : [ ]
  },
  ""aggregations"" : {
    ""geo_bounds#viewport"" : { }
  }
}";

			var bytes = Encoding.UTF8.GetBytes(json);

			var pool = new SingleNodeConnectionPool(new Uri("http://localhost:9200"));
			var connectionSettings = new ConnectionSettings(pool, new InMemoryConnection(bytes));
			var client = new ElasticClient(connectionSettings);

			Func<ISearchResponse<TestEntity>> response = () => client.Search<TestEntity>(s => s
				.Size(0)
				.Index("issue")
				.Aggregations(q => q
					.GeoBounds("viewport", t => t
						.Field(f => f.TestProperty)))
			);

			response.Should().NotThrow();
		}

		public class TestEntity {
			public string TestProperty { get; set; }
		}
	}
}
