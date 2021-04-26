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

using System.Linq;
using System.Text;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using FluentAssertions;
using Tests.Core.Client;

namespace Tests.Reproduce
{
	public class GithubIssue4582
	{
		[U]
		public void DeserializeBucketKeyWithHash()
		{
			var json = @"
{
  ""hits"": {
  },
  ""aggregations"":
  {
    ""some_agg"" : {
      ""buckets"" : {
        ""value1"" : {
          ""doc_count"" : 0
        },
        ""value2"" : {
          ""doc_count"" : 0
        },
        ""value3#something else"" : {
          ""doc_count"" : 0
        }
      }
    }
  }
}
";

			var bytes = Encoding.UTF8.GetBytes(json);
			var client = TestClient.FixedInMemoryClient(bytes);
			var response = client.Search<object>();

			var filters = response.Aggregations
				.Filters("some_agg")
				.Select(x => x.Key)
				.ToList();

			filters[2].Should().Be("value3#something else");
		}
	}
}
