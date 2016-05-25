using System;
using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using Tests.Framework;
using Tests.Framework.Integration;
using Tests.Framework.MockData;
using Xunit;

namespace Tests.QueryDsl
{
	[Collection(TypeOfCluster.ReadOnly)]
	public abstract class QueryDslUsageTestsBase : ApiTestBase<ISearchResponse<Project>, ISearchRequest, SearchDescriptor<Project>, SearchRequest<Project>>
	{
		protected QueryDslUsageTestsBase(IIntegrationCluster cluster, EndpointUsage usage) : base(cluster, usage) { }
		protected override LazyResponses ClientUsage() => Calls(
			fluent: (client, f) => client.Search<Project>(f),
			fluentAsync: (client, f) => client.SearchAsync<Project>(f),
			request: (client, r) => client.Search<Project>(r),
			requestAsync: (client, r) => client.SearchAsync<Project>(r)
		);

		protected override HttpMethod HttpMethod => HttpMethod.POST;
		protected override string UrlPath => "/project/project/_search";

		protected abstract object QueryJson { get; }

		protected abstract QueryContainer QueryInitializer { get; }
		protected abstract QueryContainer QueryFluent(QueryContainerDescriptor<Project> q);

		protected override object ExpectJson => new { query = this.QueryJson };

		[U] public void FluentIsNotConditionless() =>
			AssertIsNotConditionless(this.QueryFluent(new QueryContainerDescriptor<Project>()));

		[U] public void InitializerIsNotConditionless() =>
			AssertIsNotConditionless(this.QueryInitializer);

		private void AssertIsNotConditionless(IQueryContainer c)
		{
			if (!c.IsVerbatim)
				c.IsConditionless.Should().BeFalse();
		}

		protected override Func<SearchDescriptor<Project>, ISearchRequest> Fluent => s => s
			.Query(this.QueryFluent);

		protected override SearchRequest<Project> Initializer =>
			new SearchRequest<Project>
			{
				Query = this.QueryInitializer
			};

		protected virtual ConditionlessWhen ConditionlessWhen => null;

		protected readonly QueryContainer ConditionlessQuery = new QueryContainer(new TermQuery { });

		[U] public void SeenByVisitor()
		{
			var visitor = new DslPrettyPrintVisitor(TestClient.CreateSettings());
			var query = this.QueryFluent(new QueryContainerDescriptor<Project>());
			query.Accept(visitor);
			var pretty = visitor.PrettyPrint;
			pretty.Should().NotBeNullOrWhiteSpace();
		}


		[U] public void ConditionlessWhenExpectedToBe()
		{
			if (ConditionlessWhen == null) return;
			foreach (var when in ConditionlessWhen)
			{
				var query = this.QueryFluent(new QueryContainerDescriptor<Project>());
				when(query);
				query = this.QueryInitializer;
				when(query);
			}

			((IQueryContainer)this.QueryInitializer).IsConditionless.Should().BeFalse();
		}

		private void IsConditionless(IQueryContainer q, bool be) => q.IsConditionless.Should().Be(be);
	}
}
