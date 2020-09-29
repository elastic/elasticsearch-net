// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Diagnostics;
using System.Runtime.Serialization;
using Nest.Utf8Json;

namespace Nest
{
	/// <summary>
	/// A field that can index numbers so that they can later be used
	/// to boost documents in queries with a rank_feature query.
	/// </summary>
	[InterfaceDataContract]
	public interface IRankFeatureProperty : IProperty
	{
		/// <summary>
		/// Rank features that correlate negatively with the score should set <see cref="PositiveScoreImpact"/>
		/// to false (defaults to true). This will be used by the rank_feature query to modify the scoring
		/// formula in such a way that the score decreases with the value of the feature instead of
		/// increasing. For instance in web search, the url length is a commonly used feature
		/// which correlates negatively with scores.
		/// </summary>
		[DataMember(Name = "positive_score_impact")]
		bool? PositiveScoreImpact { get; set; }
	}

	/// <inheritdoc cref="IRankFeatureProperty" />
	public class RankFeatureProperty : PropertyBase, IRankFeatureProperty
	{
		public RankFeatureProperty() : base(FieldType.RankFeature) { }

		/// <inheritdoc />
		public bool? PositiveScoreImpact { get; set; }
	}

	/// <inheritdoc cref="IRankFeatureProperty" />
	[DebuggerDisplay("{DebugDisplay}")]
	public class RankFeaturePropertyDescriptor<T>
		: PropertyDescriptorBase<RankFeaturePropertyDescriptor<T>, IRankFeatureProperty, T>, IRankFeatureProperty
		where T : class
	{
		public RankFeaturePropertyDescriptor() : base(FieldType.RankFeature) { }

		bool? IRankFeatureProperty.PositiveScoreImpact { get; set; }

		/// <inheritdoc cref="IRankFeatureProperty.PositiveScoreImpact" />
		public RankFeaturePropertyDescriptor<T> PositiveScoreImpact(bool? positiveScoreImpact = true) =>
			Assign(positiveScoreImpact, (a, v) => a.PositiveScoreImpact = v);
	}
}
