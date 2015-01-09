using Newtonsoft.Json;

namespace Nest
{
	/// <summary>
	/// A tokenizer of type standard providing grammar based tokenizer that is a good tokenizer for most European language documents. 
	/// <para>The tokenizer implements the Unicode Text Segmentation algorithm, as specified in Unicode Standard Annex #29.</para>
	/// </summary>
	public class StandardTokenizer : TokenizerBase
    {
		public StandardTokenizer()
        {
            Type = "standard";
        }

		/// <summary>
		/// The maximum token length. If a token is seen that exceeds this length then it is discarded. Defaults to 255.
		/// </summary>
		[JsonProperty("max_token_length")]
		public int? MaximumTokenLength { get; set; }		
    }
}