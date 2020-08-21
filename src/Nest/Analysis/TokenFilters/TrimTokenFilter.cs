// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

namespace Nest
{
	/// <summary>
	/// The trim token filter trims surrounding whitespaces around a token.
	/// </summary>
	public interface ITrimTokenFilter : ITokenFilter { }

	/// <inheritdoc />
	public class TrimTokenFilter : TokenFilterBase, ITrimTokenFilter
	{
		public TrimTokenFilter() : base("trim") { }
	}

	/// <inheritdoc />
	public class TrimTokenFilterDescriptor
		: TokenFilterDescriptorBase<TrimTokenFilterDescriptor, ITrimTokenFilter>, ITrimTokenFilter
	{
		protected override string Type => "trim";
	}
}
