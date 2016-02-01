using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace Nest
{
	[JsonConverter(typeof(VerbatimDictionaryKeysJsonConverter<Similarities, string, ISimilarity>))]
	public interface ISimilarities : IIsADictionary<string, ISimilarity> { }

	public class Similarities : IsADictionaryBase<string, ISimilarity>, ISimilarities
	{
		public Similarities() : base() { }
		public Similarities(IDictionary<string, ISimilarity> container) : base(container) { }
		public Similarities(Dictionary<string, ISimilarity> container)
			: base(container.Select(kv => kv).ToDictionary(kv => kv.Key, kv => kv.Value))
		{ }

		/// <summary>
		/// Add any setting to the index
		/// </summary>
		public void Add(string type, ISimilarity mapping) => BackingDictionary.Add(type, mapping);

	}
	
	public class SimilaritiesDescriptor : IsADictionaryDescriptorBase<SimilaritiesDescriptor, ISimilarities, string, ISimilarity>
	{
		public SimilaritiesDescriptor() : base(new Similarities()) { }

		public SimilaritiesDescriptor BM25(string name, Func<BM25SimilarityDescriptor, IBM25Similarity> selector) => Assign(name, selector?.Invoke(new BM25SimilarityDescriptor()));
		public SimilaritiesDescriptor Default(string name, Func<DefaultSimilarityDescriptor, IDefaultSimilarity> selector) => Assign(name, selector?.Invoke(new DefaultSimilarityDescriptor()));
		public SimilaritiesDescriptor LMDirichlet(string name, Func<LMDirichletSimilarityDescriptor, ILMDirichletSimilarity> selector) => Assign(name, selector?.Invoke(new LMDirichletSimilarityDescriptor()));
		public SimilaritiesDescriptor LMJelinek(string name, Func<LMJelinekMercerSimilarityDescriptor, ILMJelinekMercerSimilarity> selector) => Assign(name, selector?.Invoke(new LMJelinekMercerSimilarityDescriptor()));
		public SimilaritiesDescriptor DFR(string name, Func<DFRSimilarityDescriptor, IDFRSimilarity> selector) => Assign(name, selector?.Invoke(new DFRSimilarityDescriptor()));
		public SimilaritiesDescriptor IB(string name, Func<IBSimilarityDescriptor, IIBSimilarity> selector) => Assign(name, selector?.Invoke(new IBSimilarityDescriptor()));
	}

}
