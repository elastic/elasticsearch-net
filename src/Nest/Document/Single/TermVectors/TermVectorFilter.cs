using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization.OptIn)]
	public interface ITermVectorFilter
	{
		[JsonProperty("max_num_terms")]
		int? MaximumNumberOfTerms { get; set; }

		[JsonProperty("min_term_freq")]
		int? MinimumTermFrequency { get; set; }

		[JsonProperty("min_doc_freq")]
		int? MinimumDocumentFrequency { get; set; }
	}

	public class TermVectorFilter : ITermVectorFilter
	{
		public int? MaximumNumberOfTerms { get; set; }

		public int? MinimumTermFrequency { get; set; }

		public int? MinimumDocumentFrequency { get; set; }
	}

	public class TermVectorFilterDescriptor
		: DescriptorBase<TermVectorFilterDescriptor, ITermVectorFilter>, ITermVectorFilter
	{
		int? ITermVectorFilter.MaximumNumberOfTerms { get; set; }

		int? ITermVectorFilter.MinimumDocumentFrequency { get; set; }

		int? ITermVectorFilter.MinimumTermFrequency { get; set; }

		public TermVectorFilterDescriptor MaximimumNumberOfTerms(int maxNumTerms) => Assign(a => a.MaximumNumberOfTerms = maxNumTerms);

		public TermVectorFilterDescriptor MinimumDocumentFrequency(int minDocFreq) => Assign(a => a.MinimumDocumentFrequency = minDocFreq);

		public TermVectorFilterDescriptor MinimumTermFrequency(int minTermFreq) => Assign(a => a.MinimumTermFrequency = minTermFreq);
	}
}
