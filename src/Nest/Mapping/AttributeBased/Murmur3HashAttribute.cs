namespace Nest
{
	public class Murmur3HashAttribute : ElasticsearchPropertyAttribute, IMurmur3HashProperty
	{
		public Murmur3HashAttribute() : base("murmur3") { }
	}
}
