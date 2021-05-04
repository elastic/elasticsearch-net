// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

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
