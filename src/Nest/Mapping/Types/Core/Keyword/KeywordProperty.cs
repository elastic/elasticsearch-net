using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nest
{
	[JsonObject(MemberSerialization.OptIn)]
	public interface IKeywordProperty : IDocValuesProperty
	{
		[JsonProperty("boost")]
		double? Boost { get; set; }

		[JsonProperty("eager_global_ordinals")]
		bool? EagerGlobalOrdinals { get; set; }

		[JsonProperty("ignore_above")]
		int? IgnoreAbove { get; set; }

		[JsonProperty("include_in_all")]
		bool? IncludeInAll { get; set; }

		[JsonProperty("index")]
		bool? Index { get; set; }

		[JsonProperty("index_options")]
		IndexOptions? IndexOptions { get; set; }

		[JsonProperty("norms")]
		bool? Norms { get; set; }

		[JsonProperty("null_value")]
		string NullValue { get; set; }
	}

	public class KeywordProperty : DocValuesPropertyBase, IKeywordProperty
	{
		public KeywordProperty() : base("keyword") { }

		public double? Boost { get; set; }
		public bool? EagerGlobalOrdinals { get; set; }
		public int? IgnoreAbove { get; set; }
		public bool? IncludeInAll { get; set; }
		public bool? Index { get; set; }
		public IndexOptions? IndexOptions { get; set; }
		public bool? Norms { get; set; }
		public string NullValue { get; set; }
	}

	public class KeywordPropertyDescriptor<T>
		: DocValuesPropertyDescriptorBase<KeywordPropertyDescriptor<T>, IKeywordProperty, T>, IKeywordProperty
		where T : class
	{
		double? IKeywordProperty.Boost { get; set; }
		bool? IKeywordProperty.EagerGlobalOrdinals{ get; set; }
		int? IKeywordProperty.IgnoreAbove{ get; set; }
		bool? IKeywordProperty.IncludeInAll{ get; set; }
		bool? IKeywordProperty.Index{ get; set; }
		IndexOptions? IKeywordProperty.IndexOptions{ get; set; }
		bool? IKeywordProperty.Norms{ get; set; }
		string IKeywordProperty.NullValue{ get; set; }

		public KeywordPropertyDescriptor() : base("keyword") { }

		public KeywordPropertyDescriptor<T> Boost(double boost) => Assign(a => a.Boost = boost);
		public KeywordPropertyDescriptor<T> EagerGlobalOrdinals(bool eagerGlobalOrdinals = true) => Assign(a => a.EagerGlobalOrdinals = eagerGlobalOrdinals);
		public KeywordPropertyDescriptor<T> IgnoreAbove(int ignoreAbove) => Assign(a => a.IgnoreAbove = ignoreAbove);
		public KeywordPropertyDescriptor<T> IncludeInAll(bool includeInAll = true) => Assign(a => a.IncludeInAll = includeInAll);
		public KeywordPropertyDescriptor<T> Index(bool index = true) => Assign(a => a.Index = index);
		public KeywordPropertyDescriptor<T> IndexOptions(IndexOptions indexOptions) => Assign(a => a.IndexOptions = indexOptions);
		public KeywordPropertyDescriptor<T> Norms(bool enabled = true) => Assign(a => a.Norms = enabled);
		public KeywordPropertyDescriptor<T> NullValue(string nullValue) => Assign(a => a.NullValue = nullValue);
	}
}
