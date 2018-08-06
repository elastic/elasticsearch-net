using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Elastic.Xunit.XunitPlumbing;
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

	//hide
	public abstract class PercolateQueryUsageTestsBase :
		ApiIntegrationTestBase<
			WritableCluster,
			ISearchResponse<ProjectPercolation>,
			ISearchRequest,
			SearchDescriptor<ProjectPercolation>,
			SearchRequest<ProjectPercolation>
		>
	{
		protected PercolateQueryUsageTestsBase(WritableCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected static readonly string PercolatorId = RandomString();

		protected override LazyResponses ClientUsage() => Calls(
			fluent: (client, f) => client.Search<ProjectPercolation>(f),
			fluentAsync: (client, f) => client.SearchAsync<ProjectPercolation>(f),
			request: (client, r) => client.Search<ProjectPercolation>(r),
			requestAsync: (client, r) => client.SearchAsync<ProjectPercolation>(r)
		);

		protected override string UrlPath => $"{this.PercolationIndex}/_search";
		protected override int ExpectStatusCode => 200;
		protected override bool ExpectIsValid => true;
		protected override HttpMethod HttpMethod => HttpMethod.POST;

		protected override void IntegrationSetup(IElasticClient client, CallUniqueValues values)
		{
			foreach (var index in values.Values)
			{
				this.Client.CreateIndex(index, c => c
                    .Settings(settings=>settings
						.NumberOfShards(1)
						.NumberOfReplicas(0)
                        .Analysis(DefaultSeeder.ProjectAnalysisSettings)
                    )
					.Mappings(m => m
						.Map<Project>(mm => mm.AutoMap()
							.Properties(DefaultSeeder.ProjectProperties)
						)
					)
				);
				var percolationIndex = index + "-queries";
				this.Client.CreateIndex(percolationIndex, c => c
                    .Settings(settings=>settings
						.NumberOfShards(1)
						.NumberOfReplicas(0)
						.Analysis(DefaultSeeder.ProjectAnalysisSettings)
                    )
					.Mappings(m => m
						.Map<ProjectPercolation>(mm => mm.AutoMap()
							.Properties(DefaultSeeder.PercolatedQueryProperties)
						)
					)
				);

				this.Client.Index(new ProjectPercolation
				{
					Id = PercolatorId,
					Query = new MatchQuery
					{
						Field = Infer.Field<Project>(f => f.LeadDeveloper.FirstName),
						Query = "Martijn"
					}
				}, d => d.Index(percolationIndex));
				this.Client.IndexDocument(Project.Instance);
				this.Client.Refresh(Nest.Indices.Index(percolationIndex).And<Project>());
			}
		}
		protected string PercolationIndex => CallIsolatedValue + "-queries";
	}

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
	public class PercolateQueryUsageTests : PercolateQueryUsageTestsBase
	{
		public PercolateQueryUsageTests(WritableCluster i, EndpointUsage usage) : base(i, usage) { }

		protected object QueryJson => new
		{
			percolate = new
			{
				document = Project.InstanceAnonymous,
				field = "query"
			}
		};

        //hide
		protected override Func<SearchDescriptor<ProjectPercolation>, ISearchRequest> Fluent => f =>
			f.Query(QueryFluent).Index(PercolationIndex).AllTypes();

        //hide
		protected override SearchRequest<ProjectPercolation> Initializer =>
			new SearchRequest<ProjectPercolation>(PercolationIndex, Types.All)
			{
				Query = this.QueryInitializer
			};

		protected QueryContainer QueryInitializer => new PercolateQuery
		{
			Document = Project.Instance,
			Field = Infer.Field<ProjectPercolation>(f => f.Query)
		};

		protected QueryContainer QueryFluent(QueryContainerDescriptor<ProjectPercolation> q) => q
			.Percolate(p => p
				.Document(Project.Instance)
				.Field(f => f.Query)
			);

		protected override void ExpectResponse(ISearchResponse<ProjectPercolation> response)
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
	public class PercolateQueryExistingDocumentUsageTests : PercolateQueryUsageTestsBase
	{
		public PercolateQueryExistingDocumentUsageTests(WritableCluster i, EndpointUsage usage) : base(i, usage) { }

		protected object QueryJson => new
		{
			percolate = new
			{
				type = "doc",
				index = "project",
				id = Project.Instance.Name,
				routing = Project.Instance.Name,
				field = "query"
			}
		};

		//hide
		protected override Func<SearchDescriptor<ProjectPercolation>, ISearchRequest> Fluent => f =>
			f.Query(QueryFluent).Index(PercolationIndex).AllTypes();

		//hide
		protected override SearchRequest<ProjectPercolation> Initializer =>
			new SearchRequest<ProjectPercolation>(PercolationIndex, Types.All)
			{
				Query = this.QueryInitializer
			};

		protected QueryContainer QueryInitializer => new PercolateQuery
		{
			Type = typeof(Project),
			Index = IndexName.From<Project>(),
			Id = Project.Instance.Name,
			Routing = Project.Instance.Name,
			Field = Infer.Field<ProjectPercolation>(f => f.Query)
		};

		protected QueryContainer QueryFluent(QueryContainerDescriptor<ProjectPercolation> q) => q
			.Percolate(p => p
				.Type<Project>()
				.Index<Project>()
				.Id(Project.Instance.Name)
				.Routing(Project.Instance.Name)
				.Field(f => f.Query)
			);

		protected override void ExpectResponse(ISearchResponse<ProjectPercolation> response)
		{
			response.Total.Should().BeGreaterThan(0);
			response.Hits.Should().NotBeNull();
			response.Hits.Count().Should().BeGreaterThan(0);
			var match = response.Documents.First();
			match.Id.Should().Be(PercolatorId);
			((IQueryContainer)match.Query).Match.Should().NotBeNull();
		}
	}

	/**[float]
	* == Percolate multiple documents
	* The percolate query can match multiple documents simultaneously with the indexed percolator queries.
	* Percolating multiple documents in a single request can improve performance as queries
	* only need to be parsed and matched once instead of multiple times.
	*
	* See the Elasticsearch documentation on {ref_current}/query-dsl-percolate-query.html[percolate query] for more details.
	*/
	[SkipVersion("5.0.0-alpha1", "percolate query changed property in query dsl from 'percolator' to 'percolate'")]
	public class PercolateMultipleDocumentsQueryUsageTests : PercolateQueryUsageTestsBase
	{
		public PercolateMultipleDocumentsQueryUsageTests(WritableCluster i, EndpointUsage usage) : base(i, usage) { }

		protected object QueryJson => new
		{
			percolate = new
			{
				documents = new []
				{
					Project.InstanceAnonymous,
					Project.InstanceAnonymous,
					Project.InstanceAnonymous
				},
				field = "query"
			}
		};

		//hide
		protected override Func<SearchDescriptor<ProjectPercolation>, ISearchRequest> Fluent => f =>
			f.Query(QueryFluent).Index(PercolationIndex).AllTypes();

		//hide
		protected override SearchRequest<ProjectPercolation> Initializer =>
			new SearchRequest<ProjectPercolation>(PercolationIndex, Types.All)
			{
				Query = this.QueryInitializer
			};

		protected QueryContainer QueryInitializer => new PercolateQuery
		{
			Documents = new [] { Project.Instance, Project.Instance, Project.Instance },
			Field = Infer.Field<ProjectPercolation>(f => f.Query)
		};

		protected QueryContainer QueryFluent(QueryContainerDescriptor<ProjectPercolation> q) => q
			.Percolate(p => p
				.Documents(Project.Instance, Project.Instance, Project.Instance)
				.Field(f => f.Query)
			);

		protected override void ExpectResponse(ISearchResponse<ProjectPercolation> response)
		{
			response.Total.Should().Be(1);
			response.Hits.Should().NotBeNull();
			response.Hits.Count.Should().Be(1);
			response.Fields.Count.Should().Be(1);

			var field = response.Fields.ElementAt(0);
			var values = field.ValuesOf<int>("_percolator_document_slot");
			values.Should().Contain(new[] {0, 1, 2});

			var match = response.Documents.First();
			match.Id.Should().Be(PercolatorId);
			((IQueryContainer)match.Query).Match.Should().NotBeNull();
		}

		protected ConditionlessWhen ConditionlessWhen => new ConditionlessWhen<IPercolateQuery>(a => a.Percolate)
		{
			q => q.Documents = null
		};
	}
}
