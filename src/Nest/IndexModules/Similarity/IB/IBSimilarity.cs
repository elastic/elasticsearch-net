using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Nest
{

	public interface IIBSimilarity : ISimilarity
	{
		[JsonProperty("distribution")]
		IBDistribution? Distribution { get; set; }

		[JsonProperty("lambda")]
		IBLambda? Lambda { get; set; }

		[JsonProperty("normalization")]
		Normalization? Normalization { get; set; }

		/// <summary>
		/// Normalization model that assumes a uniform distribution of the term frequency. 
		/// </summary>
		[JsonProperty("normalization.h1.c")]
		float? NormalizationH1C { get; set; }

		/// <summary>
		///  Normalization model in which the term frequency is inversely related to the length.
		/// </summary>
		[JsonProperty("normalization.h2.c")]
		float? NormalizationH2C { get; set; }

		/// <summary>
		///  Dirichlet Priors normalization
		/// </summary>
		[JsonProperty("normalization.h3.c")]
		float? NormalizationH3C { get; set; }

		/// <summary>
		/// Pareto-Zipf Normalization
		/// </summary>
		[JsonProperty("normalization.z.z")]
		float? NormalizationZZ { get; set; }
	}
	public class IBSimilarity : IIBSimilarity
	{
		public string Type => "IB";

		public IBDistribution? Distribution { get; set; }

		public IBLambda? Lambda { get; set; }

		public Normalization? Normalization { get; set; }

		public float? NormalizationH1C { get; set; }

		public float? NormalizationH2C { get; set; }

		public float? NormalizationH3C { get; set; }

		public float? NormalizationZZ { get; set; }
	}

	public class IBSimilarityDescriptor
		: DescriptorBase<IBSimilarityDescriptor, IIBSimilarity>, IIBSimilarity
	{
		string ISimilarity.Type => "LIB";
		IBDistribution? IIBSimilarity.Distribution { get; set; }
		IBLambda? IIBSimilarity.Lambda { get; set; }
		Normalization? IIBSimilarity.Normalization { get; set; }
		float? IIBSimilarity.NormalizationH1C { get; set; }
		float? IIBSimilarity.NormalizationH2C { get; set; }
		float? IIBSimilarity.NormalizationH3C { get; set; }
		float? IIBSimilarity.NormalizationZZ { get; set; }

		public IBSimilarityDescriptor Distribution(IBDistribution? distribution) => Assign(a => a.Distribution = distribution);
		public IBSimilarityDescriptor Lambda(IBLambda? lambda) => Assign(a => a.Lambda = lambda);
		public IBSimilarityDescriptor NoNormalization() => Assign(a => a.Normalization = Normalization.No);

		/// <summary>
		/// Normalization model that assumes a uniform distribution of the term frequency. 
		/// </summary>
		/// <param name="c">hyper-parameter that controls the term frequency normalization with respect to the document length.</param>
		public IBSimilarityDescriptor NormalizationH1(float c) => Assign(a =>
		{
			a.Normalization = Normalization.H1;
			a.NormalizationH1C = c;
		});

		/// <summary>
		/// Normalization model in which the term frequency is inversely related to the length.
		/// </summary>
		/// <param name="c">hyper-parameter that controls the term frequency normalization with respect to the document length.</param>
		public IBSimilarityDescriptor NormalizationH2(float c) => Assign(a =>
		{
			a.Normalization = Normalization.H2;
			a.NormalizationH1C = c;
		});

		/// <summary>
		/// Dirichlet Priors normalization
		/// </summary>
		/// <param name="mu">smoothing parameter μ.</param>
		public IBSimilarityDescriptor NormalizationH3(float mu) => Assign(a =>
		{
			a.Normalization = Normalization.H3;
			a.NormalizationH1C = mu;
		});

		/// <summary>
		/// Pareto-Zipf Normalization
		/// </summary>
		/// <param name="mu">represents A/(A+1) where A measures the specificity of the language..</param>
		public IBSimilarityDescriptor NormalizationZ(float z) => Assign(a =>
		{
			a.Normalization = Normalization.Z;
			a.NormalizationH1C = z;
		});
	}

}
