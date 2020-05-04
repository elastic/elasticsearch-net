// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

﻿namespace Nest
{
	/// <summary>
	/// A token filter that drops identical tokens at the same position
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
