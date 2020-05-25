// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

﻿namespace Nest
{
	/// <summary>
	/// An analyzer of type keyword that “tokenizes” an entire stream as a single token. This is useful for data like zip codes, ids and so on.
	/// <para>Note, when using mapping definitions, it make more sense to simply mark the field as not_analyzed.</para>
	/// </summary>
	public interface IKeywordAnalyzer : IAnalyzer { }

	/// <inheritdoc />
	public class KeywordAnalyzer : AnalyzerBase, IKeywordAnalyzer
	{
		public KeywordAnalyzer() : base("keyword") { }
	}

	/// <inheritdoc />
	public class KeywordAnalyzerDescriptor
		: AnalyzerDescriptorBase<KeywordAnalyzerDescriptor, IKeywordAnalyzer>, IKeywordAnalyzer
	{
		protected override string Type => "keyword";
	}
}
