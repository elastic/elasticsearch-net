// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Runtime.Serialization;

namespace Nest
{
	/// <summary>
	/// The simple_pattern tokenizer uses a regular expression to capture matching text as terms.
	/// </summary>
	public interface ISimplePatternTokenizer : ITokenizer
	{
		/// <summary>
		/// Lucene regular expression, defaults to the empty string.
		/// </summary>
		[DataMember(Name = "pattern")]
		string Pattern { get; set; }
	}

	/// <inheritdoc />
	public class SimplePatternTokenizer : TokenizerBase, ISimplePatternTokenizer
	{
		public SimplePatternTokenizer() => Type = "simple_pattern";

		/// <inheritdoc />
		public string Pattern { get; set; }
	}

	/// <inheritdoc />
	public class SimplePatternTokenizerDescriptor
		: TokenizerDescriptorBase<SimplePatternTokenizerDescriptor, ISimplePatternTokenizer>, ISimplePatternTokenizer
	{
		protected override string Type => "simple_pattern";

		string ISimplePatternTokenizer.Pattern { get; set; }

		/// <inheritdoc cref="ISimplePatternTokenizer.Pattern" />
		public SimplePatternTokenizerDescriptor Pattern(string pattern) => Assign(pattern, (a, v) => a.Pattern = v);
	}
}
