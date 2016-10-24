using System;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization.OptIn)]
	public interface ITokenCountProperty : IDocValuesProperty
	{
		[JsonProperty("analyzer")]
		string Analyzer { get; set; }

		[JsonProperty("index")]
		bool? Index { get; set; }

		[JsonProperty("boost")]
		double? Boost { get; set; }

		[JsonProperty("null_value")]
		double? NullValue { get; set; }

		[JsonProperty("include_in_all")]
		bool? IncludeInAll { get; set; }
	}

	public class TokenCountProperty : DocValuesPropertyBase, ITokenCountProperty
	{
		public TokenCountProperty() : base("token_count") { }

		public string Analyzer { get; set; }

		public bool? Index { get; set; }

		public double? Boost { get; set; }

		public double? NullValue { get; set; }

		public bool? IncludeInAll { get; set; }
	}

	public class TokenCountPropertyDescriptor<T>
		: DocValuesPropertyDescriptorBase<TokenCountPropertyDescriptor<T>, ITokenCountProperty, T>, ITokenCountProperty
		where T : class
	{
		public TokenCountPropertyDescriptor() : base("token_count") { }

		string ITokenCountProperty.Analyzer { get; set; }
		double? ITokenCountProperty.Boost { get; set; }
		bool? ITokenCountProperty.IncludeInAll { get; set; }
		bool? ITokenCountProperty.Index { get; set; }
		double? ITokenCountProperty.NullValue { get; set; }

		public TokenCountPropertyDescriptor<T> Analyzer(string analyzer) => Assign(a => a.Analyzer = analyzer);
		public TokenCountPropertyDescriptor<T> Boost(double boost) => Assign(a => a.Boost = boost);
		public TokenCountPropertyDescriptor<T> IncludeInAll(bool includeInAll = true) => Assign(a => a.IncludeInAll = includeInAll);
		public TokenCountPropertyDescriptor<T> Index(bool index = true) => Assign(a => a.Index = index);
		public TokenCountPropertyDescriptor<T> NullValue(double nullValue) => Assign(a => a.NullValue = nullValue);
	}
}
