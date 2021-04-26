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
	/// The kuromoji_readingform token filter replaces the token with its reading form in either katakana or romaji.
	/// Part of the `analysis-kuromoji` plugin: https://www.elastic.co/guide/en/elasticsearch/plugins/current/analysis-kuromoji.html
	/// </summary>
	public interface IKuromojiReadingFormTokenFilter : ITokenFilter
	{
		/// <summary>
		/// Whether romaji reading form should be output instead of katakana. Defaults to false.
		/// </summary>
		[DataMember(Name ="use_romaji")]
		[JsonFormatter(typeof(NullableStringBooleanFormatter))]
		bool? UseRomaji { get; set; }
	}

	/// <inheritdoc />
	public class KuromojiReadingFormTokenFilter : TokenFilterBase, IKuromojiReadingFormTokenFilter
	{
		public KuromojiReadingFormTokenFilter() : base("kuromoji_readingform") { }

		/// <inheritdoc />
		public bool? UseRomaji { get; set; }
	}

	/// <inheritdoc />
	public class KuromojiReadingFormTokenFilterDescriptor
		: TokenFilterDescriptorBase<KuromojiReadingFormTokenFilterDescriptor, IKuromojiReadingFormTokenFilter>, IKuromojiReadingFormTokenFilter
	{
		protected override string Type => "kuromoji_readingform";

		bool? IKuromojiReadingFormTokenFilter.UseRomaji { get; set; }

		/// <inheritdoc />
		public KuromojiReadingFormTokenFilterDescriptor UseRomaji(bool? useRomaji = true) => Assign(useRomaji, (a, v) => a.UseRomaji = v);
	}
}
