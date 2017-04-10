using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using Tests.Framework;
using Tests.Framework.Integration;
using Tests.Framework.ManagedElasticsearch.Clusters;
using Tests.Framework.ManagedElasticsearch.NodeSeeders;
using Tests.Framework.MockData;
using Xunit;

namespace Tests.QueryDsl.Specialized.Percolate
{
	/**
	* The percolate query can be used to match queries stored in an index.
	* The percolate query itself contains the document that will be used as query to match with the stored queries.
	*
	* IMPORTANT: In order for the percolate query to work, the index in which your stored queries reside must contain
	* a mapping for documents that you wish to percolate, so that they are parsed correctly at query time.
	*
	* See the Elasticsearch documentation on {ref_current}/query-dsl-percolate-query.html[percolate query] for more details.
	*
	* In this example, we have a document stored with a `query` field that is mapped as a `percolator` type. This field
	* contains a `match` query.
	*/
	[SkipVersion("5.0.0-alpha1", "percolate query changed property in query dsl from 'percolator' to 'percolate'")]
	public class PercolateQueryUsageTests : ApiIntegrationTestBase<WritableCluster, ISearchResponse<PercolatedQuery>, ISearchRequest, SearchDescriptor<PercolatedQuery>, SearchRequest<PercolatedQuery>>
	{
		private static readonly string PercolatorId = RandomString();

		protected override LazyResponses ClientUsage() => Calls(
			fluent: (client, f) => client.Search<PercolatedQuery>(f),
			fluentAsync: (client, f) => client.SearchAsync<PercolatedQuery>(f),
			request: (client, r) => client.Search<PercolatedQuery>(r),
			requestAsync: (client, r) => client.SearchAsync<PercolatedQuery>(r)
		);

		protected override string UrlPath => $"{this.CallIsolatedValue}/_search";
		protected override int ExpectStatusCode => 200;
		protected override bool ExpectIsValid => true;
		protected override HttpMethod HttpMethod => HttpMethod.POST;

		public PercolateQueryUsageTests(WritableCluster i, EndpointUsage usage) : base(i, usage) { }

		protected override void IntegrationSetup(IElasticClient client, CallUniqueValues values)
		{
			foreach (var index in values.Values)
			{
				this.Client.CreateIndex(index, c => c
                    .Settings(settings=>settings
                        .Analysis(DefaultSeeder.ProjectAnalysisSettings)
                    )
					.Mappings(m => m
						.Map<Project>(mm => mm.AutoMap()
							.Properties(DefaultSeeder.ProjectProperties)
						)
						.Map<PercolatedQuery>(mm => mm.AutoMap()
							.Properties(DefaultSeeder.PercolatedQueryProperties)
						)
					)
				);

				this.Client.Index(new PercolatedQuery
				{
					Id = PercolatorId,
					Query = new QueryContainer(new MatchQuery
					{
						Field = Infer.Field<Project>(f => f.LeadDeveloper.FirstName),
						Query = "Martijn"
					})
				}, d => d.Index(index));

				this.Client.Refresh(index);
			}
		}

		protected object QueryJson => new
		{
			percolate = new
			{
				document_type = "project",
				document = Project.InstanceAnonymous,
				field = "query"
			}
		};

        //hide
		protected override Func<SearchDescriptor<PercolatedQuery>, ISearchRequest> Fluent => f =>
			f.Query(QueryFluent).Index(CallIsolatedValue).AllTypes();

        //hide
		protected override SearchRequest<PercolatedQuery> Initializer =>
			new SearchRequest<PercolatedQuery>(CallIsolatedValue, Types.All)
			{
				Query = this.QueryInitializer
			};

		protected QueryContainer QueryInitializer => new PercolateQuery
		{
			DocumentType = typeof(Project),
			Document = Project.Instance,
			Field = Infer.Field<PercolatedQuery>(f => f.Query)
		};

		protected QueryContainer QueryFluent(QueryContainerDescriptor<PercolatedQuery> q) => q
			.Percolate(p => p
				.DocumentType(typeof(Project))
				.Document(Project.Instance)
				.Field(f => f.Query)
			);

		protected override void ExpectResponse(ISearchResponse<PercolatedQuery> response)
		{
			response.Total.Should().BeGreaterThan(0);
			response.Hits.Should().NotBeNull();
			response.Hits.Count().Should().BeGreaterThan(0);
			var match = response.Documents.First();
			match.Id.Should().Be(PercolatorId);
			((IQueryContainer)match.Query).Match.Should().NotBeNull();
		}

		protected ConditionlessWhen ConditionlessWhen => new ConditionlessWhen<IPercolateQuery>(a => a.Percolate)
		{
			q => {
				q.DocumentType = null;
			},
			q => {
				q.Document = null;
			}
		};
	}

	/**[float]
	* == Percolate an existing document
	* Instead of specifying the source of the document being percolated, the source can also be
	* retrieved from an already stored document. The percolate query will then internally execute a get request to fetch that document.
	*
	* The required fields to percolate an existing document are:
	* - `index` in which the document resides
	* - `type` of the document
	* - `field` that contains the query
	* - `id` of the document
	* - `document_type` type / mapping of the document
	*
	* See the Elasticsearch documentation on {ref_current}/query-dsl-percolate-query.html[percolate query] for more details.
	*/
	[SkipVersion("5.0.0-alpha1", "percolate query changed property in query dsl from 'percolator' to 'percolate'")]
	public class PercolateQueryExistingDocumentUsageTests : ApiIntegrationTestBase<WritableCluster, ISearchResponse<PercolatedQuery>, ISearchRequest, SearchDescriptor<PercolatedQuery>, SearchRequest<PercolatedQuery>>
	{
		private static readonly string PercolatorId = RandomString();

		protected override LazyResponses ClientUsage() => Calls(
			fluent: (client, f) => client.Search<PercolatedQuery>(f),
			fluentAsync: (client, f) => client.SearchAsync<PercolatedQuery>(f),
			request: (client, r) => client.Search<PercolatedQuery>(r),
			requestAsync: (client, r) => client.SearchAsync<PercolatedQuery>(r)
		);

		protected override string UrlPath => $"{this.CallIsolatedValue}/_search";
		protected override int ExpectStatusCode => 200;
		protected override bool ExpectIsValid => true;
		protected override HttpMethod HttpMethod => HttpMethod.POST;

		public PercolateQueryExistingDocumentUsageTests(WritableCluster i, EndpointUsage usage) : base(i, usage) { }

		//hide
		protected override void IntegrationSetup(IElasticClient client, CallUniqueValues values)
		{
			foreach (var index in values.Values)
			{
				this.Client.CreateIndex(index, c => c
                    .Settings(settings=>settings
                        .Analysis(DefaultSeeder.ProjectAnalysisSettings)
                    )
					.Mappings(m => m
						.Map<Project>(mm => mm.AutoMap()
							.Properties(DefaultSeeder.ProjectProperties)
						)
						.Map<PercolatedQuery>(mm => mm.AutoMap()
							.Properties(DefaultSeeder.PercolatedQueryProperties)
						)
					)
				);

				this.Client.Index(new PercolatedQuery
				{
					Id = PercolatorId,
					Query = new QueryContainer(new MatchQuery
					{
						Field = Infer.Field<Project>(f => f.LeadDeveloper.FirstName),
						Query = "Martijn"
					})
				}, d => d.Index(index));

				this.Client.Index(Project.Instance);
				this.Client.Refresh(Nest.Indices.Index(index).And<Project>());
			}
		}

		protected object QueryJson => new
		{
			percolate = new
			{
				type = "project",
				index = "project",
				id = Project.Instance.Name,
				document_type = "project",
				field = "query"
			}
		};

		//hide
		protected override Func<SearchDescriptor<PercolatedQuery>, ISearchRequest> Fluent => f =>
			f.Query(QueryFluent).Index(CallIsolatedValue).AllTypes();

		//hide
		protected override SearchRequest<PercolatedQuery> Initializer =>
			new SearchRequest<PercolatedQuery>(CallIsolatedValue, Types.All)
			{
				Query = this.QueryInitializer
			};

		protected QueryContainer QueryInitializer => new PercolateQuery
		{
			Type = typeof(Project),
			Index = IndexName.From<Project>(),
			Id = Project.Instance.Name,
			DocumentType = typeof(Project),
			Field = Infer.Field<PercolatedQuery>(f => f.Query)
		};

		protected QueryContainer QueryFluent(QueryContainerDescriptor<PercolatedQuery> q) => q
			.Percolate(p => p
				.Type<Project>()
				.Index<Project>()
				.Id(Project.Instance.Name)
				.Field(f => f.Query)
				.DocumentType<Project>() // <1> specify the `type`, `index`, `id`, `field`, `document_type` of the document to fetch, to percolate.
			);

		protected override void ExpectResponse(ISearchResponse<PercolatedQuery> response)
		{
			response.Total.Should().BeGreaterThan(0);
			response.Hits.Should().NotBeNull();
			response.Hits.Count().Should().BeGreaterThan(0);
			var match = response.Documents.First();
			match.Id.Should().Be(PercolatorId);
			((IQueryContainer)match.Query).Match.Should().NotBeNull();
		}
	}
}
