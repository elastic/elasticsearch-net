using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
{
	/// <summary>
	/// A token filter of type multiplexer will emit multiple tokens at the same position, each version of the token having
	/// been run through a different filter. Identical output tokens at the same position will be removed.
	/// </summary>
	public interface IMultiplexerTokenFilter : ITokenFilter
	{
		[JsonProperty("filters")]
		IEnumerable<string> Filters { get; set; }

		[JsonProperty("preserve_original")]
		bool? PreserveOriginal { get; set; }
	}
	public class MultiplexerTokenFilter : TokenFilterBase, IMultiplexerTokenFilter
	{
		internal const string TokenType = "multiplexer";
		public MultiplexerTokenFilter() : base(TokenType) { }

		/// <inheritdoc cref="IMultiplexerTokenFilter.Filters"/>
		public IEnumerable<string> Filters { get; set; }

		/// <inheritdoc cref="IMultiplexerTokenFilter.PreserveOriginal"/>
		public bool? PreserveOriginal { get; set; }

	}
	///<inheritdoc cref="IMultiplexerTokenFilter"/>
	public class MultiplexerTokenFilterDescriptor
		: TokenFilterDescriptorBase<MultiplexerTokenFilterDescriptor, IMultiplexerTokenFilter>, IMultiplexerTokenFilter
	{
		protected override string Type => MultiplexerTokenFilter.TokenType;

		IEnumerable<string> IMultiplexerTokenFilter.Filters { get; set; }
		bool? IMultiplexerTokenFilter.PreserveOriginal { get; set; }

		/// <inheritdoc cref="IMultiplexerTokenFilter.Filters"/>
		public MultiplexerTokenFilterDescriptor Filters(IEnumerable<string> filters) => Assign(a => a.Filters = filters);

		/// <inheritdoc cref="IMultiplexerTokenFilter.Filters"/>
		public MultiplexerTokenFilterDescriptor Filters(params string[] filters) => Assign(a => a.Filters = filters);

		/// <inheritdoc cref="IMultiplexerTokenFilter.PreserveOriginal"/>
		public MultiplexerTokenFilterDescriptor PreserveOriginal(bool? preserve = true) => Assign(a => a.PreserveOriginal = preserve);
	}

}
