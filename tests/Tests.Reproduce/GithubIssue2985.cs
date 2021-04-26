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
using Tests.Core.ManagedElasticsearch.Clusters;

namespace Tests.Reproduce
{
	public class GithubIssue2985 : IClusterFixture<WritableCluster>
	{
		private readonly WritableCluster _cluster;

		public GithubIssue2985(WritableCluster cluster) => _cluster = cluster;

		protected static string RandomString() => Guid.NewGuid().ToString("N").Substring(0, 8);

		[I]
		public void BadRequestErrorShouldBeWrappedInElasticsearchClientException()
		{
			var client = _cluster.Client;
			var index = $"gh2985-{RandomString()}";
			var response = client.Indices.Create(index, i => i
				.Settings(s => s
					.Analysis(a => a
						.Analyzers(an => an
							.Custom("custom", c => c.Filters("ascii_folding").Tokenizer("standard"))
						)
					)
				)
			);
			response.OriginalException.Should().NotBeNull().And.BeOfType<ElasticsearchClientException>();
			response.OriginalException.Message.Should()
				.Contain(
					"Type: illegal_argument_exception Reason: \"Custom Analyzer [custom] failed to find filter under name [ascii_folding]\""
				);

			client.Indices.Delete(index);
		}
	}
}
