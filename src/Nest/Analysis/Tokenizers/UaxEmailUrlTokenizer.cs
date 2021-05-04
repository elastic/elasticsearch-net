// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Runtime.Serialization;
using Nest.Utf8Json;

namespace Nest
{
	/// <summary>
	/// A tokenizer of type uax_url_email which works exactly like the standard tokenizer, but tokenizes emails and urls as single tokens
	/// </summary>
	public interface IUaxEmailUrlTokenizer : ITokenizer
	{
		/// <summary>
		/// The maximum token length. If a token is seen that exceeds this length then it is discarded. Defaults to 255.
		/// </summary>
		[DataMember(Name ="max_token_length")]
		[JsonFormatter(typeof(NullableStringIntFormatter))]
		int? MaxTokenLength { get; set; }
	}

	/// <summary />
	public class UaxEmailUrlTokenizer : TokenizerBase, IUaxEmailUrlTokenizer
	{
		public UaxEmailUrlTokenizer() => Type = "uax_url_email";

		/// <summary />
		public int? MaxTokenLength { get; set; }
	}

	/// <summary />
	public class UaxEmailUrlTokenizerDescriptor
		: TokenizerDescriptorBase<UaxEmailUrlTokenizerDescriptor, IUaxEmailUrlTokenizer>, IUaxEmailUrlTokenizer
	{
		protected override string Type => "uax_url_email";

		int? IUaxEmailUrlTokenizer.MaxTokenLength { get; set; }

		/// <inheritdoc />
		public UaxEmailUrlTokenizerDescriptor MaxTokenLength(int? maxLength) => Assign(maxLength, (a, v) => a.MaxTokenLength = v);
	}
}
