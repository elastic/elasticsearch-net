namespace Nest_5_2_0
{
	public class PercolatorAttribute : ElasticsearchPropertyAttributeBase, IPercolatorProperty
	{
		public PercolatorAttribute() : base(FieldType.Percolator) { }
	}
}
