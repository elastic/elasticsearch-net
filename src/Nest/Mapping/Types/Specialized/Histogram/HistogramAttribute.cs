namespace Nest
{
	public class HistogramAttribute : ElasticsearchPropertyAttributeBase, IHistogramProperty
	{
		public HistogramAttribute() : base(FieldType.Histogram) { }
		
		/// <inheritdoc cref="IHistogramProperty.IgnoreMalformed"/>
		public bool IgnoreMalformed
		{
			get => Self.IgnoreMalformed.GetValueOrDefault();
			set => Self.IgnoreMalformed = value;
		}
		
		bool? IHistogramProperty.IgnoreMalformed { get; set; }

		private IHistogramProperty Self => this;
	}
}
