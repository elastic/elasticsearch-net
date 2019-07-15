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
