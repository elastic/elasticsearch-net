using System;
using System.Collections.Generic;
using System.Linq;
using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Domain;
using Tests.Framework;
using Tests.Framework.Integration;
using Tests.Framework.ManagedElasticsearch.Clusters;
using Xunit;
using static Nest.Infer;

namespace Tests.Search.Search.Rescoring
{
	/**
	 * Rescoring can help to improve precision by reordering just the top (eg 100 - 500) documents
	 * returned by the query and post_filter phases, using a secondary (usually more costly) algorithm,
	 * instead of applying the costly algorithm to all documents in the index.
	 *
	 * See the Elasticsearch documentation on {ref_current}/search-request-rescore.html[Rescoring] for more detail.
	 */
	public class RescoreUsageTests : SearchUsageTestBase
	{
		public RescoreUsageTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override object ExpectJson => new
		{
			from = 10,
			size = 20,
			query = new
			{
				match_all = new { }
			},
			rescore = new object[]
			{
				new
				{
					window_size = 20,
					query = new
					{
						score_mode = "multiply",
						rescore_query = new
						{
							constant_score = new
							{
								filter = new
								{
									terms = new
									{
										tags = new [] { "eos", "sit", "sed" }
									}
								}
							}
						}
					}
				},
				new
				{
					query = new
					{
						score_mode = "total",
						rescore_query = new
						{
							function_score = new
							{
								functions = new object[]
								{
									new
									{
										random_score = new
										{
											seed = 1337,
											field = "_seq_no"
										}
									}
								}
							}
						}
					}
				},
			}
		};

		protected override Func<SearchDescriptor<Project>, ISearchRequest> Fluent => s => s
			.From(10)
			.Size(20)
			.Query(q => q
				.MatchAll()
			)
			.Rescore(r => r
				.Rescore(rr => rr
					.WindowSize(20)
					.RescoreQuery(rq => rq
						.ScoreMode(ScoreMode.Multiply)
						.Query(q => q
							.ConstantScore(cs => cs
								.Filter(f => f
									.Terms(t => t
										.Field(p => p.Tags.First())
										.Terms("eos", "sit", "sed")
									)
								)
							)
						)
					)
				)
				.Rescore(rr => rr
					.RescoreQuery(rq => rq
						.ScoreMode(ScoreMode.Total)
						.Query(q => q
							.FunctionScore(fs => fs
								.Functions(f => f
									.RandomScore(rs=>rs.Seed(1337).Field("_seq_no"))
								)
							)
						)
					)
				)
			);

		protected override SearchRequest<Project> Initializer => new SearchRequest<Project>
		{
			From = 10,
			Size = 20,
			Query = new QueryContainer(new MatchAllQuery()),
			Rescore = new List<IRescore>
			{
				new Rescore
				{
					WindowSize = 20,
					Query = new RescoreQuery
					{
						ScoreMode = ScoreMode.Multiply,
						Query = new ConstantScoreQuery
						{
							Filter = new TermsQuery
							{
								Field = Infer.Field<Project>(p => p.Tags.First()),
								Terms = new[] { "eos", "sit", "sed" }
							}
						}
					}
				},
				new Rescore
				{
					Query = new RescoreQuery
					{
						ScoreMode = ScoreMode.Total,
						Query = new FunctionScoreQuery
						{
							Functions = new List<IScoreFunction>
							{
								new RandomScoreFunction { Seed = 1337, Field = "_seq_no" },
							}
						}
					}
				}
			}
		};
	}
}
