using System.Collections.Generic;
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
		string Encoding { get; set; }

	}

	/// <inheritdoc/>
	public class DelimitedPayloadTokenFilter : TokenFilterBase, IDelimitedPayloadTokenFilter
	{
		public DelimitedPayloadTokenFilter() : base("delimited_payload_filter") { }

		/// <inheritdoc/>
		public char Delimiter { get; set; }

		/// <inheritdoc/>
		public string Encoding { get; set; }

	}

}