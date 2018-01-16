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
		AnalyzeCharFilters CharFilter { get; set; }

		///<summary>A collection of filters to use for the analysis</summary>
		[JsonProperty("filter")]
		AnalyzeTokenFilters Filter { get; set; }

		///<summary>Use the analyzer configured for this field (instead of passing the analyzer name)</summary>
		[JsonProperty("field")]
		Field Field { get; set; }

		///<summary>The text on which the analysis should be performed (when request body is not used)</summary>
		[JsonProperty("text")]
		IEnumerable<string> Text { get; set; }

		///<summary>The name of the tokenizer to use for the analysis</summary>
		[JsonProperty("tokenizer")]
		Union<string, ITokenizer> Tokenizer { get; set; }

		///<summary>Return more details, and output the analyzer chain per step in the process</summary>
		[JsonProperty("explain")]
		bool? Explain { get; set; }

		///<summary>Filter only certain token attributes to be returned</summary>
		[JsonProperty("attributes")]
		IEnumerable<string> Attributes { get; set; }
	}

	public partial class AnalyzeRequest
	{
		public AnalyzeRequest(IndexName indices, string textToAnalyze)
			: this(indices)
		{
			this.Text = new[] { textToAnalyze };
		}

		/// <inheritdoc />
		public Union<string, ITokenizer> Tokenizer { get; set; }

		/// <inheritdoc />
		public string Analyzer { get; set; }

		/// <inheritdoc />
		public bool? Explain { get; set; }

		/// <inheritdoc />
		public IEnumerable<string> Attributes { get; set; }

	    /// <inheritdoc />
		public AnalyzeCharFilters CharFilter { get; set; }

	    /// <inheritdoc />
		public AnalyzeTokenFilters Filter { get; set; }

		/// <inheritdoc />
		public string Normalizer { get; set; }

		/// <inheritdoc />
		public Field Field { get; set; }

		/// <inheritdoc />
		public IEnumerable<string> Text { get; set; }
	}

	[DescriptorFor("IndicesAnalyze")]
	public partial class AnalyzeDescriptor
	{
		string IAnalyzeRequest.Analyzer { get; set; }
		AnalyzeCharFilters IAnalyzeRequest.CharFilter { get; set; }
		AnalyzeTokenFilters IAnalyzeRequest.Filter { get; set; }
		string IAnalyzeRequest.Normalizer { get; set; }
		Field IAnalyzeRequest.Field { get; set; }
		IEnumerable<string> IAnalyzeRequest.Text { get; set; }
		Union<string, ITokenizer> IAnalyzeRequest.Tokenizer { get; set; }
		bool? IAnalyzeRequest.Explain { get; set; }
		IEnumerable<string> IAnalyzeRequest.Attributes { get; set; }

		///<summary>The name of the tokenizer to use for the analysis</summary>
		public AnalyzeDescriptor Tokenizer(string tokenizer) => Assign(a => a.Tokenizer = tokenizer);

		///<summary>An inline definition of a tokenizer</summary>
		public AnalyzeDescriptor Tokenizer(Func<AnalyzeTokenizersSelector, ITokenizer> tokenizer) =>
			Assign(a =>
			{
				var v = tokenizer?.Invoke(new AnalyzeTokenizersSelector());
				if (v != null) a.Tokenizer = new Union<string, ITokenizer>(v);
			});

		///<summary>The name of the analyzer to use</summary>
		public AnalyzeDescriptor Analyzer(string analyser) => Assign(a => a.Analyzer = analyser);

		///<summary>A collection of character filters to use for the analysis</summary>
		public AnalyzeDescriptor CharFilter(params string[] charFilter) => Assign(a => a.CharFilter = charFilter);

		///<summary>A collection of character filters to use for the analysis</summary>
		public AnalyzeDescriptor CharFilter(IEnumerable<string> charFilter) => Assign(a => a.CharFilter = charFilter.ToArray());

		///<summary>A collection of character filters to use for the analysis</summary>
		public AnalyzeDescriptor CharFilter(Func<AnalyzeCharFiltersDescriptor, IPromise<AnalyzeCharFilters>> charFilters) =>
			Assign(a => a.CharFilter = charFilters?.Invoke(new AnalyzeCharFiltersDescriptor())?.Value);

		///<summary>A collection of filters to use for the analysis</summary>
		public AnalyzeDescriptor Filter(params string[] filter) => Assign(a => a.Filter = filter);

		///<summary>A collection of filters to use for the analysis</summary>
		public AnalyzeDescriptor Filter(IEnumerable<string> filter) => Assign(a => a.Filter = filter.ToArray());

		///<summary>A collection of filters to use for the analysis</summary>
		public AnalyzeDescriptor Filter(Func<AnalyzeTokenFiltersDescriptor, IPromise<AnalyzeTokenFilters>> tokenFilters) =>
			Assign(a => a.Filter = tokenFilters?.Invoke(new AnalyzeTokenFiltersDescriptor())?.Value);

		///<summary>The name of the normalizer to use</summary>
		public AnalyzeDescriptor Normalizer(string normalizer) => Assign(a => a.Normalizer = normalizer);

		///<summary>Use the analyzer configured for this field (instead of passing the analyzer name)</summary>
		public AnalyzeDescriptor Field(Field field) => Assign(a => a.Field = field);

		///<summary>Use the analyzer configured for this field (instead of passing the analyzer name)</summary>
		public AnalyzeDescriptor Field<T>(Expression<Func<T, object>> field) => Assign(a => a.Field = field);

		///<summary>The text on which the analysis should be performed</summary>
		public AnalyzeDescriptor Text(params string[] text) => Assign(a => a.Text = text);

		///<summary>The text on which the analysis should be performed</summary>
		public AnalyzeDescriptor Text(IEnumerable<string> text) => Assign(a => a.Text = text);

		///<summary>Return more details, and output the analyzer chain per step in the process</summary>
		public AnalyzeDescriptor Explain(bool? explain = true) => Assign(a => a.Explain = explain);

		///<summary>Filter only certain token attributes to be returned</summary>
		public AnalyzeDescriptor Attributes(params string[] attributes) => Assign(a => a.Attributes = attributes);

		///<summary>Filter only certain token attributes to be returned</summary>
		public AnalyzeDescriptor Attributes(IEnumerable<string> attributes) => Assign(a => a.Attributes = attributes.ToArray());
	}
}
