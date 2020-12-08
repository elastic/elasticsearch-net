// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Collections.Generic;
using System.Linq;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Elastic.Transport;
using FluentAssertions;
using Nest;
using Tests.Core.Extensions;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Domain;
using Tests.Framework.EndpointTests;
using Tests.Framework.EndpointTests.TestState;

namespace Tests.Search.Search
{
	public class SearchApiTests
		: ApiIntegrationTestBase<ReadOnlyCluster, ISearchResponse<Project>, ISearchRequest, SearchDescriptor<Project>, SearchRequest<Project>>
	{
		public SearchApiTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override bool ExpectIsValid => true;

		protected override object ExpectJson => new
		{
			from = 10,
			size = 20,
			query = new
			{
				match_all = new { }
			},
			aggs = new
			{
				startDates = new
				{
					terms = new
					{
						field = "startedOn"
					}
				}
			},
			post_filter = new
			{
				term = new
				{
					state = new
					{
						value = "Stable"
					}
				}
			}
		};

		protected override int ExpectStatusCode => 200;

		protected override Func<SearchDescriptor<Project>, ISearchRequest> Fluent => s => s
			.From(10)
			.Size(20)
			.Query(q => q
				.MatchAll()
			)
			.Aggregations(a => a
				.Terms("startDates", t => t
					.Field(p => p.StartedOn)
				)
			)
			.PostFilter(f => f
				.Term(p => p.State, StateOfBeing.Stable)
			);

		protected override HttpMethod HttpMethod => HttpMethod.POST;

		protected override SearchRequest<Project> Initializer => new SearchRequest<Project>()
		{
			From = 10,
			Size = 20,
			Query = new QueryContainer(new MatchAllQuery()),
			Aggregations = new TermsAggregation("startDates")
			{
				Field = "startedOn"
			},
			PostFilter = new QueryContainer(new TermQuery
			{
				Field = "state",
				Value = "Stable"
			})
		};

		protected override string UrlPath => $"/project/_search";

		protected override LazyResponses ClientUsage() => Calls(
			(c, f) => c.Search(f),
			(c, f) => c.SearchAsync(f),
			(c, r) => c.Search<Project>(r),
			(c, r) => c.SearchAsync<Project>(r)
		);

		protected override void ExpectResponse(ISearchResponse<Project> response)
		{
			response.Total.Should().BeGreaterThan(0);
			response.Hits.Count.Should().BeGreaterThan(0);
			response.HitsMetadata.Total.Value.Should().Be(response.Total);
			response.HitsMetadata.Total.Relation.Should().Be(TotalHitsRelation.EqualTo);
			response.Hits.First().Should().NotBeNull();
			response.Hits.First().Source.Should().NotBeNull();
			response.Aggregations.Count.Should().BeGreaterThan(0);
			response.Took.Should().BeGreaterThan(0);
			var startDates = response.Aggregations.Terms("startDates");
			startDates.Should().NotBeNull();

			foreach (var document in response.Documents) document.ShouldAdhereToSourceSerializerWhenSet();
		}
	}

	public class SearchApiSequenceNumberPrimaryTermTests
		: SearchApiTests
	{
		public SearchApiSequenceNumberPrimaryTermTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override object ExpectJson => new
		{
			query = new
			{
				match_all = new { }
			}
		};

		protected override int ExpectStatusCode => 200;

		protected override Func<SearchDescriptor<Project>, ISearchRequest> Fluent => s => s
			.SequenceNumberPrimaryTerm()
			.Query(q => q
				.MatchAll()
			);

		protected override HttpMethod HttpMethod => HttpMethod.POST;

		protected override SearchRequest<Project> Initializer => new SearchRequest<Project>()
		{
			SequenceNumberPrimaryTerm = true,
			Query = new QueryContainer(new MatchAllQuery()),
		};

		protected override string UrlPath => $"/project/_search?seq_no_primary_term=true";

		protected override void ExpectResponse(ISearchResponse<Project> response)
		{
			response.Total.Should().BeGreaterThan(0);
			response.Hits.Count.Should().BeGreaterThan(0);
			response.HitsMetadata.Total.Value.Should().Be(response.Total);
			response.HitsMetadata.Total.Relation.Should().Be(TotalHitsRelation.EqualTo);

			foreach (var hit in response.Hits)
			{
				hit.Should().NotBeNull();
				hit.Source.Should().NotBeNull();
				hit.SequenceNumber.Should().HaveValue();
				hit.PrimaryTerm.Should().HaveValue();
			}
		}
	}

	public class SearchApiStoredFieldsTests : SearchApiTests
	{
		public SearchApiStoredFieldsTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override object ExpectJson => new
		{
			from = 10,
			size = 20,
			query = new
			{
				match_all = new { }
			},
			aggs = new
			{
				startDates = new
				{
					terms = new
					{
						field = "startedOn"
					}
				}
			},
			post_filter = new
			{
				term = new
				{
					state = new
					{
						value = "Stable"
					}
				}
			},
			stored_fields = new[] { "name", "numberOfCommits" }
		};

		protected override Func<SearchDescriptor<Project>, ISearchRequest> Fluent => s => s
			.From(10)
			.Size(20)
			.Query(q => q
				.MatchAll()
			)
			.Aggregations(a => a
				.Terms("startDates", t => t
					.Field(p => p.StartedOn)
				)
			)
			.PostFilter(f => f
				.Term(p => p.State, StateOfBeing.Stable)
			)
			.StoredFields(fs => fs
				.Field(p => p.Name)
				.Field(p => p.NumberOfCommits)
			);

		protected override SearchRequest<Project> Initializer => new SearchRequest<Project>()
		{
			From = 10,
			Size = 20,
			Query = new QueryContainer(new MatchAllQuery()),
			Aggregations = new TermsAggregation("startDates")
			{
				Field = "startedOn"
			},
			PostFilter = new QueryContainer(new TermQuery
			{
				Field = "state",
				Value = "Stable"
			}),
			StoredFields = Infer.Fields<Project>(p => p.Name, p => p.NumberOfCommits)
		};

		protected override void ExpectResponse(ISearchResponse<Project> response)
		{
			response.Hits.Count.Should().BeGreaterThan(0);
			response.Hits.First().Should().NotBeNull();
			response.Hits.First().Fields.ValueOf<Project, string>(p => p.Name).Should().NotBeNullOrEmpty();
			response.Hits.First().Fields.ValueOf<Project, int?>(p => p.NumberOfCommits).Should().BeGreaterThan(0);
			response.Aggregations.Count.Should().BeGreaterThan(0);
			var startDates = response.Aggregations.Terms("startDates");
			startDates.Should().NotBeNull();
		}
	}

	[SkipVersion("<6.4.0", "Doc Value Fields format only in Elasticsearch 6.4.0+")]
	public class SearchApiDocValueFieldsTests : SearchApiTests
	{
		public SearchApiDocValueFieldsTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override object ExpectJson => new
		{
			from = 10,
			size = 20,
			query = new
			{
				match_all = new { }
			},
			aggs = new
			{
				startDates = new
				{
					terms = new
					{
						field = "startedOn"
					}
				}
			},
			post_filter = new
			{
				term = new
				{
					state = new
					{
						value = "Stable"
					}
				}
			},
			docvalue_fields = new object[]
			{
				"name",
				new
				{
					field = "lastActivity",
					format = DateFormat.basic_date
				},
			}
		};

		protected override Func<SearchDescriptor<Project>, ISearchRequest> Fluent => s => s
			.From(10)
			.Size(20)
			.Query(q => q
				.MatchAll()
			)
			.Aggregations(a => a
				.Terms("startDates", t => t
					.Field(p => p.StartedOn)
				)
			)
			.PostFilter(f => f
				.Term(p => p.State, StateOfBeing.Stable)
			)
			.DocValueFields(fs => fs
				.Field(p => p.Name)
				.Field(p => p.LastActivity, format: DateFormat.basic_date)
			);

		protected override SearchRequest<Project> Initializer => new SearchRequest<Project>()
		{
			From = 10,
			Size = 20,
			Query = new QueryContainer(new MatchAllQuery()),
			Aggregations = new TermsAggregation("startDates")
			{
				Field = "startedOn"
			},
			PostFilter = new QueryContainer(new TermQuery
			{
				Field = "state",
				Value = "Stable"
			}),
			DocValueFields = Infer.Field<Project>(p => p.Name)
				.And<Project>(p => p.LastActivity, format: DateFormat.basic_date)
		};

		protected override void ExpectResponse(ISearchResponse<Project> response)
		{
			response.HitsMetadata.Should().NotBeNull();
			response.Hits.Count().Should().BeGreaterThan(0);
			response.Hits.First().Should().NotBeNull();
			response.Hits.First().Fields.ValueOf<Project, string>(p => p.Name).Should().NotBeNullOrEmpty();
			var lastActivityYear = Convert.ToInt32(response.Hits.First().Fields.Value<string>("lastActivity"));
			lastActivityYear.Should().BeGreaterThan(0);
			response.Aggregations.Count.Should().BeGreaterThan(0);
			var startDates = response.Aggregations.Terms("startDates");
			startDates.Should().NotBeNull();
		}
	}

	public class SearchApiContainingConditionlessQueryContainerTests : SearchApiTests
	{
		public SearchApiContainingConditionlessQueryContainerTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override object ExpectJson => new
		{
			query = new
			{
				@bool = new
				{
					must = new object[] { new { query_string = new { query = "query" } } },
					should = new object[] { new { query_string = new { query = "query" } } },
					must_not = new object[] { new { query_string = new { query = "query" } } }
				}
			}
		};

		protected override Func<SearchDescriptor<Project>, ISearchRequest> Fluent => s => s
			.Query(q => q
				.Bool(b => b
					.Must(
						m => m.QueryString(qs => qs.Query("query")),
						m => m.QueryString(qs => qs.Query(string.Empty)),
						m => m.QueryString(qs => qs.Query(null)),
						m => new QueryContainer(),
						null
					)
					.Should(
						m => m.QueryString(qs => qs.Query("query")),
						m => m.QueryString(qs => qs.Query(string.Empty)),
						m => m.QueryString(qs => qs.Query(null)),
						m => new QueryContainer(),
						null
					)
					.MustNot(
						m => m.QueryString(qs => qs.Query("query")),
						m => m.QueryString(qs => qs.Query(string.Empty)),
						m => m.QueryString(qs => qs.Query(null)),
						m => new QueryContainer(),
						null
					)
				)
			);

		protected override SearchRequest<Project> Initializer => new SearchRequest<Project>()
		{
			Query = new BoolQuery
			{
				Must = new List<QueryContainer>
				{
					new QueryStringQuery { Query = "query" },
					new QueryStringQuery { Query = string.Empty },
					new QueryStringQuery { Query = null },
					new QueryContainer(),
					null
				},
				Should = new List<QueryContainer>
				{
					new QueryStringQuery { Query = "query" },
					new QueryStringQuery { Query = string.Empty },
					new QueryStringQuery { Query = null },
					new QueryContainer(),
					null
				},
				MustNot = new List<QueryContainer>
				{
					new QueryStringQuery { Query = "query" },
					new QueryStringQuery { Query = string.Empty },
					new QueryStringQuery { Query = null },
					new QueryContainer(),
					null
				}
			}
		};

		protected override void ExpectResponse(ISearchResponse<Project> response) => response.ShouldBeValid();
	}

	public class SearchApiNullQueryContainerTests : SearchApiTests
	{
		public SearchApiNullQueryContainerTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override object ExpectJson => new { };

		protected override Func<SearchDescriptor<Project>, ISearchRequest> Fluent => s => s
			.Query(q => q
				.Bool(b => b
					.Must((Func<QueryContainerDescriptor<Project>, QueryContainer>)null)
					.Should((Func<QueryContainerDescriptor<Project>, QueryContainer>)null)
					.MustNot((Func<QueryContainerDescriptor<Project>, QueryContainer>)null)
				)
			);

		protected override SearchRequest<Project> Initializer => new SearchRequest<Project>()
		{
			Query = new BoolQuery
			{
				Must = null,
				Should = null,
				MustNot = null
			}
		};

		protected override void ExpectResponse(ISearchResponse<Project> response) => response.ShouldBeValid();
	}

	public class SearchApiNullQueriesInQueryContainerTests : SearchApiTests
	{
		public SearchApiNullQueriesInQueryContainerTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override object ExpectJson => new
		{
			query = new
			{
				@bool = new { }
			}
		};

		// There is no *direct equivalent* to a query container collection only with a null querycontainer
		// since the fluent methods filter them out
		protected override Func<SearchDescriptor<Project>, ISearchRequest> Fluent => s => s
			.Query(q => q
				.Bool(b =>
				{
					b.Verbatim();
					IBoolQuery bq = b;
					bq.Must = new QueryContainer[] { null };
					bq.Should = new QueryContainer[] { null };
					bq.MustNot = new QueryContainer[] { null };
					return bq;
				})
			);

		protected override SearchRequest<Project> Initializer => new SearchRequest<Project>()
		{
			Query = new BoolQuery
			{
				IsVerbatim = true,
				Must = new QueryContainer[] { null },
				Should = new QueryContainer[] { null },
				MustNot = new QueryContainer[] { null }
			}
		};

		// when we serialize we write and empty bool, when we read the fact it was verbatim is lost so while
		// we technically DO support deserialization here (and empty bool will get set) when we write it a second
		// time it will NOT write that bool because the is verbatim did not carry over.
		protected override bool SupportsDeserialization => false;

		protected override void ExpectResponse(ISearchResponse<Project> response) => response.ShouldBeValid();
	}


	[SkipVersion("<6.2.0", "OpaqueId introduced in 6.2.0")]
	public class OpaqueIdApiTests
		: ApiIntegrationTestBase<ReadOnlyCluster, ListTasksResponse, IListTasksRequest, ListTasksDescriptor, ListTasksRequest>
	{
		public OpaqueIdApiTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override bool ExpectIsValid => true;

		protected override object ExpectJson => null;
		protected override int ExpectStatusCode => 200;

		protected override Func<ListTasksDescriptor, IListTasksRequest> Fluent => s => s
			.RequestConfiguration(r => r.OpaqueId(CallIsolatedValue));

		protected override HttpMethod HttpMethod => HttpMethod.GET;

		protected override ListTasksRequest Initializer => new ListTasksRequest()
		{
			RequestConfiguration = new RequestConfiguration { OpaqueId = CallIsolatedValue },
		};

		protected override bool SupportsDeserialization => false;
		protected override string UrlPath => $"/_tasks?pretty=true&error_trace=true";

		protected override LazyResponses ClientUsage() => Calls(
			(c, f) => c.Tasks.List(f),
			(c, f) => c.Tasks.ListAsync(f),
			(c, r) => c.Tasks.List(r),
			(c, r) => c.Tasks.ListAsync(r)
		);

		protected override void OnBeforeCall(IElasticClient client)
		{
			var searchResponse = client.Search<Project>(s => s
					.RequestConfiguration(r => r.OpaqueId(CallIsolatedValue))
					.Scroll("10m") // Create a scroll in order to keep the task around.
			);

			searchResponse.ShouldBeValid();
		}

		protected override void ExpectResponse(ListTasksResponse response)
		{
			response.ShouldBeValid();
			foreach (var node in response.Nodes)
			foreach (var task in node.Value.Tasks)
			{
				task.Value.Headers.Should().NotBeNull();
				if (task.Value.Headers.TryGetValue(RequestData.OpaqueIdHeader, out var opaqueIdValue))
					opaqueIdValue.Should()
						.Be(CallIsolatedValue,
							$"OpaqueId header {opaqueIdValue} did not match {CallIsolatedValue}");
				// TODO: Determine if this is a valid assertion i.e. should all tasks returned have an OpaqueId header?
//				else
//				{
//					Assert.True(false,
//						$"No OpaqueId header for task {task.Key} and OpaqueId value {this.CallIsolatedValue}");
//				}
			}
		}
	}

	[SkipVersion("<6.5.0", "_clusters on response only available in 6.1.0+, but ability to skip_unavailable only works in 6.5.0+")]
	public class CrossClusterSearchApiTests
		: ApiIntegrationTestBase<CrossCluster, ISearchResponse<Project>, ISearchRequest, SearchDescriptor<Project>, SearchRequest<Project>>
	{
		public CrossClusterSearchApiTests(CrossCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override bool ExpectIsValid => true;

		protected override object ExpectJson => new
		{
			query = new
			{
				match_all = new { }
			}
		};

		protected override int ExpectStatusCode => 200;

		protected override Func<SearchDescriptor<Project>, ISearchRequest> Fluent => s => s
			.Index(Nest.Indices.Index<Project>().And("cluster_two:project"))
			.Query(q => q
				.MatchAll()
			);

		protected override HttpMethod HttpMethod => HttpMethod.POST;

		protected override SearchRequest<Project> Initializer => new SearchRequest<Project>(Nest.Indices.Index<Project>().And("cluster_two:project"))
		{
			Query = new MatchAllQuery()
		};

		protected override string UrlPath => $"/project%2Ccluster_two%3Aproject/_search";

		protected override LazyResponses ClientUsage() => Calls(
			(c, f) => c.Search(f),
			(c, f) => c.SearchAsync(f),
			(c, r) => c.Search<Project>(r),
			(c, r) => c.SearchAsync<Project>(r)
		);

		protected override void ExpectResponse(ISearchResponse<Project> response)
		{
			response.Clusters.Should().NotBeNull();
			response.Clusters.Total.Should().Be(2);
			response.Clusters.Skipped.Should().Be(1);
			response.Clusters.Successful.Should().Be(1);
		}
	}

	public class SearchWithPointInTimeApiTests
		: ApiTestBase<ReadOnlyCluster, ISearchResponse<Project>, ISearchRequest, SearchDescriptor<Project>, SearchRequest<Project>>
	{
		public SearchWithPointInTimeApiTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }
		
		protected override object ExpectJson => new
		{
			size = 1,
			query = new
			{
				match_all = new { }
			},
			pit = new
			{
				id = "a-long-id",
				keep_alive = "1m"
			}
		};
		
		protected override Func<SearchDescriptor<Project>, ISearchRequest> Fluent => s => s
			.Size(1)
			.Query(q => q.MatchAll())
			.AllIndices()
			.PointInTime("a-long-id", pit => pit.KeepAlive(new Time(TimeSpan.FromMinutes(1))));

		protected override HttpMethod HttpMethod => HttpMethod.POST;

		protected override SearchRequest<Project> Initializer => new SearchRequest<Project>(Nest.Indices.All)
		{
			Size = 1,
			Query = new QueryContainer(new MatchAllQuery()),
			PointInTime = new Nest.PointInTime("a-long-id", "1m")
		};

		protected override string UrlPath => "/_search";

		protected override LazyResponses ClientUsage() => Calls(
			(c, f) => c.Search(f),
			(c, f) => c.SearchAsync(f),
			(c, r) => c.Search<Project>(r),
			(c, r) => c.SearchAsync<Project>(r)
		);
	}
}
