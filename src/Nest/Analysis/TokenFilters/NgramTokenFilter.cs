using System.Runtime.Serialization;
using Utf8Json;

namespace Nest
{
	/// <summary>
	/// A token filter of type nGram.
	/// </summary>
	public interface INGramTokenFilter : ITokenFilter
	{
		/// <summary>
		/// Defaults to 2
		/// </summary>
		[DataMember(Name ="max_gram")]
		[JsonFormatter(typeof(NullableStringIntFormatter))]
		int? MaxGram { get; set; }

		/// <summary>
		/// Defaults to 1.
		/// </summary>
		[DataMember(Name ="min_gram")]
		[JsonFormatter(typeof(NullableStringIntFormatter))]
		int? MinGram { get; set; }
	}

	/// <inheritdoc />
	public class NGramTokenFilter : TokenFilterBase, INGramTokenFilter
	{
		public NGramTokenFilter() : base("ngram") { }

		/// <inheritdoc />
		public int? MaxGram { get; set; }

		/// <inheritdoc />
		public int? MinGram { get; set; }
	}

	/// <inheritdoc />
	public class NGramTokenFilterDescriptor
		: TokenFilterDescriptorBase<NGramTokenFilterDescriptor, INGramTokenFilter>, INGramTokenFilter
	{
		protected override string Type => "ngram";
		int? INGramTokenFilter.MaxGram { get; set; }

		int? INGramTokenFilter.MinGram { get; set; }

		/// <inheritdoc />
		public NGramTokenFilterDescriptor MinGram(int? minGram) => Assign(a => a.MinGram = minGram);

		/// <inheritdoc />
		public NGramTokenFilterDescriptor MaxGram(int? maxGram) => Assign(a => a.MaxGram = maxGram);
	}
}
