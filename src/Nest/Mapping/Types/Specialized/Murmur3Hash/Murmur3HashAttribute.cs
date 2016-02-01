namespace Nest
{
	public class Murmur3HashAttribute : ElasticsearchPropertyAttributeBase, IMurmur3HashProperty
	{
		public Murmur3HashAttribute() : base("murmur3") { }
	}
}
