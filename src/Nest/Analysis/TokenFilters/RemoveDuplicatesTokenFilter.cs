using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
{
	/// <summary>
	/// A token filter of type multiplexer will emit multiple tokens at the same position, each version of the token having
	/// been run through a different filter. Identical output tokens at the same position will be removed.
	/// </summary>
	public interface IRemoveDuplicatesTokenFilter : ITokenFilter { }

	///<inheritdoc cref="IRemoveDuplicatesTokenFilter"/>
	public class RemoveDuplicatesTokenFilter : TokenFilterBase, IRemoveDuplicatesTokenFilter
	{
		internal const string TokenType = "remove_duplicates";
		public RemoveDuplicatesTokenFilter() : base(TokenType) { }
	}

	///<inheritdoc cref="IRemoveDuplicatesTokenFilter"/>
	public class RemoveDuplicatesTokenFilterDescriptor
		: TokenFilterDescriptorBase<RemoveDuplicatesTokenFilterDescriptor, IRemoveDuplicatesTokenFilter>, IRemoveDuplicatesTokenFilter
	{
		protected override string Type => RemoveDuplicatesTokenFilter.TokenType;
	}

}
