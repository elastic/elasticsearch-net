// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using Elastic.Transport;
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
