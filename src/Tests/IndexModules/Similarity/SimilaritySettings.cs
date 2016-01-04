using System;
using System.Collections.Generic;
using Nest;
using Tests.Framework;

namespace Tests.IndexModules.Similarity
{
	public class SimilaritySettings
	{
		/**
		 */

		public class Usage : PromiseUsageTestBase<ISimilarities, SimilaritiesDescriptor, Similarities>
		{
			protected override object ExpectJson => new
			{
				bm25 = new
				{
					k1 = 1.1,
					b = 1.0,
					discount_overlaps = true,
					type = "BM25"
				},
				def = new
				{
					discount_overlaps = true,
					type = "default"
				},
				dfr = new Dictionary<string, object>{
					{ "basic_model", "d" },
					{"after_effect", "b" },
					{"normalization", "h1" },
					{"normalization.h1.c", 1.1 },
					{"type", "DFR" }
				},
				ib = new Dictionary<string, object> {
					{ "distribution", "ll" },
					{"lambda", "df" },
					{"normalization", "h1" },
					{"normalization.h1.c", 1.2 },
					{"type", "IB" }
				},
				lmd = new
				{
					mu = 2,
					type = "LMDirichlet"
				},
				lmj = new
				{
					lambda = 2.0,
					type = "LMJelinekMercer"
				}
			};

			/**
			 * 
			 */
			protected override Func<SimilaritiesDescriptor, IPromise<ISimilarities>> Fluent => s => s
				.BM25("bm25", b => b
					.B(1.0)
					.K1(1.1)
					.DiscountOverlaps()
				)
				.Default("def", d => d.DiscountOverlaps())
				.DFR("dfr", d => d
					.AfterEffect(DFRAfterEffect.B)
					.BasicModel(DFRBasicModel.D)
					.NormalizationH1(1.1)
				)
				.IB("ib", d => d
					.Lambda(IBLambda.DocumentFrequency)
					.NormalizationH1(1.2)
					.Distribution(IBDistribution.LogLogistic)
				)
				.LMDirichlet("lmd", d => d.Mu(2))
				.LMJelinek("lmj", d => d.Lamdba(2.0));
			/**
			 */
			protected override Similarities Initializer =>
				new Similarities
				{
					{ "bm25", new BM25Similarity { B = 1.0, K1 = 1.1, DiscountOverlaps = true } },
					{ "def", new DefaultSimilarity { DiscountOverlaps = true } },
					{ "dfr", new DFRSimilarity
					{
						AfterEffect = DFRAfterEffect.B,
						BasicModel = DFRBasicModel.D,
						Normalization = Normalization.H1,
						NormalizationH1C = 1.1
					}},
					{ "ib", new IBSimilarity
					{
						Distribution = IBDistribution.LogLogistic,
						Lambda = IBLambda.DocumentFrequency,
						Normalization = Normalization.H1,
						NormalizationH1C = 1.2
					} },
					{ "lmd", new LMDirichletSimilarity { Mu = 2 } },
					{ "lmj", new LMJelinekMercerSimilarity { Lambda = 2.0 } }
				};
		}
	}
}
