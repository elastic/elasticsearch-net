using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
{
	/// <summary>
	/// The kuromoji_iteration_mark normalizes Japanese horizontal iteration marks (odoriji) to their expanded form.
	/// Part of the `analysis-kuromoji` plugin: https://www.elastic.co/guide/en/elasticsearch/plugins/current/analysis-kuromoji.html
	/// </summary>
	public interface IKuromojiIterationMarkCharFilter : ICharFilter
	{
		[JsonProperty("normalize_kanji")]
		bool? NormalizeKanji { get; set; }

		[JsonProperty("normalize_kana")]
		bool? NormalizeKana { get; set; }
	}
	/// <inheritdoc/>
	public class KuromojiIterationMarkCharFilter : CharFilterBase, IKuromojiIterationMarkCharFilter
	{
		public KuromojiIterationMarkCharFilter() : base("kuromoji_iteration_mark") { }

		/// <inheritdoc/>
		public bool? NormalizeKanji { get; set; }

		/// <inheritdoc/>
		public bool? NormalizeKana { get; set; }
	}

	/// <inheritdoc/>
	public class KuromojiIterationMarkCharFilterDescriptor
		: CharFilterDescriptorBase<KuromojiIterationMarkCharFilterDescriptor, IKuromojiIterationMarkCharFilter>, IKuromojiIterationMarkCharFilter
	{
		protected override string Type => "kuromoji_iteration_mark";
		bool? IKuromojiIterationMarkCharFilter.NormalizeKanji { get; set; }
		bool? IKuromojiIterationMarkCharFilter.NormalizeKana { get; set; }

		/// <inheritdoc/>
		public KuromojiIterationMarkCharFilterDescriptor NormalizeKanji(bool? normalize = true) =>
			Assign(a => a.NormalizeKanji = normalize);

		/// <inheritdoc/>
		public KuromojiIterationMarkCharFilterDescriptor NormalizeKana(bool? normalize = true) =>
			Assign(a => a.NormalizeKana = normalize);

	}
}
