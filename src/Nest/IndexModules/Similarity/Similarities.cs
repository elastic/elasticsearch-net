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
		public Similarities() { }
		public Similarities(IDictionary<string, ISimilarity> container) : base(container) { }
		public Similarities(Dictionary<string, ISimilarity> container) : base(container) { }

		/// <summary>
		/// Add an <see cref="ISimilarity"/>
		/// </summary>
		public void Add(string type, ISimilarity mapping) => BackingDictionary.Add(type, mapping);
	}

	public class SimilaritiesDescriptor : IsADictionaryDescriptorBase<SimilaritiesDescriptor, ISimilarities, string, ISimilarity>
	{
		public SimilaritiesDescriptor() : base(new Similarities()) { }

		public SimilaritiesDescriptor BM25(string name, Func<BM25SimilarityDescriptor, IBM25Similarity> selector) =>
			Assign(name, selector?.Invoke(new BM25SimilarityDescriptor()));

		public SimilaritiesDescriptor Classic(string name, Func<ClassicSimilarityDescriptor, IClassicSimilarity> selector) =>
			Assign(name, selector?.Invoke(new ClassicSimilarityDescriptor()));

		public SimilaritiesDescriptor LMDirichlet(string name, Func<LMDirichletSimilarityDescriptor, ILMDirichletSimilarity> selector) =>
			Assign(name, selector?.Invoke(new LMDirichletSimilarityDescriptor()));

		public SimilaritiesDescriptor LMJelinek(string name, Func<LMJelinekMercerSimilarityDescriptor, ILMJelinekMercerSimilarity> selector) =>
			Assign(name, selector?.Invoke(new LMJelinekMercerSimilarityDescriptor()));

		public SimilaritiesDescriptor DFI(string name, Func<DFISimilarityDescriptor, IDFISimilarity> selector) =>
			Assign(name, selector?.Invoke(new DFISimilarityDescriptor()));

		public SimilaritiesDescriptor DFR(string name, Func<DFRSimilarityDescriptor, IDFRSimilarity> selector) =>
			Assign(name, selector?.Invoke(new DFRSimilarityDescriptor()));

		public SimilaritiesDescriptor IB(string name, Func<IBSimilarityDescriptor, IIBSimilarity> selector) =>
			Assign(name, selector?.Invoke(new IBSimilarityDescriptor()));

		public SimilaritiesDescriptor Custom(string name, string type, Func<CustomSimilarityDescriptor, IPromise<ICustomSimilarity>> selector) =>
			Assign(name, selector?.Invoke(new CustomSimilarityDescriptor().Type(type))?.Value);
	}
}
