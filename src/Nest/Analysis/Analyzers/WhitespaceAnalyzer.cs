// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

﻿namespace Nest
{
	/// <summary>
	/// An analyzer of type whitespace that is built using a Whitespace Tokenizer.
	/// </summary>
	public interface IWhitespaceAnalyzer : IAnalyzer { }

	/// <inheritdoc />
	public class WhitespaceAnalyzer : AnalyzerBase, IWhitespaceAnalyzer
	{
		public WhitespaceAnalyzer() : base("whitespace") { }
	}

	/// <inheritdoc />
	public class WhitespaceAnalyzerDescriptor : AnalyzerDescriptorBase<WhitespaceAnalyzerDescriptor, IWhitespaceAnalyzer>, IWhitespaceAnalyzer
	{
		protected override string Type => "whitespace";
	}
}
