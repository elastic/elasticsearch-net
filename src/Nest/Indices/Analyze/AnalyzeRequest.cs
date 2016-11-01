using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Newtonsoft.Json;

namespace Nest
{
	[JsonConverter(typeof(ReadAsTypeJsonConverter<AnalyzeRequest>))]
	public partial interface IAnalyzeRequest
	{
		///<summary>The name of the analyzer to use</summary>
		[JsonProperty("analyzer")]
		string Analyzer { get; set; }

		///<summary>A collection of character filters to use for the analysis</summary>
		[Obsolete("Deprecated in 2.4.0. Removed in 5.0.0. Use CharFilter instead")]
		[JsonIgnore]
		string[] CharFilters { get; set; }

		///<summary>A collection of character filters to use for the analysis</summary>
		[JsonProperty("char_filters")]
		string[] CharFilter { get; set; }

		///<summary>A collection of filters to use for the analysis</summary>
		[JsonProperty("filters")]
		string[] Filter { get; set; }

		///<summary>Use the analyzer configured for this field (instead of passing the analyzer name)</summary>
		[JsonProperty("field")]
		Field Field { get; set; }

		///<summary>A collection of filters to use for the analysis</summary>
		[Obsolete("Deprecated in 2.4.0. Removed in 5.0.0. Use Filter instead")]
		[JsonIgnore]
		string[] Filters { get; set; }

		///<summary>The text on which the analysis should be performed (when request body is not used)</summary>
		[JsonProperty("text")]
		string[] Text { get; set; }

		///<summary>The name of the tokenizer to use for the analysis</summary>
		[JsonProperty("tokenizer")]
		string Tokenizer { get; set; }
	}

	public partial class AnalyzeRequest
	{
		public AnalyzeRequest(IndexName indices, string textToAnalyze)
			:this(indices)
		{
			this.Text = new[] { textToAnalyze };
		}

		///<summary>The name of the analyzer to use</summary>
		public string Analyzer { get; set; }

		///<summary>A collection of character filters to use for the analysis</summary>
		[Obsolete("Deprecated in 2.4.0. Removed in 5.0.0. Use CharFilter instead")]
		public string[] CharFilters { get { return CharFilter; } set { CharFilter = value; } }

		///<summary>A collection of character filters to use for the analysis</summary>
		public string[] CharFilter { get; set; }

		///<summary>A collection of filters to use for the analysis</summary>
		public string[] Filter { get; set; }

		///<summary>Use the analyzer configured for this field (instead of passing the analyzer name)</summary>
		public Field Field { get; set; }

		///<summary>A collection of filters to use for the analysis</summary>
		[Obsolete("Deprecated in 2.4.0. Removed in 5.0.0. Use Filter instead")]
		public string[] Filters { get { return Filter; } set { Filter = value; } }

		///<summary>The text on which the analysis should be performed</summary>
		public string[] Text { get; set; }

		///<summary>The name of the tokenizer to use for the analysis</summary>
		public string Tokenizer { get; set; }
	}

	[DescriptorFor("IndicesAnalyze")]
	public partial class AnalyzeDescriptor
	{
		string IAnalyzeRequest.Analyzer { get; set; }
		string[] IAnalyzeRequest.CharFilters { get { return Self.CharFilter; } set { Self.CharFilter = value; } }
		string[] IAnalyzeRequest.CharFilter { get; set; }
		string[] IAnalyzeRequest.Filter { get; set; }
		Field IAnalyzeRequest.Field { get; set; }
		string[] IAnalyzeRequest.Filters { get { return Self.Filter; } set { Self.Filter = value; } }
		string[] IAnalyzeRequest.Text { get; set; }
		string IAnalyzeRequest.Tokenizer { get; set; }

		///<summary>The name of the analyzer to use</summary>
		public AnalyzeDescriptor Analyzer(string analyser) => Assign(a => a.Analyzer = analyser);

		///<summary>A collection of character filters to use for the analysis</summary>
		public AnalyzeDescriptor CharFilter(params string[] charFilter) => Assign(a => a.CharFilter = charFilter);

		///<summary>A collection of character filters to use for the analysis</summary>
		public AnalyzeDescriptor CharFilter(IEnumerable<string> charFilter) => Assign(a => a.CharFilter = charFilter.ToArray());

		///<summary>A collection of filters to use for the analysis</summary>
		public AnalyzeDescriptor Filter(params string[] filter) => Assign(a => a.Filter = filter);

		///<summary>A collection of filters to use for the analysis</summary>
		public AnalyzeDescriptor Filter(IEnumerable<string> filter) => Assign(a => a.Filter = filter.ToArray());

		///<summary>Use the analyzer configured for this field (instead of passing the analyzer name)</summary>
		public AnalyzeDescriptor Field(Field field) => Assign(a => a.Field = field);

		///<summary>Use the analyzer configured for this field (instead of passing the analyzer name)</summary>
		public AnalyzeDescriptor Field<T>(Expression<Func<T, object>> field) => Assign(a => a.Field = field);

		///<summary>The text on which the analysis should be performed</summary>
		public AnalyzeDescriptor Text(params string[] text) => Assign(a => a.Text = text);

		///<summary>The text on which the analysis should be performed</summary>
		public AnalyzeDescriptor Text(IEnumerable<string> text) => Assign(a => a.Text = text.ToArray());

		///<summary>The name of the tokenizer to use for the analysis</summary>
		public AnalyzeDescriptor Tokenizer(string tokenizer) => Assign(a => a.Tokenizer = tokenizer);
	}
}
