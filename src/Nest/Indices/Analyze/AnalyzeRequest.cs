// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.Serialization;

namespace Nest
{
	[MapsApi("indices.analyze.json")]
	[ReadAs(typeof(AnalyzeRequest))]
	public partial interface IAnalyzeRequest
	{
		///<summary>The name of the analyzer to use</summary>
		[DataMember(Name ="analyzer")]
		string Analyzer { get; set; }

		///<summary>Filter only certain token attributes to be returned</summary>
		[DataMember(Name ="attributes")]
		IEnumerable<string> Attributes { get; set; }

		///<summary>A collection of character filters to use for the analysis</summary>
		[DataMember(Name ="char_filter")]
		AnalyzeCharFilters CharFilter { get; set; }

		///<summary>Return more details, and output the analyzer chain per step in the process</summary>
		[DataMember(Name ="explain")]
		bool? Explain { get; set; }

		///<summary>Use the analyzer configured for this field (instead of passing the analyzer name)</summary>
		[DataMember(Name ="field")]
		Field Field { get; set; }

		///<summary>A collection of filters to use for the analysis</summary>
		[DataMember(Name ="filter")]
		AnalyzeTokenFilters Filter { get; set; }

		///<summary>The name of the normalizer to use</summary>
		[DataMember(Name ="normalizer")]
		string Normalizer { get; set; }

		///<summary>The text on which the analysis should be performed (when request body is not used)</summary>
		[DataMember(Name ="text")]
		IEnumerable<string> Text { get; set; }

		///<summary>The name of the tokenizer to use for the analysis</summary>
		[DataMember(Name ="tokenizer")]
		Union<string, ITokenizer> Tokenizer { get; set; }
	}

	public partial class AnalyzeRequest
	{
		public AnalyzeRequest(IndexName indices, string textToAnalyze)
			: this(indices) => Text = new[] { textToAnalyze };

		/// <inheritdoc />
		public string Analyzer { get; set; }

		/// <inheritdoc />
		public IEnumerable<string> Attributes { get; set; }

		/// <inheritdoc />
		public AnalyzeCharFilters CharFilter { get; set; }

		/// <inheritdoc />
		public bool? Explain { get; set; }

		/// <inheritdoc />
		public Field Field { get; set; }

		/// <inheritdoc />
		public AnalyzeTokenFilters Filter { get; set; }

		/// <inheritdoc />
		public string Normalizer { get; set; }

		/// <inheritdoc />
		public IEnumerable<string> Text { get; set; }

		/// <inheritdoc />
		public Union<string, ITokenizer> Tokenizer { get; set; }
	}

	public partial class AnalyzeDescriptor
	{
		string IAnalyzeRequest.Analyzer { get; set; }
		IEnumerable<string> IAnalyzeRequest.Attributes { get; set; }
		AnalyzeCharFilters IAnalyzeRequest.CharFilter { get; set; }
		bool? IAnalyzeRequest.Explain { get; set; }
		Field IAnalyzeRequest.Field { get; set; }
		AnalyzeTokenFilters IAnalyzeRequest.Filter { get; set; }
		string IAnalyzeRequest.Normalizer { get; set; }
		IEnumerable<string> IAnalyzeRequest.Text { get; set; }
		Union<string, ITokenizer> IAnalyzeRequest.Tokenizer { get; set; }

		///<summary>The name of the tokenizer to use for the analysis</summary>
		public AnalyzeDescriptor Tokenizer(string tokenizer) => Assign(tokenizer, (a, v) => a.Tokenizer = v);

		///<summary>An inline definition of a tokenizer</summary>
		public AnalyzeDescriptor Tokenizer(Func<AnalyzeTokenizersSelector, ITokenizer> tokenizer) =>
			Assign(tokenizer, (a, v) =>
			{
				var u = v?.Invoke(new AnalyzeTokenizersSelector());
				if (u != null) a.Tokenizer = new Union<string, ITokenizer>(u);
			});

		///<summary>The name of the analyzer to use</summary>
		public AnalyzeDescriptor Analyzer(string analyser) => Assign(analyser, (a, v) => a.Analyzer = v);

		///<summary>A collection of character filters to use for the analysis</summary>
		public AnalyzeDescriptor CharFilter(params string[] charFilter) => Assign(charFilter, (a, v) => a.CharFilter = v);

		///<summary>A collection of character filters to use for the analysis</summary>
		public AnalyzeDescriptor CharFilter(IEnumerable<string> charFilter) => Assign(charFilter.ToArray(), (a, v) => a.CharFilter = v);

		///<summary>A collection of character filters to use for the analysis</summary>
		public AnalyzeDescriptor CharFilter(Func<AnalyzeCharFiltersDescriptor, IPromise<AnalyzeCharFilters>> charFilters) =>
			Assign(charFilters, (a, v) => a.CharFilter = v?.Invoke(new AnalyzeCharFiltersDescriptor())?.Value);

		///<summary>A collection of filters to use for the analysis</summary>
		public AnalyzeDescriptor Filter(params string[] filter) => Assign(filter, (a, v) => a.Filter = v);

		///<summary>A collection of filters to use for the analysis</summary>
		public AnalyzeDescriptor Filter(IEnumerable<string> filter) => Assign(filter.ToArray(), (a, v) => a.Filter = v);

		///<summary>A collection of filters to use for the analysis</summary>
		public AnalyzeDescriptor Filter(Func<AnalyzeTokenFiltersDescriptor, IPromise<AnalyzeTokenFilters>> tokenFilters) =>
			Assign(tokenFilters, (a, v) => a.Filter = v?.Invoke(new AnalyzeTokenFiltersDescriptor())?.Value);

		///<summary>The name of the normalizer to use</summary>
		public AnalyzeDescriptor Normalizer(string normalizer) => Assign(normalizer, (a, v) => a.Normalizer = v);

		///<summary>Use the analyzer configured for this field (instead of passing the analyzer name)</summary>
		public AnalyzeDescriptor Field(Field field) => Assign(field, (a, v) => a.Field = v);

		///<summary>Use the analyzer configured for this field (instead of passing the analyzer name)</summary>
		public AnalyzeDescriptor Field<T, TValue>(Expression<Func<T, TValue>> field) => Assign(field, (a, v) => a.Field = v);

		///<summary>Use the analyzer configured for this field (instead of passing the analyzer name)</summary>
		public AnalyzeDescriptor Field<T>(Expression<Func<T, object>> field) => Assign(field, (a, v) => a.Field = v);

		///<summary>The text on which the analysis should be performed</summary>
		public AnalyzeDescriptor Text(params string[] text) => Assign(text, (a, v) => a.Text = v);

		///<summary>The text on which the analysis should be performed</summary>
		public AnalyzeDescriptor Text(IEnumerable<string> text) => Assign(text, (a, v) => a.Text = v);

		///<summary>Return more details, and output the analyzer chain per step in the process</summary>
		public AnalyzeDescriptor Explain(bool? explain = true) => Assign(explain, (a, v) => a.Explain = v);

		///<summary>Filter only certain token attributes to be returned</summary>
		public AnalyzeDescriptor Attributes(params string[] attributes) => Assign(attributes, (a, v) => a.Attributes = v);

		///<summary>Filter only certain token attributes to be returned</summary>
		public AnalyzeDescriptor Attributes(IEnumerable<string> attributes) => Assign(attributes.ToArray(), (a, v) => a.Attributes = v);
	}
}
