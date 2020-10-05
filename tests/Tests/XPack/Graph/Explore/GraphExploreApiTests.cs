// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Collections.Generic;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Elastic.Transport;
using FluentAssertions;
using Nest;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Domain;
using Tests.Framework.EndpointTests;
using Tests.Framework.EndpointTests.TestState;
using static Nest.Infer;

namespace Tests.XPack.Graph.Explore
{
	[SkipVersion("<2.3.0", "")]
	public class GraphExploreApiTests
		: ApiIntegrationTestBase<XPackCluster, GraphExploreResponse, IGraphExploreRequest, GraphExploreDescriptor<Project>, GraphExploreRequest>
	{
		public GraphExploreApiTests(XPackCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override bool ExpectIsValid => true;

		protected override object ExpectJson { get; } = new
		{
			query = new { term = new { state = new { value = "VeryActive" } } },
			vertices = new[]
			{
				new { field = "name" },
				new { field = "description" }
			},
			connections = new
			{
				query = new { match_all = new { } },
				vertices = new[]
				{
					new { field = "description" }
				},
				connections = new
				{
					query = new { match_all = new { } },
					vertices = new[]
					{
						new
						{
							field = "name",
							size = 500,
							min_doc_count = 20,
							shard_min_doc_count = 1,
							include = new[]
							{
								new { term = "0", boost = 1.1 }
							}
						}
					}
				}
			},
			controls = new
			{
				use_significance = true,
				sample_size = 5
			}
		};

		protected override int ExpectStatusCode => 200;

		protected override Func<GraphExploreDescriptor<Project>, IGraphExploreRequest> Fluent => d => d
			.Query(q => q.Term(p => p.State, StateOfBeing.VeryActive))
			.Vertices(v => v
				.Vertex(p => p.Name)
				.Vertex(p => p.Description)
			)
			.Connections(c => c
				.Query(q => q.MatchAll())
				.Vertices(v => v
					.Vertex(p => p.Description)
				)
				.Connections(cc => cc
					.Query(q => q.MatchAll())
					.Vertices(v => v
						.Vertex(p => p.Name, gv => gv
							.MinimumDocumentCount(20)
							.ShardMinimumDocumentCount(1)
							.Size(500)
							.Include(i => i
								.Include("0", 1.1)
							)
						)
					)
				)
			)
			.Controls(c => c
				.UseSignificance()
				.SampleSize(5)
			);

		protected override HttpMethod HttpMethod => HttpMethod.POST;

		protected override GraphExploreRequest Initializer => new GraphExploreRequest(Index<Project>())
		{
			Query = new TermQuery { Field = Field<Project>(p => p.State), Value = StateOfBeing.VeryActive },
			Vertices = new List<IGraphVertexDefinition>
			{
				new GraphVertexDefinition { Field = "name" },
				new GraphVertexDefinition { Field = Field<Project>(p => p.Description) }
			},
			Connections = new Hop
			{
				Query = new MatchAllQuery(),
				Vertices = new List<GraphVertexDefinition>
				{
					new GraphVertexDefinition { Field = Field<Project>(p => p.Description) }
				},
				Connections = new Hop
				{
					Query = new MatchAllQuery(),
					Vertices = new List<GraphVertexDefinition>
					{
						new GraphVertexDefinition
						{
							Field = Field<Project>(p => p.Name),
							MinimumDocumentCount = 20,
							ShardMinimumDocumentCount = 1,
							Size = 500,
							Include = new[] { new GraphVertexInclude { Term = "0", Boost = 1.1 } }
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

		protected override bool SupportsDeserialization => false;

		protected override string UrlPath => $"/project/_graph/explore";

		protected override LazyResponses ClientUsage() => Calls(
			(client, f) => client.Graph.Explore(f),
			(client, f) => client.Graph.ExploreAsync(f),
			(client, r) => client.Graph.Explore(r),
			(client, r) => client.Graph.ExploreAsync(r)
		);

		protected override void ExpectResponse(GraphExploreResponse response)
		{
			response.Failures.Should().BeEmpty();
			response.Connections.Should().NotBeEmpty();
			response.Connections.Should().Contain(c => c.DocumentCount > 0);
			response.Connections.Should().Contain(c => c.Target > 0);
			response.Connections.Should().Contain(c => c.Source > 0);
			response.Connections.Should().OnlyContain(c => c.Weight > 0);

			response.Vertices.Should().NotBeEmpty();
			response.Vertices.Should().OnlyContain(c => c.Field != null);
			response.Vertices.Should().OnlyContain(c => c.Term != null);
			response.Vertices.Should().Contain(c => c.Depth > 0);
			response.Vertices.Should().OnlyContain(c => c.Weight > 0);
		}
	}
}
