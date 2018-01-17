using System;

namespace Nest
{
	public class IpAttribute : ElasticsearchDocValuesPropertyAttributeBase, IIpProperty
	{
		private IIpProperty Self => this;

		public IpAttribute() : base(FieldType.Ip) { }

		double? IIpProperty.Boost { get; set; }
		[Obsolete("Scheduled to be removed in 6.0")]
		bool? IIpProperty.IncludeInAll { get; set; }
		bool? IIpProperty.Index { get; set; }
		string IIpProperty.NullValue { get; set; }

		public double Boost { get { return Self.Boost.GetValueOrDefault(); } set { Self.Boost = value; } }

		[Obsolete("Scheduled to be removed in 6.0")]
		public bool IncludeInAll { get { return Self.IncludeInAll.GetValueOrDefault(); } set { Self.IncludeInAll = value; } }
		public bool Index { get { return Self.Index.GetValueOrDefault(); } set { Self.Index = value; } }
		public string NullValue { get { return Self.NullValue; } set { Self.NullValue = value; } }

	}
}
