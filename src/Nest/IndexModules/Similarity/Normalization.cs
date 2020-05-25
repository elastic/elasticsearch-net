// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Runtime.Serialization;
using Elasticsearch.Net;


namespace Nest
{
	[StringEnum]
	public enum Normalization
	{
		/// <summary>
		/// Implementation used when there is no normalization.
		/// </summary>
		[EnumMember(Value = "no")]
		No,

		/// <summary>
		/// Normalization model that assumes a uniform distribution of the term frequency.
		/// </summary>
		[EnumMember(Value = "h1")]
		H1,

		/// <summary>
		///  Normalization model in which the term frequency is inversely related to the length.
		/// </summary>
		[EnumMember(Value = "h2")]
		H2,

		/// <summary>
		/// Dirichlet Priors normalization
		/// </summary>
		[EnumMember(Value = "h3")]
		H3,

		/// <summary>
		/// Pareto-Zipf Normalization
		/// </summary>
		[EnumMember(Value = "z")]
		Z,
	}
}
