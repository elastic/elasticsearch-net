using Newtonsoft.Json;

namespace Nest
{
	/// <summary>
	/// A tokenizer of type keyword that emits the entire input as a single input.
	/// </summary>
	public class KeywordTokenizer : TokenizerBase
    {
		public KeywordTokenizer()
        {
            Type = "keyword";
        }

		/// <summary>
		/// The term buffer size. Defaults to 256.
		/// </summary>
		[JsonProperty("buffer_size")]
		public int? BufferSize { get; set; }
    }
}