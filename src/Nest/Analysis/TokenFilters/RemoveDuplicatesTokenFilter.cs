namespace Nest
{
	/// <summary>
	///     A token filter that drops identical tokens at the same position
	/// </summary>
	public interface IRemoveDuplicatesTokenFilter : ITokenFilter { }

	/// <inheritdoc cref="IRemoveDuplicatesTokenFilter" />
	public class RemoveDuplicatesTokenFilter : TokenFilterBase, IRemoveDuplicatesTokenFilter
	{
		internal const string TokenType = "remove_duplicates";

		public RemoveDuplicatesTokenFilter() : base(TokenType) { }
	}

	/// <inheritdoc cref="IRemoveDuplicatesTokenFilter" />
	public class RemoveDuplicatesTokenFilterDescriptor
		: TokenFilterDescriptorBase<RemoveDuplicatesTokenFilterDescriptor, IRemoveDuplicatesTokenFilter>, IRemoveDuplicatesTokenFilter
	{
		protected override string Type => RemoveDuplicatesTokenFilter.TokenType;
	}
}
