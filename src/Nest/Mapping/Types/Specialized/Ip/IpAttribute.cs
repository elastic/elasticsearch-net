namespace Nest
{
	public class IpAttribute : ElasticsearchPropertyAttributeBase, IIpProperty
	{
		IIpProperty Self => this;

		double? IIpProperty.Boost { get; set; }
		bool? IIpProperty.IncludeInAll { get; set; }
		NonStringIndexOption? IIpProperty.Index { get; set; }
		string IIpProperty.NullValue { get; set; }
		int? IIpProperty.PrecisionStep { get; set; }

		public double Boost { get { return Self.Boost.GetValueOrDefault(); } set { Self.Boost = value; } }
		public bool IncludeInAll { get { return Self.IncludeInAll.GetValueOrDefault(); } set { Self.IncludeInAll = value; } }
		public NonStringIndexOption Index { get { return Self.Index.GetValueOrDefault(); } set { Self.Index = value; } }
		public string NullValue { get { return Self.NullValue; } set { Self.NullValue = value; } }
		public int PrecisionStep { get { return Self.PrecisionStep.GetValueOrDefault(); } set { Self.PrecisionStep = value; } }

		public IpAttribute() : base("ip") { }
	}
}
