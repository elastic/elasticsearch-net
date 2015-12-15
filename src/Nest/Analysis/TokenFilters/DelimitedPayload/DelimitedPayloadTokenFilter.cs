using Newtonsoft.Json;

namespace Nest
{
	/// <summary>
	/// Splits tokens into tokens and payload whenever a delimiter character is found.
	/// </summary>
	public interface IDelimitedPayloadTokenFilter : ITokenFilter
	{
		/// <summary>
		/// Character used for splitting the tokens.
		/// </summary>
		[JsonProperty("delimiter")]
		char Delimiter { get; set; }

		/// <summary>
		/// The type of the payload. int for integer, float for float and identity for characters. 
		/// </summary>
		[JsonProperty("encoding")]
		DelimitedPayloadEncoding? Encoding { get; set; }

	}

	/// <inheritdoc/>
	public class DelimitedPayloadTokenFilter : TokenFilterBase, IDelimitedPayloadTokenFilter
	{
		public DelimitedPayloadTokenFilter() : base("delimited_payload_filter") { }

		/// <inheritdoc/>
		public char Delimiter { get; set; }

		/// <inheritdoc/>
		public DelimitedPayloadEncoding? Encoding { get; set; }

	}

	///<inheritdoc/>
	public class DelimitedPayloadTokenFilterDescriptor 
		: TokenFilterDescriptorBase<DelimitedPayloadTokenFilterDescriptor, IDelimitedPayloadTokenFilter>, IDelimitedPayloadTokenFilter
	{
		protected override string Type => "delimited_payload_filter";

		char IDelimitedPayloadTokenFilter.Delimiter { get; set; }
		DelimitedPayloadEncoding? IDelimitedPayloadTokenFilter.Encoding { get; set; }

		///<inheritdoc/>
		public DelimitedPayloadTokenFilterDescriptor Delimiter(char delimiter) => Assign(a => a.Delimiter = delimiter);

		///<inheritdoc/>
		public DelimitedPayloadTokenFilterDescriptor Encoding(DelimitedPayloadEncoding? encoding) => Assign(a => a.Encoding = encoding);

	}

}