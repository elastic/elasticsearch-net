// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

﻿using System.Runtime.Serialization;

namespace Nest
{
	/// <summary>
	/// Implements the divergence from randomness (DFR) framework introduced in Gianni Amati and Cornelis Joost Van Rijsbergen.
	/// 2002.
	/// Probabilistic models of information retrieval based on measuring the divergence from randomness. ACM Trans. Inf. Syst.
	/// 20, 4 (October
	/// 2002), 357-389.
	/// The DFR scoring formula is composed of three separate components: the basic model, the aftereffect and an additional
	/// normalization
	/// component,
	/// represented by the classes BasicModel, AfterEffect and Normalization, respectively.The names of these classes were
	/// chosen to match the
	/// names of their counterparts in the Terrier IR engine.
	/// </summary>
	public interface IDFRSimilarity : ISimilarity
	{
		/// <summary>
		/// The after effect
		/// </summary>
		[DataMember(Name = "after_effect")]
		DFRAfterEffect? AfterEffect { get; set; }

		/// <summary>
		/// The basic model
		/// </summary>
		[DataMember(Name = "basic_model")]
		DFRBasicModel? BasicModel { get; set; }

		/// <summary>
		/// The normalization
		/// </summary>
		[DataMember(Name = "normalization")]
		Normalization? Normalization { get; set; }

		/// <summary>
		/// Normalization model that assumes a uniform distribution of the term frequency.
		/// </summary>
		[DataMember(Name = "normalization.h1.c")]
		double? NormalizationH1C { get; set; }

		/// <summary>
		///  Normalization model in which the term frequency is inversely related to the length.
		/// </summary>
		[DataMember(Name = "normalization.h2.c")]
		double? NormalizationH2C { get; set; }

		/// <summary>
		///  Dirichlet Priors normalization
		/// </summary>
		[DataMember(Name = "normalization.h3.c")]
		double? NormalizationH3C { get; set; }

		/// <summary>
		/// Pareto-Zipf Normalization
		/// </summary>
		[DataMember(Name = "normalization.z.z")]
		// ReSharper disable once InconsistentNaming
		double? NormalizationZZ { get; set; }
	}

	/// <inheritdoc />
	public class DFRSimilarity : IDFRSimilarity
	{
		/// <inheritdoc />
		public DFRAfterEffect? AfterEffect { get; set; }

		/// <inheritdoc />
		public DFRBasicModel? BasicModel { get; set; }

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

		public string Type => "DFR";
	}

	/// <inheritdoc />
	public class DFRSimilarityDescriptor
		: DescriptorBase<DFRSimilarityDescriptor, IDFRSimilarity>, IDFRSimilarity
	{
		DFRAfterEffect? IDFRSimilarity.AfterEffect { get; set; }
		DFRBasicModel? IDFRSimilarity.BasicModel { get; set; }
		Normalization? IDFRSimilarity.Normalization { get; set; }
		double? IDFRSimilarity.NormalizationH1C { get; set; }
		double? IDFRSimilarity.NormalizationH2C { get; set; }
		double? IDFRSimilarity.NormalizationH3C { get; set; }
		double? IDFRSimilarity.NormalizationZZ { get; set; }
		string ISimilarity.Type => "DFR";

		/// <inheritdoc />
		public DFRSimilarityDescriptor BasicModel(DFRBasicModel? model) => Assign(model, (a, v) => a.BasicModel = v);

		/// <inheritdoc />
		public DFRSimilarityDescriptor AfterEffect(DFRAfterEffect? afterEffect) => Assign(afterEffect, (a, v) => a.AfterEffect = v);

		/// <inheritdoc />
		public DFRSimilarityDescriptor NoNormalization() => Assign(Normalization.No, (a, v) => a.Normalization = v);

		/// <summary>
		/// Normalization model that assumes a uniform distribution of the term frequency.
		/// </summary>
		/// <param name="c">hyper-parameter that controls the term frequency normalization with respect to the document length.</param>
		public DFRSimilarityDescriptor NormalizationH1(double? c) => Assign(c, (a, v) =>
		{
			a.Normalization = v == null ? (Normalization?)null : Normalization.H1;
			a.NormalizationH1C = v;
		});

		/// <summary>
		/// Normalization model in which the term frequency is inversely related to the length.
		/// </summary>
		/// <param name="c">hyper-parameter that controls the term frequency normalization with respect to the document length.</param>
		public DFRSimilarityDescriptor NormalizationH2(double? c) => Assign(c, (a, v) =>
		{
			a.Normalization = v == null ? (Normalization?)null : Normalization.H2;
			a.NormalizationH1C = v;
		});

		/// <summary>
		/// Dirichlet Priors normalization
		/// </summary>
		/// <param name="mu">smoothing parameter μ.</param>
		public DFRSimilarityDescriptor NormalizationH3(double? mu) => Assign(mu, (a, v) =>
		{
			a.Normalization = v == null ? (Normalization?)null : Normalization.H3;
			a.NormalizationH1C = v;
		});

		/// <summary> Pareto-Zipf Normalization </summary>
		/// <param name="z">represents A/(A+1) where A measures the specificity of the language..</param>
		public DFRSimilarityDescriptor NormalizationZ(double? z) => Assign(z, (a, v) =>
		{
			a.Normalization = v == null ? (Normalization?)null : Normalization.Z;
			a.NormalizationH1C = v;
		});
	}
}
