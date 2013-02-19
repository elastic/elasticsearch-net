using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
{
	/// <summary>
	/// The unique token filter can be used to only index unique tokens during analysis. By default it is applied on all the token stream
	/// </summary>
    public class UniqueTokenFilter : TokenFilterBase
    {
		public UniqueTokenFilter()
			: base("unique")
        {
        }

		/// <summary>
		///  If only_on_same_position is set to true, it will only remove duplicate tokens on the same position.
		/// </summary>
		[JsonProperty("only_on_same_position ")]
        public bool? OnlyOnSamePosition { get; set; }
    }
}