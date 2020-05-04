// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

﻿namespace Nest
{
	/// <summary>
	/// The kstem token filter is a high performance filter for english.
	/// <para> All terms must already be lowercased (use lowercase filter) for this filter to work correctly.</para>
	/// </summary>
	public interface IKStemTokenFilter : ITokenFilter { }

	/// <inheritdoc />
	public class KStemTokenFilter : TokenFilterBase, IKStemTokenFilter
	{
		public KStemTokenFilter() : base("kstem") { }
	}

	/// <inheritdoc />
	public class KStemTokenFilterDescriptor
		: TokenFilterDescriptorBase<KStemTokenFilterDescriptor, IKStemTokenFilter>, IKStemTokenFilter
	{
		protected override string Type => "kstem";
	}
}
