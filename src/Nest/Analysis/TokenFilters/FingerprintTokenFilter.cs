using System.Runtime.Serialization;
using Elasticsearch.Net;

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
		/// The maximum token size to emit. Defaults to 255.
		/// </summary>
		[DataMember(Name ="max_output_size")]
		[JsonFormatter(typeof(NullableStringIntFormatter))]
		int? MaxOutputSize { get; set; }

		/// <summary>
		/// The character that separates the tokens after concatenation.
		/// Defaults to a space.
		/// </summary>
		[DataMember(Name ="separator")]
		string Separator { get; set; }
	}

	/// <inheritdoc />
	public class FingerprintTokenFilter : TokenFilterBase, IFingerprintTokenFilter
	{
		public FingerprintTokenFilter() : base("fingerprint") { }

		/// <summary>
		/// The maximum token size to emit. Defaults to 255.
		/// </summary>
		public int? MaxOutputSize { get; set; }

		/// <summary>
		/// The character that separates the tokens after concatenation.
		/// Defaults to a space.
		/// </summary>
		public string Separator { get; set; }
	}

	/// <inheritdoc />
	public class FingerprintTokenFilterDescriptor
		: TokenFilterDescriptorBase<FingerprintTokenFilterDescriptor, IFingerprintTokenFilter>, IFingerprintTokenFilter
	{
		protected override string Type => "fingerprint";
		int? IFingerprintTokenFilter.MaxOutputSize { get; set; }

		string IFingerprintTokenFilter.Separator { get; set; }

		public FingerprintTokenFilterDescriptor Separator(string separator) => Assign(a => a.Separator = separator);

		public FingerprintTokenFilterDescriptor MaxOutputSize(int? maxOutputSize) => Assign(a => a.MaxOutputSize = maxOutputSize);
	}
}
