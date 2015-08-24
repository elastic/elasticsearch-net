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
	///<inheritdoc/>
	public class PatternCaptureTokenFilterDescriptor 
		: TokenFilterDescriptorBase<PatternCaptureTokenFilterDescriptor, IPatternCaptureTokenFilter>, IPatternCaptureTokenFilter
	{
		protected override string Type => "pattern_capture";

		bool? IPatternCaptureTokenFilter.PreserveOriginal { get; set; }
		IEnumerable<string> IPatternCaptureTokenFilter.Patterns { get; set; }

		///<inheritdoc/>
		public PatternCaptureTokenFilterDescriptor PreserveOriginal(bool? preserve = true) => Assign(a => a.PreserveOriginal = preserve);

		///<inheritdoc/>
		public PatternCaptureTokenFilterDescriptor Patterns(IEnumerable<string> patterns) => Assign(a => a.Patterns = patterns);

		///<inheritdoc/>
		public PatternCaptureTokenFilterDescriptor Patterns(params string[] patterns) => Assign(a => a.Patterns = patterns);

	}

}
