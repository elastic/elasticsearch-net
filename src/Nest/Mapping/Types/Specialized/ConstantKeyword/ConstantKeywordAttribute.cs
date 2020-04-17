namespace Nest
{
	/// <inheritdoc cref="IConstantKeywordProperty"/>
	public class ConstantKeywordAttribute : ElasticsearchPropertyAttributeBase, IConstantKeywordProperty
	{
		public ConstantKeywordAttribute() : base(FieldType.ConstantKeyword) { }

		/// <inheritdoc />
		public object Value { get; set; }
	}
}
