/*
 * Licensed to Elasticsearch B.V. under one or more contributor
 * license agreements. See the NOTICE file distributed with
 * this work for additional information regarding copyright
 * ownership. Elasticsearch B.V. licenses this file to you under
 * the Apache License, Version 2.0 (the "License"); you may
 * not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *    http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing,
 * software distributed under the License is distributed on an
 * "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY
 * KIND, either express or implied.  See the License for the
 * specific language governing permissions and limitations
 * under the License.
 */

using System.Collections.Generic;
using System.Runtime.Serialization;
using Nest.Utf8Json;

namespace Nest
{
	/// <summary>
	/// A tokenizer of type nGram.
	/// </summary>
	public interface INGramTokenizer : ITokenizer
	{
		/// <summary>
		/// Maximum size in codepoints of a single n-gram, defaults to 2.
		/// </summary>
		[DataMember(Name ="max_gram")]
		[JsonFormatter(typeof(NullableStringIntFormatter))]
		int? MaxGram { get; set; }

		/// <summary>
		/// Minimum size in codepoints of a single n-gram, defaults to 1.
		/// </summary>
		[DataMember(Name ="min_gram")]
		[JsonFormatter(typeof(NullableStringIntFormatter))]
		int? MinGram { get; set; }

		/// <summary>
		/// Characters classes to keep in the tokens, Elasticsearch will
		/// split on characters that don’t belong to any of these classes.
		/// </summary>
		[DataMember(Name ="token_chars")]
		IEnumerable<TokenChar> TokenChars { get; set; }

		/// <summary>
		/// Custom characters that should be treated as part of a token. For example,
		/// setting this to +-_ will make the tokenizer treat the plus, minus and
		/// underscore sign as part of a token.
		/// <para />
		/// Requires setting <see cref="TokenChar.Custom"/> as part of <see cref="TokenChars"/>
		/// <para />
		/// Available in Elasticsearch 7.6.0+.
		/// </summary>
		[DataMember(Name = "custom_token_chars")]
		string CustomTokenChars { get; set; }
	}

	/// <inheritdoc />
	public class NGramTokenizer : TokenizerBase, INGramTokenizer
	{
		public NGramTokenizer() => Type = "ngram";

		/// <inheritdoc />
		public int? MaxGram { get; set; }

		/// <inheritdoc />
		public int? MinGram { get; set; }

		/// <inheritdoc />
		public IEnumerable<TokenChar> TokenChars { get; set; }

		/// <inheritdoc />
		public string CustomTokenChars { get; set; }
	}

	/// <inheritdoc />
	public class NGramTokenizerDescriptor
		: TokenizerDescriptorBase<NGramTokenizerDescriptor, INGramTokenizer>, INGramTokenizer
	{
		protected override string Type => "ngram";
		int? INGramTokenizer.MaxGram { get; set; }
		int? INGramTokenizer.MinGram { get; set; }
		IEnumerable<TokenChar> INGramTokenizer.TokenChars { get; set; }

		string INGramTokenizer.CustomTokenChars { get; set; }

		/// <inheritdoc cref="INGramTokenizer.MinGram" />
		public NGramTokenizerDescriptor MinGram(int? minGram) => Assign(minGram, (a, v) => a.MinGram = v);

		/// <inheritdoc cref="INGramTokenizer.MaxGram" />
		public NGramTokenizerDescriptor MaxGram(int? minGram) => Assign(minGram, (a, v) => a.MaxGram = v);

		/// <inheritdoc cref="INGramTokenizer.TokenChars" />
		public NGramTokenizerDescriptor TokenChars(IEnumerable<TokenChar> tokenChars) =>
			Assign(tokenChars, (a, v) => a.TokenChars = v);

		/// <inheritdoc cref="INGramTokenizer.TokenChars" />
		public NGramTokenizerDescriptor TokenChars(params TokenChar[] tokenChars) => Assign(tokenChars, (a, v) => a.TokenChars = v);

		/// <inheritdoc cref="INGramTokenizer.CustomTokenChars" />
		public NGramTokenizerDescriptor CustomTokenChars(string customTokenChars) =>
			Assign(customTokenChars, (a, v) => a.CustomTokenChars = v);
	}
}
