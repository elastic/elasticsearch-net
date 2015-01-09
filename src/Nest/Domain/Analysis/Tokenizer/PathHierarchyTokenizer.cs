using Newtonsoft.Json;

namespace Nest
{
	/// <summary>
	/// The path_hierarchy tokenizer takes something like this:
	///<para>/something/something/else</para>
	///<para>And produces tokens:</para>
	///<para></para>
	///<para>/something</para>
	///<para>/something/something</para>
	///<para>/something/something/else</para>
	/// </summary>
	public class PathHierarchyTokenizer : TokenizerBase
    {
		public PathHierarchyTokenizer()
        {
			Type = "path_hierarchy";
        }

		/// <summary>
		/// The character delimiter to use, defaults to /.
		/// </summary>
		[JsonProperty("delimiter")]
		public string Delimiter { get; set; }

		/// <summary>
		/// An optional replacement character to use. Defaults to the delimiter
		/// </summary>
		[JsonProperty("replacement")]
		public string Replacement { get; set; }

		/// <summary>
		/// The buffer size to use, defaults to 1024.
		/// </summary>
		[JsonProperty("buffer_size")]
		public int? BufferSize { get; set; }

		/// <summary>
		/// Generates tokens in reverse order, defaults to false.
		/// </summary>
		[JsonProperty("reverse")]
		public bool? Reverse { get; set; }

		/// <summary>
		/// Controls initial tokens to skip, defaults to 0.
		/// </summary>
		[JsonProperty("skip")]
		public int? Skip { get; set; }	
    }
}