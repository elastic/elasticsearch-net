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

		///<summary>The name of the normalizer to use</summary>
		[JsonProperty("normalizer")]
		string Normalizer { get; set; }

		///<summary>A collection of character filters to use for the analysis</summary>
		[JsonProperty("char_filter")]
		string[] CharFilter { get; set; }

		///<summary>A collection of filters to use for the analysis</summary>
		[JsonProperty("filter")]
		string[] Filter { get; set; }

		///<summary>Use the analyzer configured for this field (instead of passing the analyzer name)</summary>
		[JsonProperty("field")]
		Field Field { get; set; }

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
			: this(indices)
		{
			this.Text = new[] { textToAnalyze };
		}

		/// <inheritdoc />
		public string Analyzer { get; set; }

		/// <inheritdoc />
		public string Normalizer { get; set; }

	    /// <inheritdoc />
		public string[] CharFilter { get; set; }

	    /// <inheritdoc />
	    public string[] Filter { get; set; }

		/// <inheritdoc />
		public Field Field { get; set; }

		/// <inheritdoc />
		public string[] Text { get; set; }

		/// <inheritdoc />
		public string Tokenizer { get; set; }
	}

	[DescriptorFor("IndicesAnalyze")]
	public partial class AnalyzeDescriptor
	{
		string IAnalyzeRequest.Analyzer { get; set; }
		string IAnalyzeRequest.Normalizer { get; set; }
		string[] IAnalyzeRequest.CharFilter { get; set; }
		string[] IAnalyzeRequest.Filter { get; set; }
		Field IAnalyzeRequest.Field { get; set; }
		string[] IAnalyzeRequest.Text { get; set; }
		string IAnalyzeRequest.Tokenizer { get; set; }

		///<summary>The name of the analyzer to use</summary>
		public AnalyzeDescriptor Analyzer(string analyser) => Assign(a => a.Analyzer = analyser);

		///<summary>The name of the normalizer to use</summary>
		public AnalyzeDescriptor Normalizer(string normalizer) => Assign(a => a.Normalizer = normalizer);

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
