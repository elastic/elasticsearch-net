using System;
using System.Collections.Generic;
using System.Linq;
using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using Tests.Framework;
using Tests.Framework.Integration;
using Tests.Framework.ManagedElasticsearch.Clusters;
using Tests.Framework.MockData;
using Tests.XPack.Security;
using Xunit;
using static Nest.Infer;

namespace Tests.XPack.Graph.Explore
{
	[SkipVersion("<2.3.0", "")]
	public class GraphExploreApiTests : ApiIntegrationTestBase<ReadOnlyCluster, IGraphExploreResponse, IGraphExploreRequest, GraphExploreDescriptor<Project>, GraphExploreRequest>
	{
		public GraphExploreApiTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override LazyResponses ClientUsage() => Calls(
			fluent: (client, f) => client.GraphExplore<Project>(f),
			fluentAsync: (client, f) => client.GraphExploreAsync<Project>(f),
			request: (client, r) => client.GraphExplore(r),
			requestAsync: (client, r) => client.GraphExploreAsync(r)
		);

		protected override bool ExpectIsValid => true;
		protected override int ExpectStatusCode => 200;
		protected override HttpMethod HttpMethod => HttpMethod.POST;

		protected override string UrlPath => $"/project/project/_xpack/graph/_explore";

		protected override bool SupportsDeserialization => false;

		protected override object ExpectJson { get; } = new {
			query = new { term = new { state = new { value = "VeryActive" } } },
			vertices = new [] {
				new { field = "name" },
				new { field = "description" }
			},
			connections = new {
				query = new { match_all = new {} },
				vertices = new [] {
					new { field = "description" }
				},
				connections = new {
					query = new { match_all = new {} },
					vertices = new [] {
						new
						{
							field = "name",
							size = 500,
							min_doc_count = 20,
							shard_min_doc_count = 1,
							include = new [] {
								new { term = "0", boost = 1.1 }
							}
						}
					}
				}
			},
			controls = new {
				use_significance = true,
				sample_size = 5
			}
		};

		protected override Func<GraphExploreDescriptor<Project>, IGraphExploreRequest> Fluent => d => d
			.Query(q=>q.Term(p=>p.State, StateOfBeing.VeryActive))
			.Vertices(v=>v
				.Vertex(p=>p.Name)
				.Vertex(p=>p.Description)
			)
			.Connections(c=>c
				.Query(q=>q.MatchAll())
				.Vertices(v=>v
					.Vertex(p=>p.Description)
				)
				.Connections(cc=>cc
					.Query(q=>q.MatchAll())
					.Vertices(v=>v
						.Vertex(p=>p.Name, gv=>gv
							.MinimumDocumentCount(20)
							.ShardMinimumDocumentCount(1)
							.Size(500)
							.Include(i=>i
								.Include("0", 1.1)
							)
						)
					)
				)
			)
			.Controls(c=>c
				.UseSignificance()
				.SampleSize(5)
			)
		;

		protected override GraphExploreRequest Initializer => new GraphExploreRequest(Index<Project>(), Type<Project>())
		{
			Query = new TermQuery { Field = Field<Project>(p => p.State), Value = StateOfBeing.VeryActive },
			Vertices = new List<IGraphVertexDefinition>
			{
				new GraphVertexDefinition { Field = "name" },
				new GraphVertexDefinition { Field = Field<Project>(p=>p.Description) }
			},
			Connections = new Hop
			{
				Query = new MatchAllQuery(),
				Vertices = new List<GraphVertexDefinition>
				{
					new GraphVertexDefinition { Field = Field<Project>(p=>p.Description) }
				},
				Connections = new Hop
				{
					Query = new MatchAllQuery(),
					Vertices = new List<GraphVertexDefinition>
					{
						new GraphVertexDefinition
						{
							Field = Field<Project>(p=>p.Name),
							MinimumDocumentCount = 20,
							ShardMinimumDocumentCount = 1,
							Size = 500,
							Include = new [] { new GraphVertexInclude { Term = "0", Boost = 1.1 } }
						}
					}

				}

			},
			Controls = new GraphExploreControls
			{
				UseSignificance = true,
				SampleSize = 5
			}
		};

		protected override void ExpectResponse(IGraphExploreResponse response)
		{
			response.Failures.Should().BeEmpty();
			response.Connections.Should().NotBeEmpty();
			response.Connections.Should().Contain(c => c.DocumentCount > 0);
			response.Connections.Should().Contain(c => c.Target > 0);
			response.Connections.Should().Contain(c => c.Source > 0);
			response.Connections.Should().OnlyContain(c => c.Weight > 0);

			response.Vertices.Should().NotBeEmpty();
			response.Vertices.Should().OnlyContain(c=>c.Field != null);
			response.Vertices.Should().OnlyContain(c => c.Term !=  null);
			response.Vertices.Should().Contain(c => c.Depth > 0);
			response.Vertices.Should().OnlyContain(c => c.Weight > 0);

		}
	}
}
