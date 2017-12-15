using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace Nest
{
	/// <summary>
	/// A custom similarity
	/// </summary>
	public interface ICustomSimilarity : ISimilarity, IIsADictionary<string, object> { }

	/// <inheritdoc/>
	public class CustomSimilarity : IsADictionaryBase<string, object>, ICustomSimilarity
	{
		public string Type
		{
			get => this["type"] as string;
			set => this.Add("type", value);
		}

		public CustomSimilarity(string type)
		{
			if (!string.IsNullOrEmpty(type)) this.Type = type;
		}

		internal CustomSimilarity(IDictionary<string, object> container) : base(container) { }

		internal CustomSimilarity(Dictionary<string, object> container)
			: base(container.Select(kv => kv).ToDictionary(kv => kv.Key, kv => kv.Value)) { }

		public void Add(string key, object value) => BackingDictionary.Add(key, value);
	}

	/// <inheritdoc/>
	public class CustomSimilarityDescriptor
		: IsADictionaryDescriptorBase<CustomSimilarityDescriptor, ICustomSimilarity, string, object>
	{
		public CustomSimilarityDescriptor() : base(new CustomSimilarity(string.Empty)) { }

		internal CustomSimilarityDescriptor Type(string type) => Assign("type", type);

		public CustomSimilarityDescriptor Add(string key, object value) => Assign(key, value);
	}

}
