using Newtonsoft.Json;

namespace Nest
{
	/// <summary>
	/// A tokenizer of type pattern that can flexibly separate text into terms via a regular expression. 
	/// </summary>
	public interface IPatternTokenizer : ITokenizer
	{
		/// <summary>
		/// The regular expression pattern, defaults to \W+.
		/// </summary>
		[JsonProperty("pattern")]
		string Pattern { get; set; }

		/// <summary>
		/// The regular expression flags.
		/// </summary>
		[JsonProperty("flags")]
		string Flags { get; set; }
		
		/// <summary>
		/// Which group to extract into tokens. Defaults to -1 (split).
		/// </summary>
		[JsonProperty("group")]
		int? Group { get; set; }

	}

	/// <inheritdoc/>
	public class PatternTokenizer : TokenizerBase, IPatternTokenizer
    {
		public PatternTokenizer() { Type = "pattern"; }

		/// <summary/>
		public string Pattern { get; set; }

		/// <summary/>
		public string Flags { get; set; }
		
		/// <summary/>
		public int? Group { get; set; }
    }
	/// <inheritdoc/>
	public class PatternTokenizerDescriptor 
		: TokenizerDescriptorBase<PatternTokenizerDescriptor, IPatternTokenizer>, IPatternTokenizer
	{
		protected override string Type => "pattern";

		int? IPatternTokenizer.Group { get; set; }
		string IPatternTokenizer.Pattern { get; set; }
		string IPatternTokenizer.Flags { get; set; }

		/// <inheritdoc/>
		public PatternTokenizerDescriptor Group(int? group) => Assign(a => a.Group = group);

		/// <inheritdoc/>
		public PatternTokenizerDescriptor Pattern(string pattern) => Assign(a => a.Pattern = pattern);

		/// <inheritdoc/>
		public PatternTokenizerDescriptor Flags(string flags) => Assign(a => a.Flags = flags);

	}
}