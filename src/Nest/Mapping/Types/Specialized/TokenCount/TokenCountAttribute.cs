using System;

namespace Nest
{
	public class TokenCountAttribute : ElasticsearchDocValuesPropertyAttributeBase, ITokenCountProperty
	{
		private ITokenCountProperty Self => this;

		public TokenCountAttribute() : base(FieldType.TokenCount) { }

		string ITokenCountProperty.Analyzer { get; set; }
		bool? ITokenCountProperty.Index { get; set; }
		double? ITokenCountProperty.Boost { get; set; }
		double? ITokenCountProperty.NullValue { get; set; }

		public string Analyzer { get => Self.Analyzer; set => Self.Analyzer = value; }
		public bool Index { get => Self.Index.GetValueOrDefault(); set => Self.Index = value; }
		public double Boost { get => Self.Boost.GetValueOrDefault(); set => Self.Boost = value; }
		public double NullValue { get => Self.NullValue.GetValueOrDefault(); set => Self.NullValue = value; }

	}
}
