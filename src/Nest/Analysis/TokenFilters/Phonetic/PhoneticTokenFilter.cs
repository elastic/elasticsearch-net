using Newtonsoft.Json;

namespace Nest
{
	/// <summary>
	/// The phonetic token filter is provided as a plugin.
	/// </summary>
	public interface IPhoneticTokenFilter : ITokenFilter
	{
		/// <summary>
		/// The type of phonetic encoding to use
		/// </summary>
		[JsonProperty("encoder")]
		PhoneticEncoder? Encoder { get; set; }

		/// <summary>
		///The replace parameter (defaults to true) controls if the token processed should be replaced 
		/// with the encoded one (set it to true), or added (set it to false). 
		/// </summary>
		[JsonProperty("replace")]
		bool? Replace { get; set; }
	}
	/// <inheritdoc/>
	public class PhoneticTokenFilter : TokenFilterBase, IPhoneticTokenFilter
	{
		public PhoneticTokenFilter() : base("phonetic") { }

		/// <inheritdoc/>
		PhoneticEncoder? string Encoder { get; set; }

		/// <inheritdoc/>
		public bool? Replace { get; set; }

	}


}



