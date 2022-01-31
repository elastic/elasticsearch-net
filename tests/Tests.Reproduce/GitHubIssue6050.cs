// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Text;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Elasticsearch.Net;
using FluentAssertions;
using Nest;

namespace Tests.Reproduce
{
	public class GitHubIssue6050
	{
		private static readonly byte[] ResponseBytes = Encoding.UTF8.GetBytes(@"{
  ""took"" : 1,
  ""timed_out"" : false,
  ""_shards"" : {
    ""total"" : 1,
    ""successful"" : 1,
    ""skipped"" : 0,
    ""failed"" : 0
  },
  ""hits"" : {
    ""total"" : {
      ""value"" : 0,
      ""relation"" : ""eq""
    },
    ""max_score"" : null,
    ""hits"" : [ ]
  },
  ""aggregations"" : {
    ""summary_boxplot"" : {
      ""min"" : ""Infinity"",
      ""max"" : ""-Infinity"",
      ""q1"" : ""NaN"",
      ""q2"" : ""NaN"",
      ""q3"" : ""NaN"",
      ""lower"" : ""NaN"",
      ""upper"" : ""-Infinity""
    }
  }
}");

		[U]
		public void BoxplotHandlesNaNValues()
		{
			var pool = new SingleNodeConnectionPool(new Uri($"http://localhost:9200"));
			var settings = new ConnectionSettings(pool, new InMemoryConnection(ResponseBytes));
			var client = new ElasticClient(settings);

			var response = client.Search<TestData>(s => s
				.Size(0)
				.Index("test")
				.Aggregations(a => a
					.Boxplot("summary_boxplot", mt => mt.Field(f => f.Population))));

			var boxplot = response.Aggregations.Boxplot("summary_boxplot");
			
			double.IsNaN(boxplot.Lower).Should().BeTrue();
			double.IsNaN(boxplot.Q1).Should().BeTrue();
			double.IsNaN(boxplot.Q2).Should().BeTrue();
			double.IsNaN(boxplot.Q3).Should().BeTrue();
			double.IsInfinity(boxplot.Min).Should().BeTrue();
			double.IsNegativeInfinity(boxplot.Max).Should().BeTrue();
			double.IsNegativeInfinity(boxplot.Upper).Should().BeTrue();
		}

		private class TestData
		{
			public long Population { get; set; }
		}
	}
}
