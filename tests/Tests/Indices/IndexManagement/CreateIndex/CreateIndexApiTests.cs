// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Collections.Generic;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Elastic.Transport;
using FluentAssertions;
using Nest;
using Tests.Core.Extensions;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Framework.EndpointTests;
using Tests.Framework.EndpointTests.TestState;

namespace Tests.Indices.IndexManagement.CreateIndex
{
	public class CreateIndexApiTests
		: ApiIntegrationTestBase<WritableCluster, CreateIndexResponse, ICreateIndexRequest, CreateIndexDescriptor, CreateIndexRequest>
	{
		public CreateIndexApiTests(WritableCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override bool ExpectIsValid => true;

		protected override object ExpectJson { get; } = new
		{
			settings = new Dictionary<string, object>
			{
				{ "index.number_of_replicas", 1 },
				{ "index.number_of_shards", 1 },
				{ "index.queries.cache.enabled", true },
				{
					"similarity", new
					{
						bm25 = new
						{
							k1 = 1.1,
							b = 1.0,
							discount_overlaps = true,
							type = "BM25"
						},
						dfi = new
						{
							independence_measure = "chisquared",
							type = "DFI"
						},
						dfr = new Dictionary<string, object>
						{
							{ "basic_model", "if" },
							{ "after_effect", "b" },
							{ "normalization", "h1" },
							{ "normalization.h1.c", 1.1 },
							{ "type", "DFR" }
						},
						ib = new Dictionary<string, object>
						{
							{ "distribution", "ll" },
							{ "lambda", "df" },
							{ "normalization", "h1" },
							{ "normalization.h1.c", 1.2 },
							{ "type", "IB" }
						},
						lmd = new
						{
							mu = 2,
							type = "LMDirichlet"
						},
						lmj = new
						{
							lambda = 0.9,
							type = "LMJelinekMercer"
						},
						scripted_tfidf = new
						{
							type = "scripted",
							script = new
							{
								source =
									"double tf = Math.sqrt(doc.freq); double idf = Math.log((field.docCount+1.0)/(term.docFreq+1.0)) + 1.0; double norm = 1/Math.sqrt(doc.length); return query.boost * tf * idf * norm;"
							}
						}
					}
				}
			}
		};

		protected override int ExpectStatusCode => 200;

		protected override Func<CreateIndexDescriptor, ICreateIndexRequest> Fluent => d => d
			.Settings(s => s
				.NumberOfReplicas(1)
				.NumberOfShards(1)
				.Queries(q => q
					.Cache(c => c
						.Enabled()
					)
				)
				.Similarity(si => si
					.BM25("bm25", b => b
						.B(1.0)
						.K1(1.1)
						.DiscountOverlaps()
					)
					.DFI("dfi", df => df
						.IndependenceMeasure(DFIIndependenceMeasure.ChiSquared)
					)
					.DFR("dfr", df => df
						.AfterEffect(DFRAfterEffect.B)
						.BasicModel(DFRBasicModel.IF)
						.NormalizationH1(1.1)
					)
					.IB("ib", ib => ib
						.Lambda(IBLambda.DocumentFrequency)
						.NormalizationH1(1.2)
						.Distribution(IBDistribution.LogLogistic)
					)
					.LMDirichlet("lmd", lm => lm
						.Mu(2)
					)
					.LMJelinek("lmj", lm => lm
						.Lamdba(0.9)
					)
					.Scripted("scripted_tfidf", sc => sc
						.Script(ssc => ssc
							.Source(
								"double tf = Math.sqrt(doc.freq); double idf = Math.log((field.docCount+1.0)/(term.docFreq+1.0)) + 1.0; double norm = 1/Math.sqrt(doc.length); return query.boost * tf * idf * norm;")
						)
					)
				)
			);

		protected override HttpMethod HttpMethod => HttpMethod.PUT;

		protected override CreateIndexRequest Initializer => new CreateIndexRequest(CallIsolatedValue)
		{
			Settings = new Nest.IndexSettings
			{
				NumberOfReplicas = 1,
				NumberOfShards = 1,
				Queries = new QueriesSettings
				{
					Cache = new QueriesCacheSettings
					{
						Enabled = true
					}
				},
				Similarity = new Similarities
				{
					{
						"bm25", new BM25Similarity
						{
							B = 1.0,
							K1 = 1.1,
							DiscountOverlaps = true
						}
					},
					{
						"dfi", new DFISimilarity
						{
							IndependenceMeasure = DFIIndependenceMeasure.ChiSquared
						}
					},
					{
						"dfr", new DFRSimilarity
						{
							AfterEffect = DFRAfterEffect.B,
							BasicModel = DFRBasicModel.IF,
							Normalization = Normalization.H1,
							NormalizationH1C = 1.1
						}
					},
					{
						"ib", new IBSimilarity
						{
							Distribution = IBDistribution.LogLogistic,
							Lambda = IBLambda.DocumentFrequency,
							Normalization = Normalization.H1,
							NormalizationH1C = 1.2
						}
					},
					{
						"lmd", new LMDirichletSimilarity
						{
							Mu = 2
						}
					},
					{
						"lmj", new LMJelinekMercerSimilarity
						{
							Lambda = 0.9
						}
					},
					{
						"scripted_tfidf", new ScriptedSimilarity
						{
							Script = new InlineScript(
								"double tf = Math.sqrt(doc.freq); double idf = Math.log((field.docCount+1.0)/(term.docFreq+1.0)) + 1.0; double norm = 1/Math.sqrt(doc.length); return query.boost * tf * idf * norm;")
						}
					}
				}
			}
		};

		protected override string UrlPath => $"/{CallIsolatedValue}";

		protected override LazyResponses ClientUsage() => Calls(
			(client, f) => client.Indices.Create(CallIsolatedValue, f),
			(client, f) => client.Indices.CreateAsync(CallIsolatedValue, f),
			(client, r) => client.Indices.Create(r),
			(client, r) => client.Indices.CreateAsync(r)
		);

		protected override CreateIndexDescriptor NewDescriptor() => new CreateIndexDescriptor(CallIsolatedValue);

		protected override void ExpectResponse(CreateIndexResponse response)
		{
			response.ShouldBeValid();
			response.Acknowledged.Should().BeTrue();
			response.ShardsAcknowledged.Should().BeTrue();
			response.Index.Should().Be(CallIsolatedValue);

			var indexSettings = Client.Indices.GetSettings(CallIsolatedValue);

			indexSettings.ShouldBeValid();
			indexSettings.Indices.Should().NotBeEmpty().And.ContainKey(CallIsolatedValue);

			var settings = indexSettings.Indices[CallIsolatedValue];

			settings.Settings.NumberOfShards.Should().Be(1);
			settings.Settings.NumberOfReplicas.Should().Be(1);
			settings.Settings.Queries.Cache.Enabled.Should().Be(true);

			var similarities = settings.Settings.Similarity;

			similarities.Should().NotBeNull();
			similarities.Should().ContainKey("bm25").WhichValue.Should().BeOfType<BM25Similarity>();
			similarities.Should().ContainKey("dfi").WhichValue.Should().BeOfType<DFISimilarity>();
			similarities.Should().ContainKey("dfr").WhichValue.Should().BeOfType<DFRSimilarity>();
			similarities.Should().ContainKey("ib").WhichValue.Should().BeOfType<IBSimilarity>();
			similarities.Should().ContainKey("lmd").WhichValue.Should().BeOfType<LMDirichletSimilarity>();
			similarities.Should().ContainKey("lmj").WhichValue.Should().BeOfType<LMJelinekMercerSimilarity>();
			similarities.Should().ContainKey("scripted_tfidf").WhichValue.Should().BeOfType<ScriptedSimilarity>();

			var scriptedSimilarity = (ScriptedSimilarity)similarities["scripted_tfidf"];
			scriptedSimilarity.Script.Should().NotBeNull();
			((InlineScript)scriptedSimilarity.Script).Source.Should().NotBeNullOrEmpty();
		}
	}

	public class CreateIndexWithAliasApiTests
		: ApiIntegrationTestBase<WritableCluster, CreateIndexResponse, ICreateIndexRequest, CreateIndexDescriptor, CreateIndexRequest>
	{
		public CreateIndexWithAliasApiTests(WritableCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override bool ExpectIsValid => true;

		protected override object ExpectJson => new
		{
			settings = new Dictionary<string, object>
			{
				{ "index.number_of_replicas", 0 },
				{ "index.number_of_shards", 1 },
			},
			aliases = new Dictionary<string, object>
			{
				{ CallIsolatedValue + "-alias", new { is_write_index = true } }
			}
		};

		protected override int ExpectStatusCode => 200;

		protected override Func<CreateIndexDescriptor, ICreateIndexRequest> Fluent => d => d
			.Settings(s => s
				.NumberOfReplicas(0)
				.NumberOfShards(1)
			)
			.Aliases(a => a
				.Alias(CallIsolatedValue + "-alias", aa => aa
					.IsWriteIndex()
				)
			);

		protected override HttpMethod HttpMethod => HttpMethod.PUT;

		protected override CreateIndexRequest Initializer => new CreateIndexRequest(CallIsolatedValue)
		{
			Settings = new Nest.IndexSettings
			{
				NumberOfReplicas = 0,
				NumberOfShards = 1,
			},
			Aliases = new Aliases
			{
				{ CallIsolatedValue + "-alias", new Alias { IsWriteIndex = true } }
			}
		};

		protected override string UrlPath => $"/{CallIsolatedValue}";

		protected override LazyResponses ClientUsage() => Calls(
			(client, f) => client.Indices.Create(CallIsolatedValue, f),
			(client, f) => client.Indices.CreateAsync(CallIsolatedValue, f),
			(client, r) => client.Indices.Create(r),
			(client, r) => client.Indices.CreateAsync(r)
		);

		protected override CreateIndexDescriptor NewDescriptor() => new CreateIndexDescriptor(CallIsolatedValue);

		protected override void ExpectResponse(CreateIndexResponse response)
		{
			response.ShouldBeValid();
			response.Acknowledged.Should().BeTrue();
			response.ShardsAcknowledged.Should().BeTrue();

			var indexResponse = Client.Indices.Get(CallIsolatedValue);

			indexResponse.ShouldBeValid();
			indexResponse.Indices.Should().NotBeEmpty().And.ContainKey(CallIsolatedValue);

			var aliases = indexResponse.Indices[CallIsolatedValue].Aliases;
			aliases.Count.Should().Be(1);
			aliases[CallIsolatedValue + "-alias"].IsWriteIndex.Should().BeTrue();
		}
	}


	[SkipVersion("<7.7.0", "hidden indices and aliases introduced in 7.7.0")]
	public class CreateHiddenIndexApiTests
		: ApiIntegrationTestBase<WritableCluster, CreateIndexResponse, ICreateIndexRequest, CreateIndexDescriptor, CreateIndexRequest>
	{
		public CreateHiddenIndexApiTests(WritableCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override bool ExpectIsValid => true;

		protected override object ExpectJson => new
		{
			settings = new Dictionary<string, object>
			{
				{ "index.number_of_replicas", 0 },
				{ "index.number_of_shards", 1 },
				{ "index.hidden", true }
			},
			aliases = new Dictionary<string, object>
			{
				{ CallIsolatedValue + "-alias", new { is_write_index = true, is_hidden = true } }
			}
		};

		protected override int ExpectStatusCode => 200;

		protected override Func<CreateIndexDescriptor, ICreateIndexRequest> Fluent => d => d
			.Settings(s => s
				.NumberOfReplicas(0)
				.NumberOfShards(1)
				.Hidden()
			)
			.Aliases(a => a
				.Alias(CallIsolatedValue + "-alias", aa => aa
					.IsWriteIndex()
					.IsHidden()
				)
			);

		protected override HttpMethod HttpMethod => HttpMethod.PUT;

		protected override CreateIndexRequest Initializer => new CreateIndexRequest(CallIsolatedValue)
		{
			Settings = new Nest.IndexSettings
			{
				NumberOfReplicas = 0,
				NumberOfShards = 1,
				Hidden = true
			},
			Aliases = new Aliases
			{
				{ CallIsolatedValue + "-alias", new Alias { IsWriteIndex = true, IsHidden = true} }
			}
		};

		protected override string UrlPath => $"/{CallIsolatedValue}";

		protected override LazyResponses ClientUsage() => Calls(
			(client, f) => client.Indices.Create(CallIsolatedValue, f),
			(client, f) => client.Indices.CreateAsync(CallIsolatedValue, f),
			(client, r) => client.Indices.Create(r),
			(client, r) => client.Indices.CreateAsync(r)
		);

		protected override CreateIndexDescriptor NewDescriptor() => new CreateIndexDescriptor(CallIsolatedValue);

		protected override void ExpectResponse(CreateIndexResponse response)
		{
			response.ShouldBeValid();
			response.Acknowledged.Should().BeTrue();
			response.ShardsAcknowledged.Should().BeTrue();

			var indexResponse = Client.Indices.Get(CallIsolatedValue);

			indexResponse.ShouldBeValid();
			indexResponse.Indices.Should().NotBeEmpty().And.ContainKey(CallIsolatedValue);
			var index = indexResponse.Indices[CallIsolatedValue];

			index.Settings.Hidden.Should().BeTrue();

			var aliases = indexResponse.Indices[CallIsolatedValue].Aliases;
			aliases.Count.Should().Be(1);
			aliases[CallIsolatedValue + "-alias"].IsWriteIndex.Should().BeTrue();
			aliases[CallIsolatedValue + "-alias"].IsHidden.Should().BeTrue();
		}
	}
}
