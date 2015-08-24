using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
{
	/// <summary>
	/// The pattern_capture token filter, unlike the pattern tokenizer, emits a token for every capture group in the regular expression.
	/// </summary>
	public interface IPatternCaptureTokenFilter : ITokenFilter
	{
		/// <summary>
		/// The regular expression patterns to capture
		/// </summary>
		[JsonProperty("patterns")]
		IEnumerable<string> Patterns { get; set; }

		/// <summary>
		/// If preserve_original is set to true then it would also emit the original token
		/// </summary>
		[JsonProperty("preserve_original")]
		bool? PreserveOriginal { get; set; }
	}
	/// <inheritdoc/>
	public class PatternCaptureTokenFilter : TokenFilterBase, IPatternCaptureTokenFilter
	{
		public PatternCaptureTokenFilter() : base("pattern_capture") { }

		/// <inheritdoc/>
		public IEnumerable<string> Patterns { get; set; }

		/// <inheritdoc/>
		public bool? PreserveOriginal { get; set; }
	}
}
