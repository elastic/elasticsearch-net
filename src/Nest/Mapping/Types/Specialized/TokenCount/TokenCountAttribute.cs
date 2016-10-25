using System;

namespace Nest
{
	public class TokenCountAttribute : ElasticsearchDocValuesPropertyAttributeBase, ITokenCountProperty
	{
		ITokenCountProperty Self => this;

		string ITokenCountProperty.Analyzer { get; set; }
		bool? ITokenCountProperty.Index { get; set; }
		double? ITokenCountProperty.Boost { get; set; }
		double? ITokenCountProperty.NullValue { get; set; }
		bool? ITokenCountProperty.IncludeInAll { get; set; }

		public string Analyzer { get { return Self.Analyzer; } set { Self.Analyzer = value; } }
		public bool Index { get { return Self.Index.GetValueOrDefault(); } set { Self.Index = value; } }
		public double Boost { get { return Self.Boost.GetValueOrDefault(); } set { Self.Boost = value; } }
		public double NullValue { get { return Self.NullValue.GetValueOrDefault(); } set { Self.NullValue = value; } }
		public bool IncludeInAll { get { return Self.IncludeInAll.GetValueOrDefault(); } set { Self.IncludeInAll = value; } }

		public TokenCountAttribute() : base("token_count") { }
	}
}
