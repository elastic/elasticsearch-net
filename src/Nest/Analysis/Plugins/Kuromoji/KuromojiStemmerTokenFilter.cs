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

using System.Runtime.Serialization;
using Nest.Utf8Json;

namespace Nest
{
	/// <summary>
	/// The kuromoji_stemmer token filter normalizes common katakana spelling variations ending in a
	/// long sound character by removing this character (U+30FC). Only full-width katakana characters are supported.
	/// Part of the `analysis-kuromoji` plugin: https://www.elastic.co/guide/en/elasticsearch/plugins/current/analysis-kuromoji.html
	/// </summary>
	public interface IKuromojiStemmerTokenFilter : ITokenFilter
	{
		/// <summary>
		/// Katakana words shorter than the minimum length are not stemmed (default is 4).
		/// </summary>
		[DataMember(Name ="minimum_length")]
		[JsonFormatter(typeof(NullableStringIntFormatter))]
		int? MinimumLength { get; set; }
	}

	/// <inheritdoc />
	public class KuromojiStemmerTokenFilter : TokenFilterBase, IKuromojiStemmerTokenFilter
	{
		public KuromojiStemmerTokenFilter() : base("kuromoji_stemmer") { }

		/// <inheritdoc />
		public int? MinimumLength { get; set; }
	}

	/// <inheritdoc />
	public class KuromojiStemmerTokenFilterDescriptor
		: TokenFilterDescriptorBase<KuromojiStemmerTokenFilterDescriptor, IKuromojiStemmerTokenFilter>, IKuromojiStemmerTokenFilter
	{
		protected override string Type => "kuromoji_stemmer";

		int? IKuromojiStemmerTokenFilter.MinimumLength { get; set; }

		/// <inheritdoc />
		public KuromojiStemmerTokenFilterDescriptor MinimumLength(int? minimumLength) => Assign(minimumLength, (a, v) => a.MinimumLength = v);
	}
}
