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
using Elastic.Transport;
using FluentAssertions;
using Nest;
using Tests.Core.Extensions;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Domain;
using Tests.Framework.EndpointTests;
using Tests.Framework.EndpointTests.TestState;

namespace Tests.Search.Explain
{
	public class ExplainApiTests
		: ApiIntegrationTestBase<ReadOnlyCluster, ExplainResponse<Project>, IExplainRequest<Project>, ExplainDescriptor<Project>,
			ExplainRequest<Project>>
	{
		private readonly Project _project = new Project { Name = Project.Instance.Name };

		public ExplainApiTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override bool ExpectIsValid => true;

		protected override object ExpectJson => new
		{
			query = new
			{
				match = new
				{
					name = new
					{
						query = Project.Instance.Name
					}
				}
			}
		};

		protected override int ExpectStatusCode => 200;

		protected override Func<ExplainDescriptor<Project>, IExplainRequest<Project>> Fluent => e => e
			.Routing(Project.Instance.Name)
			.SourceEnabled()
			.Query(q => q
				.Match(m => m
					.Field(p => p.Name)
					.Query(Project.Instance.Name)
				)
			);

		protected override HttpMethod HttpMethod => HttpMethod.POST;

		protected override ExplainRequest<Project> Initializer => new ExplainRequest<Project>(_project)
		{
			Routing = Project.Instance.Name,
			SourceEnabled = true,
			Query = new QueryContainer(new MatchQuery
			{
				Field = "name",
				Query = Project.Instance.Name
			})
		};

		protected override bool SupportsDeserialization => false;

		protected override string UrlPath =>
			$"/project/_explain/{U(Project.Instance.Name)}?_source=true&routing={U(Project.Instance.Name)}";

		protected override LazyResponses ClientUsage() => Calls(
			(c, f) => c.Explain(_project, f),
			(c, f) => c.ExplainAsync(_project, f),
			(c, r) => c.Explain<Project>(r),
			(c, r) => c.ExplainAsync<Project>(r)
		);

		protected override ExplainDescriptor<Project> NewDescriptor() => new ExplainDescriptor<Project>(_project);

		protected override void ExpectResponse(ExplainResponse<Project> response)
		{
			response.IsValid.Should().BeTrue();
			response.Matched.Should().BeTrue();
			response.Get.Should().NotBeNull();
			response.Get.Source.ShouldAdhereToSourceSerializerWhenSet();
		}
	}
}
