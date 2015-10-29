using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Nest
{
	[JsonConverter(typeof(VerbatimDictionaryKeysJsonConverter<Similarities, string, ISimilarity>))]
	public interface ISimilarities : IHasADictionary { }
	public class Similarities : IsADictionary<string, ISimilarity>, ISimilarities
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
	
	public class SimilaritiesDescriptor : IsADictionaryDescriptor<SimilaritiesDescriptor, ISimilarities, string, ISimilarity>, ISimilarities
	{
		public SimilaritiesDescriptor Add(string name, ISimilarity similarity)
		{
			this.BackingDictionary.Add(name, similarity);
			return this;
		}

		public SimilaritiesDescriptor BM25(string name, Func<BM25SimilarityDescriptor, IBM25Similarity> selector) => Add(name, selector?.Invoke(new BM25SimilarityDescriptor()));
		public SimilaritiesDescriptor Default(string name, Func<DefaultSimilarityDescriptor, IDefaultSimilarity> selector) => Add(name, selector?.Invoke(new DefaultSimilarityDescriptor()));
		public SimilaritiesDescriptor LMDirichlet(string name, Func<LMDirichletSimilarityDescriptor, ILMDirichletSimilarity> selector) => Add(name, selector?.Invoke(new LMDirichletSimilarityDescriptor()));
		public SimilaritiesDescriptor LMJelinek(string name, Func<LMJelinekMercerSimilarityDescriptor, ILMJelinekMercerSimilarity> selector) => Add(name, selector?.Invoke(new LMJelinekMercerSimilarityDescriptor()));
		public SimilaritiesDescriptor DFR(string name, Func<DFRSimilarityDescriptor, IDFRSimilarity> selector) => Add(name, selector?.Invoke(new DFRSimilarityDescriptor()));
		public SimilaritiesDescriptor IB(string name, Func<IBSimilarityDescriptor, IIBSimilarity> selector) => Add(name, selector?.Invoke(new IBSimilarityDescriptor()));
	}

}
