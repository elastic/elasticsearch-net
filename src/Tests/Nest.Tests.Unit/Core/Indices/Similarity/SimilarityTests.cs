using FluentAssertions;
using NUnit.Framework;
using System;
using System.Reflection;

namespace Nest.Tests.Unit.Core.Indices.Similarity
{
	[TestFixture]
	public class SimilarityTests : BaseJsonTests
	{
		[Test]
		public void BM25SimilarityTest()
		{
			var result = this.Similarity(s => s
				.CustomSimilarities(cs => cs.Add("my_bm25_similarity", new BM25Similarity
					{
						K1 = 2.0,
						B = 0.75,
						Normalization = "h1",
						NormalizationH1C = "1.0",
						DiscountOverlaps = true
					})
				)
			);

			this.JsonEquals(result.ConnectionStatus.Request, MethodInfo.GetCurrentMethod()); 
		}

		[Test]
		public void DefaultSimilarityTest()
		{
			var result = this.Similarity(s => s
				.CustomSimilarities(cs => cs.Add("my_default_similarity", new DefaultSimilarity
					{
						DiscountOverlaps = true,
						Normalization = "h2",
						NormalizationH2C = "2.0"
					})
        )
        .Default("my_default_similarity")
			);

			this.JsonEquals(result.ConnectionStatus.Request, MethodInfo.GetCurrentMethod()); 
		}

		[Test]
		public void DFRSimilarityTest()
		{
			var result = this.Similarity(s => s
				.CustomSimilarities(cs => cs.Add("my_dfr_similarity", new DFRSimilarity
					{
						BasicModel = "g",
						AfterEffect = "l",
						Normalization = "h3",
						NormalizationH2C = "3.0"
					})
				)
			);

			this.JsonEquals(result.ConnectionStatus.Request, MethodInfo.GetCurrentMethod()); 
		}

		[Test]
		public void IBSimilarityTest()
		{
			var result = this.Similarity(s => s
				.CustomSimilarities(cs => cs.Add("my_ib_similarity", new IBSimilarity
					{
						Distribution = "spl",
						Lambda = "df",
						Normalization = "h2",
						NormalizationH2C = "3.0"
					})
				)
			);

			this.JsonEquals(result.ConnectionStatus.Request, MethodInfo.GetCurrentMethod()); 
		}

		[Test]
		public void LMDirichletSimilarityTest()
		{
			var result = this.Similarity(s => s
				.CustomSimilarities(cs => cs.Add("my_lmdirichlet_similarity", new LMDirichletSimilarity
					{
						Mu = 1000
					})
				)
			);

			this.JsonEquals(result.ConnectionStatus.Request, MethodInfo.GetCurrentMethod()); 
		}

		[Test]
		public void LMJelinekSimilarityTest()
		{
			var result = this.Similarity(s => s
				.CustomSimilarities(cs => cs.Add("my_lmjelinek_similarity", new LMJelinekSimilarity
					{
						Lambda = 0.5
					})
				)
			);

			this.JsonEquals(result.ConnectionStatus.Request, MethodInfo.GetCurrentMethod()); 
		}

		[Test]
		public void MultipleSimilaritiesTest()
		{
			var result = this.Similarity(s => s
				.CustomSimilarities(cs => cs
					.Add("my_dfr_similarity", new DFRSimilarity
						{
							BasicModel = "g",
							AfterEffect = "l",
							Normalization = "h3",
							NormalizationH2C = "3.0"
						})
					.Add("my_ib_similarity", new IBSimilarity
					{
						Distribution = "spl",
						Lambda = "df",
						Normalization = "h2",
						NormalizationH2C = "3.0"
					})
				)
			);

			this.JsonEquals(result.ConnectionStatus.Request, MethodInfo.GetCurrentMethod()); 
		}

    [Test]
    public void SimilarityTest()
    {
      var result = this.Similarity(s => s.Default("customsimilarity"));

      this.JsonEquals(result.ConnectionStatus.Request, MethodInfo.GetCurrentMethod());
    }

		private IIndicesOperationResponse Similarity(Func<SimilarityDescriptor, SimilarityDescriptor> similaritySelector)
		{
			var result = this._client.CreateIndex(UnitTestDefaults.DefaultIndex, c => c
				.Similarity(similaritySelector)
			);
			result.Should().NotBeNull();
			result.IsValid.Should().BeTrue();
			result.ConnectionStatus.Should().NotBeNull();
			return result;
		}
	}
}
