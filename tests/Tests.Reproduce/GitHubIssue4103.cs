// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Text;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Elastic.Transport;
using FluentAssertions;
using Nest;

namespace Tests.Reproduce
{
	public class GithubIssue4013
	{
		[U]
		public void CanDeserializeExtendedStats()
		{
			var json = @"{
			  ""took"" : 1,
				  ""timed_out"" : false,
				  ""_shards"" : {
				    ""total"" : 2,
				    ""successful"" : 2,
				    ""skipped"" : 0,
				    ""failed"" : 0
				  },
				  ""hits"" : {
				    ""total"" : 1100,
				    ""max_score"" : 0.0,
				    ""hits"" : [ ]
				  },
			  ""aggregations"": {
				  ""extended_stats#1"": {
			        ""count"": 3,
			        ""min"": 1569521764937,
			        ""max"": 1569526264937,
			        ""avg"": 1569524464937,
			        ""sum"": 4708573394811,
			        ""min_as_string"": ""2019-09-26T18:16:04.937Z"",
			        ""max_as_string"": ""2019-09-26T19:31:04.937Z"",
			        ""avg_as_string"": ""2019-09-26T19:01:04.937Z"",
			        ""sum_as_string"": ""2119-03-18T09:03:14.811Z"",
			        ""sum_of_squares"": 7.390221138118668e+24,
			        ""variance"": 3779929134421.3335,
			        ""std_deviation"": 1944203.9847766317,
			        ""std_deviation_bounds"": {
			            ""upper"": 1569528353344.9695,
			            ""lower"": 1569520576529.0305
			        },
			        ""sum_of_squares_as_string"": ""292278994-08-17T07:12:55.807Z"",
			        ""variance_as_string"": ""2089-10-12T04:18:54.421Z"",
			        ""std_deviation_as_string"": ""1970-01-01T00:32:24.203Z"",
			        ""std_deviation_bounds_as_string"": {
			            ""upper"": ""2019-09-26T20:05:53.344Z"",
			            ""lower"": ""2019-09-26T17:56:16.529Z""
			        }
			      }
			  }
			}";

			var bytes = Encoding.UTF8.GetBytes(json);
			var pool = new SingleNodeConnectionPool(new Uri("http://localhost:9200"));
			var connectionSettings = new ConnectionSettings(pool, new InMemoryConnection(bytes));
			var client = new ElasticClient(connectionSettings);

			Action searchResponse = () => client.Search<object>(s => s.AllIndices());

			searchResponse.Should().NotThrow();

			var response = client.Search<object>(s => s.AllIndices());

			var extendedStats = response.Aggregations.ExtendedStats("1");
			extendedStats.Should().NotBeNull();
			extendedStats.Count.Should().Be(3);
			extendedStats.Min.Should().Be(1569521764937);
			extendedStats.Max.Should().Be(1569526264937);
			extendedStats.Average.Should().Be(1569524464937);
			extendedStats.Sum.Should().Be(4708573394811);
			extendedStats.SumOfSquares.Should().Be(7.390221138118668e+24);
			extendedStats.Variance.Should().Be(3779929134421.3335);
			extendedStats.StdDeviation.Should().Be(1944203.9847766317);
			extendedStats.StdDeviationBounds.Should().NotBeNull();
			extendedStats.StdDeviationBounds.Upper.Should().Be(1569528353344.9695);
			extendedStats.StdDeviationBounds.Lower.Should().Be(1569520576529.0305);
		}
	}
}
