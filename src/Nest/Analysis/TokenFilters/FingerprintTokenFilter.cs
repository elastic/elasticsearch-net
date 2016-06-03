using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Nest
{
	/// <summary>
	/// The fingerprint token filter emits a single token which is useful
	/// for fingerprinting a body of text, and/or providing a token that can be
	/// clustered on. It does this by sorting the tokens, deduplicating and
	/// then concatenating them back into a single token.
	/// </summary>
	public interface IFingerprintTokenFilter : ITokenFilter
	{
		/// <summary>
		/// The character that separates the tokens after concatenation.
		/// Defaults to a space.
		/// </summary>
		[JsonProperty("separator")]
		string Separator { get; set; }

		/// <summary>
		/// The maximum token size to emit. Defaults to 255.
		/// </summary>
		[JsonProperty("max_output_size")]
		int? MaxOutputSize { get; set; }
	}

	/// <inheritdoc/>
	public class FingerprintTokenFilter : TokenFilterBase, IFingerprintTokenFilter
	{
		public FingerprintTokenFilter() : base("fingerprint") { }

		/// <summary>
		/// The character that separates the tokens after concatenation.
		/// Defaults to a space.
		/// </summary>
		public string Separator { get; set; }

		/// <summary>
		/// The maximum token size to emit. Defaults to 255.
		/// </summary>
		public int? MaxOutputSize { get; set; }
	}
	///<inheritdoc/>
	public class FingerprintTokenFilterDescriptor
		: TokenFilterDescriptorBase<FingerprintTokenFilterDescriptor, IFingerprintTokenFilter>, IFingerprintTokenFilter
	{
		protected override string Type => "fingerprint";

		string IFingerprintTokenFilter.Separator { get; set; }
		int? IFingerprintTokenFilter.MaxOutputSize { get; set; }

		public FingerprintTokenFilterDescriptor Separator(string separator) => Assign(a => a.Separator = separator);

		public FingerprintTokenFilterDescriptor MaxOutputSize(int? maxOutputSize) => Assign(a => a.MaxOutputSize = maxOutputSize);
	}
}
