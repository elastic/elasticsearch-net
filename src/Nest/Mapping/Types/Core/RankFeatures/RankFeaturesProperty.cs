// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Diagnostics;
using System.Runtime.Serialization;
using Elasticsearch.Net.Utf8Json;

namespace Nest
{
	/// <summary>
	/// A field that can index numeric feature vectors, so that they can later be used to boost documents in queries with a rank_feature query.
	/// It is analogous to the <see cref="IRankFeatureProperty"/> datatype, but is better suited when the list of features is sparse so that it
	/// wouldn't be reasonable to add one field to the mappings for each of them.
	/// </summary>
	[InterfaceDataContract]
	public interface IRankFeaturesProperty : IProperty
	{
		/// <summary>
		/// Rank features that correlate negatively with the score should set <see cref="PositiveScoreImpact"/>
		/// to false (defaults to true). This will be used by the rank_features query to modify the scoring
		/// formula in such a way that the score decreases with the value of the feature instead of
		/// increasing.
		/// </summary>
		[DataMember(Name = "positive_score_impact")]
		bool? PositiveScoreImpact { get; set; }
	}

	/// <inheritdoc cref="IRankFeaturesProperty" />
	public class RankFeaturesProperty : PropertyBase, IRankFeaturesProperty
	{
		public RankFeaturesProperty() : base(FieldType.RankFeatures) { }

		/// <inheritdoc />
		public bool? PositiveScoreImpact { get; set; }
	}

	/// <inheritdoc cref="IRankFeaturesProperty" />
	[DebuggerDisplay("{" + nameof(DebugDisplay) + "}")]
	public class RankFeaturesPropertyDescriptor<T>
		: PropertyDescriptorBase<RankFeaturesPropertyDescriptor<T>, IRankFeaturesProperty, T>, IRankFeaturesProperty
		where T : class
	{
		public RankFeaturesPropertyDescriptor() : base(FieldType.RankFeatures) { }
		
		bool? IRankFeaturesProperty.PositiveScoreImpact { get; set; }

		/// <inheritdoc cref="IRankFeaturesProperty.PositiveScoreImpact" />
		public RankFeaturesPropertyDescriptor<T> PositiveScoreImpact(bool? positiveScoreImpact = true) =>
			Assign(positiveScoreImpact, (a, v) => a.PositiveScoreImpact = v);
	}
}
