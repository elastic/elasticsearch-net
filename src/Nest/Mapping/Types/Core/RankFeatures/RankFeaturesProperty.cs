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
	}

	/// <inheritdoc cref="IRankFeaturesProperty" />
	public class RankFeaturesProperty : PropertyBase, IRankFeaturesProperty
	{
		public RankFeaturesProperty() : base(FieldType.RankFeatures) { }
	}

	/// <inheritdoc cref="IRankFeaturesProperty" />
	[DebuggerDisplay("{DebugDisplay}")]
	public class RankFeaturesPropertyDescriptor<T>
		: PropertyDescriptorBase<RankFeaturesPropertyDescriptor<T>, IRankFeaturesProperty, T>, IRankFeaturesProperty
		where T : class
	{
		public RankFeaturesPropertyDescriptor() : base(FieldType.RankFeatures) { }
	}
}
