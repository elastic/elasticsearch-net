using Newtonsoft.Json;

namespace Nest
{
	/// <summary>
	/// A token filter of type standard that normalizes tokens extracted with the Standard Tokenizer.
	/// </summary>
	public class StandardTokenFilter : TokenFilterBase
	{
		public StandardTokenFilter()
			: base("standard")
		{

		}

	}

}