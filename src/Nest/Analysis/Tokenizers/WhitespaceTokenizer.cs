// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Runtime.Serialization;
using Nest.Utf8Json;

namespace Nest
{
	/// <summary>
	/// A tokenizer of type whitespace that divides text at whitespace.
	/// </summary>
	public interface IWhitespaceTokenizer : ITokenizer
	{
		/// <summary>
		/// The maximum token length. If a token is seen that exceeds this length then it is split at
		/// <see cref="MaxTokenLength" /> intervals. Defaults to 255.
		/// </summary>
		/// <remarks>
		/// Valid for Elasticsearch 6.1.0+
		/// </remarks>
		[DataMember(Name ="max_token_length")]
		[JsonFormatter(typeof(NullableStringIntFormatter))]
		int? MaxTokenLength { get; set; }
	}

	/// <inheritdoc cref="IWhitespaceTokenizer" />
	public class WhitespaceTokenizer : TokenizerBase, IWhitespaceTokenizer
	{
		public WhitespaceTokenizer() => Type = "whitespace";

		/// <inheritdoc />
		public int? MaxTokenLength { get; set; }
	}

	/// <inheritdoc cref="IWhitespaceTokenizer" />
	public class WhitespaceTokenizerDescriptor
		: TokenizerDescriptorBase<WhitespaceTokenizerDescriptor, IWhitespaceTokenizer>, IWhitespaceTokenizer
	{
		protected override string Type => "whitespace";

		int? IWhitespaceTokenizer.MaxTokenLength { get; set; }

		/// <inheritdoc cref="IWhitespaceTokenizer.MaxTokenLength" />
		public WhitespaceTokenizerDescriptor MaxTokenLength(int? maxTokenLength) =>
			Assign(maxTokenLength, (a, v) => a.MaxTokenLength = v);
	}
}
