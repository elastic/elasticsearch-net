using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
{
	/// <summary>
	/// A token filter of type keep that only keeps tokens with text contained in a predefined set of words.
	/// </summary>
	public class KeepTypesTokenFilter : TokenFilterBase
	{
		public KeepTypesTokenFilter()
			: base("keep_types")
		{

		}

		/// <summary>
		/// A list of types to keep.
		/// </summary>
		[JsonProperty("types")]
		public IEnumerable<string> Types { get; set; }

	}
}