namespace Nest_5_2_0
{
	public class Murmur3HashAttribute : ElasticsearchDocValuesPropertyAttributeBase, IMurmur3HashProperty
	{
		public Murmur3HashAttribute() : base(FieldType.Murmur3Hash) { }
	}
}
