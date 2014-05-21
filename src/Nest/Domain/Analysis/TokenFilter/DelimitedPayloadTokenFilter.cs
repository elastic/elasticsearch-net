using Newtonsoft.Json;

namespace Nest
{
    /// <summary>
    /// A token filter of type delimited_token_filter. Splits tokens into tokens and payload whenever a delimiter character is found.
    /// </summary>
    public class DelimitedPayloadTokenFilter : TokenFilterBase
    {

        public DelimitedPayloadTokenFilter()
            : base("delimited_payload_filter")
        { }

        /// <summary>
        /// Character used for splitting the tokens. Default is '|'.
        /// </summary>
        [JsonProperty("delimiter")]
        public char? Delimiter { get; set; }

        /// <summary>
        /// The type of the payload. 'int' for integer, 'float' for float and 'identity' for characters. Default is 'float.'
        /// </summary>
        [JsonProperty("encoding")]
        public string Encoding { get; set; }

    }
}
