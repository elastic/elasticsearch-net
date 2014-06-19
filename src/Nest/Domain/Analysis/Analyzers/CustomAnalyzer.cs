using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
{
	/// <summary>
	/// An analyzer of type custom that allows to combine a Tokenizer with zero or more Token Filters, and zero or more Char Filters. 
	/// <para>The custom analyzer accepts a logical/registered name of the tokenizer to use, and a list of logical/registered names of token filters.</para>
	/// </summary>
	public class CustomAnalyzer : AnalyzerBase
    {
	    public CustomAnalyzer(string type)
	    {
	        Type = type;
	    }

        public CustomAnalyzer() : this("custom") {}

	    [JsonProperty("tokenizer")]
        public string Tokenizer { get; set; }

        [JsonProperty("filter")]
        public IList<string> Filter { get; set; }

        [JsonProperty("char_filter")]
        public IList<string> CharFilter { get; set; }

		[JsonProperty("alias")]
		public IList<string> Alias { get; set; }
    }
}