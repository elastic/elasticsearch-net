using System.Runtime.Serialization;

namespace Nest
{
	/// <summary>
	/// Filter terms returned based on their TF-IDF scores.
	/// This can be useful in order find out a good characteristic vector of a document.
	/// </summary>
	[DataContract]
	public interface ITermVectorFilter
	{
		/// <summary>
		/// Ignore words which occur in more than this many docs. Defaults to unbounded.
		/// </summary>
		[DataMember(Name ="max_doc_freq")]
		int? MaximumDocumentFrequency { get; set; }

		/// <summary>
		/// Maximum number of terms that must be returned per field. Defaults to 25.
		/// </summary>
		[DataMember(Name ="max_num_terms")]
		int? MaximumNumberOfTerms { get; set; }

		/// <summary>
		/// Ignore words with more than this frequency in the source doc. Defaults to unbounded.
		/// </summary>
		[DataMember(Name ="max_term_freq")]
		int? MaximumTermFrequency { get; set; }

		/// <summary>
		/// The maximum word length above which words will be ignored. Defaults to unbounded.
		/// </summary>
		[DataMember(Name ="max_word_length")]
		int? MaximumWordLength { get; set; }

		/// <summary>
		/// Ignore terms which do not occur in at least this many docs. Defaults to 1.
		/// </summary>
		[DataMember(Name ="min_doc_freq")]
		int? MinimumDocumentFrequency { get; set; }

		/// <summary>
		/// Ignore words with less than this frequency in the source doc. Defaults to 1.
		/// </summary>
		[DataMember(Name ="min_term_freq")]
		int? MinimumTermFrequency { get; set; }

		/// <summary>
		/// The minimum word length below which words will be ignored. Defaults to 0.
		/// </summary>
		[DataMember(Name ="min_word_length")]
		int? MinimumWordLength { get; set; }
	}

	/// <summary>
	/// Filter terms returned based on their TF-IDF scores.
	/// This can be useful in order find out a good characteristic vector of a document.
	/// </summary>
	public class TermVectorFilter : ITermVectorFilter
	{
		/// <summary>
		/// Ignore words which occur in more than this many docs. Defaults to unbounded.
		/// </summary>
		public int? MaximumDocumentFrequency { get; set; }

		/// <summary>
		/// Maximum number of terms that must be returned per field. Defaults to 25.
		/// </summary>
		public int? MaximumNumberOfTerms { get; set; }

		/// <summary>
		/// Ignore words with more than this frequency in the source doc. Defaults to unbounded.
		/// </summary>
		public int? MaximumTermFrequency { get; set; }

		/// <summary>
		/// The maximum word length above which words will be ignored. Defaults to unbounded.
		/// </summary>
		public int? MaximumWordLength { get; set; }

		/// <summary>
		/// Ignore terms which do not occur in at least this many docs. Defaults to 1.
		/// </summary>
		public int? MinimumDocumentFrequency { get; set; }

		/// <summary>
		/// Ignore words with less than this frequency in the source doc. Defaults to 1.
		/// </summary>
		public int? MinimumTermFrequency { get; set; }

		/// <summary>
		/// The minimum word length below which words will be ignored. Defaults to 0.
		/// </summary>
		public int? MinimumWordLength { get; set; }
	}

	/// <summary>
	/// Filter terms returned based on their TF-IDF scores.
	/// This can be useful in order find out a good characteristic vector of a document.
	/// </summary>
	public class TermVectorFilterDescriptor
		: DescriptorBase<TermVectorFilterDescriptor, ITermVectorFilter>, ITermVectorFilter
	{
		int? ITermVectorFilter.MaximumDocumentFrequency { get; set; }
		int? ITermVectorFilter.MaximumNumberOfTerms { get; set; }
		int? ITermVectorFilter.MaximumTermFrequency { get; set; }
		int? ITermVectorFilter.MaximumWordLength { get; set; }
		int? ITermVectorFilter.MinimumDocumentFrequency { get; set; }
		int? ITermVectorFilter.MinimumTermFrequency { get; set; }
		int? ITermVectorFilter.MinimumWordLength { get; set; }

		/// <summary>
		/// Maximum number of terms that must be returned per field. Defaults to 25.
		/// </summary>
		public TermVectorFilterDescriptor MaximimumNumberOfTerms(int? maxNumTerms) => Assign(a => a.MaximumNumberOfTerms = maxNumTerms);

		/// <summary>
		/// Ignore words with less than this frequency in the source doc. Defaults to 1.
		/// </summary>
		public TermVectorFilterDescriptor MinimumTermFrequency(int? minTermFreq) => Assign(a => a.MinimumTermFrequency = minTermFreq);

		/// <summary>
		/// Ignore words with more than this frequency in the source doc. Defaults to unbounded.
		/// </summary>
		public TermVectorFilterDescriptor MaximumTermFrequency(int? maxTermFreq) => Assign(a => a.MaximumTermFrequency = maxTermFreq);

		/// <summary>
		/// Ignore terms which do not occur in at least this many docs. Defaults to 1.
		/// </summary>
		public TermVectorFilterDescriptor MinimumDocumentFrequency(int? minDocFreq) => Assign(a => a.MinimumDocumentFrequency = minDocFreq);

		/// <summary>
		/// Ignore words which occur in more than this many docs. Defaults to unbounded.
		/// </summary>
		public TermVectorFilterDescriptor MaximumDocumentFrequency(int? maxDocFreq) => Assign(a => a.MaximumDocumentFrequency = maxDocFreq);

		/// <summary>
		/// The minimum word length below which words will be ignored. Defaults to 0.
		/// </summary>
		public TermVectorFilterDescriptor MinimumWordLength(int? minWordLength) => Assign(a => a.MinimumWordLength = minWordLength);

		/// <summary>
		/// The maximum word length above which words will be ignored. Defaults to unbounded.
		/// </summary>
		public TermVectorFilterDescriptor MaximumWordLength(int? maxWordLength) => Assign(a => a.MaximumWordLength = maxWordLength);
	}
}
