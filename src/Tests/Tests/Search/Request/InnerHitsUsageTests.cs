using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Bogus;
using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using Tests.Core.Client;
using Tests.Core.Extensions;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Framework;
using Tests.Framework.Integration;
using Tests.Framework.ManagedElasticsearch.Clusters;
using static Nest.Infer;

namespace Tests.Search.Request
{
	//hide
	public interface IRoyal
	{
		string Name { get; set; }
		JoinField Join { get; set; }
	}

	[ElasticsearchType(IdProperty = nameof(Name))]
	public abstract class RoyalBase<TRoyal> : IRoyal
		where TRoyal : class, IRoyal
	{
		protected static int IdState = 0;

		protected static int GenerationSeed()
		{
			unchecked
			{
				var seed = TestClient.Configuration.Seed;

				if (typeof(TRoyal) == typeof(King))
					return seed;
				if (typeof(TRoyal) == typeof(Prince))
					return seed * 2;
				if (typeof(TRoyal) == typeof(Duke))
					return seed * 3;
				if (typeof(TRoyal) == typeof(Earl))
					return seed * 4;
				if (typeof(TRoyal) == typeof(Baron))
					return seed * 5;

				return seed;
			}
		}

		public virtual JoinField Join { get; set; }
		public string Name { get; set; }

		public static Faker<TRoyal> Generator { get; } =
			new Faker<TRoyal>()
				.UseSeed(GenerationSeed())
				.RuleFor(p => p.Name, f => f.Person.Company.Name + IdState++);
	}

	public abstract class RoyalBase<TRoyal, TSubject> : RoyalBase<TRoyal>
		where TRoyal : class, IRoyal
		where TSubject : class, IRoyal
	{
	}

	public class King : RoyalBase<King>
	{
		public override JoinField Join { get; set; } = JoinField.Root<King>();
		public List<King> Foes { get; set; }
	}

	public class Prince : RoyalBase<Prince>
	{
		public string FullTextField { get; set; } = "default full text field text";
	}
	public class Duke : RoyalBase<Duke> { }
	public class Earl : RoyalBase<Earl> { }
	public class Baron : RoyalBase<Baron> { }

	//hide
	public class RoyalSeeder
	{
		private readonly IElasticClient _client;
		private readonly IndexName _index;

		public RoyalSeeder(IElasticClient client, IndexName index)
		{
			this._client = client;
			this._index = index;
		}

		public static readonly string RoyalType = "royal";

		private string AliasFor<TRoyal>() where TRoyal : IRoyal => $"{this._index}-{typeof(TRoyal).Name.ToLowerInvariant()}";

		private IAlias AliasFilterFor<TRoyal>(AliasDescriptor a) where TRoyal : class, IRoyal =>
			a.Filter<TRoyal>(f => f.Term(p => p.Join, Infer.Relation<TRoyal>()));

		public void Seed()
		{
			var create = this._client.CreateIndex(_index, c => c
				.Settings(s => s
					.NumberOfReplicas(0)
					.NumberOfShards(1)
				)
				.Aliases(a => a
					.Alias(AliasFor<King>(), AliasFilterFor<King>)
					.Alias(AliasFor<Prince>(), AliasFilterFor<Prince>)
					.Alias(AliasFor<Duke>(), AliasFilterFor<Duke>)
					.Alias(AliasFor<Earl>(), AliasFilterFor<Earl>)
					.Alias(AliasFor<Baron>(), AliasFilterFor<Baron>)
				)
				.Mappings(map => map
					.Map<King>(RoyalType, m => m
						.AutoMap()
						.Properties(props =>
							RoyalProps(props)
								.Nested<King>(n => n.Name(p => p.Foes).AutoMap())
								.Join(j => j
									.Name(p => p.Join)
									.Relations(r => r
										.Join<King, Prince>()
										.Join<Prince, Duke>()
										.Join<Duke, Earl>()
										.Join<Earl, Baron>()
									)
								)
						)
					)
				)
			);
			var kings = King.Generator.Generate(2)
				.Select(k =>
				{
					var foes = King.Generator.Generate(2).Select(f =>
					{
						f.Join = null;
						return f;
					}).ToList();
					k.Foes = foes;
					return k;
				});

			var bulk = new BulkDescriptor();
			IndexAll(bulk, () => kings, indexChildren: king =>
				IndexAll(bulk, () => Prince.Generator.Generate(2), king, prince =>
					IndexAll(bulk, () => Duke.Generator.Generate(3), prince, duke =>
						IndexAll(bulk, () => Earl.Generator.Generate(5), duke, earl =>
							IndexAll(bulk, () => Baron.Generator.Generate(1), earl)
						)
					)
				)
			);
			this._client.Bulk(bulk);
			this._client.Refresh(this._index);
		}


		private PropertiesDescriptor<TRoyal> RoyalProps<TRoyal>(PropertiesDescriptor<TRoyal> props) where TRoyal : class, IRoyal =>
			props.Keyword(s => s.Name(p => p.Name));

		private void IndexAll<TRoyal>(BulkDescriptor bulk, Func<IEnumerable<TRoyal>> create, Action<TRoyal> indexChildren = null)
			where TRoyal : class, IRoyal =>
			IndexAll<TRoyal, TRoyal>(bulk, create, null, indexChildren);

		private void IndexAll<TRoyal, TParent>(BulkDescriptor bulk, Func<IEnumerable<TRoyal>> create, TParent parent = null,
			Action<TRoyal> indexChildren = null)
			where TRoyal : class, IRoyal
			where TParent : class, IRoyal
		{
			var current = create();
			//looping twice horrible but easy to debug :)
			var royals = current.ToList();
			foreach (var royal in royals)
			{
				var royal1 = royal;
				if (parent == null) royal.Join = JoinField.Root<TRoyal>();
				if (royal.Join == null) royal.Join = JoinField.Link<TRoyal, TParent>(parent);
				bulk.Index<TRoyal>(i => i.Document(royal1).Index(_index).Type(RoyalType).Routing(parent == null ? royal.Name : parent.Name));
			}
			if (indexChildren == null) return;
			foreach (var royal in royals)
				indexChildren(royal);
		}
	}

	/**
	* [[inner-hits-usage]]
	*== Inner Hits Usage
	*
	* The {ref_current}/mapping-parent-field.html[parent/child] and {ref_current}/nested.html[nested] features allow the
	* return of documents that have matches in a different scope.
	* In the parent/child case, parent document are returned based on matches in child documents or child document
	* are returned based on matches in parent documents. In the nested case, documents are returned based on matches in nested inner objects.
	*
	* In both cases, the actual matches in the different scopes that caused a document to be returned is hidden.
	* In many cases, it’s very useful to know _which_ inner nested objects (in the case of nested) or children/parent
	* documents (in the case of parent/child) caused certain information to be returned.
	* The inner hits feature can be used for this. This feature returns per search hit in the search response additional
	* nested hits that caused a search hit to match in a different scope.
	*
	* Inner hits can be used by defining an `inner_hits` definition on a `nested`, `has_child` or `has_parent` query and filter.
	*
	* See the Elasticsearch documentation on {ref_current}/search-request-inner-hits.html[Inner hits] for more detail.
	*/
	public abstract class InnerHitsApiTestsBase<TRoyal> : ApiIntegrationTestBase<IntrusiveOperationCluster, ISearchResponse<TRoyal>,
		ISearchRequest, SearchDescriptor<TRoyal>, SearchRequest<TRoyal>>
		where TRoyal : class, IRoyal
	{
		protected InnerHitsApiTestsBase(IntrusiveOperationCluster cluster, EndpointUsage usage) : base(cluster, usage)
		{
		}

		protected abstract IndexName Index { get; }
		protected override void IntegrationSetup(IElasticClient client, CallUniqueValues values) => new RoyalSeeder(this.Client, Index).Seed();

		protected override LazyResponses ClientUsage() => Calls(
			fluent: (client, f) => client.Search<TRoyal>(f),
			fluentAsync: (client, f) => client.SearchAsync<TRoyal>(f),
			request: (client, r) => client.Search<TRoyal>(r),
			requestAsync: (client, r) => client.SearchAsync<TRoyal>(r)
		);

		protected override bool ExpectIsValid => true;
		protected override int ExpectStatusCode => 200;
		protected override HttpMethod HttpMethod => HttpMethod.POST;
		protected override string UrlPath => $"/{Index}/{RoyalSeeder.RoyalType}/_search";

		protected override SearchDescriptor<TRoyal> NewDescriptor() => new SearchDescriptor<TRoyal>().Index(Index);
	}

	/**[float]
	*=== Query Inner Hits
	*/
	public class QueryInnerHitsApiTests : InnerHitsApiTestsBase<King>
	{
		public QueryInnerHitsApiTests(IntrusiveOperationCluster cluster, EndpointUsage usage) : base(cluster, usage)
		{
		}

		private static IndexName IndexName { get; } = RandomString();
		protected override IndexName Index => QueryInnerHitsApiTests.IndexName;

		protected override object ExpectJson { get; } = new
		{
			query = new
			{
				@bool = new
				{
					should = new object[]
					{
						new
						{
							has_child = new
							{
								type = "prince",
								query = new
								{
									match = new { fullTextField = new { query = "default" } }
								},
								inner_hits = new
								{
									name = "princes",
									docvalue_fields = new []{"name"},
									highlight = new
									{
										fields = new { fullTextField = new { } }
									},
									ignore_unmapped = false,
									version = true
								}
							}
						},
						new
						{
							nested = new
							{
								query = new { match_all = new { } },
								path = "foes",
								inner_hits = new { version = true }
							}
						}
					}
				}
			},
			version = true
		};

		protected override Func<SearchDescriptor<King>, ISearchRequest> Fluent => s => s
			.Index(Index)
			.Type(RoyalSeeder.RoyalType)
			.Query(q =>
				q.HasChild<Prince>(hc => hc
					.Query(hcq => hcq.Match(m => m.Field(p => p.FullTextField).Query("default")))
					.InnerHits(ih => ih
						.DocValueFields(f=>f.Field(p=>p.Name))
						.Name("princes")
						.Highlight(h=>h.Fields(f=>f.Field(p=>p.FullTextField)))
						.IgnoreUnmapped(false)
						.Version()
					)

				) || q.Nested(n => n
					.Path(p => p.Foes)
					.Query(nq => nq.MatchAll())
					.InnerHits(i => i.Version())
				)
			)
			.Version();

		protected override SearchRequest<King> Initializer => new SearchRequest<King>(Index, RoyalSeeder.RoyalType)
		{
			Query = new HasChildQuery
			{
				Type = typeof(Prince),
				Query = new MatchQuery { Field = Field<Prince>(p=>p.FullTextField), Query = "default" },
				InnerHits = new InnerHits
				{
					Name = "princes",
					DocValueFields = Field<Prince>(p=>p.Name),
					Highlight = Highlight.Field(Field<Prince>(p=>p.FullTextField)),
					IgnoreUnmapped = false,
					Version = true
				}
			} || new NestedQuery
			{
				Path = Field<King>(p => p.Foes),
				Query = new MatchAllQuery(),
				InnerHits = new InnerHits()
				{
					Version = true
				}
			},
			Version = true
		};

		protected override void ExpectResponse(ISearchResponse<King> response)
		{
			response.Hits.Should().NotBeEmpty();
			foreach (var hit in response.Hits)
			{
				hit.Id.Should().NotBeNullOrEmpty();
				hit.Index.Should().NotBeNullOrEmpty();
				hit.Version?.Should().Be(1);


				var princes = hit.InnerHits["princes"].Documents<Prince>();
				princes.Should().NotBeEmpty();
				foreach (var princeHit in hit.InnerHits["princes"].Hits.Hits)
				{
					var highlights = princeHit.Highlights;
					highlights.Should().NotBeNull("princes should have highlights");
					highlights.Should().ContainKey("fullTextField", "we are highlighting this field");
					var hl = highlights["fullTextField"];
					hl.Highlights.Should().NotBeEmpty("all docs have the same text so should all highlight")
						.And.Contain(s => s.Contains("<em>default</em>"), "default to be highlighted as its part of the query");

					princeHit.Fields.Should().NotBeNull("all princes have a keyword name so fields should be returned");
					var docValueName = princeHit.Fields.ValueOf<Prince, string>(p=>p.Name);
					docValueName.Should().NotBeNullOrWhiteSpace("value of name on Fields");

					princeHit.Version?.Should().Be(1);
				}

				var foes = hit.InnerHits["foes"].Documents<King>();
				foes.Should().NotBeEmpty();
			}
		}
	}
}
