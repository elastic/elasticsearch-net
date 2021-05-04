// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Runtime.Serialization;

namespace Nest
{
	/// <summary>
	/// Similarity that implements the divergence from independence model
	/// </summary>
	public interface IDFISimilarity : ISimilarity
	{
		/// <summary>
		/// The independence measure
		/// </summary>
		[DataMember(Name ="independence_measure")]
		DFIIndependenceMeasure? IndependenceMeasure { get; set; }
	}

	/// <inheritdoc />
	public class DFISimilarity : IDFISimilarity
	{
		/// <inheritdoc />
		public DFIIndependenceMeasure? IndependenceMeasure { get; set; }

		public string Type => "DFI";
	}

	/// <inheritdoc />
	public class DFISimilarityDescriptor
		: DescriptorBase<DFISimilarityDescriptor, IDFISimilarity>, IDFISimilarity
	{
		DFIIndependenceMeasure? IDFISimilarity.IndependenceMeasure { get; set; }
		string ISimilarity.Type => "DFI";

		/// <inheritdoc />
		public DFISimilarityDescriptor IndependenceMeasure(DFIIndependenceMeasure? independenceMeasure) =>
			Assign(independenceMeasure, (a, v) => a.IndependenceMeasure = v);
	}
}
