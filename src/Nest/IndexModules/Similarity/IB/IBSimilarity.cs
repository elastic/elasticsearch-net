// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

﻿using System.Runtime.Serialization;
// ReSharper disable InconsistentNaming

namespace Nest
{
	/// <summary>
	/// Information based model similarity.
	/// The algorithm is based on the concept that the information content in any symbolic distribution sequence
	/// is primarily determined by the repetitive usage of its basic elements.
	/// For written texts this challenge would correspond to comparing the writing styles of different authors.
	/// </summary>
	public interface IIBSimilarity : ISimilarity
	{
		/// <summary>
		/// The distribution
		/// </summary>
		[DataMember(Name ="distribution")]
		IBDistribution? Distribution { get; set; }

		/// <summary>
		/// The lambda
		/// </summary>
		[DataMember(Name ="lambda")]
		IBLambda? Lambda { get; set; }

		/// <summary>
		/// The normalization
		/// </summary>
		[DataMember(Name ="normalization")]
		Normalization? Normalization { get; set; }

		/// <summary>
		/// Normalization model that assumes a uniform distribution of the term frequency.
		/// </summary>
		[DataMember(Name ="normalization.h1.c")]
		double? NormalizationH1C { get; set; }

		/// <summary>
		///  Normalization model in which the term frequency is inversely related to the length.
		/// </summary>
		[DataMember(Name ="normalization.h2.c")]
		double? NormalizationH2C { get; set; }

		/// <summary>
		///  Dirichlet Priors normalization
		/// </summary>
		[DataMember(Name ="normalization.h3.c")]
		double? NormalizationH3C { get; set; }

		/// <summary>
		/// Pareto-Zipf Normalization
		/// </summary>
		[DataMember(Name ="normalization.z.z")]
		double? NormalizationZZ { get; set; }
	}

	/// <inheritdoc />
	public class IBSimilarity : IIBSimilarity
	{
		/// <inheritdoc />
		public IBDistribution? Distribution { get; set; }

		/// <inheritdoc />
		public IBLambda? Lambda { get; set; }

		/// <inheritdoc />
		public Normalization? Normalization { get; set; }

		/// <inheritdoc />
		public double? NormalizationH1C { get; set; }

		/// <inheritdoc />
		public double? NormalizationH2C { get; set; }

		/// <inheritdoc />
		public double? NormalizationH3C { get; set; }

		/// <inheritdoc />
		public double? NormalizationZZ { get; set; }

		public string Type => "IB";
	}

	/// <inheritdoc cref="IIBSimilarity" />
	public class IBSimilarityDescriptor
		: DescriptorBase<IBSimilarityDescriptor, IIBSimilarity>, IIBSimilarity
	{
		IBDistribution? IIBSimilarity.Distribution { get; set; }
		IBLambda? IIBSimilarity.Lambda { get; set; }
		Normalization? IIBSimilarity.Normalization { get; set; }
		double? IIBSimilarity.NormalizationH1C { get; set; }
		double? IIBSimilarity.NormalizationH2C { get; set; }
		double? IIBSimilarity.NormalizationH3C { get; set; }
		double? IIBSimilarity.NormalizationZZ { get; set; }
		string ISimilarity.Type => "IB";

		/// <inheritdoc cref="IIBSimilarity.Distribution" />
		public IBSimilarityDescriptor Distribution(IBDistribution? distribution) => Assign(distribution, (a, v) => a.Distribution = v);

		/// <inheritdoc cref="IIBSimilarity.Lambda" />
		public IBSimilarityDescriptor Lambda(IBLambda? lambda) => Assign(lambda, (a, v) => a.Lambda = v);

		/// <inheritdoc cref="IIBSimilarity.Normalization" />
		public IBSimilarityDescriptor NoNormalization() => Assign(Normalization.No, (a, v) => a.Normalization = v);

		/// <summary>
		/// Normalization model that assumes a uniform distribution of the term frequency.
		/// </summary>
		/// <param name="c">hyper-parameter that controls the term frequency normalization with respect to the document length.</param>
		public IBSimilarityDescriptor NormalizationH1(double? c) => Assign(c, (a, v) =>
		{
			a.Normalization = Normalization.H1;
			a.NormalizationH1C = v;
		});

		/// <summary>
		/// Normalization model in which the term frequency is inversely related to the length.
		/// </summary>
		/// <param name="c">hyper-parameter that controls the term frequency normalization with respect to the document length.</param>
		public IBSimilarityDescriptor NormalizationH2(double? c) => Assign(c, (a, v) =>
		{
			a.Normalization = Normalization.H2;
			a.NormalizationH1C = v;
		});

		/// <summary>
		/// Dirichlet Priors normalization
		/// </summary>
		/// <param name="mu">smoothing parameter μ.</param>
		public IBSimilarityDescriptor NormalizationH3(double? mu) => Assign(mu, (a, v) =>
		{
			a.Normalization = Normalization.H3;
			a.NormalizationH1C = v;
		});

		/// <summary>
		/// Pareto-Zipf Normalization
		/// </summary>
		/// <param name="z">represents A/(A+1) where A measures the specificity of the language..</param>
		public IBSimilarityDescriptor NormalizationZ(double? z) => Assign(z, (a, v) =>
		{
			a.Normalization = Normalization.Z;
			a.NormalizationH1C = v;
		});
	}
}
