// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

﻿using System.Runtime.Serialization;
using Elasticsearch.Net.Utf8Json;

namespace Nest
{
	/// <summary>
	/// A tokenizer of type keyword that emits the entire input as a single input.
	/// </summary>
	public interface IKeywordTokenizer : ITokenizer
	{
		/// <summary>
		/// The term buffer size. Defaults to 256.
		/// </summary>
		[DataMember(Name ="buffer_size")]
		[JsonFormatter(typeof(NullableStringIntFormatter))]
		int? BufferSize { get; set; }
	}

	/// <inheritdoc />
	public class KeywordTokenizer : TokenizerBase, IKeywordTokenizer
	{
		public KeywordTokenizer() => Type = "keyword";

		/// <inheritdoc />
		public int? BufferSize { get; set; }
	}

	/// <inheritdoc />
	public class KeywordTokenizerDescriptor
		: TokenizerDescriptorBase<KeywordTokenizerDescriptor, IKeywordTokenizer>, IKeywordTokenizer
	{
		protected override string Type => "keyword";

		int? IKeywordTokenizer.BufferSize { get; set; }

		/// <inheritdoc />
		public KeywordTokenizerDescriptor BufferSize(int? size) => Assign(size, (a, v) => a.BufferSize = v);
	}
}
