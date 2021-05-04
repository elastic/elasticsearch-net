// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

namespace Nest
{
	/// <summary>
	/// A token filter of type reverse that simply reverses the tokens.
	/// </summary>
	public interface IReverseTokenFilter : ITokenFilter { }

	/// <inheritdoc />
	public class ReverseTokenFilter : TokenFilterBase, IReverseTokenFilter
	{
		public ReverseTokenFilter() : base("reverse") { }
	}

	/// <inheritdoc />
	public class ReverseTokenFilterDescriptor
		: TokenFilterDescriptorBase<ReverseTokenFilterDescriptor, IReverseTokenFilter>, IReverseTokenFilter
	{
		protected override string Type => "reverse";
	}
}
