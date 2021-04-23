// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

namespace Nest
{
	/// <inheritdoc cref="IRankFeaturesProperty"/>
	public class RankFeaturesAttribute : ElasticsearchPropertyAttributeBase, IRankFeaturesProperty
	{
		public RankFeaturesAttribute() : base(FieldType.RankFeatures) { }

		private IRankFeaturesProperty Self => this;

		/// <inheritdoc cref="IRankFeatureProperty.PositiveScoreImpact"/>
		public bool PositiveScoreImpact
		{
			get => Self.PositiveScoreImpact.GetValueOrDefault(true);
			set => Self.PositiveScoreImpact = value;
		}

		bool? IRankFeaturesProperty.PositiveScoreImpact { get; set; }
	}
}
