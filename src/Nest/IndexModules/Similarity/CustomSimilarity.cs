// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Collections.Generic;

namespace Nest
{
	/// <summary>
	/// A custom similarity
	/// </summary>
	[ReadAs(typeof(CustomSimilarity))]
	public interface ICustomSimilarity : ISimilarity, IIsADictionary<string, object> { }

	/// <inheritdoc />
	public class CustomSimilarity : IsADictionaryBase<string, object>, ICustomSimilarity
	{
		public CustomSimilarity(string type)
		{
			if (!string.IsNullOrEmpty(type)) Type = type;
		}

		internal CustomSimilarity(IDictionary<string, object> container) : base(container) { }

		internal CustomSimilarity(Dictionary<string, object> container) : base(container) { }

		public string Type
		{
			get => this["type"] as string;
			set => Add("type", value);
		}

		public void Add(string key, object value) => BackingDictionary.Add(key, value);
	}

	/// <inheritdoc />
	public class CustomSimilarityDescriptor
		: IsADictionaryDescriptorBase<CustomSimilarityDescriptor, ICustomSimilarity, string, object>
	{
		public CustomSimilarityDescriptor() : base(new CustomSimilarity(string.Empty)) { }

		internal CustomSimilarityDescriptor Type(string type) => Assign("type", type);

		public CustomSimilarityDescriptor Add(string key, object value) => Assign(key, value);
	}
}
