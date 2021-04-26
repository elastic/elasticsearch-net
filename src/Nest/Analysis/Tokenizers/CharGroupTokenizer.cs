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
using Elasticsearch.Net.Utf8Json;

namespace Nest
{
	/// <summary>
	/// A tokenizer that breaks text into terms whenever it encounters a character which is in a defined set. It is mostly useful
	/// for cases where a simple custom tokenization is desired, and the overhead of use of <see cref="PatternTokenizer" /> is not acceptable.
	/// </summary>
	public interface ICharGroupTokenizer : ITokenizer
	{
		/// <summary>
		/// A list containing a list of characters to tokenize the string on. Whenever a character from this list is encountered, a
		/// new token is started. This accepts either single characters like eg. -, or character groups: whitespace, letter, digit,
		/// punctuation, symbol.
		/// </summary>
		[DataMember(Name ="tokenize_on_chars")]
		IEnumerable<string> TokenizeOnCharacters { get; set; }

		/// <summary>
		/// The maximum token length. If a token is seen that exceeds this length then
		/// it is split at <see cref="MaxTokenLength"/> intervals. Defaults to `255`.
		/// <para />
		/// Valid in Elasticsearch 7.9.0+
		/// </summary>
		[DataMember(Name = "max_token_length")]
		[JsonFormatter(typeof(NullableStringIntFormatter))]
		int? MaxTokenLength { get; set; }
	}

	/// <inheritdoc cref="ICharGroupTokenizer" />
	public class CharGroupTokenizer : TokenizerBase, ICharGroupTokenizer
	{
		internal const string TokenizerType = "char_group";

		public CharGroupTokenizer() => Type = TokenizerType;

		/// <inheritdoc cref="ICharGroupTokenizer.TokenizeOnCharacters" />
		public IEnumerable<string> TokenizeOnCharacters { get; set; }

		/// <inheritdoc cref="ICharGroupTokenizer.MaxTokenLength" />
		public int? MaxTokenLength { get; set; }
	}

	/// <inheritdoc cref="ICharGroupTokenizer" />
	public class CharGroupTokenizerDescriptor
		: TokenizerDescriptorBase<CharGroupTokenizerDescriptor, ICharGroupTokenizer>, ICharGroupTokenizer
	{
		protected override string Type => CharGroupTokenizer.TokenizerType;

		IEnumerable<string> ICharGroupTokenizer.TokenizeOnCharacters { get; set; }
		int? ICharGroupTokenizer.MaxTokenLength { get; set; }

		/// <inheritdoc cref="ICharGroupTokenizer.TokenizeOnCharacters" />
		public CharGroupTokenizerDescriptor TokenizeOnCharacters(params string[] characters) =>
			Assign(characters, (a, v) => a.TokenizeOnCharacters = v);

		/// <inheritdoc cref="ICharGroupTokenizer.TokenizeOnCharacters" />
		public CharGroupTokenizerDescriptor TokenizeOnCharacters(IEnumerable<string> characters) =>
			Assign(characters, (a, v) => a.TokenizeOnCharacters = v);

		/// <inheritdoc cref="ICharGroupTokenizer.MaxTokenLength" />
		public CharGroupTokenizerDescriptor MaxTokenLength(int? maxTokenLength) =>
			Assign(maxTokenLength, (a, v) => a.MaxTokenLength = v);
	}
}
