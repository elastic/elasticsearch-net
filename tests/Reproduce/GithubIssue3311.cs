// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Collections.Generic;
using System.Linq;
using Elastic.Xunit.XunitPlumbing;
using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using Tests.Framework.ManagedElasticsearch.Clusters;

namespace Tests.Reproduce
{
	public class GithubIssue3314 : IClusterFixture<WritableCluster>
	{
		private readonly WritableCluster _cluster;
		public GithubIssue3314(WritableCluster cluster) => _cluster = cluster;

		[I]
		public void ValueAggregateWhenNullShouldStillReturn()
		{
			var indexName = "max-bucket-reproduce";

			var ec = this._cluster.Client;
			var now = DateTime.Now;
			var testdata = new DateTime[] { }
				.Concat(Enumerable.Repeat(now.AddMinutes(1), 3))
				.Concat(Enumerable.Repeat(now.AddMinutes(2), 4))
				.Concat(Enumerable.Repeat(now.AddMinutes(3), 1))
				.Select((d, i) => new MyClass() { Id = i, Time = d, Message = "test", MessageType = 1 })
				.ToArray();
			ec.DeleteIndex(indexName);
			ec.IndexMany(testdata, indexName);
			ec.Refresh(indexName);

			var res = ec
				.Search<MyClass>(s => s
					.Index(indexName)
					.Query(q => q
						.Term(t => t
								.Field(ff => ff.MessageType)
								.Value(2) //none of the docs has value 2 for messageType
						)
					)
					.Aggregations(agg => agg
						.DateHistogram("hist", dh => dh
							.Field(ff => ff.Time)
							.Interval(new Time(TimeSpan.FromMinutes(1)))
						)
						.MaxBucket("max", mb => mb
							.BucketsPath("hist>_count")
						)
					)
				);

			var max = (KeyedValueAggregate)res.Aggregations["max"];
			max.Keys.Should().BeEmpty();
			max.Value.Should().BeNull();
		}

		public class MyClass
		{
			public int Id { get; set; }
			public DateTime Time { get; set; }
			public int MessageType { get; set; }
			public string Message { get; set; }
		}
	}

}
