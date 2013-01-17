using Newtonsoft.Json;

namespace Nest
{
    /// <summary>
	/// A token filter of type lowercase that normalizes token text to lower case.
	///<para> Lowercase token filter supports Greek and Turkish lowercase token filters through the language parameter.</para>
    /// </summary>
    public class LowercaseTokenFilter : TokenFilterBase
    {
		public LowercaseTokenFilter()
            : base("lowercase")
        {

        }

    }
}