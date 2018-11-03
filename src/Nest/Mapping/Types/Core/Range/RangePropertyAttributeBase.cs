namespace Nest
{
	public abstract class RangePropertyAttributeBase : ElasticsearchDocValuesPropertyAttributeBase, IRangeProperty
	{
		protected RangePropertyAttributeBase(RangeType type) : base(type.ToFieldType()) { }

		public double Boost
		{
			get => Self.Boost.GetValueOrDefault();
			set => Self.Boost = value;
		}

		public bool Coerce
		{
			get => Self.Coerce.GetValueOrDefault();
			set => Self.Coerce = value;
		}

		public bool Index
		{
			get => Self.Index.GetValueOrDefault();
			set => Self.Index = value;
		}

		double? IRangeProperty.Boost { get; set; }

		bool? IRangeProperty.Coerce { get; set; }
		bool? IRangeProperty.Index { get; set; }
		private IRangeProperty Self => this;
	}
}
