namespace Nest
{
	public class PercolatorAttribute : ElasticsearchPropertyAttributeBase, IPercolatorProperty
	{
		public PercolatorAttribute() : base("percolator") { }
	}
}
