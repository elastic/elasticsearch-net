// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

namespace Nest
{
	/// <inheritdoc cref="IRankFeatureProperty"/>
	public class RankFeatureAttribute : ElasticsearchPropertyAttributeBase, IRankFeatureProperty
	{
		public RankFeatureAttribute() : base(FieldType.RankFeature) { }

		private IRankFeatureProperty Self => this;

		/// <inheritdoc cref="IRankFeatureProperty.PositiveScoreImpact"/>
		public bool PositiveScoreImpact
		{
			get => Self.PositiveScoreImpact.GetValueOrDefault(true);
			set => Self.PositiveScoreImpact = value;
		}

		bool? IRankFeatureProperty.PositiveScoreImpact { get; set; }
	}
}
