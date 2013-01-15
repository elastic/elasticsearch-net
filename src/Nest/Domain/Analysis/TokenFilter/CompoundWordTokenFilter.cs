using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
{
	/// <summary>
	/// Token filters that allow to decompose compound words.
	/// </summary>
    public abstract class CompoundWordTokenFilter : TokenFilterBase
    {
		protected CompoundWordTokenFilter(string type) : base(type)
		{
		}

		[JsonProperty("word_list")]
        public IEnumerable<string> MinGram { get; set; }

		[JsonProperty("word_list_path")]
        public string MaxGram { get; set; }
    }
}