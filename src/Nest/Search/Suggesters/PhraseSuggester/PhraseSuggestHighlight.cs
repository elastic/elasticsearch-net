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

using System.Runtime.Serialization;
using Nest.Utf8Json;

namespace Nest
{
	/// <summary>
	/// Highlighting for <see cref="IPhraseSuggester"/>
	/// </summary>
	[InterfaceDataContract]
	public interface IPhraseSuggestHighlight
	{
		/// <summary>
		/// The post tag
		/// </summary>
		[DataMember(Name = "post_tag")]
		string PostTag { get; set; }

		/// <summary>
		/// The pre tag
		/// </summary>
		[DataMember(Name = "pre_tag")]
		string PreTag { get; set; }
	}

	/// <inheritdoc />
	public class PhraseSuggestHighlight : IPhraseSuggestHighlight
	{
		/// <inheritdoc />
		public string PostTag { get; set; }
		/// <inheritdoc />
		public string PreTag { get; set; }
	}

	/// <inheritdoc cref="IPhraseSuggestHighlight" />
	public class PhraseSuggestHighlightDescriptor : DescriptorBase<PhraseSuggestHighlightDescriptor, IPhraseSuggestHighlight>, IPhraseSuggestHighlight
	{
		string IPhraseSuggestHighlight.PostTag { get; set; }
		string IPhraseSuggestHighlight.PreTag { get; set; }

		/// <inheritdoc cref="IPhraseSuggestHighlight.PreTag" />
		public PhraseSuggestHighlightDescriptor PreTag(string preTag) => Assign(preTag, (a, v) => a.PreTag = v);

		/// <inheritdoc cref="IPhraseSuggestHighlight.PostTag" />
		public PhraseSuggestHighlightDescriptor PostTag(string postTag) => Assign(postTag, (a, v) => a.PostTag = v);
	}
}
