using System;
using System.Threading.Tasks;
using Elastic.Xunit.XunitPlumbing;
using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using Tests.Core.Client;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Domain;
using Tests.Framework;
using Tests.Framework.Integration;

namespace Tests.QueryDsl
{
	public abstract class QueryDslUsageTestsBase
		: ApiTestBase<ReadOnlyCluster, ISearchResponse<Project>, ISearchRequest, SearchDescriptor<Project>, SearchRequest<Project>>
	{
		protected readonly QueryContainer ConditionlessQuery = new QueryContainer(new TermQuery { });

		protected readonly QueryContainer VerbatimQuery = new QueryContainer(new TermQuery { IsVerbatim = true });

		protected QueryDslUsageTestsBase(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected virtual ConditionlessWhen ConditionlessWhen => null;

		protected override object ExpectJson => new { query = QueryJson };

		protected override Func<SearchDescriptor<Project>, ISearchRequest> Fluent => s => s
			.Query(q => QueryFluent(q));

		protected override HttpMethod HttpMethod => HttpMethod.POST;

		protected override SearchRequest<Project> Initializer =>
			new SearchRequest<Project>
			{
				Query = QueryInitializer
			};

		protected virtual bool KnownParseException => false;

		protected virtual NotConditionlessWhen NotConditionlessWhen => null;

		protected abstract QueryContainer QueryInitializer { get; }

		protected abstract object QueryJson { get; }
		protected override string UrlPath => "/project/_search";

		protected override LazyResponses ClientUsage() => Calls(
			(client, f) => client.Search<Project>(f),
			(client, f) => client.SearchAsync<Project>(f),
			(client, r) => client.Search<Project>(r),
			(client, r) => client.SearchAsync<Project>(r)
		);

		protected abstract QueryContainer QueryFluent(QueryContainerDescriptor<Project> q);

		[U] public void FluentIsNotConditionless() =>
			AssertIsNotConditionless(QueryFluent(new QueryContainerDescriptor<Project>()));

		[U] public void InitializerIsNotConditionless() => AssertIsNotConditionless(QueryInitializer);

		private void AssertIsNotConditionless(IQueryContainer c)
		{
			if (!c.IsVerbatim)
				c.IsConditionless.Should().BeFalse();
		}

		[U] public void SeenByVisitor()
		{
			var visitor = new DslPrettyPrintVisitor(TestClient.DefaultInMemoryClient.ConnectionSettings);
			var query = QueryFluent(new QueryContainerDescriptor<Project>());
			query.Accept(visitor);
			var pretty = visitor.PrettyPrint;
			pretty.Should().NotBeNullOrWhiteSpace();
		}

		[U] public void ConditionlessWhenExpectedToBe()
		{
			if (ConditionlessWhen == null) return;

			foreach (var when in ConditionlessWhen)
			{
				when(QueryFluent(new QueryContainerDescriptor<Project>()));
				//this.JsonEquals(query, new { });
				when(QueryInitializer);
				//this.JsonEquals(query, new { });
			}

			((IQueryContainer)QueryInitializer).IsConditionless.Should().BeFalse();
		}

		[U] public void NotConditionlessWhenExpectedToBe()
		{
			if (NotConditionlessWhen == null) return;

			foreach (var when in NotConditionlessWhen)
			{
				var query = QueryFluent(new QueryContainerDescriptor<Project>());
				when(query);

				query = QueryInitializer;
				when(query);
			}
		}

		[I] protected async Task AssertQueryResponse() => await AssertOnAllResponses(r =>
		{
			var validOrNotParseExceptionOrKnownParseException = r.IsValid || r.ServerError?.Error.Type != "parsing_exception" || KnownParseException;
			validOrNotParseExceptionOrKnownParseException.Should().BeTrue("query should be valid or when not valid not a parsing_exception.");

			//TODO only assert IsValid == true and remove corner cases we don't have time to fix now.
		});
	}
}
