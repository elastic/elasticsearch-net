using System;

namespace Elasticsearch.Net
{
	public partial class AnalyzeRequestParameters
	{
		///<summary>The name of the analyzer to use</summary>
		[Obsolete("Deprecated. Specify the analyzer to use in the body of the request.")]
		public AnalyzeRequestParameters Analyzer(string analyzer) => this;

		///<summary>Deprecated : A comma-separated list of character filters to use for the analysis</summary>
		[Obsolete("Deprecated. Specify the char filters to use in the body of the request.")]
		public AnalyzeRequestParameters CharFilters(params string[] char_filters) => this;

		///<summary>A comma-separated list of character filters to use for the analysis</summary>
		[Obsolete("Deprecated. Specify the char filters to use in the body of the request.")]
		public AnalyzeRequestParameters CharFilter(params string[] char_filter) => this;

		///<summary>Use the analyzer configured for this field (instead of passing the analyzer name)</summary>
		[Obsolete("Deprecated. Specify the field to use in the body of the request.")]
		public AnalyzeRequestParameters Field(string field) => this;

		///<summary>Deprecated : A comma-separated list of filters to use for the analysis</summary>
		[Obsolete("Deprecated. Specify the filters to use in the body of the request.")]
		public AnalyzeRequestParameters Filters(params string[] filters) => this;

		///<summary>A comma-separated list of filters to use for the analysis</summary>
		[Obsolete("Deprecated. Specify the filters to use in the body of the request.")]
		public AnalyzeRequestParameters Filter(params string[] filter) => this;

		///<summary>The text on which the analysis should be performed (when request body is not used)</summary>
		[Obsolete("Deprecated. Specify the text to use in the body of the request.")]
		public AnalyzeRequestParameters Text(params string[] text) => this;

		///<summary>The name of the tokenizer to use for the analysis</summary>
		[Obsolete("Deprecated. Specify the tokenizer to use in the body of the request.")]
		public AnalyzeRequestParameters Tokenizer(string tokenizer) => this;
	}
}
