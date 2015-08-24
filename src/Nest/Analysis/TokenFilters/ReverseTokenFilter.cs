namespace Nest
{
	/// <summary>
	/// A token filter of type reverse that simply reverses the tokens.
	/// </summary>
	public interface IReverseTokenFilter : ITokenFilter { }
	/// <inheritdoc/>
	public class ReverseTokenFilter : TokenFilterBase, IReverseTokenFilter
	{
		public ReverseTokenFilter() : base("reverse") { }
	}

}