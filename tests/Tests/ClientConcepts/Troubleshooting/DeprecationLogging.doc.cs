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
using System.Collections.Generic;
using Elastic.Elasticsearch.Xunit.Sdk;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Domain;
using Tests.Framework;
using Tests.Framework.DocumentationTests;
using Xunit;
using static Nest.Infer;

namespace Tests.ClientConcepts.Troubleshooting
{
	/**
	 * === Deprecation logging
	 *
	 * Elasticsearch will send back `Warn` HTTP Headers when you are using an API feature that is
	 * deprecated and will be removed or rewritten in a future release.
	 *
	 * Elasticsearch.NET and NEST report these back to you so you can log and watch out for them.
	 */
	public class DeprecationLogging : IntegrationDocumentationTestBase, IClusterFixture<ReadOnlyCluster>
	{
		public DeprecationLogging(ReadOnlyCluster cluster) : base(cluster) { }

		[I] public void RequestWithMultipleWarning()
		{
			var request = new SearchRequest<Project>
			{
				Size = 0,
				Routing = new [] { "ignoredefaultcompletedhandler" },
				Aggregations = new TermsAggregation("states")
				{
					Field = Field<Project>(p => p.State.Suffix("keyword")),
					Order = new List<TermsOrder>
					{
						new TermsOrder { Key = "_term", Order = SortOrder.Ascending },
					}
				},
				Query = new FunctionScoreQuery()
				{
					Query = new MatchAllQuery { },
					Functions = new List<IScoreFunction>
					{
						new RandomScoreFunction {Seed = 1337},
					}
				}
			};
			var response = this.Client.Search<Project>(request);

			response.ApiCall.DeprecationWarnings.Should().NotBeNullOrEmpty();
			response.ApiCall.DeprecationWarnings.Should().HaveCountGreaterOrEqualTo(2);
			response.DebugInformation.Should().Contain("Deprecated aggregation order key"); // <1> `DebugInformation` also contains the deprecation warnings
		}
	}
}
