using Tests.Framework;
using Tests.Framework.Integration;
using Tests.Framework.MockData;
using Xunit;
using Nest;
using Elasticsearch.Net;
using FluentAssertions;

namespace Tests.Search.Validate
{
	[Collection(IntegrationContext.ReadOnly)]
	public class ValidateQueryApiTests 
		: ApiIntegrationTestBase<IValidateQueryResponse, IValidateQueryRequest, ValidateQueryDescriptor<Project>, ValidateQueryRequest<Project>>
	{
		public ValidateQueryApiTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override LazyResponses ClientUsage() => Calls(
			fluent: (c, f) => c.ValidateQuery<Project>(v => v.Query(q => q.MatchAll())),
			fluentAsync: (c, f) => c.ValidateQueryAsync<Project>(v => v.Query(q => q.MatchAll())),
			request: (c, r) => c.ValidateQuery(new ValidateQueryRequest<Project> { Query = new QueryContainer(new MatchAllQuery()) }),
			requestAsync: (c, r) => c.ValidateQueryAsync(new ValidateQueryRequest<Project> { Query = new QueryContainer(new MatchAllQuery()) })
		);

		protected override bool ExpectIsValid => true;
		protected override int ExpectStatusCode => 200;
		protected override HttpMethod HttpMethod => HttpMethod.POST;
		protected override string UrlPath => "/project/project/_validate/query";
	}

	[Collection(IntegrationContext.ReadOnly)]
	public class ValidateInvalidQueryApiTests 
		: ApiIntegrationTestBase<IValidateQueryResponse, IValidateQueryRequest, ValidateQueryDescriptor<Project>, ValidateQueryRequest<Project>>
	{
		public ValidateInvalidQueryApiTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override LazyResponses ClientUsage() => Calls(
			fluent: (c, f) => c.ValidateQuery<Project>(v => _descriptor),
			fluentAsync: (c, f) => c.ValidateQueryAsync<Project>(v => _descriptor),
			request: (c, r) => c.ValidateQuery(_request),
			requestAsync: (c, r) => c.ValidateQueryAsync(_request)
		);

		private ValidateQueryDescriptor<Project> _descriptor = new ValidateQueryDescriptor<Project>()
			.Query(q => q
				.Match(m => m
					.Field(p => p.StartedOn)
					.Query("shouldbeadate")
				)
			);

		private ValidateQueryRequest<Project> _request = new ValidateQueryRequest<Project>
		{
			Query = new QueryContainer(
				new MatchQuery
				{
					Field = "startedOn",
					Query = "shouldbeadate"
				}
			)
        };

		protected override bool ExpectIsValid => true;
		protected override int ExpectStatusCode => 200;
		protected override HttpMethod HttpMethod => HttpMethod.POST;
		protected override string UrlPath => "/project/project/_validate/query";

		protected override void ExpectResponse(IValidateQueryResponse response)
		{
			response.Valid.Should().BeFalse();
		}
	}
}
