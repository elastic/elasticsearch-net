using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
{
	/// <summary>
	/// A token filter which removes elisions. For example, “l’avion” (the plane) will tokenized as “avion” (plane).
	/// </summary>
    public class ElisionTokenFilter : TokenFilterBase
    {
		public ElisionTokenFilter()
			: base("elision")
        {
        }

		/// <summary>
		/// Accepts articles setting which is a set of stop words articles
		/// </summary>
        [JsonProperty("articles")]
        public IEnumerable<string> Articles { get; set; }
    }
}