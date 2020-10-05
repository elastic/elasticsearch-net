// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Transport;
using FluentAssertions;
using Nest;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Domain;
using Tests.Framework.EndpointTests;
using Tests.Framework.EndpointTests.TestState;

namespace Tests.Search.Validate
{
	public class ValidateQueryApiTests
		: ApiIntegrationTestBase<ReadOnlyCluster, ValidateQueryResponse, IValidateQueryRequest, ValidateQueryDescriptor<Project>,
			ValidateQueryRequest<Project>>
	{
		public ValidateQueryApiTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override bool ExpectIsValid => true;
		protected override int ExpectStatusCode => 200;
		protected override HttpMethod HttpMethod => HttpMethod.POST;
		protected override string UrlPath => "/project/_validate/query";

		protected override LazyResponses ClientUsage() => Calls(
			(c, f) => c.Indices.ValidateQuery<Project>(v => v.Query(q => q.MatchAll())),
			(c, f) => c.Indices.ValidateQueryAsync<Project>(v => v.Query(q => q.MatchAll())),
			(c, r) => c.Indices.ValidateQuery(new ValidateQueryRequest<Project> { Query = new QueryContainer(new MatchAllQuery()) }),
			(c, r) => c.Indices.ValidateQueryAsync(new ValidateQueryRequest<Project> { Query = new QueryContainer(new MatchAllQuery()) })
		);
	}

	public class ValidateInvalidQueryApiTests
		: ApiIntegrationTestBase<ReadOnlyCluster, ValidateQueryResponse, IValidateQueryRequest, ValidateQueryDescriptor<Project>,
			ValidateQueryRequest<Project>>
	{
		private readonly ValidateQueryDescriptor<Project> _descriptor = new ValidateQueryDescriptor<Project>()
			.Query(q => q
				.Match(m => m
					.Field(p => p.StartedOn)
					.Query("shouldbeadate")
				)
			);

		private readonly ValidateQueryRequest<Project> _request = new ValidateQueryRequest<Project>
		{
			Query = new QueryContainer(
				new MatchQuery
				{
					Field = "startedOn",
					Query = "shouldbeadate"
				}
			)
		};

		public ValidateInvalidQueryApiTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override bool ExpectIsValid => true;
		protected override int ExpectStatusCode => 200;
		protected override HttpMethod HttpMethod => HttpMethod.POST;
		protected override string UrlPath => "/project/_validate/query";

		protected override LazyResponses ClientUsage() => Calls(
			(c, f) => c.Indices.ValidateQuery<Project>(v => _descriptor),
			(c, f) => c.Indices.ValidateQueryAsync<Project>(v => _descriptor),
			(c, r) => c.Indices.ValidateQuery(_request),
			(c, r) => c.Indices.ValidateQueryAsync(_request)
		);

		protected override void ExpectResponse(ValidateQueryResponse response) => response.Valid.Should().BeFalse();
	}
}
