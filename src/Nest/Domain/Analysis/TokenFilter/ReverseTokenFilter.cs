using Newtonsoft.Json;

namespace Nest
{
	/// <summary>
	/// A token filter of type reverse that simply reverses the tokens.
	/// </summary>
	public class ReverseTokenFilter : TokenFilterBase
	{
		public ReverseTokenFilter()
			: base("reverse")
		{

		}

	}

}