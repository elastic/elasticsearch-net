namespace Nest
{
	public class IpAttribute : ElasticsearchDocValuesPropertyAttributeBase, IIpProperty
	{
		public IpAttribute() : base(FieldType.Ip) { }

		public double Boost
		{
			get => Self.Boost.GetValueOrDefault();
			set => Self.Boost = value;
		}

		/// <remarks>Removed in 6.x</remarks>
		public bool IncludeInAll
		{
			get => Self.IncludeInAll.GetValueOrDefault();
			set => Self.IncludeInAll = value;
		}

		public bool Index
		{
			get => Self.Index.GetValueOrDefault();
			set => Self.Index = value;
		}

		public string NullValue
		{
			get => Self.NullValue;
			set => Self.NullValue = value;
		}

		double? IIpProperty.Boost { get; set; }

		/// <remarks>Removed in 6.x</remarks>
		bool? IIpProperty.IncludeInAll { get; set; }

		bool? IIpProperty.Index { get; set; }
		string IIpProperty.NullValue { get; set; }
		private IIpProperty Self => this;
	}
}
