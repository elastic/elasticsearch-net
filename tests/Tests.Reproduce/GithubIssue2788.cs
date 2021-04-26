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
using System.Linq;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using Tests.Core.ManagedElasticsearch.Clusters;

namespace Tests.Reproduce
{
	public class GithubIssue2788 : IClusterFixture<WritableCluster>
	{
		private readonly WritableCluster _cluster;

		public GithubIssue2788(WritableCluster cluster) => _cluster = cluster;

		public void CanDeserializeNumberToTimeSpanInInnerHits()
		{
			var indexName = "sample";
			var client = _cluster.Client;

			//create index with automapping
			client.Indices.Create(indexName, create => create
				.Map<Root>(map => map
					.AutoMap()
				)
			);

			var startTime = new TimeSpan(1, 2, 3);
			var endTime = new TimeSpan(2, 3, 4);

			client.Index(new Root
				{
					Children = new[]
					{
						new Child
						{
							StartTime = startTime,
							EndTime = endTime
						}
					}
				}, index => index
					.Index(indexName)
					.Refresh(Refresh.WaitFor)
			);

			var result = client.Search<Root>(search => search
				.Query(query => query
					.Nested(nested => nested
						.Query(nestedQuery => nestedQuery
							.MatchAll()
						)
						.Path(i => i.Children)
						.InnerHits()
					)
				)
				.Index(indexName)
			);

			var child = result.Hits.First().InnerHits.Single().Value.Documents<Child>().Single();

			child.Should().NotBeNull();
			child.StartTime.Should().Be(startTime);
			child.EndTime.Should().Be(endTime);
		}

		// sample mapping with nested objects with TimeSpan field
		private class Root
		{
			[Nested]
			public Child[] Children { get; set; }
		}

		private class Child
		{
			public TimeSpan EndTime { get; set; }
			public TimeSpan StartTime { get; set; }
		}
	}
}
