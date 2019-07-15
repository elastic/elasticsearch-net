namespace Nest
{
	/// <inheritdoc cref="IRankFeaturesProperty"/>
	public class RankFeaturesAttribute : ElasticsearchPropertyAttributeBase, IRankFeaturesProperty
	{
		public RankFeaturesAttribute() : base(FieldType.RankFeatures) { }
	}
}
