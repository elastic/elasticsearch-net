// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Runtime.Serialization;
using Nest.Utf8Json;

namespace Nest
{
	/// <summary>
	/// A tokenizer of type standard providing grammar based tokenizer that is a good tokenizer for most European language
	/// documents.
	/// <para>The tokenizer implements the Unicode Text Segmentation algorithm, as specified in Unicode Standard Annex #29.</para>
	/// </summary>
	public interface IStandardTokenizer : ITokenizer
	{
		/// <summary>
		/// The maximum token length. If a token is seen that exceeds this length then it is discarded. Defaults to 255.
		/// </summary>
		[DataMember(Name = "max_token_length")]
		[JsonFormatter(typeof(NullableStringIntFormatter))]
		int? MaxTokenLength { get; set; }
	}

	/// <summary />
	public class StandardTokenizer : TokenizerBase, IStandardTokenizer
	{
		public StandardTokenizer() => Type = "standard";

		/// <summary />
		public int? MaxTokenLength { get; set; }
	}

	public class StandardTokenizerDescriptor
		: TokenizerDescriptorBase<StandardTokenizerDescriptor, IStandardTokenizer>, IStandardTokenizer
	{
		protected override string Type => "standard";

		int? IStandardTokenizer.MaxTokenLength { get; set; }

		/// <inheritdoc />
		public StandardTokenizerDescriptor MaxTokenLength(int? maxLength) => Assign(maxLength, (a, v) => a.MaxTokenLength = v);
	}
}
