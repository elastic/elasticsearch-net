// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Runtime.Serialization;

namespace Nest
{
	/// <summary>
	/// The simple_pattern tokenizer uses a regular expression to capture matching text as terms.
	/// </summary>
	public interface ISimplePatternSplitTokenizer : ITokenizer
	{
		/// <summary>
		/// Lucene regular expression, defaults to the empty string.
		/// </summary>
		[DataMember(Name = "pattern")]
		string Pattern { get; set; }
	}

	/// <inheritdoc />
	public class SimplePatternSplitTokenizer : TokenizerBase, ISimplePatternSplitTokenizer
	{
		public SimplePatternSplitTokenizer() => Type = "simple_pattern_split";

		/// <inheritdoc />
		public string Pattern { get; set; }
	}

	/// <inheritdoc />
	public class SimplePatternSplitTokenizerDescriptor
		: TokenizerDescriptorBase<SimplePatternSplitTokenizerDescriptor, ISimplePatternSplitTokenizer>, ISimplePatternSplitTokenizer
	{
		protected override string Type => "simple_pattern_split";

		string ISimplePatternSplitTokenizer.Pattern { get; set; }

		/// <inheritdoc cref="ISimplePatternSplitTokenizer.Pattern" />
		public SimplePatternSplitTokenizerDescriptor Pattern(string pattern) => Assign(pattern, (a, v) => a.Pattern = v);
	}
}
