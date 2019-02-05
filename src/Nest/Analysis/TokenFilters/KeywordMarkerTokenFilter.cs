using System.Collections.Generic;
using System.Runtime.Serialization;
using Utf8Json;

namespace Nest
{
	/// <summary>
	/// Protects words from being modified by stemmers. Must be placed before any stemming filters.
	/// </summary>
	public interface IKeywordMarkerTokenFilter : ITokenFilter
	{
		/// <summary>
		/// Set to true to lower case all words first. Defaults to false.
		/// </summary>
		[DataMember(Name ="ignore_case")]
		[JsonFormatter(typeof(NullableStringBooleanFormatter))]
		bool? IgnoreCase { get; set; }

		/// <summary>
		/// A list of words to use.
		/// </summary>
		[DataMember(Name ="keywords")]
		IEnumerable<string> Keywords { get; set; }

		/// <summary>
		/// A path (either relative to config location, or absolute) to a list of words.
		/// </summary>
		[DataMember(Name ="keywords_path")]
		string KeywordsPath { get; set; }
	}

	/// <inheritdoc />
	public class KeywordMarkerTokenFilter : TokenFilterBase, IKeywordMarkerTokenFilter
	{
		public KeywordMarkerTokenFilter() : base("keyword_marker") { }

		/// <inheritdoc />
		public bool? IgnoreCase { get; set; }

		/// <inheritdoc />
		public IEnumerable<string> Keywords { get; set; }

		/// <inheritdoc />
		public string KeywordsPath { get; set; }
	}

	/// <inheritdoc />
	public class KeywordMarkerTokenFilterDescriptor
		: TokenFilterDescriptorBase<KeywordMarkerTokenFilterDescriptor, IKeywordMarkerTokenFilter>, IKeywordMarkerTokenFilter
	{
		protected override string Type => "keyword_marker";
		bool? IKeywordMarkerTokenFilter.IgnoreCase { get; set; }

		IEnumerable<string> IKeywordMarkerTokenFilter.Keywords { get; set; }
		string IKeywordMarkerTokenFilter.KeywordsPath { get; set; }

		/// <inheritdoc />
		public KeywordMarkerTokenFilterDescriptor IgnoreCase(bool? ignoreCase = true) => Assign(a => a.IgnoreCase = ignoreCase);

		/// <inheritdoc />
		public KeywordMarkerTokenFilterDescriptor KeywordsPath(string path) => Assign(a => a.KeywordsPath = path);

		/// <inheritdoc />
		public KeywordMarkerTokenFilterDescriptor Keywords(IEnumerable<string> keywords) => Assign(a => a.Keywords = keywords);

		/// <inheritdoc />
		public KeywordMarkerTokenFilterDescriptor Keywords(params string[] keywords) => Assign(a => a.Keywords = keywords);
	}
}
