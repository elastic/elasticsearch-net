using System;
using System.Threading.Tasks;
using Elastic.Xunit.XunitPlumbing;
using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using Tests.Framework;
using Tests.Framework.Integration;
using Tests.Framework.ManagedElasticsearch;
using Tests.Framework.ManagedElasticsearch.Clusters;
using Tests.Framework.MockData;
using Xunit;

namespace Tests.QueryDsl
{
	public abstract class QueryDslUsageTestsBase : ApiTestBase<ReadOnlyCluster, ISearchResponse<Project>, ISearchRequest, SearchDescriptor<Project>, SearchRequest<Project>>
	{
		protected QueryDslUsageTestsBase(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override LazyResponses ClientUsage() => Calls(
			fluent: (client, f) => client.Search<Project>(f),
			fluentAsync: (client, f) => client.SearchAsync<Project>(f),
			request: (client, r) => client.Search<Project>(r),
			requestAsync: (client, r) => client.SearchAsync<Project>(r)
		);

		protected override HttpMethod HttpMethod => HttpMethod.POST;
		protected override string UrlPath => "/project/doc/_search";

		protected abstract object QueryJson { get; }

		protected abstract QueryContainer QueryInitializer { get; }
		protected abstract QueryContainer QueryFluent(QueryContainerDescriptor<Project> q);

		protected override object ExpectJson => new { query = this.QueryJson };

		[U] public void FluentIsNotConditionless() =>
			AssertIsNotConditionless(this.QueryFluent(new QueryContainerDescriptor<Project>()));

		[U] public void InitializerIsNotConditionless() => AssertIsNotConditionless(this.QueryInitializer);

		private void AssertIsNotConditionless(IQueryContainer c)
		{
			if (!c.IsVerbatim)
				c.IsConditionless.Should().BeFalse();
		}

		protected override Func<SearchDescriptor<Project>, ISearchRequest> Fluent => s => s
			.Query(q => this.QueryFluent(q));

		protected override SearchRequest<Project> Initializer =>
			new SearchRequest<Project>
			{
				Query = this.QueryInitializer
			};

		protected virtual NotConditionlessWhen NotConditionlessWhen => null;
		protected virtual ConditionlessWhen ConditionlessWhen => null;

		protected readonly QueryContainer ConditionlessQuery = new QueryContainer(new TermQuery { });

		protected readonly QueryContainer VerbatimQuery = new QueryContainer(new TermQuery { IsVerbatim = true });

		[U] public void SeenByVisitor()
		{
			var visitor = new DslPrettyPrintVisitor(TestClient.GlobalDefaultSettings);
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
				when(this.QueryFluent(new QueryContainerDescriptor<Project>()));
				//this.JsonEquals(query, new { });
				when(this.QueryInitializer);
				//this.JsonEquals(query, new { });
			}

			((IQueryContainer)this.QueryInitializer).IsConditionless.Should().BeFalse();
		}

		[U] public void NotConditionlessWhenExpectedToBe()
		{
			if (NotConditionlessWhen == null) return;
			foreach (var when in NotConditionlessWhen)
			{
				var query = this.QueryFluent(new QueryContainerDescriptor<Project>());
				when(query);

				query = this.QueryInitializer;
				when(query);
			}
		}

		protected virtual bool KnownParseException => false;

		[I] protected async Task AssertQueryResponse() => await this.AssertOnAllResponses(r =>
		{
			var validOrNotParseExceptionOrKnownParseException = r.IsValid || r.ServerError?.Error.Type != "parsing_exception" || KnownParseException;
			validOrNotParseExceptionOrKnownParseException.Should().BeTrue("query should be valid or when not valid not a parsing_exception.");

			//TODO only assert IsValid == true and remove corner cases we don't have time to fix now.
		});



	}
}
