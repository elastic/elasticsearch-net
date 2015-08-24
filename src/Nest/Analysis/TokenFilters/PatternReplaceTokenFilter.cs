using Newtonsoft.Json;

namespace Nest
{
	/// <summary>
	/// The pattern_replace token filter allows to easily handle string replacements based on a regular expression. 
	/// </summary>
	public interface IPatternReplaceTokenFilter : ITokenFilter
	{
		/// <summary>
		/// The regular expression
		/// </summary>
		[JsonProperty("pattern")]
		string Pattern { get; set; }

		/// <summary>
		/// Replacement string
		/// </summary>
		[JsonProperty("replacement")]
		string Replacement { get; set; }
	}
	/// <inheritdoc/>
	public class PatternReplaceTokenFilter : TokenFilterBase, IPatternReplaceTokenFilter
	{
		public PatternReplaceTokenFilter() : base("pattern_replace") { }

		/// <inheritdoc/>
		public string Pattern { get; set; }

		/// <inheritdoc/>
		public string Replacement { get; set; }
	}
	///<inheritdoc/>
	public class PatternReplaceTokenFilterDescriptor 
		: TokenFilterDescriptorBase<PatternReplaceTokenFilterDescriptor, IPatternReplaceTokenFilter>, IPatternReplaceTokenFilter
	{
		protected override string Type => "pattern_replace";

		string IPatternReplaceTokenFilter.Pattern { get; set; }

		string IPatternReplaceTokenFilter.Replacement { get; set; }

		///<inheritdoc/>
		public PatternReplaceTokenFilterDescriptor Pattern(string pattern) => Assign(a => a.Pattern = pattern);

		///<inheritdoc/>
		public PatternReplaceTokenFilterDescriptor Replacement(string replacement) => Assign(a => a.Replacement = replacement);

	}

}