// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

﻿namespace Nest
{
	/// <summary>
	/// A token filter of type porterStem that transforms the token stream as per the Porter stemming algorithm.
	/// </summary>
	public interface IPorterStemTokenFilter : ITokenFilter { }

	/// <inheritdoc />
	public class PorterStemTokenFilter : TokenFilterBase, IPorterStemTokenFilter
	{
		public PorterStemTokenFilter() : base("porter_stem") { }
	}

	/// <inheritdoc />
	public class PorterStemTokenFilterDescriptor
		: TokenFilterDescriptorBase<PorterStemTokenFilterDescriptor, IPorterStemTokenFilter>, IPorterStemTokenFilter
	{
		protected override string Type => "porter_stem";
	}
}
