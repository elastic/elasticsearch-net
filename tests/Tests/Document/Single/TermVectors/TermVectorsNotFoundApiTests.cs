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
using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using Tests.Core.Extensions;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Domain;
using Tests.Framework.EndpointTests;
using Tests.Framework.EndpointTests.TestState;

namespace Tests.Document.Single.TermVectors
{
	public class TermVectorsNotFoundApiTests
		: ApiIntegrationTestBase<ReadOnlyCluster, TermVectorsResponse, ITermVectorsRequest<Project>, TermVectorsDescriptor<Project>,
			TermVectorsRequest<Project>>
	{
		public TermVectorsNotFoundApiTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override bool ExpectIsValid => false;
		protected override int ExpectStatusCode => 200;
		protected override HttpMethod HttpMethod => HttpMethod.POST;
		protected override bool SupportsDeserialization => false;
		protected override string UrlPath => $"/project/_termvectors/{int.MaxValue}?offsets=true&routing={int.MaxValue}";

		protected override object ExpectJson => new
		{
			filter = new
			{
				max_num_terms = 3,
			}
		};

		protected override Func<TermVectorsDescriptor<Project>, ITermVectorsRequest<Project>> Fluent => d => d
			.Id(int.MaxValue)
			.Routing(int.MaxValue)
			.Offsets()
			.Filter(f => f.MaximimumNumberOfTerms(3));

		protected override TermVectorsRequest<Project> Initializer => new TermVectorsRequest<Project>(int.MaxValue)
		{
			Routing = int.MaxValue,
			Offsets = true,
			Filter = new TermVectorFilter
			{
				MaximumNumberOfTerms = 3,
			}
		};

		protected override LazyResponses ClientUsage() => Calls(
			(client, f) => client.TermVectors(f),
			(client, f) => client.TermVectorsAsync(f),
			(client, r) => client.TermVectors(r),
			(client, r) => client.TermVectorsAsync(r)
		);

		protected override TermVectorsDescriptor<Project> NewDescriptor() => new TermVectorsDescriptor<Project>(typeof(Project));

		protected override void ExpectResponse(TermVectorsResponse response)
		{
			response.ShouldNotBeValid();
			response.Found.Should().BeFalse();
		}
	}
}
