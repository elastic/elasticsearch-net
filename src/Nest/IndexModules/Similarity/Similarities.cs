using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Nest
{
	[JsonConverter(typeof(VerbatimDictionaryKeysJsonConverter))]
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
	
	public class SimilaritiesDescriptor : HasADictionary<SimilaritiesDescriptor, string, ISimilarity>, ISimilarities
	{
		public SimilaritiesDescriptor Add(string name, ISimilarity similarity)
		{
			this.BackingDictionary.Add(name, similarity);
			return this;
		}
	}

}
