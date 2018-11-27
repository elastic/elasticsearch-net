using System.Runtime.Serialization;

namespace Nest
{
	/// <summary>
	/// The kuromoji_iteration_mark normalizes Japanese horizontal iteration marks (odoriji) to their expanded form.
	/// Part of the `analysis-kuromoji` plugin: https://www.elastic.co/guide/en/elasticsearch/plugins/current/analysis-kuromoji.html
	/// </summary>
	public interface IKuromojiIterationMarkCharFilter : ICharFilter
	{
		[DataMember(Name ="normalize_kana")]
		bool? NormalizeKana { get; set; }

		[DataMember(Name ="normalize_kanji")]
		bool? NormalizeKanji { get; set; }
	}

	/// <inheritdoc />
	public class KuromojiIterationMarkCharFilter : CharFilterBase, IKuromojiIterationMarkCharFilter
	{
		public KuromojiIterationMarkCharFilter() : base("kuromoji_iteration_mark") { }

		/// <inheritdoc />
		public bool? NormalizeKana { get; set; }

		/// <inheritdoc />
		public bool? NormalizeKanji { get; set; }
	}

	/// <inheritdoc />
	public class KuromojiIterationMarkCharFilterDescriptor
		: CharFilterDescriptorBase<KuromojiIterationMarkCharFilterDescriptor, IKuromojiIterationMarkCharFilter>, IKuromojiIterationMarkCharFilter
	{
		protected override string Type => "kuromoji_iteration_mark";
		bool? IKuromojiIterationMarkCharFilter.NormalizeKana { get; set; }
		bool? IKuromojiIterationMarkCharFilter.NormalizeKanji { get; set; }

		/// <inheritdoc />
		public KuromojiIterationMarkCharFilterDescriptor NormalizeKanji(bool? normalize = true) =>
			Assign(a => a.NormalizeKanji = normalize);

		/// <inheritdoc />
		public KuromojiIterationMarkCharFilterDescriptor NormalizeKana(bool? normalize = true) =>
			Assign(a => a.NormalizeKana = normalize);
	}
}
