using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Domain;
using Tests.Framework;
using Tests.Framework.Integration;

namespace Tests.Search.Validate
{
	public class ValidateQueryApiTests
		: ApiIntegrationTestBase<ReadOnlyCluster, IValidateQueryResponse, IValidateQueryRequest, ValidateQueryDescriptor<Project>,
			ValidateQueryRequest<Project>>
	{
		public ValidateQueryApiTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override bool ExpectIsValid => true;
		protected override int ExpectStatusCode => 200;
		protected override HttpMethod HttpMethod => HttpMethod.POST;
		protected override string UrlPath => "/project/_validate/query";

		protected override LazyResponses ClientUsage() => Calls(
			(c, f) => c.ValidateQuery<Project>(v => v.Query(q => q.MatchAll())),
			(c, f) => c.ValidateQueryAsync<Project>(v => v.Query(q => q.MatchAll())),
			(c, r) => c.ValidateQuery(new ValidateQueryRequest<Project> { Query = new QueryContainer(new MatchAllQuery()) }),
			(c, r) => c.ValidateQueryAsync(new ValidateQueryRequest<Project> { Query = new QueryContainer(new MatchAllQuery()) })
		);
	}

	public class ValidateInvalidQueryApiTests
		: ApiIntegrationTestBase<ReadOnlyCluster, IValidateQueryResponse, IValidateQueryRequest, ValidateQueryDescriptor<Project>,
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
			(c, f) => c.ValidateQuery<Project>(v => _descriptor),
			(c, f) => c.ValidateQueryAsync<Project>(v => _descriptor),
			(c, r) => c.ValidateQuery(_request),
			(c, r) => c.ValidateQueryAsync(_request)
		);

		protected override void ExpectResponse(IValidateQueryResponse response) => response.Valid.Should().BeFalse();
	}
}
