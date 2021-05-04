// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

namespace Nest
{
	/// <summary>
	/// An analyzer of type simple that is built using a Lower Case Tokenizer.
	/// </summary>
	public interface ISimpleAnalyzer : IAnalyzer { }

	/// <inheritdoc />
	public class SimpleAnalyzer : AnalyzerBase, ISimpleAnalyzer
	{
		public SimpleAnalyzer() : base("simple") { }
	}

	/// <inheritdoc />
	public class SimpleAnalyzerDescriptor : AnalyzerDescriptorBase<SimpleAnalyzerDescriptor, ISimpleAnalyzer>, ISimpleAnalyzer
	{
		protected override string Type => "simple";
	}
}
