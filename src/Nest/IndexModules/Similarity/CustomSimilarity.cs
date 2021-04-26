/*
 * Licensed to Elasticsearch B.V. under one or more contributor
 * license agreements. See the NOTICE file distributed with
 * this work for additional information regarding copyright
 * ownership. Elasticsearch B.V. licenses this file to you under
 * the Apache License, Version 2.0 (the "License"); you may
 * not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *    http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing,
 * software distributed under the License is distributed on an
 * "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY
 * KIND, either express or implied.  See the License for the
 * specific language governing permissions and limitations
 * under the License.
 */

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
